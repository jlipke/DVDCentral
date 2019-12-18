using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using JLL.DVDCentral.BL;
using JLL.DVDCentral.BL.Models;

namespace JLL.DVDCentral.MVCUI.ViewModels
{
    public class CustomerShoppingCart
    {
        public ShoppingCart ShoppingCart { get; set; }
        public List<Customer> CustomerList { get; set; }

        [DisplayName("Customer")]
        public int CustomerId { get; set; }
    }
}