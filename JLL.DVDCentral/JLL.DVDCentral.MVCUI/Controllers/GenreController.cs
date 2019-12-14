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
    public class GenreController : Controller
    {
        List<Genre> genres;
        // GET: Genre
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                genres = GenreManager.Load();
                return View(genres);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }
        
        // GET: Genre/Details/
        public ActionResult Details(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Genre genre = GenreManager.LoadById(id);
                return View(genre);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            if (Authenticate.IsAuthenticated())
            {
                Genre genre = new Genre();
                return View(genre);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            try
            {
                // TODO: Add insert logic here
                GenreManager.Insert(genre);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Genre genre = GenreManager.LoadById(id);
                return View(genre);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Genre genre)
        {
            try
            {
                // TODO: Add update logic here
                GenreManager.Update(genre);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Genre genre = GenreManager.LoadById(id);
                return View(genre);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Genre/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Genre genre)
        {
            try
            {
                // TODO: Add delete logic here
                GenreManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            var genres = GenreManager.Load();

            return PartialView(genres);
        }
    }
}
