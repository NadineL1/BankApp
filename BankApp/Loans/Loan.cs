using BankApp.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BankApp.Loans
{
    internal class Loan
    {
        public decimal Balance { get; set; }

        public decimal Interest { get; set; } = 0.02m;   //2% Interest
        
        public Users.Customer Customer { get; set; }

        public decimal AmountOfMonths { get; set; }

        public Loan(decimal balance, decimal interest, Users.Customer customer, decimal amountofmonths)
        {
            Balance = balance;
            Interest = interest;
            Customer = customer;
            AmountOfMonths = amountofmonths;
        }



        public void PrintLoanInfo()
        {
            Console.WriteLine($"Loan ammount: {Balance}");
            Console.WriteLine($"Interest: {Balance * Interest * AmountOfMonths}");
            Console.WriteLine($"Total loan cost: {Balance + Balance * Interest * AmountOfMonths}\n\n");
        }

       
    }
}
