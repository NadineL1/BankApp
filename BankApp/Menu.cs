using BankApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


            //List of Admin username and password.
            List<string> admins = new List<string> { "admin" };
            List<string> adminPasswords = new List<string> { "1234" };
            //List of customer UserID and password.
            List<int> userID = new List<int> { 1, 2, 3 };
            List<int> userPasswords = new List<int> { 1111, 2222, 3333 };

            Console.Clear();
            Console.WriteLine("Welcome to The Five Bank!");
            Console.WriteLine("How would you like to login?");
            Console.WriteLine("\n[1] Log in as customer.");
            Console.WriteLine("[2] Log in as Admin.");
            Console.WriteLine("[3] Quit program.");
            Console.Write("\nYour choice: ");

            string choice = Console.ReadLine();

            //LOGIN AS CUSTOMER
            switch (choice)
            {
                case "1":
                    {
                        int attempts = 0;
                        bool logIn = false; //changes to true when successful login.
                        int index = -1; // Start looking from zero in the UserID List.
                        int inputId = 0; // Create a place to store the User's ID.

                        // Gives customer 2 attempts to enter a valid UserID.
                        while (attempts < 2 && index == -1)
                        {
                            Console.Write("Enter your UserID: ");
                            inputId = Convert.ToInt32(Console.ReadLine());

                            // Find the userId in the list
                            index = userID.IndexOf(inputId);

                            if (index == -1)
                            {
                                attempts++;
                                Console.WriteLine($"\nThis user ID does not exsist. Attempts left: {2 - attempts}");
                            }
                        }
                        if (index == -1)
                        {
                            Console.WriteLine("\nTo many failed attempts. Program will shut down.");
                            return;
                        }
                        // Reset attempts for password stage
                        attempts = 0;

                        while (attempts < 3 && !logIn)
                        {
                            Console.Write("Enter your password: ");
                            int inputPassword = Convert.ToInt32(Console.ReadLine());

                            // Find the correct password in the list.
                            if (userPasswords[index] == inputPassword)
                            {
                                Console.WriteLine("\nSuccess! You are now logged in as customer!");
                                // ANROPA CustomerMenu-metoden !!
                                logIn = true;
                            }
                            else
                            {
                                attempts++;
                                Console.WriteLine($"\nWrong password! Attempts left: {3 - attempts}");
                            }
                        }

                        if (!logIn)
                        {
                            Console.WriteLine("\nTo many failed attempts! Your account has been locked.");
                        }
                        break;
                    }
                // LOGIN AS ADMIN
                case "2":
                    {
                        Console.Write("Enter your username: ");
                        string username = Console.ReadLine();

                        Console.Write("Enter your password: ");
                        string password = Console.ReadLine();

                        // Find index in the admin-username-list
                        int index = admins.IndexOf(username.ToLower());

                        // If the username exists (index != -1) and the password matches.
                        if (index != -1 && adminPasswords[index] == password)
                        {
                            Console.WriteLine("\nSuccess! You are now logged in as Admin!");
                            // ANROPA AdminMenu - metoden
                        }
                        else
                        {
                            Console.WriteLine("\nWrong username or password! Access denied!");
                        }
                        break;
                    }
                // QUIT PROGRAM
                case "3":
                    {
                        {
                            Console.WriteLine("Thank you for visiting 'The Five Bank'. Hope to see you again!");
                            return;
                        }
                    }
                default:
                    {
                        Console.WriteLine("\nInvalid choice. Please restart the program and try again.");
                        break;
                    }
            }
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

            Console.WriteLine("Welcome Admin!");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] Create a customer.");
			Console.WriteLine("[2] Check customer stats.");
            Console.WriteLine("[3] Unlock customer.");
            Console.WriteLine("[4] Update exchange rates.");
			Console.WriteLine("[5] Update interest rules for loans.");
			Console.WriteLine("[6] Log out,");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
					Console.WriteLine("Create customer.");
					// call to customer
                    break;
                case "2":
                    Console.WriteLine("Customer statistics:");
                    // call method for printing statistics
                    break;
                case "3":
                    Console.WriteLine("Unlock customer");
                    // method for unlockingg
                    break;
                case "4":
                    Console.WriteLine("Update exchange rates");
                    // call to exchange rates method 
                    break;
                case "5":
                    Console.WriteLine("Update interest rules");
                    // update interest rules, loans
                    break;
                case "6":
                    Console.WriteLine("Log out.");
                    // back to login ? 
                    break;
                default:
                    Console.WriteLine("Something went wrong.");
                    break;
            }

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

            Console.WriteLine("Welcome customer!");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("\n[1] Withdraw/deposit money."); // (extra features)
            Console.WriteLine("[2] Make transaction.");
            Console.WriteLine("[3] Check transaction history.");
            Console.WriteLine("[4] Check bank accounts.");
            Console.WriteLine("[5] Create bank accounts.");
            Console.WriteLine("[6] Make loan request.");
            Console.WriteLine("[7] Check loans.");
            Console.WriteLine("[8] Update profile information.");
            Console.WriteLine("[9] Log out.");
            Console.Write("\nYour choice: ");

            string choice = Console.ReadLine();

        }


    }
}
