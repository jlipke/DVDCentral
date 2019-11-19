using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;

namespace JLL.DVDCentral.MVCUI.Controllers
{
    public class FormatController : Controller
    {
        // GET: Format
        public ActionResult Index()
        {
            return View();
        }

        // GET: Format/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Format/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Format/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Format/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Format/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Format/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Format/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
