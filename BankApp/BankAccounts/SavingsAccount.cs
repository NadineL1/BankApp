using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccountBase
    {
        public string AccountType { get; set; } = "savings";
        // interestrate gets a magic number of + 50 %, so 1,5 * balance
        public static decimal InterestRate { get; set; } = 1.5M;
        
        public SavingsAccount(int customerID, Enums.CurrencyTypes currencyTypes, decimal balance) : base(customerID, currencyTypes, balance)
        {
           
        }

        public override void PrintAccountInfo()
        {
            Console.WriteLine($"{AccountType} account, Accountnumber: {AccountNumber}, Balance: {Balance}{CurrencyType}, Interest rate: {InterestRate}");
        }
 
    }

}

