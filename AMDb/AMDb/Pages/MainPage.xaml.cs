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

        public MainPage(MovieDBService server)
        {
            InitializeComponent();
            BindingContext = new MovieListViewModel(Navigation, server, null, 0);
        }

    }
}
