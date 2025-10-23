using BankApp.BankAccounts;

namespace BankApp
{
    internal class Program
    {
        static void Main()
        {
			// Log-in metod
			// INGEN LOGIK HÄR ENDAST STARTA IGÅNG APPEN :)
			BankAccountBase bankaccount1 = new BankAccountBase(1000);
			Console.WriteLine(bankaccount1.AccountNumber);

			BankAccountBase bankaccount2 = new BankAccountBase(2000);
			Console.WriteLine(bankaccount2.AccountNumber + bankaccount2.Balance);
			BankAccountBase bankaccount3 = new BankAccountBase(30.00);
			Console.WriteLine(bankaccount3.AccountNumber + bankaccount3.Balance);

		}
    }
}
