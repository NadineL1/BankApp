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
    internal static class BankSystem
    {
        public static List<BankAccountBase> AllAccounts { get; set; } = new List<BankAccountBase>();
        public static List <SavingsAccount> Savings { get; set; } = new List<SavingsAccount> ();
        public static List<Customer> AllCustomers { get; set; } = new List<Customer>()
        {
            new Customer(2, "John Doe", "blehblah@msn.com", "test1", "0761234567", false, 20000m, 10m),
            new Customer(3, "Anna Deer", "defaultemail@hotmail.com", "test2", "0769876543", false, 30000m, 10000),
            new Customer(4, "Tina Stag", "destroyerofworlds@gmail.com", "test3", "0761011010", false, 50000m, 13337m)
        };

        public static Admin Admin { get; set; } = new Admin(1, "Adam Admin", "blandsystemmail@thisbank.com", "admin1", "0769998877");
        public static List<Transaction> PendingTransactions { get; set; } = new List<Transaction>();
        public static List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
        public static List<Loan> AllLoan { get; set; } = new List<Loan>();

        /*
        public static BankSystem()
        {
            // Lists with some default values just for testing.
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
            

            ExchangeEuro = 1m;
        }
        */

        public static void UpdateExchangeRate()
        {
            // Enter value
            // ExchangeEuro = value;
        }

        public static readonly Dictionary<(Enums.CurrencyTypes From, Enums.CurrencyTypes To), decimal> ExchangeRate = new()
        {
            { (Enums.CurrencyTypes.SEK, Enums.CurrencyTypes.EUR), 0.088m },
            { (Enums.CurrencyTypes.EUR, Enums.CurrencyTypes.SEK), 11.36m },
            { (Enums.CurrencyTypes.SEK, Enums.CurrencyTypes.USD), 0.091m },
            { (Enums.CurrencyTypes.USD, Enums.CurrencyTypes.SEK), 11.00m }
        };
        public static void FifteenMinutesMethod()
        {
            Console.WriteLine("The fifteen minute update is called.");
            // Only executes pendoing transactions if there are pending transactions.
            if (PendingTransactions.Count != 0)
            {
                // Executes the all pending transactions.
                foreach (Transaction transaction in PendingTransactions)
                {
                    transaction.ExecuteTransaction();
                }
                Helper.PauseBreak("Executing transactions", 3);
            }
        }
        // async method that calculate your savings with interest and increase the value of your savings account
        internal static async Task<decimal> SavingMoney(decimal balance, decimal interest, int accountID)
        {
            await Task.Delay(TimeSpan.FromSeconds(20));
            balance *= interest;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Your savingsaccount nr {accountID} now have {balance}kr balance! ");
            Console.ReadKey();
            return balance;
        }
    }
}
