﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLL.DVDCentral.BL.Models
{
    public class ShoppingCart
    {
        // This does not apply to the DVDCentral app
        // The cost of a movie is retrieved from the tblMovie.Cost
        const double ITEM_COST = 49.99;

        public List<Movie> Items { get; set; }
        public int TotalCount { get { return Items.Count; } }
        public double TotalCost { get; set; }

        public ShoppingCart()
        {
            Items = new List<Movie>();
        }

        public void Add(Movie movie)
        {
            Items.Add(movie);
            TotalCost += ITEM_COST;
        }

        public void Remove(Movie progDec)
        {
            Items.Remove(progDec);
            TotalCost -= ITEM_COST;
        }

        public void Checkout()
        {
            Items = new List<Movie>();
            TotalCost = 0;
        }

    }
}