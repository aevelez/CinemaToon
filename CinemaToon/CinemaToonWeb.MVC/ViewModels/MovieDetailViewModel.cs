using CinemaToon.Entities.CinemaBooking;
using CinemaToon.Entities.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaToon.Web.MVC.ViewModels
{
    public class MovieDetailViewModel
    {
        public Movie Movie { get; set; }

        public IEnumerable<CinemaFunction> CinemaBooking { get; set; }

        [Required(ErrorMessage = "Error: Must Choose a Function")]
        public int CinemaFunctionID { get; set; }

        [DisplayName("Available Seats")]
        public int AvailableSeats { get; set; }

        [Required]
        [Range(1, 10)]
        [DisplayName("Number of Tickets")]
        public int? NumberOfTickets { get; set; }


    }
}
