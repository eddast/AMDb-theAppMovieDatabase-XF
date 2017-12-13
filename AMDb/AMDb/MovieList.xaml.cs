using AMDb.Services;
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

        private MovieListViewModel _thisViewModel;

        public MovieList(List<MovieModel> movieList, MovieDBService server)
        {
            _thisViewModel = new MovieListViewModel(this.Navigation, movieList, server);
            this.BindingContext = _thisViewModel;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _thisViewModel.UpdateMovieCast();
        }
    }
}