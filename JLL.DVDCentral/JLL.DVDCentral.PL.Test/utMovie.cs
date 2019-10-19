using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;

namespace JLL.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie
    {
        protected DVDCentralEntities dc;
        protected DbContextTransaction transaction;

        [TestInitialize]
        public void Initialize()
        {
            // Will happen everytime we run a test
            dc = new DVDCentralEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TransactionCleanUp()
        {
            // Rollback changes
            transaction.Rollback();
            transaction.Dispose();
        }

        [TestMethod]
        public void LoadMoviesTest()
        {
            int expected = 3;
            int actual = 0;

            // Retrieve the degreetypes from the database
            var movies = dc.tblMovies;

            actual = movies.Count();

            // Test to see if actual equals expected
            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblMovie newrow = new tblMovie();

            // Set the column values
            newrow.Id = -99;
            newrow.Title = "My new Movie";
            newrow.Description = "Description";
            newrow.ImagePath = "ImagePath";
            newrow.Cost = 100;
            newrow.InStockQty = 100;
            newrow.RatingId = 1;
            newrow.FormatId = 1;
            newrow.DirectorId = 1;

            // Insert of the row
            dc.tblMovies.Add(newrow);

            // Commit the changes (insert a row) and then return the rows affected.
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }
    }
}
