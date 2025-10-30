using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccountBase
    {
        public decimal interest { get; set; }
        public SavingsAccount(int customerID, Enums.CurrencyTypes currencyTypes, decimal balance) : base(customerID, currencyTypes, balance)
        {

        }

        public override void PrintAccountInfo()
        {
            Console.WriteLine($"Savings account, Accountnumber: {AccountNumber}, Balance: {Balance}{CurrencyType}");
        }

    }

}

