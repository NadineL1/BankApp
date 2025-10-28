using BankApp.Transactions;
using BankApp.BankAccounts;
using BankApp.Loans;
using System.Threading;

namespace BankApp.Users
{
    internal class Customer : User
    {
        public bool LockBool { get; set; } = false;
        public List<BankAccountBase> CustomerBankAccounts { get; set; }
        public List<Loan> CustomerActiveLoans { get; set; }

        public Customer(int userId, string name, string email, string password, string phoneNumber, bool lockbool) : base(userId, name, email, password, phoneNumber, false)
        {
            LockBool = lockbool;
            CustomerBankAccounts = new List<BankAccountBase>();
            CustomerActiveLoans = new List<Loan>();
        }
        // Overloads the constructor so that it can also take two additioanl decimal values
        // These values are then used as the balance for a checking account and a savings account made with this customers userID.
        public Customer(int userId, string name, string email, string password, string phoneNumber, bool lockbool, decimal bankAccountBalance1, decimal bankAccountBalance2) : base(userId, name, email, password, phoneNumber, false)
        {
            LockBool = lockbool;
            CustomerBankAccounts = new List<BankAccountBase>() {
                new CheckingsAccount(userId, bankAccountBalance1),
                new SavingsAccount(userId, bankAccountBalance2)
            };
            foreach (BankAccountBase account in CustomerBankAccounts)
            {
                BankSystem.AllAccounts.Add(account);
            }
            CustomerActiveLoans = new List<Loan>();
        }

        public void Withdraw()
        {
            Console.WriteLine("Withdraws money");
            // Call list accounts method()
            // Ask user which bankaccount to make the withdrawal from
                // Ask user for how much to withdraw
                    // Check if customer has that much money in bankaccount
                        // If true
                        // Subtract asked amount from selected account.Saldo
                        // Write confirmation of withdrawal in console
                    // Else
                        // Tell the user they don't have enough money in that account
                        // Ask user if they want to take out a loan
                            // If yes
                            // Go to MakeLoan()

            
            //Console.WriteLine("Choose an account to withdrawal from:\n");
            //List<BankAccountBase> CustomerBankAccounts;
            //string choice = Console.ReadLine();

            //if (choice == "1")
            //{
            //    Console.WriteLine("Withdrawal amount:\n");
            //    decimal withdrawal = decimal.Parse(Console.ReadLine());

            //    if (BankAccountBase.balance >= withdrawal)
            //    {
            //        BankAccountBase.balance -- withdrawal;
            //        Console.WriteLine("Withdrawal confirmed!");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Error");
            //    }
            //}



        }
        public void StartTransaction()
        {
            Console.WriteLine("Starts Transaction"); // DEBUG: REMOVE LATER
            Console.WriteLine("Which account would like to make the transfer from?");
            // Creates a new list with all checking accounts the customer has in their name.
            List<BankAccountBase> checkingList = CustomerBankAccounts.FindAll(account => account is CheckingsAccount);
            Helper.PrintAccountList(checkingList);

            // Asks user what account to make the transaction from.
            // Uses user input to select an account based on the index of the temporary checkingaccount list.
            // Stores the selected account reference in a new variable to be sent into MakeTransaction().
            var senderAccount = checkingList[Helper.ListSelection(checkingList.Count)];

            Console.WriteLine("Do you want to make the transaction between one of your own accounts or to a external account?");
            // Creates a new selection list.
            List<string> internalExternal = new List<string>() { "Internal.", "External"};
            Helper.PrintSelectionList(internalExternal);
            // If user selects 1, internal.
            if(Helper.ListSelection(internalExternal.Count) == 0)
            {
                // Finds all the customer's accounts that aren't the sender account.
                List<BankAccountBase> userAccounts = CustomerBankAccounts.FindAll(account => account != senderAccount);
                // Asks the user to select which of their other accounts to send to money to.
                Console.WriteLine("Which of your account would you like to receive the transaction?");
                Helper.PrintAccountList(userAccounts);
                // Stores the reference to the receiver account so that we can sent it to MakeTransaction
                BankAccountBase receiverAccount = userAccounts[Helper.ListSelection(userAccounts.Count)];

                // Calls MakeTransaction with the selected sender and receiver accounts.
                MakeTransaction(senderAccount, receiverAccount);
            }
            // Else they've selected external
            else
            {
                // Initializes a loop so that the user has several attempts to enter a corrct acconut number.
                bool selectBankNumberLoop = true;
                while (selectBankNumberLoop)
                {
                    // Creates a variable to store the receiver account reference.
                    BankAccountBase? receiverAccount = null;
                    // Ask user to enter bank number
                    Console.WriteLine("Please enter the bank number of the receiving account");
                    // Prints all accounts so that we devs can see the account numbers.
                    Helper.PrintAccountList(BankSystem.AllAccounts); // DEBUG: REMOVE LATER
                    Console.WriteLine();

                    // Makes sure that the user enters a positive integer
                    if (int.TryParse(Console.ReadLine(), out int tryAccount) && tryAccount > 0)
                    {
                        // Checks all accounts in the system for a match against user input.
                        foreach (BankAccountBase account in BankSystem.AllAccounts)
                        {
                            // Stores the reference if a match is found
                            if (account.AccountNumber == tryAccount)
                            {
                                receiverAccount = account;
                            }
                        }
                        // Asks the user if they want another attempt to enter a number if they entered an incorrect number.
                        if (receiverAccount == null)
                        {
                            Console.WriteLine("Couldn't find an account with that account number.");
                            Console.WriteLine("Do you want to try to enter another banknumber?");
                            Helper.PrintSelectionList(SelectionLists.YesNo);
                            if(Helper.ListSelection(SelectionLists.YesNo.Count) == 1)
                            {
                                selectBankNumberLoop = false;
                                Console.WriteLine("Canceling transaction.");
                                Helper.PauseBreak("Returning to menu", 3);
                            }
                        }
                        // Calls maketransaction if they found a matching account number.
                        else if(receiverAccount != null)
                        {
                            Console.WriteLine("Account found"); // DEBUG: REMOVE LATER
                            selectBankNumberLoop = false;
                            MakeTransaction(senderAccount, receiverAccount);
                        }
                    }
                }
            }
        }

