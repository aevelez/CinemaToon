using CinemaToon.Entities.CinemaBooking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace CinemaToon.Booking.Data.Context
{
    public class BookingDbContext : DbContext
    {

        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {

        }

        public DbSet<CinemaFunction> CinemaBooking { get; set; }
        public DbSet<CinemaReservation> CinemaReservations { get; set; }
    }


}
