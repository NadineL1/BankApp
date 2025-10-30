using BankApp.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.BankAccounts
{
    public class BankAccountBase
    {
		public int AccountNumber;
		public decimal Balance { get; set; } = 0;
		public readonly int CustomerID;
		private static int s_nextAccountNumber;
		Enums.CurrencyTypes CurrencyType {  get; set; }
    public List<Transaction> TransactionList {  get; set; } = new List<Transaction>();

		static BankAccountBase()
		{
			Random random = new Random();
			s_nextAccountNumber = random.Next(10000000, 20000000);
		}
		public BankAccountBase(int customerID,Enums.CurrencyTypes currencyTypes,decimal balance)
		{
			this.AccountNumber = s_nextAccountNumber++;
			this.Balance = balance;
			this.CustomerID = customerID;
			this.CurrencyType = currencyTypes;
		}
		public virtual void PrintAccountInfo()
		{
			Console.WriteLine($"Accountnumber: {AccountNumber}, Balance: {Balance}kr");


		}
        public void PrintTransactionHistory()
		{
			foreach (Transaction transaction in TransactionList)
			{
				if(transaction.Sender.AccountNumber == AccountNumber)
				{
                    Console.WriteLine($"At {transaction.DateOfTransaction} you sent {transaction.TransactionAmount} from bankaccount \"{transaction.Sender.AccountNumber}\" to bankaccount \"{transaction.Receiver.AccountNumber}\".");

                }
				else if(transaction.Receiver.AccountNumber == AccountNumber)
				{
					Console.WriteLine($"At {transaction.DateOfTransaction} you received {transaction.TransactionAmount} from bankaccount \"{transaction.Sender.AccountNumber}\" to bankaccount \"{transaction.Receiver.AccountNumber}\".");
                }
				else { Console.WriteLine("Error, this transaction doesn't match this account."); }
			}
		}

	}
}
