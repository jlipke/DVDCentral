using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;

namespace JLL.DVDCentral.PL.Test
{
    [TestClass]
    public class utGenre
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
        public void LoadGenresTest()
        {
            int expected = 7;
            int actual = 0;

            // Retrieve the degreetypes from the database
            var genres = dc.tblGenres;

            actual = genres.Count();

            // Test to see if actual equals expected
            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblGenre newrow = new tblGenre();

            // Set the column values
            newrow.Id = -99;
            newrow.Description = "My new Genre";

            // Insert of the row
            dc.tblGenres.Add(newrow);

            // Commit the changes (insert a row) and then return the rows affected.
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }
    }
}
