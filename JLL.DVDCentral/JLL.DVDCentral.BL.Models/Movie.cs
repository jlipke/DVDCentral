﻿using System;
using System.Collections.Generic;
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
        public string ImagePath { get; set; }
        public decimal Cost { get; set; }
        public int InStockQty { get; set; }
        public int RatingId { get; set; }
        public int FormatId { get; set; }
        public int DirectorId { get; set; }
    }
}
