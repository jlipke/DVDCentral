using System;
using System.Collections.Generic;
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
        List<Movie> movies;
        // GET: Movie
        public ActionResult Index()
        {
            movies = MovieManager.Load();
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
            Movie movie = MovieManager.LoadById(id);
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            Movie movie = new Movie();
            return View(movie);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            try
            {
                // TODO: Add insert logic here
                MovieManager.Insert(movie);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
            mgdrf.GenreList = GenreManager.Load();  // Load them all

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
            //try
            //{
            //    // TODO: Add update logic here
            //    MovieManager.Update(movie);
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}

            try
            {
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
            catch
            {
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
