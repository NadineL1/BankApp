using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.BankAccounts
{
    internal class CheckingsAccount : BankAccountBase
    {
		
		public CheckingsAccount(int customerID, decimal balance): base (customerID, balance)
		{
			AccountType = "Checkings";
		}

	}
}
