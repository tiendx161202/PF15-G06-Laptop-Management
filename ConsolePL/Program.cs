using System;
using Persistance;
using BL;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            StaffBL sbl = new StaffBL();
            string userName;
            string pass;
            string ErrorMessage;

            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("|        LOGIN        |");
            Console.WriteLine("=======================");

            do
            {
                Console.Write("\nUser Name: ");
                userName = Console.ReadLine();
                sbl.ValidateUserName(userName, out ErrorMessage);

                if (ErrorMessage != null)
                {
                    Console.WriteLine(ErrorMessage);
                }
            }
            while (sbl.ValidateUserName(userName, out ErrorMessage) == false);

            do
            {
                Console.Write("Password: ");
                pass = GetPassword();
                sbl.ValidatePassword(pass, out ErrorMessage);

                if (ErrorMessage != null)
                {
                    Console.WriteLine("\n" + ErrorMessage);
                }
                Console.WriteLine();
            } while (sbl.ValidatePassword(pass, out ErrorMessage) == false);

            Staff staff = new Staff() { UserName = userName, Password = pass };
            staff = sbl.Login(staff);

            if (staff.Role == staff.FAIL_LOGIN)
            {
                Console.WriteLine("User Name or Password is wrong!\nPress EnterKey to re-login or any key to Exit!");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Main(args);
                }
                Environment.Exit(0);
            }
            else
            {
                if (staff.Role == staff.ROLE_SALE)
                {
                    int choice;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("==================================");
                        Console.WriteLine("|          LAPTOP SHOP T&G       |");
                        Console.WriteLine("|            Sale Menu           |");
                        Console.WriteLine("==================================");
                        Console.WriteLine("1. SEARCH LAPTOP ");
                        Console.WriteLine("2. CREATE ORDER");
                        Console.WriteLine("3. EXIT");
                        Console.WriteLine("==================================");
                        Console.Write("# YOUR CHOICE: ");

                        choice = CheckChoice(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                SearchMenu();
                                break;
                            case 2:
                                break;
                            case 3:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid! Please input 1 - 3");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadLine();
                                break;
                        }
                    } while (choice != 3);
                }
                else if (staff.Role == staff.ROLE_ACCOUNTANT)
                {
                    int choice;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("==================================");
                        Console.WriteLine("|        LAPTOP SHOP T&G         |");
                        Console.WriteLine("|        Accountant Menu         |");
                        Console.WriteLine("==================================");
                        Console.WriteLine("1. PAY ");
                        Console.WriteLine("2. EXIT");
                        Console.WriteLine("==================================");
                        Console.WriteLine(" # YOUR CHOICE: ");
                        choice = CheckChoice(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                break;
                            case 2:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid! Please input 1 or 2");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadLine();
                                break;
                        }
                    } while (choice != 2);
                }
            }
        }

        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }
        static void SearchMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine("|            SEARCH              |");
                Console.WriteLine("|1. SEARCH ID                    |");
                Console.WriteLine("|2. SEARCH NAME                  |");
                Console.WriteLine("|3. SEARCH PRICE                 |");
                Console.WriteLine("|4. Exit                          |");
                Console.WriteLine("==================================");
                Console.WriteLine("# YOUR CHOICE: ");
                choice = CheckChoice(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                    //     Console.Clear();
                    //     Console.WriteLine("==================================");
                    //     Console.WriteLine(" SEARCH ID:");
                    //     Console.ReadLine();
                        break;
                    case 2:
                        // Console.Clear();
                        // Console.WriteLine("==================================");
                        // Console.WriteLine(" SEARCH NAME:");
                        // Console.ReadLine();
                        break;
                    case 3:
                        // Console.Clear();
                        // Console.WriteLine("==================================");
                        // Console.WriteLine(" SEARCH PRICE:");
                        Console.ReadLine();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid! Please input 1 - 4");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        break;
                }
            } while (choice != 4);
        }

        private static int CheckChoice(string choice)
        {
            // catch if not a number

            int menuChoice = 0;

            try
            {
                menuChoice = Convert.ToInt32(choice);
            }
            catch
            {
                return menuChoice;
            }
            return menuChoice;
        }

    }
}
