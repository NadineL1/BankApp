using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.BankAccounts
{
    internal class CheckingsAccount : BankAccountBase
    {
        public string AccountType { get; set; } = "checking";
        public CheckingsAccount(int customerID, Enums.CurrencyTypes currencyTypes, decimal balance) : base(customerID, currencyTypes, balance)
        {

        }

        public override void PrintAccountInfo()
        {
            Console.WriteLine($"{AccountType} account, Accountnumber: {AccountNumber}, Balance: {Balance}{CurrencyType}");
        }

    }
}
