using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.PL;
using JLL.DVDCentral.BL.Models;


namespace JLL.DVDCentral.BL
{
    public class OrderManager
    {
        public static void Insert(Order order, List<Movie> items)
        {
            try
            {
                // create a tblOrder row
                // Loop through the items and create a tblOrderItem row with the new Order Id.


                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // Make new Order row
                    tblOrder Ordernewrow = new tblOrder();

                    foreach (var item in items)
                    {
                       // Make new OrderItem row
                        tblOrderItem OrderItem_newrow = new tblOrderItem();

                        // Set the properties
                        OrderItem_newrow.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(p => p.Id) + 1 : 1;     // If there are any rows, get the max id and add 1, if not use 1
                        OrderItem_newrow.OrderId = order.Id;
                        OrderItem_newrow.MovieId = item.Id;     // Should work?
                        OrderItem_newrow.Quantity = items.Count;     // Not sure how this will work yet, would require to the controller to keep track how many of the same movie was added

                        // Do the Insert
                        dc.tblOrderItems.Add(OrderItem_newrow);

                    }

                    // Set the properties
                    Ordernewrow.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(p => p.Id) + 1 : 1;     // If there are any rows, get the max id and add 1, if not use 1
                    Ordernewrow.CustomerId = order.CustomerId;
                    Ordernewrow.OrderDate = order.OrderDate;
                    Ordernewrow.UserId = order.UserId;
                    Ordernewrow.PaymentReceipt = order.PaymentReceipt;

                    
                    // Do the Insert
                    dc.tblOrders.Add(Ordernewrow);

                    // Commit the insert
                    dc.SaveChanges();
                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(Order order)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                try
                {

                    tblOrder updatedrow = dc.tblOrders.Where(dt => dt.Id == order.Id).FirstOrDefault();

                    // Check to see if object exists
                    if (order != null)
                    {
                        // Update the proper fields
                        updatedrow.CustomerId = order.CustomerId;
                        updatedrow.OrderDate = order.OrderDate;
                        updatedrow.UserId = order.UserId;
                        updatedrow.PaymentReceipt = order.PaymentReceipt;


                        // Save and commit changes
                        return dc.SaveChanges();
                    }
                    else
                    {
                        // throw an exception stating the row was not found
                        throw new Exception("Row was not found.");

                    }


                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public static int Delete(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrder deleterow = dc.tblOrders.FirstOrDefault(dt => dt.Id == id);

                    if (deleterow != null)
                    {
                        // Remove the row
                        dc.tblOrders.Remove(deleterow);

                        // Commit/Save the changes
                        return dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Order LoadById(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        tblOrder tblorder = dc.tblOrders.FirstOrDefault(p => p.Id == id);

                        if (tblorder != null)
                        {
                            Order order = new Order
                            {
                                Id = tblorder.Id,
                                CustomerId = tblorder.CustomerId,
                                OrderDate = tblorder.OrderDate,
                                UserId = tblorder.UserId,
                                PaymentReceipt = tblorder.PaymentReceipt
                            };

                            return order;

                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                }
                else
                {
                    throw new Exception("Please provide an id");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Order> LoadByCustomerId(int customerid)
        {
            try
            {
                if (customerid != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        tblOrder tblorder = dc.tblOrders.FirstOrDefault(p => p.CustomerId == customerid);

                        if (tblorder != null)
                        {
                            List<Order> orders = new List<Order>();
                            dc.tblOrders.ToList().ForEach(p => orders.Add(new Order
                            {
                                Id = tblorder.Id,
                                CustomerId = tblorder.CustomerId,
                                OrderDate = tblorder.OrderDate,
                                UserId = tblorder.UserId,
                                PaymentReceipt = tblorder.PaymentReceipt
                            }));

                            return orders;

                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                }
                else
                {
                    throw new Exception("Please provide an id");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Order> Load()
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    List<Order> orders = new List<Order>();
                    dc.tblOrders.ToList().ForEach(dt => orders.Add(new Order
                    {
                        Id = dt.Id,
                        CustomerId = dt.CustomerId,
                        OrderDate = dt.OrderDate,
                        UserId = dt.UserId,
                        PaymentReceipt = dt.PaymentReceipt
                    }));

                    return orders;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
