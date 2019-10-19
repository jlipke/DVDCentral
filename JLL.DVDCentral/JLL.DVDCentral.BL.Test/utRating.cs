using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace JLL.DVDCentral.BL.Test
{
    [TestClass]
    public class utRating
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Rating> ratings = new List<Rating>();
            ratings = RatingManager.Load();
            int expected = 5;
            Assert.AreEqual(expected, ratings.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Rating rating = new Rating();
            rating.Description = "X";


            bool result = RatingManager.Insert(rating);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Rating rating = RatingManager.LoadById(6);

            rating.Description = "Updated";

            RatingManager.Update(rating);

            Rating updateddegreetype = RatingManager.LoadById(6);

            Assert.AreEqual(rating.Description, updateddegreetype.Description);
        }

        [TestMethod]
        public void DeleteTest()
        {
            
            int results = RatingManager.Delete(4);
            Assert.IsTrue(results > 0);
        }
    }
}
