using CinemaToon.Entities.CinemaBooking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaToon.Utilities.Abstractions.Interfaces
{
    public interface ICinemasService
    {
        Task<IEnumerable<CinemaFunction>> GetFuctions(int movieId);
        int GetSeats(int functionId);
        Task<CinemaFunction> GetCinemaFunctionById(int id);
    }
}
