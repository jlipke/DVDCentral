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
        public static void Checkout(ShoppingCart cart, User user)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                
                Order order = new Order();
                order.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(p => p.Id) + 1 : 1;
                order.UserId = user.UserId;
                order.CustomerId = cart.CustomerId;
                order.PaymentReceipt = Convert.ToString(cart.TotalCost);
                order.OrderDate = DateTime.Now;

                OrderManager.Insert(order, cart.Items);
                
                // Add an order item for each movie
                foreach (Movie movie in cart.Items)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.MovieId = movie.Id;
                    orderItem.OrderId = order.Id;
                    orderItem.Quantity = 1;

                    OrderItemManager.Insert(orderItem);
                }


                cart.Items = null;  // Clear the cart
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
