using BankApp.BankAccounts;
using BankApp.Loans;
using BankApp.Transactions;
using System.Threading;
using Spectre.Console;


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
                new CheckingsAccount(userId, Enums.CurrencyTypes.SEK, bankAccountBalance1),
                new SavingsAccount(userId, Enums.CurrencyTypes.SEK, bankAccountBalance2)
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
                    // If currency are the same, no exchange needed.
                    if (sender.CurrencyType == receiver.CurrencyType)
                    {
                        Transaction newTransaction = new Transaction(sender, receiver, amount, amount);
                        BankSystem.PendingTransactions.Add(newTransaction);                        

                        Console.WriteLine($"\nTransfer request successfully created! {amount} {sender.CurrencyType} will soon be sent.");
                    }
                    else 
                    {
                        // If currency is different, start convert.
                        decimal convertedAmount = Helper.ConvertCurrency(
                            amount,
                            sender.CurrencyType,
                            receiver.CurrencyType
                            );
                        // Update the balance.
                        Transaction newTransaction = new Transaction(sender, receiver, convertedAmount, amount);
                        BankSystem.PendingTransactions.Add(newTransaction);

                        Console.WriteLine($"\nTransfer request successfully created! {amount} {sender.CurrencyType} will soon be sent.");
                        Console.WriteLine($"Converted {amount} {sender.CurrencyType} to {convertedAmount} {receiver.CurrencyType}.");
                    }             
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
            Console.WriteLine("All transactions in system:");
            foreach (Transaction transaction in BankSystem.TransactionHistory) // DEBUG, REMOVE LATER: Prints all accounts in system to make checking easier
            {
                transaction.PrintTransaction();
            }
            Console.WriteLine();

            // Actual method starts here
            // 
            Console.WriteLine("Which account's transaction history would you like to see?");
            Helper.PrintAccountList(CustomerBankAccounts);
            // An ugly append to the selectionlist for additional options.
            Console.WriteLine($"{CustomerBankAccounts.Count + 1}. All accounts.");
            Console.WriteLine($"{CustomerBankAccounts.Count + 2}. Quit.");
            int historyInput = Helper.ListSelection(CustomerBankAccounts.Count + 2);

            if (historyInput < CustomerBankAccounts.Count)
            {
                CustomerBankAccounts[historyInput].PrintTransactionHistory();
                Console.ReadLine();
            }
            else if (historyInput == CustomerBankAccounts.Count)
            {
                // Makes a new list and stores all transactions from all the user's accounts in it.
                List<Transaction> allTransactions = new List<Transaction>();
                foreach (BankAccountBase account in CustomerBankAccounts)
                {
                    foreach (Transaction transaction in account.TransactionList)
                    {
                        allTransactions.Add(transaction);
                    }
                }

                // Sorts the new list based on the date of the transaction.
                allTransactions.Sort(delegate (Transaction x, Transaction y)
                {
                    if (x.DateOfTransaction == null && y.DateOfTransaction == null) return 0;
                    else if (x.DateOfTransaction == null) return -1;
                    else if (y.DateOfTransaction == null) return 1;
                    else return x.DateOfTransaction.CompareTo(y.DateOfTransaction);
                });

                Console.WriteLine("Hopefully prints all transactions from all accounts sorted by date of transaction.");
                Transaction previousTransaction = null;
                foreach (Transaction transaction in allTransactions)
                {
                    // Checks if the this transsaction is the same as the previous one to avoid writing the same things twice.
                    if (transaction != previousTransaction)
                    {
                        // Checks if the sender is one of the user's bankaccounts.
                        BankAccountBase foundAccount = null;
                        foundAccount = CustomerBankAccounts.Find(x => x.AccountNumber == transaction.Sender.AccountNumber);
                        if (foundAccount != null)
                        {
                            Console.WriteLine($"At {transaction.DateOfTransaction} you sent {transaction.ConvertedAmount}{transaction.CurrencyType} from bankaccount \"{transaction.Sender.AccountNumber}\" to bankaccount \"{transaction.Receiver.AccountNumber}\".");
                        }

                        // Checks if the receiver is one of the user's bankaccounts.
                        foundAccount = null;
                        foundAccount = CustomerBankAccounts.Find(x => x.AccountNumber == transaction.Receiver.AccountNumber);
                        if (foundAccount != null)
                        {
                            Console.WriteLine($"At {transaction.DateOfTransaction} you received {transaction.ConvertedAmount}{transaction.CurrencyType} from bankaccount \"{transaction.Sender.AccountNumber}\" to bankaccount \"{transaction.Receiver.AccountNumber}\".");
                        }
                        previousTransaction = transaction;
                    }
                }
                Console.ReadLine();
            }
            else
            {
                Helper.PauseBreak("Returning to menu", 3);
            }

            // Call list accounts method()

            // Select BankAccount from list
            // PrintTransaction foreach TRansaction in BankAccount.Transaction history
            Console.ReadLine();
        }
        
        public void CheckBankAccounts()
        {
            Console.Clear();          
            Console.WriteLine();

            // If customer has no bank account, show a message and return to menu.
            if (CustomerBankAccounts.Count == 0) 
            {
                AnsiConsole.MarkupLine("[red]You currently have no bank accounts at the Five bank.[/]");
                Console.WriteLine("\nPress any key to return to menu.");
                Console.ReadKey();
                return;
            }

            // Create a new objekt with Spectre (table). This one will help us use columns, rows and style.
            var table = new Table();
            // Titel design.
            table.Border = TableBorder.Rounded;
            table.BorderColor(Color.Gold1);
            table.Title("[bold yellow]Your bank accounts[/]");

            // Add Columns with colour, titel and position.
            table.AddColumn(new TableColumn("[bold cyan]Account type[/]").Centered());
            table.AddColumn(new TableColumn("[bold green]Account Number[/]").Centered());
            table.AddColumn(new TableColumn("[bold yellow]Currency[/]").Centered());
            table.AddColumn(new TableColumn("[bold magenta]Balance[/]").RightAligned());

            // Look through Accounts.
            foreach (var account in CustomerBankAccounts) 
            {  // If the Balance is less than 100, it will show in red, else it will be green colour.
                var balanceColour = account.Balance < 100 ? "red" : "green";
                table.AddRow( // Add 4 new rows.
                    $"[cyan]{account.GetType().Name}[/]", // Shows the type of account (CheckingAccount or SavingsAccount).
                    $"[white]{account.AccountNumber}[/]",
                    $"[yellow]{account.CurrencyType}[/]",
                    $"[{balanceColour}]{account.Balance:F2}[/]" // Shows the balance with two decimals (F2) and colour it (red or green) based on value. 
                    );
            }
            // Show table.
            AnsiConsole.Write(table);
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();                    
        }

        public void CreateBankAccount()
        {
            // Confirm that the customer want to make an new account
            Console.WriteLine("Are you sure you want to create a new account?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            int input = Helper.ListSelection(2) + 1;

                switch (input)
                {
                    case 1:
                        // Which type of bankaccount do they want (check or savings)
                        Console.WriteLine("What type of account would you like to create?");
                        Console.WriteLine("1. Checkings account");
                        Console.WriteLine("2. Savings account");
                        int accountType = int.Parse(Console.ReadLine());

                        // Ask user which currency they want the account to be in.
                        Console.WriteLine("Choose currency for your new account.");
                        Console.WriteLine("1. SEK");
                        Console.WriteLine("2. EUR");
                        Console.WriteLine("3. USD");

                        int currencyChoice = int.Parse(Console.ReadLine());
                        Enums.CurrencyTypes chosenCurrency;

                        switch (currencyChoice)
                        {
                            case 1:
                                chosenCurrency = Enums.CurrencyTypes.SEK;
                                break;
                            case 2:
                                chosenCurrency = Enums.CurrencyTypes.EUR;
                                break;
                            case 3:
                                chosenCurrency = Enums.CurrencyTypes.USD;
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Defaulting to SEK.");
                                chosenCurrency = Enums.CurrencyTypes.SEK;
                                break;
                        }
                        Console.WriteLine($"You chose to have the account in {chosenCurrency}");

                        // Create a BankAccount object of the correct type
                        switch (accountType)
                        {
                            // Add it to Customer AccountList, BankSystem account list
                            // Write confirmation of the new BankAccount
                            case 1:
                                CheckingsAccount checkingsaccount = new CheckingsAccount(123, chosenCurrency, 0);
                                CustomerBankAccounts.Add(checkingsaccount);
                                BankSystem.AllAccounts.Add(checkingsaccount);
                                Console.WriteLine("Checkings account created successfully!");
                                break;

                            // Add it to Customer AccountList, BankSystem account list
                            // Write confirmation of the new BankAccount
                            case 2:
                                SavingsAccount savingsaccount = new SavingsAccount(123, chosenCurrency, 0);
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
        }
        public void CheckLoans()
        {
            // Foreach Loan in Customer Loan list
            // Write Loan.info
            Console.Clear();
            Console.WriteLine("ACTIVE LOANS\n");
            foreach (var loan in CustomerActiveLoans)
            {
                loan.PrintLoanInfo();
            }
            //Ask user if they want to repay the loan
            if (CustomerActiveLoans.Count > 0)
            {
                Console.WriteLine("Would you like to repay your loan?");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");

                int choice;

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice < 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }

                //If yes, run Repay() Method
                switch (choice)
                {
                    case 1:
                        RepayLoan();
                        break;

                    case 2:
                        break;

                    default:
                        Console.WriteLine("Invalid input");
                        break;

                }

            }
            else
            {
                Console.Clear();
                Console.WriteLine("You have no active loans.");
                Console.WriteLine("Press anywhere to go back");
                Console.ReadKey();
            }
        }

        public void RepayLoan()
        {
            Console.Clear();
            Console.WriteLine("Which loan would you like to pay?");


            // Prints out the list of active loans
            Helper.PrintLoanList(CustomerActiveLoans);

            int selectedLoan = Helper.ListSelection(CustomerActiveLoans.Count);
            Loan loan = CustomerActiveLoans[selectedLoan];
            
            //Ask user what account they want to payback with
            Console.Clear();
            Console.WriteLine("Select which account you want to pay with");
            Helper.PrintAccountList(CustomerBankAccounts);


            int accountChoice = Helper.ListSelection(CustomerBankAccounts.Count);
            BankAccountBase selectedAccount = CustomerBankAccounts[accountChoice];

            Console.WriteLine($"Enter the amount to repay (remaining balance: {loan.Balance}):");
            while (true)
            { 
            
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    if (selectedAccount.Balance >= amount)
                    {
                       //Prevents the user from paying more than the active loan
                        if (amount <= loan.Balance)
                        { 
                    
                            selectedAccount.Balance -= amount;
                            loan.Balance -= amount;

                            Console.WriteLine($"Successfully paid {amount} SEK towards your loan.");

                                if (loan.Balance <= 0)
                                {
                                    Console.WriteLine("Loan fully repaid!");
                                    CustomerActiveLoans.Remove(loan);
                                    break;
                                }
                        
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Cannot pay more than your active loan");
                            Console.WriteLine("Please enter a new amount");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds in selected account.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. No repayment made.");
                    Console.WriteLine("Please enter a new amount");
                }
            }

            Console.WriteLine("\nPress any key to return to the menu...");

            Console.ReadKey();
        }
        public void LoanRequest()
        {
            Console.Clear();
            // Ask user which account they want the money from the loan to go to
            Console.WriteLine("Which account would you like the loan to be deposited into");
            Helper.PrintAccountList(CustomerBankAccounts);

            int input;
            
            while (true)
            {
                Console.WriteLine("Enter account number: ");
                if (int.TryParse(Console.ReadLine(), out input) && input > 0 && input <= CustomerBankAccounts.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid account number. Please try again.");
                }
            }
            
            BankAccountBase selectedAccount = CustomerBankAccounts[input - 1];
            
            // Ask user the amount they want to borrow
            Console.WriteLine("How much would you like to borrow?");

            decimal loanAmount;
            decimal totalBalance = 0;

            foreach (var account in CustomerBankAccounts)
            {
                totalBalance += account.Balance;
            }
            
            // If loan amount isn't more than 5x total balance in all of customer accounts
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out loanAmount) && loanAmount > 0 && loanAmount <= totalBalance * 5)
                {
                    break;
                }
                else if (loanAmount > totalBalance * 5)
                {
                    Console.WriteLine($"You can only borrow up to {totalBalance * 5} SEK");
                    Console.WriteLine("Please enter a new amount: ");
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                    Console.WriteLine("Please enter a new amount: ");
                }
            }
           
            // Show how much customer wants to borrow and how much extra they have to pay in interest
            
            Console.WriteLine("Choose payback period in months:");
            decimal paybackInMonths;

            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out paybackInMonths) && paybackInMonths > 0)
                {
                    Console.WriteLine($"Total loan cost:");
                    Console.WriteLine($"{loanAmount} + {loanAmount * 0.02m * paybackInMonths}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid amount of months");
                    Console.WriteLine("Choose payback period in months:");
                }
            }
            
                // Ask if they still want to take the loan
                Console.WriteLine("Do you accept the loan?");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");

            // If yes
            int choice;

            while (true)
            { 
                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice < 3)
                {
                    switch (choice)
                    {
                        // Create Loan object
                        // Add Loan object to Customer Loan list
                        // Add Loan to system Loan list
                        case 1:
                            Loan loan = new Loan(loanAmount, 0.02m, this, paybackInMonths);
                            CustomerActiveLoans.Add(loan);
                            BankSystem.AllLoan.Add(loan);
                            selectedAccount.Balance += loanAmount;

                            Console.WriteLine("Loan approved!");
                            Console.ReadKey();

                            break;

                        case 2:
                            break;
                    }
                        break;
                }
                else
                {
                    Console.WriteLine("Please select Yes or No");
                }
            
            }
            
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
