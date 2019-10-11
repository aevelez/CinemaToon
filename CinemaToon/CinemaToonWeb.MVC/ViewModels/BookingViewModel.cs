using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CinemaToon.Entities.CinemaBooking;
using CinemaToon.Entities.Movies;

namespace CinemaToon.Web.MVC.ViewModels
{
    public class BookingViewModel
    {
        [Required]
        [Range(1, 10)]
        [DisplayName("Number of Tickets")]
        public Nullable<int> NumberOfTickets { get; set; }

        [DisplayName("Price per ticket")]
        public double PricePerTicket { get; set; }

        [DisplayName("Total Price")]
        public double TotalPrice { get; set; }

        [DisplayName("Original Title")]
        public string OriginalTitle { get; set; }

        public int Id { get; set; }

        public int CinemaFuctionId { get; set; }

        [DisplayName("Available Seats")]
        public int AvailableSeats { get; set; }

    }
}
