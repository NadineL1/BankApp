using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Users
{
    internal class Admin : User
    {

        public Admin( int userId, string name, string email, string password, string phoneNumber, bool isAdmin) : base(userId, name, email, password, phoneNumber, isAdmin)
        {
            
        }


        public void CreateCustomer()
        {
            // Confirm admin wants to make a new customer
            // Ask for Name, Phone, Mail, Password
            // Print entered info and ask if it looks good
                // Create new customer object
                // Add new customer object to system user list
            // Ask if admin wants to add default bankaccounts (1 saving account and 1 checking account)
                // newCustomer.BankAccounts.Add(new CheckingAccount)
                // newCustomer.BankAccounts.Add(new SavingAccount)
        }
        public void UnlockCustomerAccount()
        {
            // List all locked accounts
                // Write user.Name + UserID
            // Select one account from the list
            // Confirm you want to unlock the account
        }
        public void GetCustomerStatistics()
        {
            // List all customers
            // Select which customer to show stats of
            // Call user stats method()
        }
        public void UpdateInterestRates()
        {
            // Enter new interest rate
            // Foreach Loan in syste.Loan list
                // Loan InterestRate = new value
        }
        public void Exchangerates()
        {
            // Enter new exchangerate for Euro
            // Update euro exchangerate with new value
        }

    }
}
