using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AMDb
{
    public partial class InitializeTabbedPage : TabbedPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((TopRatedPage)((NavigationPage)Children[1]).RootPage).viewModel.GetListAsync();
            await ((PopularMoviesPage)((NavigationPage)Children[2]).RootPage).viewModel.GetListAsync();
        }
    }
}
