using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AMDb.Services;


namespace AMDb
{
    public partial class TopRatedPage: ContentPage
    {
        private MovieDBService _server;

        public TopRatedPage(MovieDBService server)
        {
            InitializeComponent();
            this._server = server;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _server.GetBasicTopMoviesInfoAsync();

        }
    }
}
