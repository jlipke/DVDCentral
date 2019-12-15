using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace JLL.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrder
    {
        [TestMethod]
        public void Insert()
        {
            Order order = new Order();
            order.CustomerId = 99;
            order.OrderDate = DateTime.Now;
            order.UserId = 123;
            order.PaymentReceipt = "Payment Receipt";

            OrderItem orderItem_1 = new OrderItem();
            orderItem_1.OrderId = 4;
            orderItem_1.MovieId = 5555;
            orderItem_1.Quantity = 1;

            OrderItem orderItem_2 = new OrderItem();
            orderItem_2.OrderId = 5;
            orderItem_2.MovieId = 333;
            orderItem_2.Quantity = 2;

            //bool OrderResult = OrderManager.Insert(order);
            bool OrderItemResult1 = OrderItemManager.Insert(orderItem_1);
            bool OrderItemResult2 = OrderItemManager.Insert(orderItem_2);

            //Assert.IsTrue(OrderResult);
            Assert.IsTrue(OrderItemResult1);
            Assert.IsTrue(OrderItemResult2);
        }

        [TestMethod]
        public void Update()
        {
            Order order = OrderManager.LoadById(4);

            order.CustomerId = 999;
            order.OrderDate = DateTime.Now;
            order.UserId = 123456789;
            order.PaymentReceipt = "Updated Payment Receipt";

            OrderManager.Update(order);

            Order updated_order = OrderManager.LoadById(4);

            Assert.AreEqual(order.CustomerId, updated_order.CustomerId);
        }

        [TestMethod]
        public void Delete()
        {
            int results = OrderManager.Delete(4);
            Assert.IsTrue(results > 0);
        }

        [TestMethod]
        public void Load()
        {
            List<Order> orders = new List<Order>();
            orders = OrderManager.Load();
            int expected = 4;
            Assert.AreEqual(expected, orders.Count);
        }
    }
}
