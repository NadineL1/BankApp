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
        public string name { get; set; }
        public string email { get; set; }
        private string password { get; set; }
        public string phoneNumber {  get; set; }
        public bool isAdmin { get; set; }

        public User()
        {

        }

        
    }
}
