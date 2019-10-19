using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.PL;
using JLL.DVDCentral.BL.Models;


namespace JLL.DVDCentral.BL
{
    public class RatingManager
    {
        public static bool Insert(Rating rating)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // Make a new row
                    tblRating newrow = new tblRating();

                    // Set the properties
                    // Ternary Operator condition ? true : false
                    newrow.Id = dc.tblRatings.Any() ? dc.tblRatings.Max(p => p.Id) + 1 : 1;     // If there are any rows, get the max id and add 1, if not use 1
                    newrow.Description = rating.Description;


                    // Do the Insert
                    dc.tblRatings.Add(newrow);

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

        public static int Update(Rating rating)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                try
                {

                    tblRating updatedrow = dc.tblRatings.Where(dt => dt.Id == rating.Id).FirstOrDefault();

                    // Check to see if object exists
                    if (rating != null)
                    {
                        // Update the proper fields
                        updatedrow.Description = rating.Description;


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
                    tblRating deleterow = dc.tblRatings.FirstOrDefault(dt => dt.Id == id);

                    if (deleterow != null)
                    {
                        // Remove the row
                        dc.tblRatings.Remove(deleterow);

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

        public static Rating LoadById(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        tblRating tblrating = dc.tblRatings.FirstOrDefault(p => p.Id == id);

                        if (tblrating != null)
                        {
                            Rating rating = new Rating
                            {
                                Id = tblrating.Id,
                                Description = tblrating.Description
                            };

                            return rating;

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

        public static List<Rating> Load()
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    List<Rating> ratings = new List<Rating>();
                    dc.tblRatings.ToList().ForEach(dt => ratings.Add(new Rating
                    {
                        Id = dt.Id,
                        Description = dt.Description
                    }));

                    return ratings;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

