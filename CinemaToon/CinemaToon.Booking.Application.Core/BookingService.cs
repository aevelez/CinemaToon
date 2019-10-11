using CinemaToon.Booking.Data.Context;
using CinemaToon.Entities.CinemaBooking;
using CinemaToon.Entities.DTOs;
using CinemaToon.Utilities.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaToon.Bookings.Application.Core
{
    public class BookingService : IBookingService
    {
        private readonly BookingDbContext _context;
        private readonly IPaymentService _paymentService;
        private static object _threadSafeBooking = new object();

        public BookingService(BookingDbContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }


        public ReserveResultDTO ProcessBooking(ReserveDTO reserveDTO)
        {
            var result = new ReserveResultDTO();
            try
            {

                lock (_threadSafeBooking)
                {
                    var availableSeats = _context.CinemaBooking.First(x => x.CinemaFuctionId == reserveDTO.FunctionId).AvailableSeats;
                    if (availableSeats < reserveDTO.NumberOfTickets)
                    {
                        result.Message = "NO SEATS";
                        result.AvailableSeats = availableSeats;
                        result.MessageCode = -1;
                        return result;
                    }

                    var paymentResult = _paymentService.Pay(reserveDTO.User, reserveDTO.NumberOfTickets);
                    if (paymentResult.Equals("OK"))
                    {
                        var cinemaReservation = new CinemaReservation();
                        cinemaReservation.CinemaFunctionId = reserveDTO.FunctionId;
                        cinemaReservation.CreateDate = DateTime.Now;
                        cinemaReservation.IsActive = true;
                        cinemaReservation.TotalPaid = reserveDTO.Total;
                        cinemaReservation.TotalTickets = reserveDTO.NumberOfTickets;
                        cinemaReservation.User = reserveDTO.User;
                        cinemaReservation.OriginalMovieTitle = reserveDTO.OriginalMovieTitle;
                        _context.CinemaReservations.Add(cinemaReservation);
                        var cinemaFunction = _context.CinemaBooking.First(x => x.CinemaFuctionId == reserveDTO.FunctionId);
                        cinemaFunction.AvailableSeats -= reserveDTO.NumberOfTickets;
                        _context.SaveChanges();
                        result.MessageCode = 1;
                        result.Message = "OK";
                        return result;
                    }
                    result.MessageCode = -2;
                    result.Message = "PAYMENT SERVICE ERROR";
                    return result;
                }
            }
            catch (Exception ex)
            {
                //Here we can log the exception
                result.MessageCode = -3;
                result.Message = "SYSTEM ERROR";
                return result;
            }
        }

        public async Task<IEnumerable<CinemaReservation>> GetBookingByUser(object user)
        {
            return await _context.CinemaReservations.Where(x => x.User.Equals(user) && x.IsActive)
                .Include(x => x.CinemaFunction).ToListAsync();
        }

        public async Task<bool> CancelBooking(CancelDTO cancelDTO)
        {
            try
            {
                var booking = _context.CinemaReservations.FirstOrDefault(x => x.Id == cancelDTO.Id);
                if (booking == null) return false;
                booking.IsActive = false;
                var function = _context.CinemaBooking.FirstOrDefault(x => x.CinemaFuctionId == booking.CinemaFunctionId);
                function.AvailableSeats += booking.TotalTickets;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task<CinemaReservation> GetBookingById(int id)
        {
            return await _context.CinemaReservations.Include(x => x.CinemaFunction)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

    }
}
