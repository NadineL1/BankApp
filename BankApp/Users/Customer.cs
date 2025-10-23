
using BankApp.Loans;
using BankApp.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace BankApp.Users
{
    internal class Customer : User
    {
        public bool LockBool { get; set; } = false;
        public List<BankAccountBase> CustomerBankAccounts { get; set; }
        public List<Loan> CustomerActiveLoans { get; set; }

        public Customer( int userId, string name, string email, string password, string phoneNumber, bool lockbool) : base(userId, name, email, password, phoneNumber, false)
        {
            LockBool = lockbool;
            CustomerBankAccounts = new List<BankAccountBase>();
            CustomerActiveLoans = new List<Loan>();
        }

        public void Withdraw()
        {
            // Call list accounts method()
            // Ask user which bankaccount to make the withdrawal from
                // ASk user for how much to withdraw
                // Check if customer has that much money in bankaccount
                    // If true
                        // Subtract asked amount from selected account.Saldo
                        // Write confirmation of withdrawal in console
                    // Else
                        // Tell the user they don't have enough money in that account
                        // Ask user if they want to take out a loan
                        // If yes
                            // Go to MakeLoan()
        }
        public void StartTransaction()
        {
            // Call list accounts method()
            // Ask user which account to make the transaction from
                // Ask user what account to make the transaction to internal or external
                // If Internal
                // Select account with list accounts method
                    // MakeTransaction();
                // Else if external
                // Ask user to enter bank number
                    // If matches another BankAccount number
                        // MakeTransaction()
            }

        public void MakeTransaction(BankAccountBase sender, BankAccountBase receiver)
        {
            // Ask user how much money to transact
                // Check if account has the money to do so
                    // (This should be delayed 15 min, we'll look at that in week 2)
                    // If yes, subtrac from active Bankaccount.Saldo
                    // Add that money to receivning BankAccount.Saldo
        }
        public void CheckTransactionHistory()
        {
            // Call list accounts method()
            // Select BankAccount from list
                // PrintTransaction foreach TRansaction in BankAccount.Transaction history
        }
        /*
        public void CheckBankAccounts()
        {

        }
        */
        public void CreateBankAccount()
        {
            // Confirm that the customer want to make an new account
                // Which type of bankaccount do they want (check or savings)
                // (Later check what currency they want the acount in)
                // Create a BankAccount object of thr correct type
                    // Add it to Customer AccountList, BankSystem account list
                    // Write confirmation of the new BankAccount
        }
        public void CheckLoans()
        {
            // Foreach Loan in Customer Loan list
                // Write Loan.info
            // (Later, maybe make the customer select a loan to make repayments)
        }
        public void TakeoutLoan()
        {
            // Ask user what account they want the money from the loan to go to
            // Ask user the amount they want to borrow
            // If loan amount isn't more than 5x total balance in all of customer accounts
                // Show how much customer wants to borrow and how much extra they have to pay in interest
                // Ask if they still want to take the loan
                // If yes
                    // Create Loan object
                        // Add Loan object to Customer Loan list
                        // Add Loan to syste Loan list
        }
        public void UpdateCustomerInformation()
        {
            // Ask what the user wants to change (mail/phone/password)
            // Ask the user to enter new value
            // Ask the user to enter the value again
            // If they match
            // Update specified info
            // Else
            // Ask if they want to try again or go back to customer menu

            Console.WriteLine("What would you like to change?");
            Console.WriteLine("1. Mail\n2. Phonenumber\n3, Password");
            string choice = Console.ReadLine();

            switch (choice)
            {
                //CHANGE EMAIL
                case "1":
                    {
                        while (true)
                        {
                            Console.WriteLine("Change Email");

                            Console.WriteLine("Please enter your new Email:");
                            string newMail1 = Console.ReadLine();

                            Console.WriteLine("Please confirm your new Email:");
                            string newMail2 = Console.ReadLine();

                            if (newMail1 != newMail2)
                            {
                                Console.WriteLine("The Emailnames does not match. Please try again.");
                            }
                            else
                            {
                                this.Email = newMail2;
                                Console.WriteLine("Email has been changed!");

                                break;

                            }
                        }
                        break;
                    }
                //CHANGE PHONENUMBER
                case "2":
                    {
                        while (true)
                        {
                            Console.WriteLine("Change Phonenumber");

                            Console.WriteLine("Please enter your new phonenumber:");
                            string newPhoneNumber1 = Console.ReadLine();

                            Console.WriteLine("Please confirm your new phonenumber:");
                            string newPhoneNumber2 = Console.ReadLine();

                            if (newPhoneNumber1 != newPhoneNumber2)
                            {
                                Console.WriteLine("The phonenumbers does not match. Please try again.");
                            }
                            else
                            {
                                Console.WriteLine("Phonenumber has been changed!");
                                this.PhoneNumber = newPhoneNumber2;
                                break;

                            }
                        }
                        break;
                    }
                //CHANGE PASSWORD
                case "3":
                    {
                        while (true)
                        {
                            Console.WriteLine("Change Password");

                            Console.WriteLine("Please enter your new password:");
                            string newPassword1 = Console.ReadLine();

                            Console.WriteLine("Please confirm your new password:");
                            string newPassword2 = Console.ReadLine();

                            if (newPassword1 != newPassword2)
                            {
                                Console.WriteLine("The passwords does not match. Please try again.");
                            }
                            else
                            {
                                Console.WriteLine("Password has been changed!");

                                break;

                            }
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid choice.");
                        break;
                    }
            
            }
                

         }



    }
}
