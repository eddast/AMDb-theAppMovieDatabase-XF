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

            // Dependency inject the MovieDBService object to
            // all pages requiring communication with the API
            MovieDBService server = new MovieDBService();

            // Create the search page as a navigation page
            var SearchPage = new MainPage(server);
            var SearchNavigationPage = new NavigationPage(SearchPage);
            SearchNavigationPage.Title = "Movie Search";

            // Create the top rated movies page as a navigation page
            var TopMoviesPage = new TopRatedPage(server);
            var TopMoviesNavigationPage = new NavigationPage(TopMoviesPage);
            TopMoviesNavigationPage.Title = "Top Rated Movies";

            // Create the popular movies page as a navigation page
            var PopularPage = new PopularMoviesPage(server);
            var PopularNavigationPage = new NavigationPage(PopularPage);
            PopularNavigationPage.Title = "Popular Movies";

            // Add all created pages into a tabbed page, set as main page
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
