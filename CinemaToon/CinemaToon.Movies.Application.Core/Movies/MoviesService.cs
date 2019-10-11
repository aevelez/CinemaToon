using CinemaToon.Entities;
using CinemaToon.Entities.Movies;
using CinemaToon.Utilities.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaToon.Application.Movies.Core.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly MovieSettings _movieSettings;
        private readonly IAPIClient _apiClient;

        public MoviesService(IAPIClient apiClient, MovieSettings movieSettings)
        {
            _movieSettings = movieSettings;
            _apiClient = apiClient;
        }

        public async Task<Movie> GetMovie(int id)
        {
            var movie = await _apiClient.GetAsyncWithUnderscorePropertyNames<Movie>(new Uri(string.Format(_movieSettings.BaseUrlDetails, id, _movieSettings.ServiceApiKey)));
            movie.FullPosterPath = string.Format(_movieSettings.BaseUrlImage, movie.PosterPath);
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movies = await _apiClient.GetAsync<IEnumerable<Movie>>(new Uri(string.Format(_movieSettings.BaseUrl, _movieSettings.ServiceApiKey)), _movieSettings.TokenName);
            movies.ToList().ForEach(x => x.FullPosterPath = string.Format(_movieSettings.BaseUrlImage, x.PosterPath));            
            return movies;
        }
    }
}
