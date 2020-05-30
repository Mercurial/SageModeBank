using System;

namespace SageModeBank
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MAX_ACCOUNTS = 100;
            bool shouldExit = false;
            int totalRegisteredAccounts = 0;
            string tempUsername = string.Empty;
            string tempPassword = string.Empty;
            string[] usernames = new string[MAX_ACCOUNTS];
            string[] passwords = new string[MAX_ACCOUNTS];
            decimal[] balances = new decimal[MAX_ACCOUNTS];
            string[] ledger = new string[MAX_ACCOUNTS];

            while (!shouldExit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to SageModeBank");
                Console.Write("Press 1 to Register, 2 to Login, E to exit : ");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("[Registration]");
                        Console.Write("Please enter your username: ");
                        tempUsername = Console.ReadLine();
                        bool isExist = false;
                        // Search if username exist
                        for (int x = 0; x < MAX_ACCOUNTS; x++)
                        {
                            if (tempUsername == usernames[x])
                            {
                                isExist = true;
                                break;
                            }
                        }
                        // Check if the search found existing record
                        if (isExist)
                        {
                            Console.WriteLine("Username already exist...");
                            Console.ReadKey();
                            continue;
                        }

                        usernames[totalRegisteredAccounts] = tempUsername;
                        Console.Write("Please enter your password: ");
                        passwords[totalRegisteredAccounts] = Console.ReadLine();
                        ledger[totalRegisteredAccounts] = string.Empty;
                        totalRegisteredAccounts++;
                        Console.WriteLine($"Succesfully Registered Total: {totalRegisteredAccounts}, Please login!");
                        Console.ReadKey();

                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("[Login]");
                        Console.Write("Please enter your username: ");
                        tempUsername = Console.ReadLine();
                        Console.Write("Please enter your password: ");
                        tempPassword = Console.ReadLine();
                        bool isLogged = false;
                        bool shouldLogout = false;
                        int currentAccountIndex = -1;
                        for (int x = 0; x < MAX_ACCOUNTS; x++)
                        {
                            if (tempUsername == usernames[x] && tempPassword == passwords[x])
                            {
                                isLogged = true;
                                currentAccountIndex = x;
                            }
                        }

                        // Check if succesfully logged-in
                        if (!isLogged)
                        {
                            Console.WriteLine("Invalid Username or Password...");
                            Console.ReadKey();
                            continue;
                        }

                        while (!shouldLogout)
                        {
                            Console.Clear();
                            Console.WriteLine("[Dashboard]");
                            Console.WriteLine($"Balance: P {balances[currentAccountIndex]}");
                            Console.Write("Press 1 to Deposit, 2 to Withdraw, 3 to show transactions, 4 Transfer to others, E to logout : ");
                            ConsoleKeyInfo dashboardKey = Console.ReadKey();
                            Console.WriteLine();
                            switch (dashboardKey.KeyChar)
                            {

                                case '1':
                                    Console.Clear();
                                    Console.WriteLine("[Deposit]");
                                    Console.Write("Enter the amount to deposit: ");
                                    decimal dAmount = 0;
                                    if (decimal.TryParse(Console.ReadLine(), out dAmount))
                                    {
                                        balances[currentAccountIndex] += dAmount;
                                        ledger[currentAccountIndex] += $"DPS\t\t{DateTime.Now}\t\tP {dAmount}\tP {balances[currentAccountIndex]}\n";
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Amount!");
                                        Console.ReadKey();
                                    }
                                    break;
                                case '2':
                                    Console.Clear();
                                    Console.WriteLine("[withdraw]");
                                    Console.Write("Enter the amount to withdraw: ");
                                    decimal wAmount = 0;
                                    if (decimal.TryParse(Console.ReadLine(), out wAmount))
                                    {
                                        if (wAmount > balances[currentAccountIndex])
                                        {
                                            Console.WriteLine("Not enough funds!");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            balances[currentAccountIndex] -= wAmount;
                                            ledger[currentAccountIndex] += $"WTH\t\t{DateTime.Now}\t\tP {wAmount}\tP {balances[currentAccountIndex]}\n";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Amount!");
                                        Console.ReadKey();
                                    }
                                    break;
                                case '3':
                                    Console.Clear();
                                    Console.WriteLine("[Transactions]");
                                    Console.WriteLine("Action\t\tDate\t\t\tAmount\tBalance");
                                    Console.WriteLine(ledger[currentAccountIndex]);
                                    Console.ReadKey();
                                    break;
                                case '4':
                                    Console.Clear();
                                    Console.WriteLine("[Transfer to another account]");
                                    Console.Write("Enter Account Id: ");
                                    int receiverId = -1;
                                    if (int.TryParse(Console.ReadLine(), out receiverId))
                                    {
                                        if (receiverId >= 0 && receiverId < MAX_ACCOUNTS && usernames[receiverId] != null)
                                        {
                                            Console.Write("Enter the amount to transfer: ");
                                            decimal tAmount = 0;
                                            if (decimal.TryParse(Console.ReadLine(), out tAmount))
                                            {
                                                balances[currentAccountIndex] -= tAmount;
                                                ledger[currentAccountIndex] += $"TRO\t\t{DateTime.Now}\t\tP {tAmount}\tP {balances[currentAccountIndex]}\n";
                                                balances[receiverId] += tAmount;
                                                ledger[receiverId] += $"TRI\t\t{DateTime.Now}\t\tP {tAmount}\tP {balances[receiverId]}\n";
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid Amount!");
                                                Console.ReadKey();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid Account Id");
                                            Console.ReadKey();
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Account Id");
                                        Console.ReadKey();
                                    }
                                    break;
                                case 'e':
                                case 'E':
                                    shouldLogout = true;
                                    continue;
                            }
                        }
                        break;
                    case 'e':
                    case 'E':
                        Console.WriteLine("Good bye!!!");
                        shouldExit = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