        public void MakeTransaction(BankAccountBase sender, BankAccountBase receiver)
        {
            // Ask user how much money to transact
            // Check if account has the money to do so
            // (This should be delayed 15 min, we'll look at that in week 2)
            // If yes, subtrac from active Bankaccount.Saldo
            // Add that money to receivning BankAccount.Saldo

            Console.Clear();
            Console.Write("Enter the amount you want to transfer: ");
            
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0) 
            {
                if (sender.Balance < amount)
                {
                    Console.WriteLine("\nFailed to transfer! Not enough balance.");
                }
                else 
                {
                    //Console.WriteLine("\nPlease hold, this prossesing transaction will take 15-minutes.");
                    // Need to make a Thread.sleep?
                    Transaction transaction = new Transaction(sender, receiver, amount);
                    transaction.ExecuteTransaction();
                    // FIX: Needs to add this transaction to transaction history of both sender and receiver.
                    
                   
                    Console.WriteLine($"\nTransfer successful! {amount} has been sent.");
                }               
            }
            else
            {
                Console.WriteLine("\nYou have entered an incorrect value.");
            }
        }
        public void CheckTransactionHistory()
        {
            Console.WriteLine("Checks Transaction History");
            foreach(Transaction transaction in BankSystem.TransactionHistory)
            {
                transaction.PrintTransaction();
            }
            Console.ReadLine();
            // Call list accounts method()
            // Select BankAccount from list
            // PrintTransaction foreach TRansaction in BankAccount.Transaction history
        }
        
        public void CheckBankAccounts()
        {
            Console.Clear();
            Console.WriteLine("Your Accounts:\n");
            foreach (var account in CustomerBankAccounts)
            {
                account.PrintAccountInfo();
            }
            
            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
            
        }
        
