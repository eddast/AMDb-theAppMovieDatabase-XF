using AMDb.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMDb
{
    internal class SearchViewModel : INotifyPropertyChanged
    {
        private MovieDBService _server;
        private INavigation _navigation;
        private bool searching;
        private bool buttonClickable;
        private string query;
        private List<MovieModel> _movies;
        public bool IsSearching
        {
            get => this.searching;
            set {
                this.searching = value;
                OnPropertyChanged();
            }
        }
        public bool ButtonClickable
        {
            get => this.buttonClickable;
            set
            {
                this.buttonClickable = value;
                OnPropertyChanged();
            }
        }
        public string Query
        {
            get => query;
            set {

                if (value != null) {

                    query= value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SearchCommand { protected set; get; }

        public SearchViewModel(INavigation navigation, MovieDBService server)
        {
            this._server = server;
            this._navigation = navigation;
            searching = false;
            buttonClickable = true;
            _movies = new List<MovieModel>();

            SearchCommand = new Command(async () =>{

                if (query != "") {

                    IsSearching = true; ButtonClickable = false;
                    _movies = await _server.GetBasicMovieInfoByTitleAsync(query);
                    await navigation.PushAsync(new MovieList(_movies, _server));
                    IsSearching = false; ButtonClickable = true;
                    Query = ""; //_movies = new List<MovieModel>();
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}