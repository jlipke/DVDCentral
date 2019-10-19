using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;

namespace JLL.DVDCentral.PL.Test
{
    [TestClass]
    public class utRating
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
        public void LoadRatingsTest()
        {
            int expected = 5;
            int actual = 0;

            // Retrieve the degreetypes from the database
            var ratings = dc.tblRatings;

            actual = ratings.Count();

            // Test to see if actual equals expected
            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblRating newrow = new tblRating();

            // Set the column values
            newrow.Id = -99;
            newrow.Description = "Test";

            // Insert of the row
            dc.tblRatings.Add(newrow);

            // Commit the changes (insert a row) and then return the rows affected.
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblRating existingRating = (from dt in dc.tblRatings
                                            where dt.Id == -99
                                            select dt).FirstOrDefault();

            if (existingRating != null)
            {
                // Update the description
                existingRating.Description = "Test";
                dc.SaveChanges();
            }

            tblRating updatedRating = (from dt in dc.tblRatings
                                             where dt.Id == -99
                                             select dt).FirstOrDefault();

            Assert.AreEqual(existingRating, updatedRating);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblRating existingRating = (from dt in dc.tblRatings
                                                where dt.Id == -99
                                                select dt).FirstOrDefault();

            if (existingRating != null)
            {
                // Delete the row
                dc.tblRatings.Remove(existingRating);
                dc.SaveChanges();
            }

            tblRating deletedRating = (from dt in dc.tblRatings
                                               where dt.Id == -99
                                               select dt).FirstOrDefault();

            Assert.IsNull(deletedRating);
        }
    }
}
