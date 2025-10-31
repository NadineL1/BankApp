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
        public BankAccountBase Receiver { get; private set; }
        public BankAccountBase Sender { get; private set; }
        public decimal ConvertedAmount {  get; set; }
        public decimal PreConvertedAmount { get; set; }
        public Enums.CurrencyTypes CurrencyType { get; set; }
        public DateTime DateOfTransaction { get; set; }

        public Transaction(BankAccountBase sender, BankAccountBase receiver, decimal convertedAmount, decimal preConvertedAmount)
        {
            Sender = sender;
            Receiver = receiver;
            ConvertedAmount = convertedAmount;
            PreConvertedAmount = preConvertedAmount;
            CurrencyType = receiver.CurrencyType;
            DateOfTransaction = DateTime.Now;
        }

        public void ExecuteTransaction()
        {          
            Sender.Balance -= PreConvertedAmount;
            Receiver.Balance += ConvertedAmount;            
            BankSystem.TransactionHistory.Add(this);
            Sender.TransactionList.Add(this);
            Receiver.TransactionList.Add(this);
        }
        public void PrintTransaction()
        {
            // Print: The dateTime you sent amount from sender to receiver
            Console.WriteLine($"At {DateOfTransaction} this transaction sent {ConvertedAmount}{CurrencyType} from bankaccount \"{Sender.AccountNumber}\" to bankaccount \"{Receiver.AccountNumber}\".");
        }
    }


}
