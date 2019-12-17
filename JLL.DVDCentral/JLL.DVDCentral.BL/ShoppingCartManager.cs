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
                 Add in an OrderManager

                  I passed in the cart and user into the method, created the order using the credentials 
                  of the user, retrieved the last order, got the number off that and then foreached the orderitem inserts
                 */

                Order order = new Order();
                order.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(p => p.Id) + 1 : 1;
                order.UserId = user.UserId;

                var cid = (from u in dc.tblUsers
                             join c in dc.tblCustomers on u.UserId equals c.UserId
                             where u.UserId == user.UserId
                             select new
                             {
                                 CustomerId = c.Id      // Only need the CustomerId
                             }).FirstOrDefault();
               
                order.CustomerId = cid.CustomerId;   // Works maybe?
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
