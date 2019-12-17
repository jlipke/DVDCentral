using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.BL;

namespace JLL.DVDCentral.MVCUI.ViewModels
{
    public class CustomerOrders
    {
        public Customer Customer { get; set; }
        public User User { get; set; }

        public string CU_FirstName { get; set; }        
        public string CU_LastName { get; set; }     // These act as middlemen to combine the User data with the Customer data that is entered by only one input
        public string CU_UserId { get; set; }       
    }
}