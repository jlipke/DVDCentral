using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.PL;

namespace JLL.DVDCentral.BL
{
    public static class UserManager
    {
       
        private static string GetHash(string passcode)
        {
            using (var hash = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(passcode);
                return Convert.ToBase64String(hash.ComputeHash(hashbytes));
            }
        }

        public static void Map()
        {

        }

        public static void Insert(string userid, string firstname, string lastname, string userpass)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblUser newuser = new tblUser();
                    newuser.Id = dc.tblUsers.Any() ? dc.tblUsers.Max(u => u.Id) + 1 : 1;        // If there are any users I will take the max and add 1. If there arent any, use 1    
                    newuser.UserPass = GetHash(userpass);
                    newuser.FirstName = firstname;
                    newuser.LastName = lastname;
                    newuser.UserId = userid;

                    dc.tblUsers.Add(newuser);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void Insert(User user)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblUser newuser = new tblUser();
                    newuser.Id = dc.tblUsers.Any() ? dc.tblUsers.Max(u => u.Id) + 1 : 1;        // If there are any users I will take the max and add 1. If there arent any, use 1    
                    newuser.UserPass = GetHash(user.PassCode);
                    newuser.FirstName = user.FirstName;
                    newuser.LastName = user.LastName;
                    newuser.UserId = user.UserId;

                    dc.tblUsers.Add(newuser);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void Seed()
        {
            // Used to default some data
            User newuser = new User("cpine", "Chris", "Pine", "maple");
            Insert(newuser);
        }

        public static bool Login(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.UserId))
                {
                    if (!string.IsNullOrEmpty(user.PassCode))
                    {
                        using (DVDCentralEntities dc = new DVDCentralEntities())
                        {
                            tblUser tblUser = dc.tblUsers.FirstOrDefault(u => u.UserId == user.UserId);
                            if (tblUser != null)
                            {
                                // Check the password
                                if (tblUser.UserPass == GetHash(user.PassCode))
                                {
                                    // User could log in
                                    user.FirstName = tblUser.FirstName;
                                    user.LastName = tblUser.LastName;
                                    user.Id = tblUser.Id;
                                    return true;
                                }
                                else
                                {
                                    //throw new Exception("Cannot login with these credentials. Your IP has been saved.");
                                    return false;
                                }
                            }
                            else
                            {
                                throw new Exception("User Id could not be found.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Password was not set.");
                    }
                }
                else
                {
                    throw new Exception("UserId was not set.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int GetCustomerId(string userId)
        {
            try
            {
                if(userId != null || userId != "")
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        var customer = (from c in dc.tblCustomers
                                        join u in dc.tblUsers on c.UserId equals u.UserId
                                        where u.UserId == userId
                                        select new
                                        {
                                            CustomerId = c.Id,
                                            CUserId = u.UserId,   // Check later
                                            CFirstName = c.FirstName,
                                            CLastName = c.LastName,
                                            CAddress = c.Address,
                                            CCity = c.City,
                                            CState = c.State,
                                            CZIP = c.ZIP,
                                            CPhone = c.Phone

                                       //FormatId = f.Id,
                                       //RatingId = r.Id,
                                       //DirectorId = d.Id,
                                       //d.FirstName,
                                       //d.LastName,
                                       //FormatName = f.Description,
                                       //RatingName = r.Description,
                                       //MVTitle = mv.Title,
                                       //MVDescription = mv.Description,
                                       //MVImagePath = mv.ImagePath,
                                       //MVCost = mv.Cost,
                                       //MVInStockQuantity = mv.InStockQty

                                   }).FirstOrDefault();

                       

                        if (customer != null)
                        {
                            JLL.DVDCentral.BL.Models.Customer Customer = new JLL.DVDCentral.BL.Models.Customer
                             
                            {
                                Id = customer.CustomerId,
                                UserId = customer.CUserId,
                                FirstName = customer.CFirstName,
                                LastName = customer.CLastName,
                                Address = customer.CAddress,
                                City = customer.CCity,
                                State = customer.CState,
                                ZIP = customer.CZIP,
                                Phone = customer.CPhone,
                                
                            };

                            return Customer.Id;

                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                }
                else
                {
                    throw new Exception("Row was not found.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
