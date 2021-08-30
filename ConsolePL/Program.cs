using System;
using Persistance;
using BL;


namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("|        LOGIN        |");
            Console.WriteLine("=======================");
            Console.Write("User Name: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string pass = GetPassword();
            Console.WriteLine();

            //valid username password here
            Staff staff = new Staff() { UserName = userName, Password = pass };
            StaffBL sbl = new StaffBL();
            staff = sbl.Login(staff);

            if (staff == null)
            {
                Console.WriteLine("Can't Login");
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
                        Console.WriteLine("1. SEARCH LATOP ");
                        Console.WriteLine("2. CREATE ORDER");
                        Console.WriteLine("3. EXIT");
                        Console.WriteLine("==================================");
                        Console.Write("# YOUR CHOICE: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid!!!");
                                Console.WriteLine("please choose again from 1 - 3");
                                Console.ReadLine();
                                break;
                        }
                    } while (choice != 3);
                }
                else if (staff.Role == staff.ROLE_ACCOUNTANT)
                {
                    int choices;
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
                        Console.WriteLine(" # YOUR CHOICES: ");
                        choices = Convert.ToInt32(Console.ReadLine());
                        switch (choices)
                        {
                            case 1:
                                break;
                            case 2:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid!!!");
                                Console.WriteLine("please choose again from 1 - 2");
                                Console.ReadLine();
                                break;
                        }
                    } while (choices != 2);
                }
                else
                {
                    Console.WriteLine("Invalid!!");
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

    }
}
