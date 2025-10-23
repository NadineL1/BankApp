using BankApp.Users;
using System.Xml.Linq;

namespace BankApp
{
    internal class Program
    {
        static void Main()
        {
            // Log-in metod
            // INGEN LOGIK HÄR ENDAST STARTA IGÅNG APPEN :)


            Customer customer = new Customer(123, "abc", "abc.se", "password", "070", false);

            customer.Withdraw();

        }
    }
}
