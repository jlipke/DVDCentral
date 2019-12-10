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
        
        public List<Genre> GenreList { get; set; }
        public List<Director> DirectorList { get; set; }
        public List<Rating> RatingList { get; set; }
        public List<Format> FormatList { get; set; }


        // Working List of Genre Id's for this Movie
        public IEnumerable<int> GenreIds { get; set; }

    }
}