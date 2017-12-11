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
        public MainPage()
        {
            InitializeComponent();
        }

        private async void MovieButton_Clicked(object sender, EventArgs e)
        {
            MovieDBService server = new MovieDBService();
            this.LoadSpinner.IsRunning = true;
            this.LoadSpinner.IsVisible = true;
            var firstMovieResult = await server.GetMovieListByTitleAsync(this.MovieEntry.Text);
            this.MovieLabel.Text = firstMovieResult[0].title;
            this.LoadSpinner.IsRunning = false;
        }
    }
}
