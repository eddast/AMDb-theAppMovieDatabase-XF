using AMDb.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using System.Windows.Input;

namespace AMDb
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private ObservableCollection<MovieModel> _movies;
        private MovieModel _selectedMovie;
        private MovieDBService _server;
        private bool _isRefreshing = false;
        private int _listType;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set {

                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand
        {
            get {
                return new Command(async () => {

                    IsRefreshing = true;
                    GetListAsync(null, _listType);
                });
            }
        }

        public MovieListViewModel(INavigation navigation, MovieDBService server, List<MovieModel> movies, int ListType = 0)
        {
            this._navigation = navigation;
            this._server = server;
            this._listType = ListType;
            GetListAsync(movies, ListType);
        }

        private async void GetListAsync(List<MovieModel> movies, int listType)
        {
            if(movies != null ) {
                Movies = new ObservableCollection<MovieModel>(movies);
            }
            else if (listType == 1) {
                var MovieModelList = await _server.GetBasicTopMoviesInfoAsync();
                Movies = new ObservableCollection<MovieModel>(MovieModelList);
            }
            else if (listType == 2) {
                var MovieModelList = await _server.GetPopularMoviesInfoAsync();
                Movies = new ObservableCollection<MovieModel>(MovieModelList);
            }

            IsRefreshing = false;

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
            foreach (var movie in Movies) {
                movie.Cast = await _server.GetThreeCastMembersAsync(movie.Id);
            }

        }
        
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
