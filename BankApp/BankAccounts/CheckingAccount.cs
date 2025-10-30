using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankApp.Enums;

namespace BankApp.BankAccounts
{
    internal class CheckingsAccount : BankAccountBase
    {
        public CheckingsAccount(int customerID, Enums.CurrencyTypes currencyTypes, decimal balance) : base(customerID, currencyTypes, balance)
        {

        }

        public override void PrintAccountInfo()
        {
            Console.WriteLine($"Checkings account, Accountnumber: {AccountNumber}, Balance: {Balance}kr\n");
        }

    }
}
