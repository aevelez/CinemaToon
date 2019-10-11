using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaToon.Entities.CinemaBooking;
using CinemaToon.Entities.DTOs;
using CinemaToon.Utilities.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaToon.CinemaBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public UserBookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }



        // GET: UserBooking/{userid}
        [HttpGet("{userId}")]
        public async Task<ActionResult> Index(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest();
            }
            return Ok(await _bookingService.GetBookingByUser(userId));
        }


        // GET: UserBooking/{userid}
        [HttpGet("Detail/{id}")]
        public async Task<ActionResult> Detail(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            return Ok(await _bookingService.GetBookingById(id));
        }

        // GET: UserBooking/{userid}
        [HttpPost]
        public async Task<ActionResult> Cancel(CancelDTO cancelDTO)
        {
            if (cancelDTO.Id < 0)
            {
                return BadRequest();
            }
            return Ok(await _bookingService.CancelBooking(cancelDTO));
        }


    }
}