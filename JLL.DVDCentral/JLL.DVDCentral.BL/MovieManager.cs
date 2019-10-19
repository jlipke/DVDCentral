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
                        tblMovie tblmovie = dc.tblMovies.FirstOrDefault(p => p.Id == id);

                        if (tblmovie != null)
                        {
                            Movie movie = new Movie
                            {
                                Id = tblmovie.Id,
                                Title = tblmovie.Title,
                                Description = tblmovie.Description,
                                ImagePath = tblmovie.ImagePath,
                                Cost = tblmovie.Cost,
                                InStockQty = tblmovie.InStockQty,
                                RatingId = tblmovie.RatingId,
                                FormatId = tblmovie.FormatId,
                                DirectorId = tblmovie.DirectorId
                            };

                            return movie;

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

        public static List<Movie> Load()
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    List<Movie> movies = new List<Movie>();
                    dc.tblMovies.ToList().ForEach(dt => movies.Add(new Movie
                    {
                        Id = dt.Id,
                        Title = dt.Title,
                        Description = dt.Description,
                        ImagePath = dt.ImagePath,
                        Cost = dt.Cost,
                        InStockQty = dt.InStockQty,
                        RatingId = dt.RatingId,
                        FormatId = dt.FormatId,
                        DirectorId = dt.DirectorId
                    }));

                    return movies;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
