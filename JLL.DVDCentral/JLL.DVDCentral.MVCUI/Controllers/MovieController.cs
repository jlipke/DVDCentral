using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.MVCUI.ViewModels;
using JLL.DVDCentral.MVCUI.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace JLL.DVDCentral.MVCUI.Controllers
{
    public class MovieController : Controller
    {
        #region "PreWebAPI"
        // GET: Movie
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                ViewBag.Title = "Movies";
                var movies = MovieManager.Load();
                return View(movies);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        public ActionResult Load(int Id)
        {
            var movies = MovieManager.Load(Id);
            return View("Index", movies);
        }

        // GET: Movie/Details/
        public ActionResult Details(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                ViewBag.Title = "Details";
                var movie = MovieManager.LoadById(id);
                return View(movie);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            if (Authenticate.IsAuthenticated())
            {
                ViewBag.Title = "Create";
                MovieGenresDirectorsRatingsFormats mgdrf = new MovieGenresDirectorsRatingsFormats();

                mgdrf.Movie = new DVDCentral.BL.Models.Movie();
                mgdrf.DirectorList = DirectorManager.Load();
                mgdrf.RatingList = RatingManager.Load();
                mgdrf.FormatList = FormatManager.Load();
                mgdrf.Genres = GenreManager.Load();  // Load them all

                return View(mgdrf);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieGenresDirectorsRatingsFormats mgdrf)
        {
            try
            {
                if (mgdrf.File != null)
                {
                    mgdrf.Movie.ImagePath = mgdrf.File.FileName;
                    string target = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(mgdrf.File.FileName));

                    if (!System.IO.File.Exists(target))
                    {
                        mgdrf.File.SaveAs(target);
                        ViewBag.Message = "File Uploaded Successfullly...";
                    }
                    else
                    {
                        ViewBag.Message = "File already exists...";
                    }
                }

                MovieManager.Insert(mgdrf.Movie);
                mgdrf.GenreIds.ToList().ForEach(a => MovieGenreManager.Add(mgdrf.Movie.Id, a));
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(mgdrf);
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                MovieGenresDirectorsRatingsFormats mgdrf = new MovieGenresDirectorsRatingsFormats();

                mgdrf.Movie = MovieManager.LoadById(id);
                mgdrf.DirectorList = DirectorManager.Load();
                mgdrf.RatingList = RatingManager.Load();
                mgdrf.FormatList = FormatManager.Load();
                mgdrf.Genres = GenreManager.Load();  // Load them all

                // Deal with the selected ones
                mgdrf.Movie.Genres = MovieManager.LoadGenres(id);
                mgdrf.GenreIds = mgdrf.Movie.Genres.Select(a => a.Id);  // Select the ids

                // Put them into session
                Session["genreids"] = mgdrf.GenreIds;

                return View(mgdrf);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MovieGenresDirectorsRatingsFormats mgdrf)
        {
            
            try
            {
                if (mgdrf.File != null)
                {
                    mgdrf.Movie.ImagePath = mgdrf.File.FileName;
                    string target = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(mgdrf.File.FileName));

                    if (!System.IO.File.Exists(target))
                    {
                        mgdrf.File.SaveAs(target);
                        ViewBag.Message = "File Uploaded Successfullly...";
                    }
                    else
                    {
                        ViewBag.Message = "File already exists...";
                    }
                }

                // Deal with the Genres
                IEnumerable<int> oldgenreids = new List<int>();
                if (Session["genreids"] != null)
                    oldgenreids = (IEnumerable<int>)Session["genreids"];

                IEnumerable<int> newgenreids = new List<int>();
                if (mgdrf.GenreIds != null)
                    newgenreids = mgdrf.GenreIds;

                // Identify the deletes
                IEnumerable<int> deletes = oldgenreids.Except(newgenreids);    // Get the old ones that arent in the new ones

                // Identify the adds
                IEnumerable<int> adds = newgenreids.Except(oldgenreids);

                deletes.ToList().ForEach(d => MovieGenreManager.Delete(id, d));
                adds.ToList().ForEach(a => MovieGenreManager.Add(id, a));

                // TODO: Add update logic here
                MovieManager.Update(mgdrf.Movie);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Movie movie = MovieManager.LoadById(id);
                return View(movie);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Movie movie)
        {
            try
            {
                // TODO: Add delete logic here
                MovieManager.Delete(id);
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
            client.BaseAddress = new Uri("http://localhost:64499/api/");
            return client;

        }

        public ActionResult Get()
        {
            HttpClient client = InitializeClient();

            // Do the actual call to the WebAPI
            HttpResponseMessage response = client.GetAsync("Movie").Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;

            // Parse the result into generic objects
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);

            // Parse the items into a list of movie objects
            List<Movie> movies = items.ToObject<List<Movie>>();

            ViewBag.Source = "Get";
            return View("Index", movies);
        }

        public ActionResult GetOne(int id)
        {
            HttpClient client = InitializeClient();

            // Do the actual call to the WebAPI
            HttpResponseMessage response = client.GetAsync("Movie/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;

            // Parse the result into generic objects
            Movie movie = JsonConvert.DeserializeObject<Movie>(result);

            return View("Details", movie);
        }

        public ActionResult Insert()
        {
            HttpClient client = InitializeClient();
            Movie movie = new Movie();
            return View("Create", movie);

        }

        [HttpPost]
        public ActionResult Insert(Movie movie)
        {
            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.PostAsJsonAsync("Movie", movie).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Create", movie);
            }

        }

        public ActionResult Update(int id)
        {
            HttpClient client = InitializeClient();

            HttpResponseMessage response = client.GetAsync("Movie/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            Movie movie = JsonConvert.DeserializeObject<Movie>(result);

            return View("Edit", movie);
        }


        [HttpPost]
        public ActionResult Update(int id, Movie movie)
        {
            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.PutAsJsonAsync("Movie/" + id, movie).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Edit", movie);
            }

        }

        public ActionResult Remove(int id)
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.GetAsync("Movie/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            Movie movie = JsonConvert.DeserializeObject<Movie>(result);

            return View("Delete", movie);
        }

        [HttpPost]
        public ActionResult Remove(int id, Movie movie)
        {
            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Movie/" + id).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View("Delete", movie);
            }
        }
        #endregion
    }
}
