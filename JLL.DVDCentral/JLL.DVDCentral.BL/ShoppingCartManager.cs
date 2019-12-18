using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.PL;

namespace JLL.DVDCentral.BL
{
    public static class ShoppingCartManager
    {
        public static void Checkout(ShoppingCart cart, User user /*, Customer customer */)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                /* For DVD Central, do these things when you checkout
                 1) Insert an tblOrder. Get the Order.Id
                 2) Loop through the Items, insert a tblOrderItem record with the new Order.Id
                 3) Remove the item from the cart.
                 Add in an OrderManager*/

                 
                Order order = new Order();
                order.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(p => p.Id) + 1 : 1;
                order.UserId = user.UserId;

                var cid = (from c in dc.tblCustomers
                           join u in dc.tblUsers on c.UserId equals u.UserId
                           where c.UserId == user.UserId
                           select new
                           {
                               CustomerId = c.Id,
                               CFirstName = c.FirstName,
                               CLastName = c.LastName,
                               CAddress = c.Address,
                               CCity = c.City,
                               CState = c.State,
                               CZip = c.ZIP,
                               CPhone = c.Phone,
                               UserID = c.UserId

                           }).FirstOrDefault();

                //if (cid != null)
                //{
                //    Customer customer = new Customer
                //    {

                //    };

                //}

                order.CustomerId = 1;   // Until cid isn't null
                order.PaymentReceipt = Convert.ToString(cart.TotalCost);
                order.OrderDate = DateTime.Now;
                OrderManager.Insert(order, cart.Items);
                cart.Checkout();
            }
            
        }

        public static void Add(ShoppingCart cart, Movie movie)
        {
            cart.Add(movie);
        }

        public static void Remove(ShoppingCart cart, Movie movie)
        {
            cart.Remove(movie);
        }
    }
}
