using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.MVCUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace JLL.DVDCentral.MVCUI.Controllers
{
    public class FormatController : Controller
    {
       
        List<Format> formats;
        // GET: Format

        #region "Pre-WebAPI"
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
        #endregion

        #region "WebAPI"

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51554/api/");
            return client;
        }



        public ActionResult Get()
        {

            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Format").Result;

            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            
            // Parse the result into generic objects
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);

            // Parse the items into a list
            List<Format> formats = items.ToObject<List<Format>>();

            return View("Index", formats);
        }

        public ActionResult GetOne(int id)
        {
            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API
            HttpResponseMessage response = client.GetAsync("Format/" + id).Result;

            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            Format format = JsonConvert.DeserializeObject<Format>(result);

            return View("Details", format);
        }


        public ActionResult Insert()
        {
            //Initialize Cient
            HttpClient client = InitializeClient();

            Format format = new Format();

            return View("Create", format);
        }

        [HttpPost]
        public ActionResult Insert(Format format)
        {
            try
            {
                //Initialize Cient
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.PostAsJsonAsync("Format", format).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Create", format);
            }
        }


        public ActionResult Update(int id)
        {
            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API to get a program
            HttpResponseMessage response = client.GetAsync("Format/" + id).Result;
            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            Format format = JsonConvert.DeserializeObject<Format>(result);

            return View("Edit", format);
        }

        [HttpPost]
        public ActionResult Update(int id, Format format)
        {
            try
            {
                //Initialize Cient
                HttpClient client = InitializeClient();

                HttpResponseMessage response = client.PutAsJsonAsync("Format/" + id, format).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Edit", format);
            }
        }



        public ActionResult Remove(int id)
        {
            //Initialize Cient
            HttpClient client = InitializeClient();

            //Call the API to get a program
            HttpResponseMessage response = client.GetAsync("Format/" + id).Result;
            //Deserialize the json
            string result = response.Content.ReadAsStringAsync().Result;
            Format format = JsonConvert.DeserializeObject<Format>(result);


            return View("Delete", format);
        }

        [HttpPost]
        public ActionResult Remove(int id, Format format)
        {
            try
            {
                //Initialize Cient
                HttpClient client = InitializeClient();

                HttpResponseMessage response = client.DeleteAsync("Format/" + id).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Delete", format);
            }
        }
#endregion
    }
}
