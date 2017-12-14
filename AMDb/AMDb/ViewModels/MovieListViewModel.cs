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
        private string _query;
        public bool RefreshEnabled { get; set; }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set {

                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public string Query
        {
            get => _query;
            set {

                if (value != null){
                    _query = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RefreshCommand
        {
            get { return new Command( async () => { GetListAsync(); }); }
        }

        public MovieListViewModel(INavigation navigation, MovieDBService server, List<MovieModel> movies, int ListType = 0)
        {
            this._navigation = navigation;
            this._server = server;
            this._listType = ListType;

            if (ListType == 0)  { this.RefreshEnabled = false; }
            else                { this.RefreshEnabled = true;
                                  GetListAsync(); }

            SearchCommand = new Command(async () =>{
                if (Query != "") { GetListAsync(Query); }
                else { /*TODO*/ }
            });
    }

        public ICommand SearchCommand { protected set; get; }
        

        private async void GetListAsync(string query = "")
        {
            IsRefreshing = true;

            if (_listType == 0 ) {
                var MovieModelList = await _server.GetBasicMovieInfoByTitleAsync(query);
                Movies = new ObservableCollection<MovieModel>(MovieModelList);
            }
            else if (_listType == 1) {
                var MovieModelList = await _server.GetBasicTopMoviesInfoAsync();
                Movies = new ObservableCollection<MovieModel>(MovieModelList);
            }
            else if (_listType == 2) {
                IsRefreshing = true;
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
