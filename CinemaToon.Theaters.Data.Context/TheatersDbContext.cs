using CinemaToon.Entities.Theaters;
using Microsoft.EntityFrameworkCore;
using System;

namespace CinemaToon.Theaters.Data.Context
{
    public class TheatersDbContext : DbContext
    {
        public TheatersDbContext(DbContextOptions<TheatersDbContext> options) : base(options)
        {

        }

        public DbSet<Theater> Theater { get; set; }

    }
}
