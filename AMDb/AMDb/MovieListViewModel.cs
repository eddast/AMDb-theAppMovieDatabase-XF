using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AMDb
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private List<MovieModel> _movies;
        private MovieModel _selectedMovie;

        public MovieListViewModel(INavigation navigation, List<MovieModel> movies)
        {
            this._navigation = navigation;
            this._movies = movies;
        }

        public List<MovieModel> Movies
        {
            get => this._movies; 
            set {
                this._movies = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MovieModel SelectedMovie
        {
            get => this._selectedMovie;
            set {
                if(value != null) {
                    this._selectedMovie = value;
                    this._navigation.PushAsync((new MovieDetailPage(_selectedMovie)), true);
                }
            }
        } 
  
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
