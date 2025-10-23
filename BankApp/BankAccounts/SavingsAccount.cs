using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount: BankAccountBase
    {
        public double interest { get; set; }
        public SavingsAccount(int customerID, double balance): base(customerID, balance)
        { 
            
        }
	}
}
