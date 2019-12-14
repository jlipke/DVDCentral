using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JLL.DVDCentral.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Password")]
        public string PassCode { get; set; }

        public User()
        {

        }

        public User(string userid, string passcode)
        {
            // A user is trying to log in
            UserId = userid;
            PassCode = passcode;
        }

        public User(int id, string userid, string firstname, string lastname, string passcode)
        {
            // Updating a user
            UserId = userid;
            PassCode = passcode;
            Id = id;
            FirstName = firstname;
            LastName = lastname;
        }

        public User(string userid, string firstname, string lastname, string passcode)
        {
            // Inserting/creating a user
            UserId = userid;
            PassCode = passcode;
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
