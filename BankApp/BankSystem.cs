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
        List<User> AllCustomers { get; set; } = new List<User>();
        Admin Admin { get; set; } = new Admin(1, "Adam Admin", "blandsystemmail@thisbank.com", "admin1", "0769998877");
        List<BankAccountBase> AllAccounts { get; set; } = new List<BankAccountBase>();
        List<Transaction> PendingTransactions { get; set; } = new List<Transaction>();
        List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
        List<Loan> AllLoan { get; set; } = new List<Loan>();

        public decimal ExchangeEuro {get; set;}

        public BankSystem()
        {
            // Lists with some default values just for testing.
            AllCustomers = new List<User>()
            {                
                new Customer(2, "John Doe", "blehblah@msn.com", "test1", "0761234567", false),
                new Customer(3, "Anna Deer", "defaultemail@hotmail.com", "test2", "0769876543", false),
                new Customer(4, "Tina Stag", "destroyerofworlds@gmail.com", "test3", "0761011010", false)
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
