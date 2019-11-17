using CinemaToon.Entities.Theaters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaToon.Utilities.Abstractions.Interfaces
{
    public interface ITheatersService
    {
        IEnumerable<Theater> GetTheaters();

        string GetTheatersbyId(int theaterid);
    }
}
