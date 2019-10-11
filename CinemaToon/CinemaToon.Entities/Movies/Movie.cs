using System;
using System.ComponentModel;

namespace CinemaToon.Entities.Movies
{
    public class Movie
    {
        public int Id { get; set; }

        [DisplayName("Vote Average")]
        public double VoteAverage { get; set; }

        [DisplayName("Original Title")]
        public string OriginalTitle { get; set; }

        [DisplayName("Original Language")]
        public string OriginalLanguage { get; set; }

        [DisplayName("Overview")]
        public string Overview { get; set; }

        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }

        public string PosterPath { get; set; }

        public string FullPosterPath { get; set; }

    }
}
