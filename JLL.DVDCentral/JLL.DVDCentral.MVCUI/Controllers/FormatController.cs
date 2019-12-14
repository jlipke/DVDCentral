﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.MVCUI.Models;

namespace JLL.DVDCentral.MVCUI.Controllers
{
    public class FormatController : Controller
    {
        List<Format> formats;
        // GET: Format
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                formats = FormatManager.Load();
                return View(formats);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Format/Details/
        public ActionResult Details(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Format format = FormatManager.LoadById(id);
                return View(format);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Format/Create
        public ActionResult Create()
        {
            if (Authenticate.IsAuthenticated())
            {
                Format format = new Format();
                return View(format);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Format/Create
        [HttpPost]
        public ActionResult Create(Format format)
        {
            try
            {
                // TODO: Add insert logic here
                FormatManager.Insert(format);
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
            if (Authenticate.IsAuthenticated())
            {
                Format format = FormatManager.LoadById(id);
                return View(format);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Format/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Format format)
        {
            try
            {
                // TODO: Add update logic here
                FormatManager.Update(format);
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
            if (Authenticate.IsAuthenticated())
            {
                Format format = FormatManager.LoadById(id);
                return View(format);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Format/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Format format)
        {
            try
            {
                // TODO: Add delete logic here
                FormatManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
