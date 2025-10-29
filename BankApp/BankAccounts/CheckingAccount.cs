using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.BankAccounts
{
    internal class CheckingsAccount : BankAccountBase
    {
        public CheckingsAccount(int customerID, decimal balance) : base(customerID, balance)
        {

        }

        public override void PrintAccountInfo()
        {
            Console.Write($"Checkings account, Accountnumber: {AccountNumber}, Balance: {Balance}kr\n");
        }

    }
}
