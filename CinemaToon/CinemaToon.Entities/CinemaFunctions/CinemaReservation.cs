using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaToon.Entities.CinemaBooking
{
    public class CinemaReservation
    {

        [Key]
        public int Id { get; set; }

        [DisplayName("User")]
        public string User { get; set; }

        [DisplayName("Total Paid")]
        public double TotalPaid { get; set; }

        public int CinemaFunctionId { get; set; }

        public CinemaFunction CinemaFunction { get; set; }

        [DisplayName("Total Tickets")]
        public int TotalTickets { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        [DisplayName("Movie Title")]
        public string OriginalMovieTitle { get; set; }

        public int TheaterId { get; set; }

        [DisplayName("Theater Name")]
        [NotMapped]
        public string TheaterName { get; set; }

    }
}
