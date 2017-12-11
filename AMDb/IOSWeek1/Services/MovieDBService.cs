using System.Collections.Generic;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace AMDb.Services
{
    public class MovieDBService
    {
        private MovieDBSettings _settings;
        private IApiMovieRequest _movieApi;

        public MovieDBService() {
            
            // Register settings with MovieDBSettings class
            // Create query API and search by movieField value
            _settings = new MovieDBSettings();
            MovieDbFactory.RegisterSettings(_settings);
            _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
        }

        // Returns movie model list with appropriate info searhing by title substring
        public async System.Threading.Tasks.Task<List<MovieModel>> GetMovieListByTitleAsync(string title)
        {
            // Conduct query and await response
            // If query returns no result, movieList becomes a null list
            ApiSearchResponse<MovieInfo> response_m = await _movieApi.SearchByTitleAsync(title);
            IReadOnlyList<MovieInfo> movieInfoList = response_m.Results;
            List<MovieModel> movieList = await getMovieModelListByMovieInfoAsync(movieInfoList);


            return movieList;
        }

        public async System.Threading.Tasks.Task<List<MovieModel>> GetTopMoviesViewAsync()
        {
            // Conduct query and await response
            // If query returns no result, movieList becomes a null list
            var response_m = await _movieApi.GetTopRatedAsync();
            IReadOnlyList<MovieInfo> movieInfoList = response_m.Results;
            List<MovieModel> movieModelList = await getMovieModelListByMovieInfoAsync(movieInfoList);


            return movieModelList;
        }

        // retrieves movie model information from movie info
        private async System.Threading.Tasks.Task<List<MovieModel>> getMovieModelListByMovieInfoAsync(IReadOnlyList<MovieInfo> movieInfoList)
        {
            List<MovieModel> movieModelList = new List<MovieModel>();

            foreach (MovieInfo movie in movieInfoList) {

                // Get poster path, starring cast and movie runtime
                // Then create a model with those values and add it to list
                //var localFilePath = await DownloadPosterAsync(movie.PosterPath);
                //var localFilePathBackdrop = await DownloadPosterAsync(movie.BackdropPath);
                var movieCast = await GetThreeCastMembersAsync(movie.Id);
                var runtime = await GetRuntimeAsync(movie.Id);

                MovieModel movieModel = new MovieModel(movie, movieCast, runtime);
                movieModelList.Add(movieModel);
            }


            return movieModelList;
        }

        // Extract three starring actors from MovieCredit object by movie ID
        private async System.Threading.Tasks.Task<string> GetThreeCastMembersAsync(int id)
        {
            ApiQueryResponse<MovieCredit> responseCast = await _movieApi.GetCreditsAsync(id);
            string movieCast = "";

            for (int i = 0; i < 3; i++){

                if (i == responseCast.Item.CastMembers.Count) break;
                if (i != 0) movieCast = movieCast + ", ";
                movieCast = movieCast + responseCast.Item.CastMembers[i].Name;
            }


            return movieCast;
        }

        // Extract movie runtime from Movie object by movie ID
        private async System.Threading.Tasks.Task<string> GetRuntimeAsync(int id)
        {
            ApiQueryResponse<Movie> tm_movie = await _movieApi.FindByIdAsync(id);
            string runtime = tm_movie.Item.Runtime.ToString();


            return runtime;
        }
    }
}
