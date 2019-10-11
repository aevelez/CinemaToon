using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CinemaToon.Entities.CinemaBooking;
using CinemaToon.Entities.Movies;
using CinemaToon.Utilities.Abstractions.Interfaces;
using CinemaToon.Web.MVC.Models;
using CinemaToon.Web.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CinemaToon.Web.MVC.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IAPIClient _apiClient;
        private readonly IConfiguration _configuration;

        public MoviesController(IAPIClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        // GET: Movies
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var viewModel = new MoviesViewModel();
            try
            {
                var movies = await _apiClient.GetAsync<IEnumerable<Movie>>(new Uri(_configuration["Movies:BaseUrl"]));
                viewModel.Movies = movies;
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        // GET: Movies/Details        
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                Movie movie = await GetMovie(id);
                IEnumerable<CinemaFunction> functions = await GetFunctions(movie.Id);
                return View(new MovieDetailViewModel() { Movie = movie, CinemaBooking = functions });
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        private async Task<Movie> GetMovie(int id)
        {
            return await _apiClient.GetAsync<Movie>(new Uri($"{_configuration["Movies:BaseUrl"]}{id}"));
        }

        private async Task<IEnumerable<CinemaFunction>> GetFunctions(int movieId)
        {
            return await _apiClient.GetAsync<IEnumerable<CinemaFunction>>(new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodFunctions"]}{movieId}"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetSeats(MovieDetailViewModel movieDetailViewModel)
        {
            try
            {
                var seats = await _apiClient.GetAsync<int>(new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodSeats"]}{movieDetailViewModel.CinemaFunctionID}"));
                var functions = await _apiClient.GetAsync<IEnumerable<CinemaFunction>>(new Uri($"{_configuration["Booking:BaseUrl"]}{_configuration["Booking:MethodFunctions"]}{movieDetailViewModel.Movie.Id}"));
                var movie = await _apiClient.GetAsync<Movie>(new Uri($"{_configuration["Movies:BaseUrl"]}{movieDetailViewModel.Movie.Id}"));
                var model = new MovieDetailViewModel() { Movie = movie, CinemaBooking = functions, AvailableSeats = seats, CinemaFunctionID = movieDetailViewModel.CinemaFunctionID };
                model.AvailableSeats = seats;
                return View("Details", model);
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }
    }
}