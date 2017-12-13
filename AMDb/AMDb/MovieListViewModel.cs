using AMDb.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AMDb
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private ObservableCollection<MovieModel> _movies;
        private MovieModel _selectedMovie;
        private MovieDBService _server;

        public MovieListViewModel(INavigation navigation, List<MovieModel> movies, MovieDBService server)
        {
            this._navigation = navigation;
            this._movies = new ObservableCollection<MovieModel>(movies);
            this._server = server;
        }

        public ObservableCollection<MovieModel> Movies
        {
            get => this._movies; 
            set {
                this._movies = value;
                OnPropertyChanged();
            }
        }

        public async Task UpdateMovieCast()
        {
            foreach(var movie in _movies) {
                movie.cast = await _server.GetThreeCastMembersAsync(movie.id);
                Movies = _movies;
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        public MovieModel SelectedMovie
        {
            get => this._selectedMovie;
            set {
                if(value != null) {

                    this._selectedMovie = value;
                    this._navigation.PushAsync((new MovieDetailPage(_selectedMovie, _server)), true);
                    _selectedMovie = null;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
