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
    public partial class MovieDetailPage : ContentPage
    {
        private readonly MovieDetailViewModel _thisViewModel;
        private MovieModel _movie;
        private MovieDBService _server;

        public MovieDetailPage(MovieModel movie, MovieDBService server)
        {
            _movie = movie; _server = server;
            _thisViewModel = new MovieDetailViewModel(this.Navigation, movie, server);
            this.BindingContext = this._thisViewModel;
            InitializeComponent();
            this.titleAndYear.Text = movie.Title + " (" + movie.Year + ")";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _thisViewModel.UpdateMovieInformationAsync();
            this.details.Text = _thisViewModel.Runtime + " mins | " + _thisViewModel.Movie.Genres;
        }
    }
}