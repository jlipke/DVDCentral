using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.PL;
using JLL.DVDCentral.BL.Models;


namespace JLL.DVDCentral.BL
{
    public class MovieManager
    {
        public static bool Insert(Movie movie)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // Make a new row
                    tblMovie newrow = new tblMovie();

                    // Set the properties
                    // Ternary Operator condition ? true : false
                    newrow.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(p => p.Id) + 1 : 1;     // If there are any rows, get the max id and add 1, if not use 1
                    newrow.Title = movie.Title;
                    newrow.Description = movie.Description;
                    newrow.ImagePath = movie.ImagePath;
                    newrow.Cost = movie.Cost;
                    newrow.InStockQty = movie.InStockQty;
                    newrow.RatingId = movie.RatingId;
                    newrow.FormatId = movie.FormatId;
                    newrow.DirectorId = movie.DirectorId;
                    
                    

                    // Do the Insert
                    dc.tblMovies.Add(newrow);

                    // Commit the insert
                    dc.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(Movie movie)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                try
                {

                    tblMovie updatedrow = dc.tblMovies.Where(dt => dt.Id == movie.Id).FirstOrDefault();

                    // Check to see if object exists
                    if (movie != null)
                    {
                        // Update the proper fields
                        updatedrow.Title = movie.Title;
                        updatedrow.Description = movie.Description;
                        updatedrow.ImagePath = movie.ImagePath;
                        updatedrow.Cost = movie.Cost;
                        updatedrow.InStockQty = movie.InStockQty;
                        updatedrow.RatingId = movie.RatingId;
                        updatedrow.FormatId = movie.FormatId;
                        updatedrow.DirectorId = movie.DirectorId;


                        // Save and commit changes
                        return dc.SaveChanges();
                    }
                    else
                    {
                        // throw an exception stating the row was not found
                        throw new Exception("Row was not found.");

                    }


                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public static int Delete(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovie deleterow = dc.tblMovies.FirstOrDefault(dt => dt.Id == id);

                    if (deleterow != null)
                    {
                        // Remove the row
                        dc.tblMovies.Remove(deleterow);

                        // Commit/Save the changes
                        return dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Movie LoadById(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        //tblMovie tblmovie = dc.tblMovies.FirstOrDefault(p => p.Id == id);

                        //if (tblmovie != null)
                        //{
                        //    Movie movie = new Movie
                        //    {
                        //        Id = tblmovie.Id,
                        //        Title = tblmovie.Title,
                        //        Description = tblmovie.Description,
                        //        ImagePath = tblmovie.ImagePath,
                        //        Cost = tblmovie.Cost,
                        //        InStockQty = tblmovie.InStockQty,
                        //        RatingId = tblmovie.RatingId,
                        //        FormatId = tblmovie.FormatId,
                        //        DirectorId = tblmovie.DirectorId
                        //    };

                        //    return movie;

                        //}
                        //else
                        //{
                        //    throw new Exception("Row was not found.");
                        //}

                        var movie = (from mv in dc.tblMovies
                                       join f in dc.tblFormats on mv.FormatId equals f.Id
                                       join r in dc.tblRatings on mv.RatingId equals r.Id
                                       join d in dc.tblDirectors on mv.DirectorId equals d.Id
                                       where mv.Id == id
                                       select new
                                       {
                                           MovieId = mv.Id,
                                           FormatId = f.Id,
                                           RatingId = r.Id,
                                           DirectorId = d.Id,
                                           d.FirstName,
                                           d.LastName,
                                           FormatName = f.Description,
                                           RatingName = r.Description,
                                           MVTitle = mv.Title,
                                           MVDescription = mv.Description,
                                           MVImagePath = mv.ImagePath,
                                           MVCost = mv.Cost,
                                           MVInStockQuantity = mv.InStockQty

                                       }).FirstOrDefault();


                        if (movie != null)
                        {
                            JLL.DVDCentral.BL.Models.Movie Movie = new JLL.DVDCentral.BL.Models.Movie   
                            {
                                Id = movie.MovieId,
                                FormatId = movie.FormatId,
                                RatingId = movie.RatingId,
                                DirectorId = movie.DirectorId,
                                DirectorName = movie.LastName + ", " + movie.FirstName,
                                FormatName = movie.FormatName,
                                RatingName = movie.RatingName,
                                Title = movie.MVTitle,
                                Description = movie.MVDescription,
                                ImagePath = movie.MVImagePath,
                                Cost = movie.MVCost,
                                InStockQty = movie.MVInStockQuantity
                            };

                            return Movie;

                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                }
                else
                {
                    throw new Exception("Please provide an id");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<DVDCentral.BL.Models.Movie> Load()
        {
            return Load(null);
        }
        
        public static List<Movie> Load(int? genreId)  
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    

                    List<DVDCentral.BL.Models.Movie> results = new List<DVDCentral.BL.Models.Movie>();

                    var movies = (from mv in dc.tblMovies
                                  join g in dc.tblMovieGenres on mv.Id equals g.MovieId  // Joins the tblMovieGenres on with the MovieId
                                  join f in dc.tblFormats on mv.FormatId equals f.Id
                                  join r in dc.tblRatings on mv.RatingId equals r.Id
                                  join d in dc.tblDirectors on mv.DirectorId equals d.Id
                                  where (g.GenreId == genreId || genreId == null)   
                                  select new
                                  {
                                    
                                      MovieId = mv.Id,
                                      FormatId = f.Id,
                                      RatingId = r.Id,
                                      DirectorId = d.Id,
                                      d.FirstName,
                                      d.LastName,
                                      FormatName = f.Description,
                                      RatingName = r.Description,
                                      MVTitle = mv.Title,
                                      MVDescription = mv.Description,
                                      MVImagePath = mv.ImagePath,
                                      MVCost = mv.Cost,
                                      MVInStockQuantity = mv.InStockQty

                                  }).Distinct().ToList();

                    movies.ForEach(p => results.Add(new Movie
                    {
                        Id = p.MovieId,
                        Title = p.MVTitle,
                        Description = p.MVDescription,
                        ImagePath = p.MVImagePath,
                        Cost = p.MVCost,
                        InStockQty = p.MVInStockQuantity,
                        RatingId = p.RatingId,
                        RatingName = p.RatingName,
                        FormatId = p.FormatId,
                        FormatName = p.FormatName,
                        DirectorId = p.DirectorId,
                        DirectorName = p.LastName + ", " + p.FirstName,

                    }));

                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Genre> LoadGenres(int movieId)
        {
            return GenreManager.Load(movieId);
        }
    }
}
