using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JLL.DVDCentral.BL.Models;

namespace JLL.DVDCentral.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void SeedTest()
        {
            UserManager.Seed();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void LoginPassTest()
        {
            User user = new User("cpine", "maple");
            bool actual = UserManager.Login(user);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void LoginFailTest()
        {
            User user = new User("cpine", "test");
            bool actual = UserManager.Login(user);
            Assert.IsFalse(actual);
        }
    }
}
