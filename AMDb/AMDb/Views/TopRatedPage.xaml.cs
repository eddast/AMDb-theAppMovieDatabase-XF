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
    public partial class TopRatedPage : ContentPage
    {
        private MovieListViewModel _thisViewModel;
        private MovieDBService _server;
        List<MovieModel> topMovies;

        public TopRatedPage(MovieDBService server)
        {
            InitializeComponent();
            this._server = server;
            FillList();
        }

        private async void FillList()
        {
            topMovies = await _server.GetBasicTopMoviesInfoAsync();
            _thisViewModel = new MovieListViewModel(this.Navigation, topMovies, _server);
            this.BindingContext = _thisViewModel;
            _thisViewModel.UpdateMovieCast();
        }
    }
}
