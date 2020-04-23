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
        [BindProperty]
        public string SearchTerms { get; set; }


        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet( double? IMDBMini, double? IMDBMaxi, double? RTMaxi, double? RTMini)
        {

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
            
        }

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        /// 
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered Genres
        /// </summary>
        [BindProperty]
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum tomatos Rating
        /// </summary>
        [BindProperty]
        public double? RTMin { get; set; }

        /// <summary>
        /// The maximum tomatos Rating
        /// </summary>
        [BindProperty]
        public double? RTMax { get; set; }


    }
}
