using AMDb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AMDb
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MovieDBService server = new MovieDBService();
            var SearchPage = new MainPage(server);
            var SearchNavigationPage = new NavigationPage(SearchPage);
            SearchNavigationPage.Title = "Movie Search";
            
            var TopMoviesPage = new TopRatedPage(server);
            var TopMoviesNavigationPage = new NavigationPage(TopMoviesPage);
            TopMoviesNavigationPage.Title = "Top Rated Movies";


            var PopularPage = new PopularMoviesPage(server);
            var PopularNavigationPage = new NavigationPage(PopularPage);
            PopularNavigationPage.Title = "Popular Movies";

            var tabbedPage = new TabbedPage();

            tabbedPage.Children.Add(SearchNavigationPage);
            tabbedPage.Children.Add(TopMoviesNavigationPage);
            tabbedPage.Children.Add(PopularNavigationPage);

            MainPage = tabbedPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
