using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMDb
{
    public partial class MovieList : ContentPage {

        public MovieList(List<MovieModel> movieList)
        {
            this.BindingContext = new MovieListViewModel(this.Navigation, movieList);
            InitializeComponent();
        }
    }
}