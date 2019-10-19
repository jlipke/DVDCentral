using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace JLL.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovie
    {
        [TestMethod]
        public void LoadByIdTest()
        {
           
            Movie movies = MovieManager.LoadById(2);
            
            Assert.IsNotNull(movies.Title, movies.Description,
                             movies.ImagePath, movies.Cost,
                             movies.InStockQty, movies.RatingId,
                             movies.FormatId, movies.DirectorId);
        }
        [TestMethod]
        public void DeleteTest()
        {
            // Insert a movie to be deleted

            Movie movie = new Movie();
            movie.Title = "test";
            movie.Description = "test";
            movie.ImagePath = "test";
            movie.Cost = 1;
            movie.InStockQty = 3;
            movie.RatingId = 3;
            movie.FormatId = 1;
            movie.DirectorId = 1;

            bool result = MovieManager.Insert(movie);

            // Delete new movie
            int MovieDelete = MovieManager.Delete(4);
            Assert.IsTrue(MovieDelete > 0);
        }
            
    }
}
