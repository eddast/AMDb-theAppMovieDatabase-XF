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

        public MovieDetailPage(MovieModel movie, MovieDBService server)
        {
            _thisViewModel = new MovieDetailViewModel(this.Navigation, movie, server);
            this.BindingContext = this._thisViewModel;
            InitializeComponent();
        }

        // Asynchronously commands it's view model to update it's information for view
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _thisViewModel.UpdateMovieInformationAsync();
            this.details.Text = _thisViewModel.Runtime + " mins | " + _thisViewModel.Movie.Genres;
        }
    }
}