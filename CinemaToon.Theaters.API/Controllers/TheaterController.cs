﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaToon.Utilities.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaToon.Theaters.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterController : Controller
    {
        private readonly ITheatersService _theatersService;

        public TheaterController(ITheatersService theatersService)
        {
            _theatersService = theatersService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_theatersService.GetTheaters());
        }

        // GET api/originalTitle
        [HttpGet("ReturnTheaterName/{TheaterId}")]
        public IActionResult Get(int TheaterId)
        {
            if (TheaterId < 1)
            {
                return BadRequest();
            }
            return Ok(_theatersService.GetTheatersbyId(TheaterId));
        }
    }
}