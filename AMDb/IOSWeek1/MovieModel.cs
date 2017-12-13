using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AMDb
{
    // Retrieves necessary info only based on the MovieInfo object only
    public class MovieModel : INotifyPropertyChanged
    {
        // Accessible properties of a movie model
        public int Id { get => this.id; set => this.id = value; }
        public string Title { get => this.title; set => this.title = value; }
        public string Year { get => this.year; set => this.year = value; }
        public string PosterPath { get => this.posterPath; set => this.posterPath = value; }
        public string BackdropPath { get => this.backdropPath; set => this.backdropPath =value; }
        public string Overview { get => this.overview; set => this.overview = value; }
        public string Genres { get => this.genres; set => this.genres = value; }

        // Methods that may change or be fetched after initialization
        // Implement the on property changed method to notify view of change
        public string Runtime {
            get => this.runtime;
            set {
                this.runtime = value;
                OnPropertyChanged();
            }
        }

        public string Tagline {
            get => this.tagline;
            set {
                this.tagline = value;
                OnPropertyChanged();
            }
        }

        public string Cast {
            get => this.cast;
            set {
                this.cast = value;
                OnPropertyChanged();
            }
        }

        private int id;
        private string title;
        private string year;
        private string posterPath;
        private string backdropPath;
        private string overview;
        private string genres;
        private string runtime;
        private string tagline;
        private string cast;

        public MovieModel() { }

        // Retrieves necessary info only based on the MovieInfo object
        public MovieModel ( MovieInfo movie ) {

            // Values acquired from basic MoveInfo value
            this.id = movie.Id;
            this.title = movie.Title;
            this.year = movie.ReleaseDate.Year.ToString();
            this.genres = get_movie_genres(movie);
            this.overview = movie.Overview;
            this.posterPath = "http://image.tmdb.org/t/p/original" + movie.PosterPath;
            this.backdropPath = "http://image.tmdb.org/t/p/original" + movie.BackdropPath;

            // Placeholders for upcoming async values
            this.runtime = "";
            this.tagline = "";
            this.cast = "";
        }
        
        // Generates string from a list of genres
        private string get_movie_genres(MovieInfo _movie)
        {
            var listgenres = _movie.Genres;
            string Genres = ""; int iteration = 0;

            foreach (DM.MovieApi.MovieDb.Genres.Genre genre in listgenres) {
                if (iteration != 0) { Genres = Genres + ", " + genre.Name; }
                else { Genres = Genres + genre.Name; }
                iteration++;
            }


            return Genres;
        }


        // On property change event, calling this function will notify the view
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
