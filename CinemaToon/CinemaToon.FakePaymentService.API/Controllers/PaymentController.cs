using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaToon.FakePaymentService.API.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        // GET: Payment
        [HttpGet("{user}/{value}")]
        public ActionResult Index(string user, double value)
        {
            Random random = new Random();
            var next = random.Next(100);
            string result = next % 2 == 0 ? "OK" : "FAIL";
            return Ok(result);
        }
    }
}