using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.PL;
using JLL.DVDCentral.BL.Models;


namespace JLL.DVDCentral.BL
{
    public class OrderItemManager
    {
        public static bool Insert(OrderItem orderItem)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // Make a new row
                    tblOrderItem newrow = new tblOrderItem();

                    // Set the properties
                    // Ternary Operator condition ? true : false
                    newrow.Id = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(p => p.Id) + 1 : 1;     // If there are any rows, get the max id and add 1, if not use 1
                    newrow.OrderId = orderItem.OrderId;
                    newrow.MovieId = orderItem.MovieId;
                    newrow.Quantity = orderItem.Quantity;

                    // Do the Insert
                    dc.tblOrderItems.Add(newrow);

                    // Commit the insert
                    dc.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(OrderItem orderItem)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                try
                {

                    tblOrderItem updatedrow = dc.tblOrderItems.Where(dt => dt.Id == orderItem.Id).FirstOrDefault();

                    // Check to see if object exists
                    if (orderItem != null)
                    {
                        // Update the proper fields
                        updatedrow.OrderId = orderItem.OrderId;
                        updatedrow.MovieId = orderItem.MovieId;
                        updatedrow.Quantity = orderItem.Quantity;


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
                    tblOrderItem deleterow = dc.tblOrderItems.FirstOrDefault(dt => dt.Id == id);

                    if (deleterow != null)
                    {
                        // Remove the row
                        dc.tblOrderItems.Remove(deleterow);

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

        public static OrderItem LoadById(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        tblOrderItem tblorderItem = dc.tblOrderItems.FirstOrDefault(p => p.Id == id);

                        if (tblorderItem != null)
                        {
                            OrderItem orderItem = new OrderItem
                            {
                                Id = tblorderItem.Id,
                                OrderId = tblorderItem.OrderId,
                                MovieId = tblorderItem.MovieId,
                                Quantity = tblorderItem.Quantity
                            };

                            return orderItem;

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

        public static List<OrderItem> LoadByOrderId(int orderid)
        {
            try
            {
                if (orderid != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        
                        tblOrderItem tblorderItem = dc.tblOrderItems.FirstOrDefault(p => p.OrderId == orderid);

                        if (tblorderItem != null)
                        {
                            List<OrderItem> orderItems = new List<OrderItem>();
                            dc.tblOrderItems.ToList().ForEach(p => orderItems.Add(new OrderItem
                            {
                                Id = tblorderItem.Id,
                                OrderId = tblorderItem.OrderId,
                                MovieId = tblorderItem.MovieId,
                                Quantity = tblorderItem.Quantity
                            }));

                            return orderItems;

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

        
    }
}
