using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Users
{
    internal class Admin : User
    {

        public Admin( int userId, string name, string email, string password, string phoneNumber) : base(userId, name, email, password, phoneNumber, true)
        {

        }

        public void CreateCustomer()
        {
            // Confirm admin wants to make a new customer
            Console.WriteLine("Do you want to create a new user? (yes/no)");
            string? answerConfirm = Console.ReadLine();
            if (answerConfirm.ToLower() != "yes")
            {
                Console.WriteLine("Exiting create user.");
                return;
            }

            // Ask for Name, Phone, Mail, 
            Console.WriteLine("Please enter the customer's name:");
            string? newName = Console.ReadLine();

            Console.WriteLine("Please enter the customer's e-mail:");
            string? newMail = Console.ReadLine();

            Console.WriteLine("Please enter the customer's phonenumber:");
            string? newPhonenumber = Console.ReadLine();

            Console.WriteLine("Please enter the customer's password:");
            string? newPassword = Console.ReadLine();

            // Print entered info and ask if it looks good
            Console.WriteLine($"Create a new user with the name: {newName}, e-mail: {newMail}, phonenumber: {newPhonenumber}, and password: {newPassword}? (yes/no)");
            answerConfirm = Console.ReadLine();
            if (answerConfirm.ToLower() != "yes")
            {
                Console.WriteLine("Dropping create user and going back to adminmenu");
                return;
            }
            // Create new customer object
            Customer newCustomer = new Customer(5 ,newName, newMail, newPassword, newPhonenumber, false);
            // Add new customer object to system user list
            BankSystem.AllCustomers.Add(newCustomer);

            // Ask if admin wants to add default bankaccounts (1 saving account and 1 checking account)
            // newCustomer.BankAccounts.Add(new CheckingAccount)
            // newCustomer.BankAccounts.Add(new SavingAccount)
        }
        public void UnlockCustomerAccount()
        {
            Console.WriteLine("Unlock user method");
            // List all locked accounts
            // Write user.Name + UserID
            // Select one account from the list
            // Confirm you want to unlock the account
        }
        public void GetCustomerStatistics()
        {
            Console.WriteLine("Get user statistics method");
            // List all customers
            // Select which customer to show stats of
            // Call user stats method()
        }
        public void UpdateInterestRates()
        {
            Console.WriteLine("Update interest rates method");
            // Enter new interest rate
            // Foreach Loan in syste.Loan list
            // Loan InterestRate = new value
        }
        public void Exchangerates()
        {
            Console.WriteLine("Update exchangerates method");
            // Enter new exchangerate for Euro
            // Update euro exchangerate with new value
        }

    }
}
