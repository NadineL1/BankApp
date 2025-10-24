using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    internal static class Helper
    {
        // Prints the message parameter followed by "..." every second for as long as duration is.
        public static void PauseBreak(string message, int duration)
        {
            Console.WriteLine($"{message}...");
            for (int i = 0; i < duration; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("...");
            }
        }
    }
}
