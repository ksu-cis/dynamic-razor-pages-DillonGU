using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        
        /// <summary>
        /// The movies to display on the index page 
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms 
        /// </summary>
        [BindProperty(SupportsGet =true)]
        public string SearchTerms { get; set; }


        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet( double? IMDBMini, double? IMDBMaxi, double? RTMaxi, double? RTMini)
        {
            /*
            //# Nullable conversion workaround
            
            this.IMDBMin = IMDBMini;
            this.IMDBMax = IMDBMaxi;
            //this.SearchTerms = SearchTerms;
           // this.MPAARatings = MPAARatings;
            //this.Genres = Genres;
            this.RTMax = RTMaxi;
            this.RTMin = RTMini;
            SearchTerms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            Genres = Request.Query["Genres"];
            Movies = MovieDatabase.All;
            
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRTRating(Movies, RTMin, RTMax);
            */
            Movies = MovieDatabase.All;
            if (SearchTerms != null)
            {
                Movies= Movies.Where(movie => 
                movie.Title != null && 
                movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase));
            }
            //genre
            if(Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie =>
                movie.MajorGenre != null &&
                Genres.Contains(movie.MajorGenre));
            }
            // Filter by MPAA Rating 
            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie =>
                    movie.MPAARating != null &&
                    MPAARatings.Contains(movie.MPAARating)
                    );
            }
            //imdb
            if(IMDBMax != null && IMDBMax != 0)
            
            {
                Movies = Movies.Where(movie =>
                movie.IMDBRating <= IMDBMax);
            }
            if (IMDBMin != null && IMDBMin != 0)

            {
                Movies = Movies.Where(movie =>
                movie.IMDBRating >= IMDBMin);
            }
            if (IMDBMax != null && IMDBMax != 0 && IMDBMin != null && IMDBMin != 0)
            {
                Movies = Movies.Where(movie =>
                movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax);
            }
            //rotten
            if (RTMax != null && RTMax != 0)

            {
                Movies = Movies.Where(movie =>
                movie.RottenTomatoesRating <= RTMax);
            }
            if (RTMin != null && RTMin != 0)

            {
                Movies = Movies.Where(movie =>
                movie.RottenTomatoesRating >= RTMin);
            }
            if (RTMax != null && RTMax != 0 && RTMin != null && RTMin != 0)
            {
                Movies = Movies.Where(movie =>
                movie.RottenTomatoesRating >= RTMin && movie.RottenTomatoesRating <= RTMax);
            }
        }

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        /// 
        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered Genres
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum tomatos Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RTMin { get; set; }

        /// <summary>
        /// The maximum tomatos Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RTMax { get; set; }


    }
}
