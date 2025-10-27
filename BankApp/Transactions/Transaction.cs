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
            Sender.Balance -= TransactionAmount;
            Receiver.Balance += TransactionAmount;
            BankSystem.TransactionHistory.Add(this);
        }
        public void PrintTransaction()
        {
            // Print: The dateTime you sent amount from sender to receiver
            Console.WriteLine($"This transaction sent {TransactionAmount} from bankaccount {Sender.AccountNumber} to {Receiver.AccountNumber} at {DateOfTransaction}.");
        }
    }


}
