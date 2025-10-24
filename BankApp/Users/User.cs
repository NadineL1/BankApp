using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Users
{
    internal class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber {  get; set; }
        public bool IsAdmin { get; set; }

        public User(int userid, string name, string email, string password, string phonenumber, bool isAdmin)
        {
            UserID = userid;
            Name = name;
            Email = email;
            Password = password;
            PhoneNumber = phonenumber;
            IsAdmin = isAdmin;


        }

        
    }
}
