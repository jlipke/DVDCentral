using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.BL;

namespace JLL.DVDCentral.MVCUI.ViewModels
{
    public class MovieGenresDirectorsRatingsFormats
    {
        public DVDCentral.BL.Models.Movie Movie { get; set; }
        
        public List<Director> DirectorList { get; set; }
        public List<Rating> RatingList { get; set; }
        public List<Format> FormatList { get; set; }

        // List of all the genre objects
        public List<Genre> Genres { get; set; }         // Change to GenreList

        // Working List of Genre Id's for this Movie
        public IEnumerable<int> GenreIds { get; set; }

    }
}