using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;

namespace JLL.DVDCentral.PL.Test
{
    [TestClass]
    public class utDirector
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
        public void LoadDirectorsTest()
        {
            int expected = 3;
            int actual = 0;

            // Retrieve the degreetypes from the database
            var directors = dc.tblDirectors;

            actual = directors.Count();

            // Test to see if actual equals expected
            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblDirector newrow = new tblDirector();

            // Set the column values
            newrow.Id = -99;
            newrow.FirstName = "Ronald";
            newrow.LastName = "McDonald";

            // Insert of the row
            dc.tblDirectors.Add(newrow);

            // Commit the changes (insert a row) and then return the rows affected.
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblDirector existingDirector = (from dt in dc.tblDirectors
                                            where dt.Id == -99
                                                select dt).FirstOrDefault();

            if (existingDirector != null)
            {
                // Update the First and Last Name
                existingDirector.FirstName = "Ronald";
                existingDirector.FirstName = "McDon";
                dc.SaveChanges();
            }

            tblDirector updatedDirector= (from dt in dc.tblDirectors
                                               where dt.Id == -99
                                               select dt).FirstOrDefault();

            Assert.AreEqual(existingDirector, updatedDirector);
        }
    }
}
