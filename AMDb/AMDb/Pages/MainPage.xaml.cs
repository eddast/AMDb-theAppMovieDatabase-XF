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
            BindingContext = new MovieListViewModel(Navigation, server, null, 0);
        }

    }
}
