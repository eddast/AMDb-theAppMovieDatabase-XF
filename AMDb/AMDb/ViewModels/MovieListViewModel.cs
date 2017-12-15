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
        // Private variables for view model
        private INavigation _navigation;
        private ObservableCollection<MovieModel> _movies;
        private MovieModel _selectedMovie;
        private MovieDBService _server;
        private bool _isRefreshing = false;
        private int _listType;
        private string _query;

        // Public properties
        public bool RefreshEnabled { get; set; }

        // Public properties that notify view of changes
        // Are binding properties of the XAML view

        // Determines if list is being refreshed
        public bool IsRefreshing {
            get => _isRefreshing;
            set {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        } // Query, a two-way bindable object, retrieves input from XAML UI
        public string Query {
            get => _query;
            set {
                if (value != null) {
                    _query = value;
                    OnPropertyChanged();
                }
            }
        } // The collection of movies to display as list
        public ObservableCollection<MovieModel> Movies {
            get => this._movies;
            set {
                this._movies = value;
                OnPropertyChanged();
            }
        } // Refresh command initiated on list pull
        public ICommand RefreshCommand{

            get { return new Command( async () => { GetListAsync(); }); }

        } //A two-way binding object, denotes movie currently selected in list
        public MovieModel SelectedMovie {
            get => this._selectedMovie;
            set {
                if (value != null) {
                    this._selectedMovie = value;
                    this._navigation.PushAsync((new MovieDetailPage(_selectedMovie, _server)), true);
                    _selectedMovie = null;
                    OnPropertyChanged();
                }
            }
        }
     
        public MovieListViewModel(INavigation navigation, MovieDBService server, List<MovieModel> movies, int ListType = 0)
        {
            this._navigation = navigation;
            this._server = server;
            this._listType = ListType;
            this._movies = new ObservableCollection<MovieModel>();

            if (ListType == 0)  { this.RefreshEnabled = false; }
            else                { this.RefreshEnabled = true;
                                  GetListAsync(); }

            SearchCommand = new Command(async () =>{
                if (Query != "" && IsRefreshing == false) { GetListAsync(Query); }
            });
    }

        public ICommand SearchCommand { protected set; get; }
        

        private async void GetListAsync(string query = "")
        {
            IsRefreshing = true;

            if (_listType == 0 ) {
                Movies = new ObservableCollection<MovieModel>(await _server.GetBasicMovieInfoByTitleAsync(query));
            }
            else if (_listType == 1) {
                Movies = new ObservableCollection<MovieModel>(await _server.GetBasicTopMoviesInfoAsync());
            }
            else if (_listType == 2) {
                Movies = new ObservableCollection<MovieModel>(await _server.GetPopularMoviesInfoAsync());
            }

            IsRefreshing = false;

            UpdateMovieCast();
        }

        public async Task UpdateMovieCast()
        {
            foreach (var movie in Movies) {
                movie.Cast = await _server.GetThreeCastMembersAsync(movie.Id);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
