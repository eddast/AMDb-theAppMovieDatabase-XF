using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDb
{
    // Models a movie object with necessary information:
    // MovieInfo object, starring cast, poster path and runtime
    public class MovieModel
    {
        public MovieInfo movie;
        public string title;
        public string year;
        public string cast;
        public string posterPath;
        public string backdropPath;
        public string runtime;
        public string genres;
        public double vote_rate;

        public MovieModel() { }

        public MovieModel ( MovieInfo movie, string cast, string runtime ) {
            
            this.movie = movie; // DEPRICATED for week 2 (still here for iOS dependency problem)

            this.title = movie.Title;
            this.year = movie.ReleaseDate.Year.ToString();
            this.genres = get_movie_genres(movie);
            this.cast = cast;
            this.posterPath = movie.PosterPath;
            this.backdropPath = movie.BackdropPath;
            this.vote_rate = movie.VoteAverage;
            this.runtime = runtime;
        }

        private string get_movie_genres(MovieInfo _movie)
        {
            var listgenres = _movie.Genres;
            string Genres = ""; int iteration = 0;

            foreach (DM.MovieApi.MovieDb.Genres.Genre genre in listgenres)
            {
                if (iteration != 0) { Genres = Genres + ", " + genre.Name; }
                else { Genres = Genres + genre.Name; }
                iteration++;
            }


            return Genres;
        }

    }
}
