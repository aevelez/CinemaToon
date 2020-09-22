using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CinemaToon.Entities.CinemaBooking;
using CinemaToon.Entities.DTOs;
using CinemaToon.Entities.Theaters;
using CinemaToon.Utilities.Abstractions.Interfaces;
using CinemaToon.Web.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CinemaToon.Web.MVC.Controllers
{
    [Authorize]
    public class UserBookingController : Controller
    {
        private readonly IAPIClient _apiClient;
        private readonly IConfiguration _configuration;

        public UserBookingController(IAPIClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        // GET: UserBooking
        public async Task<ActionResult> Index()
        {
            IEnumerable<CinemaReservation> bookings = default;
            try
            {
                bookings = await _apiClient.GetAsync<IEnumerable<CinemaReservation>>
                    (new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodBooking"]}{User.FindFirst(x => x.Type == "sub").Value}"));
                foreach (var item in bookings)
                {
                    item.TheaterName = GetTheaters(item.TheaterId);
                }
                return View(bookings);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }

        // GET: UserBooking/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var booking = await _apiClient.GetAsync<CinemaReservation>
                 (new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodBookingDetail"]}{id}"));


            return View(booking);
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(int id)
        {
            var result = await _apiClient.PostAsync<bool>($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodBooking"]}", new CancelDTO { Id = id });

            if (result)
            {
                return RedirectToAction("Index");
            }

            return View("Error", new ErrorViewModel());
        }

        private string GetTheaters(int id)
        {
            return _apiClient.Get(new Uri($"{_configuration["Theaters:BaseUrl"]}{_configuration["Theaters:MethodTheaterName"]}{id}"));
        }
    }
}