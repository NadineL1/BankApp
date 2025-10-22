
using BankApp.Loans;
using BankApp.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Users
{
    internal class Customer : User
    {
        public bool LockBool {  get; set; } = false;
        public List<BankAccountBase> CustomerBankAccounts { get; set; }
        public List<Loan> CustomerActiveLoans { get; set; }

        public Customer() { }

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
                // 
        }
        public void CheckBankAccounts()
        {

        }
        public void CreateBankAccount()
        {

        }
        public void CheckLoans()
        {

        }
        public void MakeLoan()
        {

        }



    }
}
