using CinemaToon.Booking.Data.Context;
using CinemaToon.Entities.CinemaBooking;
using CinemaToon.Utilities.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaToon.Bookings.Application.Core
{
    public class CinemaService : ICinemasService
    {
        private readonly BookingDbContext _context;
        private readonly CinemaBookingSettings _CinemaBookingSettings;
        private static object _threadSafe = new object();

        public CinemaService(BookingDbContext context, CinemaBookingSettings CinemaBookingSettings)
        {
            _context = context;
            _CinemaBookingSettings = CinemaBookingSettings;
        }

        public async Task<IEnumerable<CinemaFunction>> GetFuctions(int movieId)
        {
            try
            {
                lock (_threadSafe)
                {
                    if (!_context.CinemaBooking.Any(x => x.MovieId == movieId && x.FunctionDateTime > DateTime.Now))
                    {
                        SeedFuctions(movieId);
                    }
                }
                return await _context.CinemaBooking.Where(x => x.MovieId == movieId && x.FunctionDateTime > DateTime.Now).ToListAsync();

            }
            catch (Exception ex)
            {
                return new List<CinemaFunction>();
            }
        }

        private void SeedFuctions(int movieId)
        {
            for (int i = 0; i < 5; i++)
            {
                DateTime randomDate = DateTime.Today.AddDays(i);

                _context.Add(new CinemaFunction()
                {
                    MovieId = movieId,
                    FunctionDateTime = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, 15, 0, 0),
                    AvailableSeats = _CinemaBookingSettings.SeatsNumber,
                    BasePrice = _CinemaBookingSettings.BasePrice
                });

                _context.Add(new CinemaFunction()
                {
                    MovieId = movieId,
                    FunctionDateTime = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, 18, 0, 0),
                    AvailableSeats = _CinemaBookingSettings.SeatsNumber,
                    BasePrice = _CinemaBookingSettings.BasePrice
                });

                _context.Add(new CinemaFunction()
                {
                    MovieId = movieId,
                    FunctionDateTime = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, 21, 0, 0),
                    AvailableSeats = _CinemaBookingSettings.SeatsNumber,
                    BasePrice = _CinemaBookingSettings.BasePrice
                });
                _context.SaveChanges();
            }
        }

        public int GetSeats(int functionId)
        {
            return _context.CinemaBooking.First(x => x.CinemaFuctionId == functionId).AvailableSeats;
        }


        public async Task<CinemaFunction> GetCinemaFunctionById(int id)
        {
            return await _context.CinemaBooking
                .FirstOrDefaultAsync(x => x.CinemaFuctionId == id);
        }

    }
}
