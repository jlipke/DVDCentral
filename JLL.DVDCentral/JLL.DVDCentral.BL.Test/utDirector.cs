using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace JLL.DVDCentral.BL.Test
{
    [TestClass]
    public class utDirector
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Director> genres = new List<Director>();
            genres = DirectorManager.Load();
            int expected = 3;
            Assert.AreEqual(expected, genres.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Director director = new Director();
            director.FirstName = "The Rock";
            director.LastName = "Johnson";


            bool result = DirectorManager.Insert(director);
            Assert.IsTrue(result);
        }
    }
}
