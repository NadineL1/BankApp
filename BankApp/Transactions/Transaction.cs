using BankApp.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Transactions
{
    internal class Transaction
    {
        // BankAccounr Receiver, Sender, Value, dateTime
        BankAccountBase Receiver {  get; set; }
        BankAccountBase Sender { get; set; }
        public decimal TransactionAmount {  get; set; }
        public DateTime DateOfTransaction { get; set; }

        public Transaction(BankAccountBase sender, BankAccountBase receiver, decimal transactionAmount)
        {
            Sender = sender;
            Receiver = receiver;
            TransactionAmount = transactionAmount;
            DateOfTransaction = DateTime.Now;
        }

        public void ExecuteTransaction()
        {
            // Check to see if Cutomer has enough money.
            if (Sender.Balance < TransactionAmount) 
            {
                Console.WriteLine("Transfer failed. Not enough funds.");
                return;
            }
            // If currency are the same, no exchange needs.
            if (Sender.CurrencyType == Receiver.CurrencyType)
            {
                Sender.Balance -= TransactionAmount;
                Receiver.Balance += TransactionAmount;

                Console.WriteLine($"Transfer successful! {TransactionAmount} has been sent.");
            }
            else 
            {
                // If currency is different, start exchange
                decimal convertedAmount = Helper.ConvertCurrency(
                    TransactionAmount,  // The amount to convert
                    Sender.CurrencyType,  
                    Receiver.CurrencyType
                    );

                // Update the balance.
                Sender.Balance -= TransactionAmount;
                Receiver.Balance += TransactionAmount;

                Console.WriteLine($"Transfer successful! {TransactionAmount} has been sent.");
                Console.WriteLine($"Converted {TransactionAmount} {Sender.CurrencyType} to {convertedAmount} {Receiver.CurrencyType}.");
            }
            // Add transaction to history.
            BankSystem.TransactionHistory.Add(this);
        }
        public void PrintTransaction()
        {
            // Print: The dateTime you sent amount from sender to receiver
            Console.WriteLine($"This transaction sent {TransactionAmount} from bankaccount {Sender.AccountNumber} to {Receiver.AccountNumber} at {DateOfTransaction}.");
        }
    }


}
