                                                  The BANK APP : Five Bank 
    This is a group project for school made by: 
    https://github.com/NadineL1    
    https://github.com/Erkan334    
    https://github.com/Vic-Linden    
    https://github.com/EsnoGitAccount
            
            
**USER INSTRUCTIONS:**

Admin 
default username: Adam Admin            password: admin1 


Customer1 
userID: 1001 
password: test1


Customer2 -
userID: 1002 
password: test2


Customer3 -
userID: 1003 
password: test3


If you fail to login with customer 3x - the account will be locked
(can be unlocked in adminMenu)



**CLASSES: **


**Menu:**
Contains the logic for our three main menus; log-in menu, customer menu, admin menu. Log-in menu figures out which account object is trying to login and redirects the user to the proper menu. The customer menu gives the user a set of customer options and sends the user to the selected options method. Admin menu does the same but for the admin methods.


**User:**
The User class holds all the personal information of a user like name, password, e-mail, etc. It is then inherited to create the Customer and Admin classes.


**Customer: **
also holds information like which bank accounts are associated with that customer. Customer also holds most of the methods that are called from the customer menu. For example methods that make transaction objects that will later be processed, or methods that allow the user to change the account information of that customer.


**Admin: **
only adds the methods called from the adminmenu. 


**BankAccountBase:**
Contains the base bank account properties like the account number, balance, and currency type. 
Inherited into CheckingsAccount and SavingsAccount.


**CheckingsAccount: **
functions pretty much like BankBaseAccount.


**Savingsaccount: **
adds an interest-rate property that allows us to simulate a savingsaccount accruing interest.


**Loans:**
properties: Balance, Interest Users, AmountofMonths, TotalLoanCost method: PrintLoanInfo()


**Transactions: **
Information about the transactions properties: Sender, Receiver, ConvertedAmount, PreConvertedAmount, CurrencyType, DateOfTransaction methods: ExecuteTransaction() PrintTransaction()


**BankSystem:**
Contains all information for the different objects that are active (Bank accounts, transactions, loans etc.). 
This class also contains methods that make sure that transactions aren’t processed immediately, as well as a method to change the exchange rates in the whole system.
Methods: UpdateExchangeRate(), FifteenMinutesMethod()


**Enums:**
Contains an enum called CurrencyTypes for SEK, EUR and USD. It will be used to keep track of which currency an account and amount uses.


**Helper:**
Bundle of helper-methods for easier and cleaner code methods: PauseBreak(), PrinSelectionList(), PrintAccountList(), ListSelection(), ConvertCurrency(), PrintLoanList()


**Logo:**
Holds the ASCII logo used in the log-in menu.


**SelectionLists:**
Bundle of alternative-lists that are reused in the project. They are kept in a separate class to make the code easier to follow.

