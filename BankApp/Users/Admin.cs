using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.BankAccounts;

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
            Console.WriteLine();
            Console.WriteLine("Which account would you like to unlock?");
            // Makes a list of all locked accounts
            var lockedAccounts = BankSystem.AllCustomers.FindAll(x => x.LockBool == true);
            int index = 0;
            // Gives each locked account a selection number and writes info about it.
            foreach (var lockedAccount in lockedAccounts)
            {
                Console.WriteLine($"{index + 1}. Account id: {lockedAccount.UserID}, Name: {lockedAccount.Name}");
                index++;
            }
            Console.WriteLine($"{index + 1}. Quit.");
            
            int input = Helper.ListSelection(index + 1);
            if(input < lockedAccounts.Count)
            {
                lockedAccounts[input].LockBool = false;
                Console.WriteLine($"Unlocked userID: {lockedAccounts[input].UserID}, owned by: {lockedAccounts[input].Name}");
                
                Helper.PauseBreak("Returning to adminmenu", 3);
            }
            else
            {
                Helper.PauseBreak("Returning to admin menu", 3);
            }
        }
        public void GetCustomerStatistics()
        {
            Console.WriteLine("Get user statistics method");
            Console.WriteLine();
            Console.WriteLine("Which user's statistics would you like to view?");

            // List all customers
            int index = 0;
            foreach(Customer customer in BankSystem.AllCustomers)
            {
                Console.WriteLine($"{index + 1}. UserID: {customer.UserID}, Name: {customer.Name}");
                index++;
            }
            Console.WriteLine($"{index + 1}. Quit.");

            // Select which customer to show stats of
            int input = Helper.ListSelection(BankSystem.AllCustomers.Count + 1);
            if (input < BankSystem.AllCustomers.Count)
            { 
                decimal customerTotalBalance = 0m;
                // decimal customerTotalTransactionAmount = 0m;

                // Adds the balancne of all the user's bankaccounts to a total.
                foreach(BankAccountBase bankAccount in BankSystem.AllCustomers[input].CustomerBankAccounts)
                {
                    if (bankAccount.CurrencyType == Enums.CurrencyTypes.SEK)
                    {
                        customerTotalBalance += bankAccount.Balance;
                    }
                    // If the currencytype of the bankaccount isn't SEK I convert it to SEK before adding it.
                    else 
                    {
                        customerTotalBalance += Helper.ConvertCurrency(bankAccount.Balance, bankAccount.CurrencyType, Enums.CurrencyTypes.SEK);
                    }
                }
                Console.WriteLine($"{BankSystem.AllCustomers[input].Name} has approximately {customerTotalBalance}SEK placed in {BankSystem.AllCustomers[input].CustomerBankAccounts.Count} bankaccounts with us.");


                Console.Write("Press any key to continue:");
                Console.ReadKey();
                Helper.PauseBreak("Returning to admin menu", 3);
            }
            else
            {
                Helper.PauseBreak("Returning to admin menu", 3);
            }
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

            Console.WriteLine("Which exchangerate do you want to update?");
            Console.WriteLine($"1. SEK per EUR - We are currently paying {BankSystem.ExchangeRate.Values.ElementAt(1)} SEK per EUR, or {BankSystem.ExchangeRate.Values.ElementAt(0)} EUR per SEK.");
            Console.WriteLine($"2. SEK per USD - We are currently paying {BankSystem.ExchangeRate.Values.ElementAt(3)} SEK per USD, or {BankSystem.ExchangeRate.Values.ElementAt(2)} USD per SEK.");
            Console.WriteLine("3. Abort.");

            int input = Helper.ListSelection(3) + 1;
            // Exits the method if the user chooses the abort option. 
            if (input < 1 || input >= 3) { Helper.PauseBreak("Returning to adminmenu", 3); return; }
            
            // Uses the input integer to assign a enum with the selected currency type.
            Enums.CurrencyTypes selectedCurrencyType = (Enums.CurrencyTypes)input;

            Console.WriteLine($"How many SEK should equal one {selectedCurrencyType}?");
            if (decimal.TryParse(Console.ReadLine(), out decimal valueInput) && valueInput > 0m)
            {
                // Fetches the exchange value of the current exchange rate in order to show it to the user.
                BankSystem.ExchangeRate.TryGetValue((selectedCurrencyType, Enums.CurrencyTypes.SEK), out decimal currentExchangeRate);
                Console.WriteLine($"Are you sure you want to change the currency conversion from {currentExchangeRate} SEK per one {selectedCurrencyType}, to {valueInput} SEK per one {selectedCurrencyType}?");
                Helper.PrintSelectionList(SelectionLists.YesNo);
                if (Helper.ListSelection(SelectionLists.YesNo.Count) == 0)
                {
                    // If the user is happy with the proposed new exchangerate value we call the method in BankSystem that actually updates the exchangerates.
                    BankSystem.UpdateExchangeRate(selectedCurrencyType, valueInput);
                }
                else
                {
                    Console.WriteLine("Aborting the operation. The exchangerates have not been changed.");
                    Helper.PauseBreak("Returning to adminmenu", 3);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. The exchangerates have not been changed.");
                Helper.PauseBreak("Returning to adminmenu", 3);
            }
            // Enter new exchangerate for Euro
            // Update euro exchangerate with new value
        }

    }
}