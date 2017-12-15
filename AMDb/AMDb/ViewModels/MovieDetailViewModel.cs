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
        // Private variables
        private INavigation _navigation;
        private MovieModel _movie;
        private MovieDBService _server;

        // Public properties that notify view of changes
        // Are binding properties of the XAML view
        public MovieModel Movie {
            get => this._movie;
            set {
                this._movie = value;
                OnPropertyChanged();
            }
        }
        public string Runtime {
            get => this._movie.Runtime;
            set {
                this._movie.Runtime = value;
                OnPropertyChanged();
            }
        }

        public string Tagline {
            get => this._movie.Tagline;
            set {
                this._movie.Tagline = value;
                OnPropertyChanged();
            }
        }

        public MovieDetailViewModel(INavigation navigation, MovieModel movie, MovieDBService server)
        {
            this._navigation = navigation;
            this._movie = movie;
            this._server = server;
        }

        // Updates information after rendering view
        public async Task UpdateMovieInformationAsync()
        {
            await UpdateRuntimeAsync();
            await UpdateTaglineAsync();
        }

        // Updates runtime after rendering view
        private async Task UpdateRuntimeAsync()
        {
            var movieRuntime = await _server.GetRuntimeAsync(_movie.Id);
            Movie.Runtime = movieRuntime;
            Runtime = movieRuntime;
        }

        // Updates tagline after rendering view
        private async Task UpdateTaglineAsync()
        {
            var movieTagline = await _server.GetTaglineAsync(_movie.Id);
            if (movieTagline != "" && movieTagline != null){
                Tagline = "\"" + movieTagline + "\"";
            }
        }

        // Function notifying the view of CallMemberName property change
        // So that property can be re-rendered in view
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
