using AMDb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMDb
{
    public partial class TopRatedPage : ContentPage
    {

        public TopRatedPage(MovieDBService server)
        {
            InitializeComponent();
            //this.BindingContext = new MovieListViewModel(this.Navigation, server, null, 1);
        }
    }
}
