using BankApp.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Transactions
{
    public class Transaction
    {
        // BankAccounr Receiver, Sender, Value, dateTime
        public BankAccountBase Receiver {  get; private set; }
        public BankAccountBase Sender { get; private set; }
        public decimal TransactionAmount {  get; set; }
        public DateTime DateOfTransaction { get; set; }
        // TODO: Probably gonna need to add currency type to the transaction class.
        // TODO: Implementing a transaction ID would probably be smart.

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
            Sender.TransactionList.Add(this);
            Receiver.TransactionList.Add(this);
        }
        public void PrintTransaction()
        {
            // Print: The dateTime you sent amount from sender to receiver
            Console.WriteLine($"At {DateOfTransaction} this transaction sent {TransactionAmount} from bankaccount \"{Sender.AccountNumber}\" to bankaccount \"{Receiver.AccountNumber}\".");
        }
    }


}
