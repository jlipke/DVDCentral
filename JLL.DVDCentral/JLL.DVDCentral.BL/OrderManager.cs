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
        public static int Insert(Order order, List<Movie> items)  
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // Make new Order row
                    tblOrder newrow = new tblOrder();

                    // Set the properties
                    newrow.Id = order.Id;    
                    newrow.CustomerId = order.CustomerId;
                    newrow.OrderDate = order.OrderDate;
                    newrow.UserId = order.UserId;
                    newrow.PaymentReceipt = order.PaymentReceipt;
                    
                    
                    // Do the Insert
                    dc.tblOrders.Add(newrow);

                    // Commit the insert
                    return dc.SaveChanges();
                    
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
