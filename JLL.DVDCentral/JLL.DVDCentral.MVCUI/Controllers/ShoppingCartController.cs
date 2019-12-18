using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.MVCUI.Models;
using JLL.DVDCentral.MVCUI.ViewModels;

namespace JLL.DVDCentral.MVCUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                GetShoppingCart();
                return View(cart);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // Show the Cart in the side bar
        [ChildActionOnly]
        public ActionResult CartDisplay()
        {
            GetShoppingCart();
            return PartialView(cart);
        }

        public ActionResult RemoveFromCart(int id)
        {
            GetShoppingCart();
            Movie movie = cart.Items.FirstOrDefault(i => i.Id == id);
            ShoppingCartManager.Remove(cart, movie);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int id)
        {
            GetShoppingCart();
            Movie movie = MovieManager.LoadById(id);
            ShoppingCartManager.Add(cart, movie);
            Session["cart"] = cart;
            return RedirectToAction("Index", "Movie");
        }

        private void GetShoppingCart()
        {
            if  (Session["cart"] == null){
                    cart = new ShoppingCart();
                    Session["cart"] = cart;
                }
            else
                cart = (ShoppingCart)Session["cart"];
            
        }
        
        public ActionResult Checkout()
        {
            try
            {
                CustomerOrders co = new CustomerOrders();
                co.CustomerList = CustomerManager.Load();
                User currentUser = (User)Session["user"];
                
                GetShoppingCart();
                
                // We need to get the CustomerId from the user with a sql join
                cart.CustomerId = UserManager.GetCustomerId(currentUser.UserId);
                
                ShoppingCartManager.Checkout(cart, currentUser);    
                Session["cart"] = null;
                return RedirectToAction("ThankYou");
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ThankYou()
        {
            return View();
        }

    }
}