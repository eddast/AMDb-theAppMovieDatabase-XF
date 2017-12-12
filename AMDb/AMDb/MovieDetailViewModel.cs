using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMDb.Services;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMDb
{
    class MovieDetailViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private MovieModel _movie;
        private MovieDBService _server;

        public MovieDetailViewModel(INavigation navigation, MovieModel movie, MovieDBService server)
        {
            this._navigation = navigation;
            this._movie = movie;
            this._server = server;
        }

        public MovieModel Movie
        {
            get => this._movie;
            set
            {
                this._movie = value;
                OnPropertyChanged();
            }
        }

        public string Runtime
        {
            get => this._movie.runtime;
            set {
                this._movie.runtime = value;
                OnPropertyChanged();
            }
        }

        public string Tagline
        {
            get => this._movie.tagline;
            set {
                this._movie.tagline = value;
                OnPropertyChanged();
            }
        }

        public async Task UpdateMovieInformationAsync()
        {
            await UpdateRuntimeAsync();
            await UpdateTaglineAsync();
        }

        private async Task UpdateRuntimeAsync()
        {
            var movieRuntime = await _server.GetRuntimeAsync(_movie.id);
            Movie.runtime = movieRuntime;
            Runtime = movieRuntime;
        }

        private async Task UpdateTaglineAsync()
        {
            var movieTagline = "\"" + await _server.GetTaglineAsync(_movie.id) + "\"";
            Tagline = movieTagline;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
