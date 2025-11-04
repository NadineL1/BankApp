using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    // Holds all the selection options as lists in one file.
    internal static class SelectionLists
    {
        public static List<string> YesNo = new List<string>()
        {
            "Yes.",
            "No.",
        };
        public static List<string> StartMenu = new List<string>()
        {
            "Log in as customer.",
            "Log in as Admin.",
            "Quit program."
        };
        public static List<string> AdminMenu = new List<string>()
        {
            "Create a customer.",
            "Check customer stats.",
            "Unlock customer.",
            "Update exchange rates.",
            "Update interest rules for loans.",
            "Make 15-minutes pass.",
            "Log out."
        };
        public static List<string> CustomerMenu = new List<string>()
        {
            "Withdraw/deposit money.", // (extra features)
            "Make transaction.",
            "Check transaction history.",
            "Check bank accounts.",
            "Create bank accounts.",
            "Make loan request.",
            "Check loans.",
            "Update profile information.",
            "Log out."
        };
    }
}
