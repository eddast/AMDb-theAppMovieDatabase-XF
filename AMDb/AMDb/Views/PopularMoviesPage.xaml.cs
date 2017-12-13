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
    public partial class PopularMoviesPage: ContentPage
    {
        private MovieListViewModel _thisViewModel;
        private MovieDBService _server;
        List<MovieModel> popularMovies;

        public PopularMoviesPage(MovieDBService server)
        {
            InitializeComponent();
            this._server = server;
            FillList();
        }

        private async void FillList()
        {
            popularMovies = await _server.GetPopularMoviesInfoAsync();
            _thisViewModel = new MovieListViewModel(this.Navigation, popularMovies, _server);
            this.BindingContext = _thisViewModel;
            _thisViewModel.UpdateMovieCast();
        }
    }
}