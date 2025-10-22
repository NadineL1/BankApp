using BankApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    internal static class Menu
    {
        public static void LogInMenu()
        {
            // ASk if user wants to quit program
            // Ask if user admin
            // Take user input for userID
            // Take user input for password

            // if inputAdminBool true
                // check list of admins acc
                // Check input username + account
                    // if match
                        // Send matching admin object to admin menu
                    // else ask for input again
            // Else
                // Check input username + account against list of customer accounts
                    // if match
                        // Send matching customer object to customer menu
                    // else if 3rd attempt to login to one user
                        // Set account lock bool on that user object
                    // else ask for login details again
        }

        public static void AdminMenu(Admin admin) 
        {
            // Show list of options
                /*
                    Create user method
                    Check customer stats
                    Unlock user (Extra user)
                    Update currency
                    Interest rules( additional task)
                    Log out
                 */
            // Ask user for input on which menu option they want
            // Switch case
                // Call to method matching the selected option
        }
        public static void CustomerMenu(Customer customer)
        {
            // Print overview of customer's accounts

            // Show list of options
            /*
                Withdraw/deposit money( extra feature )
                Make transaction
                Check transaction history
                Check bank accounts
                Create bank account
                Make loan request
                Check loans
                    Pick a loan
                         Repay loan
                Update profile information
                Log out
             */
            // Ask user for input on which menu option they want
            // Switch case
                // Call to method matching the selected option
        }


    }
}
