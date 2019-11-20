using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLL.DVDCentral.BL.Models
{
    public class Movie
    { 
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DisplayName("Image")]
        public string ImagePath { get; set; }
        public decimal Cost { get; set; }

        [DisplayName("Quantity in Stock")]
        public int InStockQty { get; set; }

        [DisplayName("Rating")]
        public int RatingId { get; set; }

        [DisplayName("Format")]
        public int FormatId { get; set; }

        [DisplayName("Director")]
        public int DirectorId { get; set; }

        [DisplayName("Director Name")]
        public string DirectorName { get; set; }

        [DisplayName("Format Description")]
        public string FormatName { get; set; }

        [DisplayName("Rating Description")]
        public string RatingName { get; set; }
    }
}
