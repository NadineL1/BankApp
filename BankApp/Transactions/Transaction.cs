using BankApp.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Transaction(BankAccountBase sender, BankAccountBase recever, decimal transactionAmount)
        {
            Sender = sender;
            TransactionAmount = transactionAmount;
            DateOfTransaction = DateTime.Now;
        }

        public void ExecuteTransaction()
        {

        }
        public void PrintTransaction()
        {
            // Print: The dateTime you sent amount from sender to receiver
            Console.WriteLine($"This transaction sent {TransactionAmount} from bankaccount {Sender.AccountNumber} to {Receiver.AccountNumber} at {DateOfTransaction}.");
        }
    }


}
