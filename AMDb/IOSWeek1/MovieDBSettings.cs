using DM.MovieApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDb
{
    public class MovieDBSettings : IMovieDbSettings
    {
        public MovieDBSettings() { }

        string IMovieDbSettings.ApiKey => "f8cd2203fcf9b6a32d21cee25d2c6ac8";
        string IMovieDbSettings.ApiUrl => "http://api.themoviedb.org/3/";
    }
}


