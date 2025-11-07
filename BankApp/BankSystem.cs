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
            new Customer(1001, "John Doe", "blehblah@msn.com", "test1", "0761234567", false, 20000m, 10m),
            new Customer(1002, "Anna Deer", "defaultemail@hotmail.com", "test2", "0769876543", false, 30000m, 10000),
            new Customer(1003, "Tina Stag", "destroyerofworlds@gmail.com", "test3", "0761011010", false, 50000m, 13337m)
        };

        public static Admin Admin { get; set; } = new Admin(0001, "Adam Admin", "blandsystemmail@thisbank.com", "admin1", "0769998877");
        public static Queue<Transaction> PendingTransactions { get; set; } = new Queue<Transaction>();
        public static List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
        public static List<Loan> AllLoan { get; set; } = new List<Loan>();

        private static decimal InitialSEKperEUR { get; set; } = 11.36m;
        private static decimal InitialSEKperUSD { get; set; } = 11.00m;


        public static void UpdateExchangeRate(Enums.CurrencyTypes currencyType, decimal newExchangeRate)
        {
            
            if ( (currencyType == Enums.CurrencyTypes.EUR || currencyType == Enums.CurrencyTypes.USD) && newExchangeRate > 0)
            {
                // Stores the SEK-to-selectedCurrencyType and vice-versa as variables so that we can access the values in the dictionary.
                var key1 = (Enums.CurrencyTypes.SEK, currencyType);
                var key2 = (currencyType, Enums.CurrencyTypes.SEK);

                // Uses the variables holding the keys to update the value of that key-value pair.
                BankSystem.ExchangeRate[key1] = decimal.Round(1m / newExchangeRate, 4);
                BankSystem.ExchangeRate[key2] = newExchangeRate;

                // Writes a confirmation that the exchange rates have been updated.
                Console.WriteLine($"The new exhangerate for {currencyType} is now: {BankSystem.ExchangeRate[key2]} SEK per one {currencyType}.");
                Helper.PauseBreak("Returning to adminmenu", 3);
                return;
            }
            else
            {
                Console.WriteLine("Something went wrong.");
                Console.WriteLine("The exchangerates have not been updated. Please try again.");
                Helper.PauseBreak("Returning to adminmenu", 3);
                return;
            }
        }

        public static Dictionary<(Enums.CurrencyTypes From, Enums.CurrencyTypes To), decimal> ExchangeRate { get; private set; } = new()
        {
            { (Enums.CurrencyTypes.SEK, Enums.CurrencyTypes.EUR), decimal.Round(1m/InitialSEKperEUR, 4) },
            { (Enums.CurrencyTypes.EUR, Enums.CurrencyTypes.SEK), InitialSEKperEUR },
            { (Enums.CurrencyTypes.SEK, Enums.CurrencyTypes.USD), decimal.Round(1m/InitialSEKperUSD, 4) },
            { (Enums.CurrencyTypes.USD, Enums.CurrencyTypes.SEK), InitialSEKperUSD }
        };
        public static void FifteenMinutesMethod()
        {
            Console.WriteLine("The fifteen minute update is called.");
            // Only executes pending transactions if there are pending transactions.
            if (PendingTransactions.Count != 0)
            {
                // Executes the all pending transactions and removes them from the queue.
                for(int i = 0; i < PendingTransactions.Count; i++)
                {
                    PendingTransactions.Dequeue().ExecuteTransaction();
                }
                Helper.PauseBreak("Executing transactions", 3);
            }
        }
        // async method that calculates your savings with interest and increase the value of your savings account
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
