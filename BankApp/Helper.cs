using BankApp.BankAccounts;
using BankApp.Loans;
using BankApp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    internal static class Helper
    {
        /// <summary>  
        /// Prints a message followed by "...", then prints "..." every second until it reaches duration.
        /// </summary>  
        /// <param name="message">Message printed in console followed by "..."</param>  
        /// <param name="duration">How many "..." will be printed in seconds</param>
        public static void PauseBreak(string message, int duration)
        {
            Console.WriteLine($"{message}...");
            for (int i = 0; i < duration; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("...");
            }
        }
        /// <summary>
        /// Prints a numbered list of strings to the console.
        /// </summary>
        /// <remarks>This method outputs each item in the <paramref name="selectionList"/> to the console,
        /// with each item numbered sequentially.</remarks>
        /// <param name="selectionList">The list of strings to be printed. Each string is prefixed with its index in the list, starting from 1.</param>
        public static void PrintSelectionList(List<string> selectionList)
        {
            for (int i = 0; i < selectionList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectionList[i]}");
            }
        }
        /// <summary>
        /// Prints a numbered list of the account information of every BankAccountBase in a BankAccountList
        /// </summary>
        /// <param name="accountList"></param>
        public static void PrintAccountList(List<BankAccountBase> accountList)
        {
            for (int i = 0; i < accountList.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                accountList[i].PrintAccountInfo();
            }
        }
        /// <summary>
        /// Prompts the user to select an item from a list by entering a number.
        /// </summary>
        /// <remarks>The method repeatedly prompts the user until a valid selection is made. A valid
        /// selection is an integer between 1 and <paramref name="listSize"/>.</remarks>
        /// <param name="listSize">The total number of items in the list. Must be greater than zero.</param>
        /// <returns>The number entered by the user, representing their selection, but subtractet by one in order to be used to index a list
        /// <paramref name="listSize"/>.</returns>
        public static int ListSelection(int listSize)
        {
            int selection = 0;
            while (selection == 0)
            {
                if (int.TryParse(Console.ReadLine(), out selection) && selection > 0 && selection <= listSize)
                {
                    // Adds an empty line between user input and next line.
                    Console.WriteLine();
                    return selection - 1;
                }
                else
                {
                    Console.WriteLine($"Invald input. Please enter a number between 1 and {listSize}");
                    selection = 0;
                }
            }
            Console.WriteLine();
            return selection;
        }

        public static decimal ConvertCurrency(decimal amount, Enums.CurrencyTypes from, Enums.CurrencyTypes to) 
        {
            if (from == to)
                return amount;
            if (BankSystem.ExchangeRate.TryGetValue((from, to), out  decimal rate))
            {
                return amount * rate;
            }

            throw new ArgumentException("Error! Error!");                       
        }


        public static void PrintLoanList(List<Loan> loanList)
        {
            for (int i = 0; i < loanList.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]");
                loanList[i].PrintLoanInfo();
            }
        }

    }
}
