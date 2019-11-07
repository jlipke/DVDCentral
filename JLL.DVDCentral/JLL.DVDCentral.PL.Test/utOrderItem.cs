using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.PL;
using System.Linq;


namespace JLL.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem
    {

        protected DVDCentralEntities dc;
        protected DbContextTransaction transaction;

        [TestInitialize]
        public void Initialize()
        {
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
        public void Insert()
        {
            tblOrderItem newrow = new tblOrderItem();

            // Set the column values
            newrow.Id = -99;
            newrow.OrderId = 123456789;
            newrow.MovieId = 55555;
            newrow.Quantity = 10;

            // Insert of the row
            dc.tblOrderItems.Add(newrow);

            // Commit the changes, return the rows affected
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);

        }

        [TestMethod]
        public void Update()
        {
            tblOrderItem existingOrderItem = (from dt in dc.tblOrderItems
                                      where dt.Id == -99
                                      select dt).FirstOrDefault();

            if (existingOrderItem != null)
            {
                // Update the Information
                existingOrderItem.OrderId = 234567890;
                existingOrderItem.MovieId = 66666;
                existingOrderItem.Quantity = 11;
                dc.SaveChanges();
            }

            tblOrderItem updatedOrderItem = (from dt in dc.tblOrderItems
                                     where dt.Id == -99
                                     select dt).FirstOrDefault();

            Assert.AreEqual(existingOrderItem, updatedOrderItem);
        }

        [TestMethod]
        public void Delete()
        {
            tblOrderItem existingOrderItem = (from dt in dc.tblOrderItems
                                      where dt.Id == -99
                                      select dt).FirstOrDefault();

            if (existingOrderItem != null)
            {
                // Delete the row
                dc.tblOrderItems.Remove(existingOrderItem);
                dc.SaveChanges();
            }

            tblOrderItem deletedOrderItem = (from dt in dc.tblOrderItems
                                     where dt.Id == -99
                                     select dt).FirstOrDefault();

            Assert.IsNull(deletedOrderItem);
        }

        [TestMethod]
        public void Load()
        {
            int expected = 3;
            int actual = 0;

            // Retrieve the orderItems from the database
            var orderItems = dc.tblOrderItems;

            actual = orderItems.Count();

            // Test to see if actual equals expected
            Assert.AreEqual(expected, actual);

            dc = null;
        }

    }
}