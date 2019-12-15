using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.MVCUI.Models;

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
            if (Session["cart"] == null)
                cart = new ShoppingCart();
            else
                cart = (ShoppingCart)Session["cart"];
        }

        public ActionResult Checkout()
        {
            GetShoppingCart();
            //ShoppingCartManager.Checkout(cart);
            return View();
        }
    }
}