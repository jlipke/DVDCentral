using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.MVCUI.ViewModels;

namespace JLL.DVDCentral.MVCUI.Controllers
{
    public class MovieController : Controller
    {
       // List<Movie> movies;
        // GET: Movie
        public ActionResult Index()
        {
            ViewBag.Title = "Movies";
            var movies = MovieManager.Load();
            return View(movies);
        }

        public ActionResult Load(int Id)
        {
            var movies = MovieManager.Load(Id);
            return View("Index", movies);
        }

        // GET: Movie/Details/
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Details";
            var movie = MovieManager.LoadById(id);
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
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
            Movie movie = MovieManager.LoadById(id);
            return View(movie);
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
    }
}
