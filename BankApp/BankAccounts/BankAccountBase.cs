using System;
using System.Collections.Generic;
using System.Linq;
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

		static BankAccountBase()
		{
			Random random = new Random();
			s_nextAccountNumber = random.Next(10000000, 20000000);
		}
		public BankAccountBase(int customerID,decimal balance)
		{
			this.AccountNumber = s_nextAccountNumber++;
			this.Balance = balance;
			this.CustomerID = customerID;
		}
		public void PrintAccountInfo()
		{
			Console.WriteLine($"Accountnumber: {AccountNumber}, Balance: {Balance}kr");
		}

	}
}
