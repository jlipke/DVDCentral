using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.PL;

namespace JLL.DVDCentral.BL
{
    public static class MovieGenreManager
    {
        public static void Delete(int movieId, int genreId)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                tblMovieGenre mg = dc.tblMovieGenres.FirstOrDefault(p => p.MovieId == movieId
                                        && p.GenreId == genreId);
                
                if (mg != null)
                {
                    dc.tblMovieGenres.Remove(mg);
                    dc.SaveChanges();
                }
            }
        }

        public static void Add(int movieId, int genreId)
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                tblMovieGenre mg = new tblMovieGenre();
                mg.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(p => p.Id) + 1 : 1;
                mg.MovieId = movieId;
                mg.GenreId = genreId;

                dc.tblMovieGenres.Add(mg);
                dc.SaveChanges();
            }
        }
    }
}
