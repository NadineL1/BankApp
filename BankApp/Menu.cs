using BankApp.Users;
using BankApp.BankAccounts; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;


namespace BankApp
{
    internal static class Menu
    {
        public static void LogInMenu()
        {
            bool isRunning = true;
            while (isRunning)
            { 
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
                            // Create a prompt object that asks the user for input. The answer will be returned as string.
                            var inputPassword = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:") 
                                .Secret()); // turns the prompt into a secret input.                           
                            attempts++;

                            // Checks if the stored customer's password matches user input
                            if (loginCustomer.Password == inputPassword)
                            {
                                Console.WriteLine("\nSuccess! You are now logged in as customer!");
                                Thread.Sleep(700); // A little break before we clear in 0,7 sec.
                                Console.Clear(); // Clears the logo from the loginmenu.

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

                        // Use spectre.console for hidden password input.
                        var password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your password:")
                            .Secret());

                        // Checks if username and password matches system admin
                        if (username.ToLower() == BankSystem.Admin.Name.ToLower() && password == BankSystem.Admin.Password)
                        {
                            Console.WriteLine("\nSuccess! You are now logged in as admin!");
                            Thread.Sleep(700); // A little break before we clear in 0,7 sec.
                            Console.Clear(); // Clears the logo from loginmenu.
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
                AnsiConsole.Clear();
                Logo.BankLogo();
                Console.WriteLine();

                AnsiConsole.MarkupLine("[bold yellow]Welcome Admin to [underline]The Five Bank[/]![/]");
                // Create the menu with navigation.
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>() // Creates a listmenu with a string.
                    .Title("[bold gold1]What would you like to do today?[/]")
                    .PageSize(8) // How many options in menu you can see.
                    .HighlightStyle(new Style(Color.Gold1)) // Highlight's with colour what user is choosing in menu.
                    .AddChoices(new[] // the menu option.
                    {
                        "Create a customer",
                        "Check customer statistics",
                        "Unlock customer account",
                        "Update exchange rates",
                        "Update loan interest rules",
                        "Log out"
                    }));
                switch (choice) 
                {
                    case "Create a customer":
                        admin.CreateCustomer();
                        break;
                    case "Check customer statistics":
                        admin.GetCustomerStatistics();
                        break;
                    case "Unlock customer account":
                        admin.UnlockCustomerAccount();
                        break;
                    case "Update exchange rates":
                        admin.Exchangerates();
                        break;
                    case "Update loan interest rules":
                        admin.UpdateInterestRates();
                        break;
                    case "Log out":
                        AnsiConsole.MarkupLine("\n[green]Logging out. Thank you for today![/]");
                        Helper.PauseBreak("Returning to start screen", 3);
                        adminMenu = false;
                        break;
                }
            }
        }
        
        public static void CustomerMenu(Customer customer)
        {
            bool customermenu = true;
            while (customermenu)
            {
                AnsiConsole.Clear();
                Logo.BankLogo();
                Console.WriteLine();

                AnsiConsole.MarkupLine("[bold yellow]Welcome to [underline]The Five Bank[/]![/]");
                
                // Create the menu with navigation.
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>() // Creates a listmenu with a string.
                    .Title("[bold gold1]What would you like to do today?[/]")
                    .PageSize(10) // How many options in menu you can see.
                    .HighlightStyle(new Style(Color.Gold1)) // Highlight's with colour what user is choosing in menu.
                    .AddChoices(new[] // the menu option.
                    {
                        "Withdraw / Deposit money",
                        "Make transaction",
                        "Check transaction history",
                        "Check bank accounts",
                        "Create new bank account",
                        "Make loan request",
                        "Check your loan request",
                        "Update profile information",
                        "Fiften minutes pass",
                        "Log out"
                    }));
                switch (choice)
                {
                    case "Withdraw / Deposit money":
                        customer.DepositWithdraw();
                        break;
                    case "Make transaction":
                        customer.StartTransaction();
                        break;
                    case "Check transaction history":
                        customer.CheckTransactionHistory();
                        break;
                    case "Check bank accounts":
                        customer.CheckBankAccounts();
                        break;
                    case "Create new bank account":
                        customer.CreateBankAccount();
                        break;
                    case "Make loan request":
                        customer.LoanRequest();
                        break;
                    case "Check your loan request":
                        customer.CheckLoans();
                        break;
                    case "Update profile information":
                        customer.UpdateCustomerInformation();
                        break;
                    case "Fiften minutes pass":
                        BankSystem.FifteenMinutesMethod();
                        break;
                    case "Log out":
                        AnsiConsole.MarkupLine("\n[green]Thank you for visiting The Five Bank![/]");
                        Helper.PauseBreak("Returning to start screen", 3);
                        customermenu = false;
                        break;
                }             
            }
        }
    }
}



