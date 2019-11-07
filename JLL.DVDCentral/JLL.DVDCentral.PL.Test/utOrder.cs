using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.PL;
using System.Linq;
using System.Data.Entity;


namespace JLL.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
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
            tblOrder newrow = new tblOrder();

            // Set the column values
            newrow.Id = -99;
            newrow.CustomerId = 123456789;
            newrow.OrderDate = DateTime.Now;
            newrow.UserId = 9998888;
            newrow.PaymentReceipt = "Payment Receipt";

            // Insert of the row
            dc.tblOrders.Add(newrow);

            // Commit the changes, return the rows affected
            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);

        }

        [TestMethod]
        public void Update()
        {
            tblOrder existingOrder = (from dt in dc.tblOrders
                                      where dt.Id == -99
                                      select dt).FirstOrDefault();

            if (existingOrder != null)
            {
                // Update the Information
                existingOrder.CustomerId = 234567890;
                existingOrder.OrderDate = DateTime.Now;
                existingOrder.UserId = 1112223;
                existingOrder.PaymentReceipt = "Updated Payment Receipt";
                dc.SaveChanges();
            }

            tblOrder updatedOrder = (from dt in dc.tblOrders
                                     where dt.Id == -99
                                     select dt).FirstOrDefault();

            Assert.AreEqual(existingOrder, updatedOrder);
        }

        [TestMethod]
        public void Delete()
        {
            tblOrder existingOrder = (from dt in dc.tblOrders
                                        where dt.Id == -99
                                        select dt).FirstOrDefault();

            if (existingOrder != null)
            {
                // Delete the row
                dc.tblOrders.Remove(existingOrder);
                dc.SaveChanges();
            }

            tblOrder deletedOrder = (from dt in dc.tblOrders
                                      where dt.Id == -99
                                       select dt).FirstOrDefault();

            Assert.IsNull(deletedOrder);
        }

        [TestMethod]
        public void Load()
        {
            int expected = 3;
            int actual = 0;

            // Retrieve the orders from the database
            var orders = dc.tblOrders;

            actual = orders.Count();

            // Test to see if actual equals expected
            Assert.AreEqual(expected, actual);

            dc = null;
        }

    }
}
