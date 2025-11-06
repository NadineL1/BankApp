using BankApp.Users;
using BankApp.BankAccounts; 
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
            bool isRunning = true;
            while (isRunning)
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

                Console.Clear();
                Logo.BankLogo();
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
                        int attempts = 0;
                        int inputId = 0; // Create a place to store the User's ID.
                        Customer loginCustomer = null; // If a customer with matching ID is found we store it here
                        bool lockCheck = false;

                        // Gives customer 2 attempts to enter a valid UserID
                        while (attempts < 2 && loginCustomer == null)
                        {
                            Console.Write("Enter your UserID: ");
                            int.TryParse(Console.ReadLine(), out inputId);
                            attempts++;

                            // Checks if there is a Customer in the customer list with an UserID that matches user input  
                            foreach (Customer customer in BankSystem.AllCustomers)
                            {
                                // If a customer is found we make a note of that customer and use it for the rest of the login
                                if (inputId == customer.UserID)
                                {
                                    // Stops the program if the customer's account is locked.
                                    if (customer.LockBool == true)
                                    {
                                        lockCheck = true;
                                        break;
                                    }
                                    loginCustomer = customer;
                                }
                            }
                            if(lockCheck == true)
                            {
                                Console.WriteLine("This account is locked and cannot be accessed.\nPlease contact an admin to resolve this.");
                                Helper.PauseBreak("Returning to start screen", 3);
                                break;
                            }
                            // If we don't find a matching customer we check if the user still has attempts left
                            if (loginCustomer == null && attempts < 2)
                            {
                                Console.WriteLine($"\nThis user ID does not exist. Attempts left: {2 - attempts}");

                            }
                            // Or if they have used all their attempts
                            else if (loginCustomer == null && attempts == 2)
                            {
                                Console.WriteLine("\nToo many failed attempts. Program will shut down.");
                                return;
                            }
                        }
                        // Reset attempts for password stage
                        attempts = 0;

                        bool logIn = false; //changes to true when successful login.
                        while (attempts < 3 && !logIn && lockCheck != true)
                        {
                            Console.Write("Enter your password: ");
                            string? inputPassword = Console.ReadLine();
                            attempts++;

                            // Checks if the stored customer's password matches user input
                            if (loginCustomer.Password == inputPassword)
                            {
                                Console.WriteLine("\nSuccess! You are now logged in as customer!");                                
                                

                              

                                // ANROPA CustomerMenu-metoden !!
                                // Sends the found customer object into the customer menu
                                CustomerMenu(loginCustomer);
                                logIn = true;
                            }
                            // If not matching check if they still have attempts left
                            else if (logIn == false && attempts < 3)
                            {
                                Console.WriteLine($"\nWrong password! Attempts left: {3 - attempts}");
                            }
                            // Or if they've used all their attempts
                            else if (logIn == false && attempts >= 3)
                            {
                                Console.WriteLine("\nToo many failed attempts! Your account has been locked.");
                                // Locks that customer's account.
                                loginCustomer.LockBool = true;
                                Helper.PauseBreak("Returning to start screen", 3);
                            }
                        }
                            break;
                    // LOGIN AS ADMIN
                    case "2":
                        Console.Write("Enter your username: ");
                        string? username = Console.ReadLine();

                        Console.Write("Enter your password: ");
                        string? password = Console.ReadLine();


                        // Checks if username and password matches system admin
                        if (username.ToLower() == BankSystem.Admin.Name.ToLower() && password == BankSystem.Admin.Password)
                        {
                            Console.WriteLine("\nSuccess! You are now logged in as admin!");
                            AdminMenu(BankSystem.Admin);
                        }
                        else
                        {
                            Console.WriteLine("\nWrong username or password! Access denied!");
                            Helper.PauseBreak("Returning to start screen", 3);
                        }
                        break;
                    // QUIT PROGRAM
                    case "3":
                        Console.WriteLine("Thank you for visiting 'The Five Bank'. Hope to see you again!");
                        isRunning = false;
                        return;

                    default:
                        Console.WriteLine("\nInvalid choice. Please restart the program and try again.");
                        break;
                }
			}
		}

        public static void AdminMenu(Admin admin)
        {
            bool adminMenu = true;
            while (adminMenu)
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
                Console.WriteLine("[6] Log out.");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Create customer.");
                        // call to customer
                        admin.CreateCustomer();
                        break;
                    case "2":
                        Console.WriteLine("Customer statistics:");
                        // call method for printing statistics
                        admin.GetCustomerStatistics();
                        break;
                    case "3":
                        Console.WriteLine("Unlock customer");
                        // method for unlockingg
                        admin.UnlockCustomerAccount();
                        break;
                    case "4":
                        Console.WriteLine("Update exchange rates");
                        // call to exchange rates method 
                        admin.Exchangerates();
                        break;
                    case "5":
                        Console.WriteLine("Update interest rules");
                        // update interest rules, loans
                        admin.UpdateInterestRates();
                        break;
                    case "6":
                        Console.WriteLine("Log out? Thank you for today.");
                        Console.WriteLine("Press any key to exit.");
                        Console.ReadKey();
                        adminMenu = false;
                        break;
                    default:
                        Console.WriteLine("Something went wrong.Try again with a different number choice.");
                        break;
                }
            }
        }
        
        public static void CustomerMenu(Customer customer)
        {
            bool customermenu = true;
            while (customermenu)
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

                Console.Clear();
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

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Withdraw/Deposit");
                        // Call to Withdraw() from Customer
                        break;
                    case "2":
                        // Commented out test code to try proper implementation
                        /*
                        Console.WriteLine("Make transaction.");
                        // Temporarily test accounts (Costumer ID, starting amount)
                        BankAccountBase sender = new BankAccountBase(1, 5000);
                        BankAccountBase receiver = new BankAccountBase(2, 500);
                        // Call to method
                        customer.MakeTransaction(sender, receiver);
                        */

                        customer.StartTransaction();
                        break;
                    case "3":
                        customer.CheckTransactionHistory();
                        // Call to CheckTransactionHistory() from Customer.
                        break;
                    case "4":
                        Console.WriteLine("Check bank accounts.");
                        // Call to CheckingAccount
                        customer.CheckBankAccounts();
                        break;
                    case "5":
                        Console.WriteLine("Create bank account");
                        // Call to CreateBankAccount() from Customer
                        customer.CreateBankAccount();
                        break;
                    case "6":
                        Console.WriteLine("Make loan request.");
                        // Call to LoanRequest() from customer.
                        break;
                    case "7":
                        Console.WriteLine("check loans.");
                        // Call to CheckLoans() from Customer.
                        break;
                    case "8":
                        Console.WriteLine("Update profile information.");
                        // Call to UpdateCustomerInformation() from Customer.
                        customer.UpdateCustomerInformation();
                        break;
                    case "9":
                        Console.WriteLine("Thank you for visiting 'The Five Bank'. Hope to see you again!");
                        Console.WriteLine("Press any key to exit.");
                        Console.ReadKey();
                        customermenu = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please restart the program and try again.");
                        break;
                }

            }

        }


    }

}



