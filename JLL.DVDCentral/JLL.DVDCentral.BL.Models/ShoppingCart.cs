using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLL.DVDCentral.BL.Models
{
    public class ShoppingCart
    {
        
        public List<Movie> Items { get; set; }
        public int TotalCount { get { return Items.Count; } }
        public double TotalCost { get; set; }
        public double SubTotalCost { get; set; }
        public double TaxCost { get; set; }
        public double Cost { get; set; }

        public ShoppingCart()
        {
            Items = new List<Movie>();
        }

        public void Add(Movie movie)
        {
            Items.Add(movie);
            SubTotalCost += Convert.ToDouble(movie.Cost);
            TaxCost = (SubTotalCost * 0.055);
            TotalCost = SubTotalCost + TaxCost;
            
        }

        public void Remove(Movie movie)
        {
            Items.Remove(movie);
            SubTotalCost -= Convert.ToDouble(movie.Cost);
            TaxCost = (SubTotalCost * 0.055);
            TotalCost = SubTotalCost + TaxCost;

        }

        public void Checkout()
        {
            Items = new List<Movie>();
            TotalCost = 0;
            SubTotalCost = 0;
            TaxCost = 0;
        }

    }
}
