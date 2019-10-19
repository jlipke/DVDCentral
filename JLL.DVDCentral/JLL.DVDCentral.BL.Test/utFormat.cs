using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;
using System.Collections.Generic;

namespace JLL.DVDCentral.BL.Test
{
    [TestClass]
    public class utFormat
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Format> formats = new List<Format>();
            formats = FormatManager.Load();
            int expected = 3;
            Assert.AreEqual(expected, formats.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Format format = new Format();
            format.Description = "Cartrivision";
            

            bool result = FormatManager.Insert(format);
            Assert.IsTrue(result);
        }
    }
}
