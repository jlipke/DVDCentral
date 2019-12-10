using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.PL;
using JLL.DVDCentral.BL.Models;


namespace JLL.DVDCentral.BL
{
    public class GenreManager
    {
        public static bool Insert(Genre genre)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // Make a new row
                    tblGenre newrow = new tblGenre();

                    // Set the properties
                    // Ternary Operator condition ? true : false
                    newrow.Id = dc.tblGenres.Any() ? dc.tblGenres.Max(p => p.Id) + 1 : 1;     // If there are any rows, get the max id and add 1, if not use 1
                    newrow.Description = genre.Description;


                    // Do the Insert
                    dc.tblGenres.Add(newrow);

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

        public static int Update(Genre genre)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                try
                {

                    tblGenre updatedrow = dc.tblGenres.Where(dt => dt.Id == genre.Id).FirstOrDefault();

                    // Check to see if object exists
                    if (genre != null)
                    {
                        // Update the proper fields
                        updatedrow.Description = genre.Description;


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
                    tblGenre deleterow = dc.tblGenres.FirstOrDefault(dt => dt.Id == id);

                    if (deleterow != null)
                    {
                        // Remove the row
                        dc.tblGenres.Remove(deleterow);

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

        public static Genre LoadById(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        tblGenre tblgenre = dc.tblGenres.FirstOrDefault(p => p.Id == id);

                        if (tblgenre != null)
                        {
                            Genre genre = new Genre
                            {
                                Id = tblgenre.Id,
                                Description = tblgenre.Description
                            };

                            return genre;

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

        public static List<Genre> Load()
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    List<Genre> genres = new List<Genre>();
                    dc.tblGenres.ToList().ForEach(dt => genres.Add(new Genre
                    {
                        Id = dt.Id,
                        Description = dt.Description
                    }));

                    return genres;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Genre> Load(int? movieId)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    List<Genre> genres = new List<Genre>();

                    var results = (from a in dc.tblGenres
                                   join pda in dc.tblMovieGenres on a.Id equals pda.GenreId
                                   where pda.MovieId == movieId
                                   select new
                                   {
                                       a.Id,
                                       a.Description
                                   }).ToList();

                    results.ForEach(r => genres.Add(new Genre { Id = r.Id, Description = r.Description }));
                    return genres;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
