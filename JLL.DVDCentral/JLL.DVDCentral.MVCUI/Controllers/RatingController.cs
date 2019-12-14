using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.MVCUI.Models;

namespace JLL.DVDCentral.MVCUI.Controllers
{
    public class RatingController : Controller
    {
        List<Rating> ratings;
        // GET: Rating
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                ratings = RatingManager.Load();
                return View(ratings);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Rating/Details/
        public ActionResult Details(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Rating rating = RatingManager.LoadById(id);
                return View(rating);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Rating/Create
        public ActionResult Create()
        {
            if (Authenticate.IsAuthenticated())
            {
                Rating rating = new Rating();
                return View(rating);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Rating/Create
        [HttpPost]
        public ActionResult Create(Rating rating)
        {
            try
            {
                // TODO: Add insert logic here
                RatingManager.Insert(rating);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rating/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Rating rating = RatingManager.LoadById(id);
                return View(rating);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Rating/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Rating rating)
        {
            try
            {
                // TODO: Add update logic here
                RatingManager.Update(rating);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rating/Delete/5
        public ActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Rating rating = RatingManager.LoadById(id);
                return View(rating);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Rating/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Rating rating)
        {
            try
            {
                // TODO: Add delete logic here
                RatingManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
