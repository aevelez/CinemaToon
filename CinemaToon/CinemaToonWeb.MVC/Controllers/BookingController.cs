using CinemaToon.Entities.CinemaBooking;
using CinemaToon.Entities.DTOs;
using CinemaToon.Entities.Movies;
using CinemaToon.Utilities.Abstractions.Interfaces;
using CinemaToon.Web.MVC.Models;
using CinemaToon.Web.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CinemaToon.Web.MVC.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IAPIClient _apiClient;
        private readonly IConfiguration _configuration;

        public BookingController(IAPIClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int movieId, int cinemaFunctionId, int cinemaTheaterId)
        {
            try
            {
                var movie = await _apiClient.GetAsync<Movie>(new Uri($"{_configuration["Movies:BaseUrl"]}{movieId}"));
                var cinemaFunction = await _apiClient.GetAsync<CinemaFunction>(new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodFunctionById"]}{cinemaFunctionId}"));
                var viewModel = new BookingViewModel
                {
                    AvailableSeats = cinemaFunction.AvailableSeats,
                    PricePerTicket = cinemaFunction.BasePrice * movie.VoteAverage / 10,
                    OriginalTitle = movie.OriginalTitle,
                    Id = movie.Id,
                    CinemaFuctionId = cinemaFunction.CinemaFuctionId,
                    CinemaTheaterId = cinemaTheaterId
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GoToPay(BookingViewModel reserverViewModel)
        {
            if (ModelState.IsValid)
            {
                reserverViewModel.TotalPrice = (double)reserverViewModel.NumberOfTickets * reserverViewModel.PricePerTicket;
                return View("Payment", reserverViewModel);
            }
            return View("Index", reserverViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reserver(BookingViewModel reserverViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _apiClient.PostAsync<ReserveResultDTO>($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodFunctions"]}",
                        new
                        {
                            functionId = reserverViewModel.CinemaFuctionId,
                            numberOfTickets = reserverViewModel.NumberOfTickets,
                            user = User.FindFirst(x => x.Type == "sub").Value,
                            originalMovieTitle = reserverViewModel.OriginalTitle,
                            total = reserverViewModel.TotalPrice,
                            theaterId = reserverViewModel.CinemaTheaterId
                        });

                    switch (result.MessageCode)
                    {
                        case 1: return View("PaymentSuccesfully");
                        case -1:
                            ModelState.AddModelError("Error", "There are not seats available");
                            reserverViewModel.AvailableSeats = result.AvailableSeats;
                            break;
                        case -2: ModelState.AddModelError("Error", "Payment service error"); break;
                        case -3: ModelState.AddModelError("Error", "Please try again"); break;
                        default: break;
                    }
                }
                return View("Index", reserverViewModel);
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}