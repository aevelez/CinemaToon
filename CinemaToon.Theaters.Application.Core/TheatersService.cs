using CinemaToon.Entities.Theaters;
using CinemaToon.Theaters.Data.Context;
using CinemaToon.Utilities.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaToon.Theaters.Application.Core
{
    public class TheatersService : ITheatersService
    {
        private readonly TheatersDbContext _context;
        private static object _threadSafeBooking = new object();

        public TheatersService(TheatersDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Theater> GetTheaters()
        {
            return _context.Theater.ToList();
        }

        public string GetTheatersbyId(int theaterid)
        {
            return  _context.Theater.Where(x => x.Id == theaterid).FirstOrDefault().Name;
        }
    }
}
