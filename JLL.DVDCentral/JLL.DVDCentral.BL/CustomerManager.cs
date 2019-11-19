using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.PL;
using JLL.DVDCentral.BL.Models;


namespace JLL.DVDCentral.BL
{
    public class CustomerManager
    {
        public static bool Insert(Customer customer)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // Make a new row
                    tblCustomer newrow = new tblCustomer();

                    // Set the properties
                    // Ternary Operator condition ? true : false
                    newrow.Id = dc.tblCustomers.Any() ? dc.tblCustomers.Max(p => p.Id) + 1 : 1;     // If there are any rows, get the max id and add 1, if not use 1
                    newrow.FirstName = customer.FirstName;
                    newrow.LastName = customer.LastName;
                    newrow.Address = customer.Address;
                    newrow.City = customer.City;
                    newrow.State = customer.State;
                    newrow.ZIP = customer.ZIP;
                    newrow.Phone = customer.Phone;
                    newrow.UserId = customer.UserId;


                    // Do the Insert
                    dc.tblCustomers.Add(newrow);

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

        public static int Update(Customer customer)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                try
                {

                    tblCustomer updatedrow = dc.tblCustomers.Where(dt => dt.Id == customer.Id).FirstOrDefault();

                    // Check to see if object exists
                    if (customer != null)
                    {
                        // Update the proper fields
                        updatedrow.FirstName = customer.FirstName;
                        updatedrow.LastName = customer.LastName;
                        updatedrow.Address = customer.Address;
                        updatedrow.City = customer.City;
                        updatedrow.State = customer.State;
                        updatedrow.ZIP = customer.ZIP;
                        updatedrow.Phone = customer.Phone;
                        updatedrow.UserId = customer.UserId;


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
                    tblCustomer deleterow = dc.tblCustomers.FirstOrDefault(dt => dt.Id == id);

                    if (deleterow != null)
                    {
                        // Remove the row
                        dc.tblCustomers.Remove(deleterow);

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

        public static Customer LoadById(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        tblCustomer tblcustomer = dc.tblCustomers.FirstOrDefault(p => p.Id == id);

                        if (tblcustomer != null)
                        {
                            Customer customer = new Customer
                            {
                                Id = tblcustomer.Id,
                                FirstName = tblcustomer.FirstName,
                                LastName = tblcustomer.LastName,
                                Address = tblcustomer.Address,
                                City = tblcustomer.City,
                                State = tblcustomer.State,
                                ZIP = tblcustomer.ZIP,
                                Phone = tblcustomer.Phone,
                                UserId = tblcustomer.UserId
                            };

                            return customer;

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

        public static List<Customer> Load()
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    List<Customer> customers = new List<Customer>();
                    dc.tblCustomers.ToList().ForEach(dt => customers.Add(new Customer
                    {
                        Id = dt.Id,
                        FirstName = dt.FirstName,
                        LastName = dt.LastName,
                        Address = dt.Address,
                        City = dt.City,
                        State = dt.State,
                        ZIP = dt.ZIP,
                        Phone = dt.Phone,
                        UserId = dt.UserId
                    }));

                    return customers;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
