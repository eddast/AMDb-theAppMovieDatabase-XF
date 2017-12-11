using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMDb
{
    public partial class MovieList : ContentPage
    {
        List<MovieModel> _movieList;

        public MovieList(List<MovieModel> movieList)
        {
            InitializeComponent();
            this._movieList = movieList;
        }
    }
}