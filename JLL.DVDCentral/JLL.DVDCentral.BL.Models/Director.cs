using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLL.DVDCentral.BL.Models
{
    public class Director
    {
        public int Id { get; set; }

        [DisplayName ("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string DirectorFullNameFirst
        {
            get { return FirstName + ", " + LastName; }
        }

        public string DirectorFullNameLast
        {
            get { return LastName + ", " + FirstName; }
        }
    }
}
