using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace JLL.DVDCentral.BL.Test
{
    [TestClass]
    public class utGenre
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Genre> genres = new List<Genre>();
            genres = GenreManager.Load();
            int expected = 7;
            Assert.AreEqual(expected, genres.Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Genre genre = GenreManager.LoadById(4);

            genre.Description = "Updated";

            GenreManager.Update(genre);

            Genre updateddegreetype = GenreManager.LoadById(4);

            Assert.AreEqual(genre.Description, updateddegreetype.Description);
        }
    }
}
