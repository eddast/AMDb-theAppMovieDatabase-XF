using AMDb.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;

namespace AMDb
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private ObservableCollection<MovieModel> _movies;
        private MovieModel _selectedMovie;
        private MovieDBService _server;

        public MovieListViewModel(INavigation navigation, MovieDBService server, List<MovieModel> movies, int ListType = 0)
        {
            this._navigation = navigation;
            this._server = server;
            GetListAsync(movies, ListType);
        }

        private async void GetListAsync(List<MovieModel> movies, int listType)
        {
            if(movies != null ) {
                _movies = new ObservableCollection<MovieModel>(movies);
            }
            else if (listType == 1) {
                var MovieModelList = await _server.GetBasicTopMoviesInfoAsync();
                Movies = new ObservableCollection<MovieModel>(MovieModelList);
            }
            else if (listType == 2) {
                var MovieModelList = await _server.GetPopularMoviesInfoAsync();
                Movies = new ObservableCollection<MovieModel>(MovieModelList);
            }

            UpdateMovieCast();
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
            foreach (var movie in _movies) {
                movie.Cast = await _server.GetThreeCastMembersAsync(movie.Id);
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