        public void CreateBankAccount()
        {
            // Confirm that the customer want to make an new account
            Console.WriteLine("Are you sure you want to create a new account?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            int input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    // Which type of bankaccount do they want (check or savings)
                    Console.WriteLine("What type of account would you like to create?");
                    Console.WriteLine("1. Checkings account");
                    Console.WriteLine("2. Savings account");
                    int accountType = int.Parse(Console.ReadLine());

                    // Create a BankAccount object of the correct type
                    switch (accountType)
                    {
                        // Add it to Customer AccountList, BankSystem account list
                        // Write confirmation of the new BankAccount
                        case 1:
                            CheckingsAccount checkingsaccount = new CheckingsAccount(123, 0);
                            CustomerBankAccounts.Add(checkingsaccount);
                            BankSystem.AllAccounts.Add(checkingsaccount);
                            Console.WriteLine("Checkings account created successfully!");
                            break;

                        // Add it to Customer AccountList, BankSystem account list
                        // Write confirmation of the new BankAccount
                        case 2:
                            SavingsAccount savingsaccount = new SavingsAccount(123, 0);
                            CustomerBankAccounts.Add(savingsaccount);
                            BankSystem.AllAccounts.Add(savingsaccount);
                            Console.WriteLine("Savings account created successfully!");
                            break;


                        default:
                            Console.WriteLine("Invalid account type selected.");
                            break;
                    }
                    break;

                case 2:
                    break;


                default:
                    Console.WriteLine("Invalid option.");
                    break;

            }

            // (Later check what currency they want the acount in)

        }
        public void CheckLoans()
        {
            Console.WriteLine("Checks Loans");
            // Foreach Loan in Customer Loan list
            // Write Loan.info
            // (Later, maybe make the customer select a loan to make repayments)
        }
        public void LoanRequest()
        {
            Console.WriteLine("Takes out Loan");
            // Ask user what account they want the money from the loan to go to
                // Ask user the amount they want to borrow
                    // If loan amount isn't more than 5x total balance in all of customer accounts
                    // Show how much customer wants to borrow and how much extra they have to pay in interest
                        // Ask if they still want to take the loan
                            // If yes
                            // Create Loan object
                            // Add Loan object to Customer Loan list
                            // Add Loan to syste Loan list
        }
        public void UpdateCustomerInformation()
        {
            // Ask what the user wants to change (mail/phone/password)
                // Ask the user to enter new value
                // Ask the user to enter the value again
                    // If they match
                        // Update specified info
                    // Else
                        // Ask if they want to try again or go back to customer menu

            Console.WriteLine("What would you like to change?");
            Console.WriteLine($"1. Email (current: {this.Email})\n2. Phonenumber (current: {this.PhoneNumber})\n3. Password");
            string choice = Console.ReadLine();

            switch (choice)
            {
                //CHANGE EMAIL
                case "1":
                        while (true)
                        {
                            Console.WriteLine("Change Email");

                            Console.WriteLine("Please enter your new Email:");
                            string newMail1 = Console.ReadLine();

                            Console.WriteLine("Please confirm your new Email:");
                            string newMail2 = Console.ReadLine();

                            if (newMail1 != newMail2)
                            {
                                Console.WriteLine("The Emailnames does not match. Please try again.");
                            }
                            else
                            {
                                this.Email = newMail2;
                                Console.WriteLine("Email has been changed!");

                                break;
                            }
                        }
                        break;
                    
                //CHANGE PHONENUMBER
                case "2":
                        while (true)
                        {
                            Console.WriteLine("Change Phonenumber");

                            Console.WriteLine("Please enter your new phonenumber:");
                            string newPhoneNumber1 = Console.ReadLine();

                            Console.WriteLine("Please confirm your new phonenumber:");
                            string newPhoneNumber2 = Console.ReadLine();

                            if (newPhoneNumber1 != newPhoneNumber2)
                            {
                                Console.WriteLine("The phonenumbers does not match. Please try again.");
                            }
                            else
                            {
                                Console.WriteLine("Phonenumber has been changed!");
                                this.PhoneNumber = newPhoneNumber2;
                                break;
                            }
                        }
                        break;
                    
                //CHANGE PASSWORD
                case "3":
                        while (true)
                        {
                            Console.WriteLine("Change Password");

                            Console.WriteLine("Please enter your new password:");
                            string newPassword1 = Console.ReadLine();

                            Console.WriteLine("Please confirm your new password:");
                            string newPassword2 = Console.ReadLine();

                            if (newPassword1 != newPassword2)
                            {
                                Console.WriteLine("The passwords does not match. Please try again.");
                            }
                            else
                            {
                                Console.WriteLine("Password has been changed!");
                                this.Password = newPassword2;
                                break;
                            }
                        }
                        break;
                    
                default:
                        Console.WriteLine("Invalid choice.");
                        break;
                    

            }


        }



    }
}
