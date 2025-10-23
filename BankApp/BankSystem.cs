using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Users;
using BankApp.BankAccounts;
using BankApp.Loans;
using BankApp.Transactions;

namespace BankApp
{
    internal class BankSystem
    {
        List<User> AllUsers { get; set; } = new List<User>();
        List<BankAccountBase> AllAccounts { get; set; } = new List<BankAccountBase>();
        List<Transaction> PendingTransactions { get; set; } = new List<Transaction>();
        List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
        List<Loan> AllLoan { get; set; } = new List<Loan>();

        public decimal ExchangeEuro {get; set;}

        public BankSystem()
        {
            // Lists with some default values just for testing.
            List<User> defaultUsers = new List<User>()
            {
                new Customer(1, )
            };
            /*


            List<BankAccountBase> defaultAccounts = new List<BankAccountBase>()
            {

            }
            List<Transaction> defaultTransactions = new List<Transaction>()
            {

            }
            List<Loan> defaultLoans = new List<Loan>()
            {

            }
            */

            ExchangeEuro = 1m;
        }
        // Tries to go execute all pending transactions
        public void ExecuteTransactions()
        {
            foreach (Transaction transaction in PendingTransactions)
            {
                // Move the money and move the transaction to Transaction history
                // transaction.ExecuteTransaction();
            }
        }

        public void UpdateExchangeRate()
        {
            // Enter value
            // ExchangeEuro = value;
        }
    }
}
