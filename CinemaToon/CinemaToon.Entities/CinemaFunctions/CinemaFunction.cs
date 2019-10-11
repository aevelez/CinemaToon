using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CinemaToon.Entities.CinemaBooking
{
    public class CinemaFunction
    {
        [Key]
        public int CinemaFuctionId { get; set; }

        public int MovieId { get; set; }

        [DisplayName("Date and Time")]
        public DateTime FunctionDateTime { get; set; }

        [DisplayName("Base Price")]
        public double BasePrice { get; set; }

        [DisplayName("Available Seats")]
        public int AvailableSeats { get; set; }

    }

  
}
