using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.PL;
using JLL.DVDCentral.BL.Models;


namespace JLL.DVDCentral.BL
{
    public class DirectorManager
    {
        public static bool Insert(Director director)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // Make a new row
                    tblDirector newrow = new tblDirector();

                    // Set the properties
                    // Ternary Operator condition ? true : false
                    newrow.Id = dc.tblDirectors.Any() ? dc.tblDirectors.Max(p => p.Id) + 1 : 1;     // If there are any rows, get the max id and add 1, if not use 1
                    newrow.FirstName = director.FirstName;
                    newrow.LastName = director.LastName;

                    // Do the Insert
                    dc.tblDirectors.Add(newrow);

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

        public static int Update(Director director)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                try
                {
                   
                    tblDirector updatedrow = dc.tblDirectors.Where(dt => dt.Id == director.Id).FirstOrDefault();

                    // Check to see if object exists
                    if (director != null)
                    {
                        // Update the proper fields
                        updatedrow.FirstName = director.FirstName;
                        updatedrow.LastName = director.LastName;

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
                    tblDirector deleterow = dc.tblDirectors.FirstOrDefault(dt => dt.Id == id);

                    if (deleterow != null)
                    {
                        // Remove the row
                        dc.tblDirectors.Remove(deleterow);

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

        public static Director LoadById(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        tblDirector tbldirector = dc.tblDirectors.FirstOrDefault(p => p.Id == id);

                        if (tbldirector != null)
                        {
                            Director director = new Director
                            {
                                Id = tbldirector.Id,
                                FirstName = tbldirector.FirstName,
                                LastName = tbldirector.LastName
                            };

                            return director;

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

        public static List<Director> Load()
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    List<Director> directors = new List<Director>();
                    dc.tblDirectors.ToList().ForEach(dt => directors.Add(new Director
                    {
                        Id = dt.Id,
                        FirstName = dt.FirstName,
                        LastName = dt.LastName
                    }));

                    return directors;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
