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

            if (MovieEntry.Text != null && MovieEntry.Text != "") {

                var movieList = await _server.GetBasicMovieInfoByTitleAsync(this.MovieEntry.Text);
                await Navigation.PushAsync(new MovieList(movieList));
            } else { /* TODO */ }

            // Disable spinner and clear text after resolving request
            this.LoadSpinner.IsRunning = false;
            this.LoadSpinner.IsVisible = false;
            this.MovieEntry.Text = "";
        }
    }
}
