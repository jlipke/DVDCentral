using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.PL;
using JLL.DVDCentral.BL.Models;


namespace JLL.DVDCentral.BL
{
    public class FormatManager
    {
        public static bool Insert(Format format)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    // Make a new row
                    tblFormat newrow = new tblFormat();

                    // Set the properties
                    // Ternary Operator condition ? true : false
                    newrow.Id = dc.tblFormats.Any() ? dc.tblFormats.Max(p => p.Id) + 1 : 1;     // If there are any rows, get the max id and add 1, if not use 1
                    newrow.Description = format.Description;
                   

                    // Do the Insert
                    dc.tblFormats.Add(newrow);

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

        public static int Update(Format format)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                try
                {

                    tblFormat updatedrow = dc.tblFormats.Where(dt => dt.Id == format.Id).FirstOrDefault();

                    // Check to see if object exists
                    if (format != null)
                    {
                        // Update the proper fields
                        updatedrow.Description = format.Description;
                        

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
                    tblFormat deleterow = dc.tblFormats.FirstOrDefault(dt => dt.Id == id);

                    if (deleterow != null)
                    {
                        // Remove the row
                        dc.tblFormats.Remove(deleterow);

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

        public static Format LoadById(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (DVDCentralEntities dc = new DVDCentralEntities())
                    {
                        tblFormat tblformat = dc.tblFormats.FirstOrDefault(p => p.Id == id);

                        if (tblformat != null)
                        {
                            Format format = new Format
                            {
                                Id = tblformat.Id,
                                Description = tblformat.Description
                            };

                            return format;

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

        public static List<Format> Load()
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    List<Format> formats = new List<Format>();
                    dc.tblFormats.ToList().ForEach(dt => formats.Add(new Format
                    {
                        Id = dt.Id,
                        Description = dt.Description
                    }));

                    return formats;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
