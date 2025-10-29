using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount: BankAccountBase
    {
        public static decimal InterestRate { get; set; } = 7.5M;
        public SavingsAccount(int customerID, decimal balance): base(customerID, balance)
        {
            AccountType = "Savings account";
        }
	}
}
