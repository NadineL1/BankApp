using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount: BankAccountBase
    {
        public decimal interest { get; set; }
        public SavingsAccount(int customerID, decimal balance): base(customerID, balance)
        { 
            
        }
	}
}
