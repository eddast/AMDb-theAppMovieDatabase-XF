using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AMDb.Services;


namespace AMDb
{
    public partial class MainPage : ContentPage
    {
        private MovieDBService _server;

        public MainPage(MovieDBService server)
        {
            InitializeComponent();
            this._server = server;
        }

        private async void MovieButton_Clicked(object sender, EventArgs e)
        {
            // Indicate background activity by enabling spinner
            this.LoadSpinner.IsRunning = true;
            this.LoadSpinner.IsVisible = true;

            var movieList = await _server.GetBasicMovieInfoByTitleAsync(this.MovieEntry.Text);
            if (movieList.Count != 0 ) await Navigation.PushAsync(new MovieList(movieList));

            // Disable spinner and clear text after resolving request
            this.LoadSpinner.IsRunning = false;
            this.LoadSpinner.IsVisible = false;
            this.MovieEntry.Text = "";
        }
    }
}
