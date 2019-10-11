using CinemaToon.Entities.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaToon.Utilities.Abstractions.Interfaces
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetMovies();
    
        Task<Movie> GetMovie(int id);
    }
}
