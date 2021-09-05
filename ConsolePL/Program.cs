using System;
using Persistance;
using BL;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            SearchName();
            Login();
        }

        private static void SearchName()
        {
            string _name = "";
            LaptopBL lbl = new LaptopBL();

            Console.Write("Input your search: ");
            _name = Console.ReadLine();

            Laptop laptop = new Laptop() { Name = _name };
            List<Laptop> LaptopList = new List<Laptop>();
            LaptopList = lbl.GetLaptopByName(laptop);

            Console.WriteLine("=================================================================================================");
            Console.WriteLine("|                                       SUCCESSFUL SEARCH                                       |");
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("|ID | NAME\t      | PRICE\t      | CPU\t   |RAM\t|  GRAPHICSCARAD \t| HARDDISK\t| MONITOR\t|OS\t|");
            Console.WriteLine("=================================================================================================");


            foreach (Laptop lt in LaptopList)
            {
                // if(string.Compare(lt.Name, _name) == 0)
                // {
                Console.WriteLine("|{0}| {1} | {2}| {3} | {4} | {5} |{6} |{7}| {8} | {9} |", lt.LaptopId, lt.Name, lt.Price, lt.Cpu, lt.Ram, lt.GraphicsCard, lt.HardDisk, lt.Monitor, lt.Os);
                Console.WriteLine("=================================================================================================");

                //Console.WriteLine(LaptopList.IndexOf(lt));
                //DisplayLaptopInfo(lt);
                // }
            }
            Console.ReadKey();

        }

        private static void Login()
        {
            StaffBL sbl = new StaffBL();

            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("|        LOGIN        |");
            Console.WriteLine("=======================");

            string UserName = InputUserName(sbl);
            string Password = InputPassword(sbl);

            Staff staff = new Staff() { UserName = UserName, Password = Password };
            staff = sbl.Login(staff);

            if (staff.Role == staff.FAIL_LOGIN)
            {
                Console.WriteLine("User Name or Password is wrong!\nPress EnterKey to re-login or any key to Exit!");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Login();
                }
                Environment.Exit(0);
            }
            else
            {
                if (staff.Role == staff.ROLE_SALE)
                {
                    SaleMenu();
                }
                else if (staff.Role == staff.ROLE_ACCOUNTANT)
                {
                    AccountantMenu();
                }
            }
        }

        private static void SearchId()
        {
            int _LaptopId = 0;

            LaptopBL lbl = new LaptopBL();

            do
            {
                Console.Write("Input your Laptop ID: ");
                _LaptopId = CheckChoice(Console.ReadLine());
                if (_LaptopId == 0)
                {
                    Console.WriteLine("Your Laptop ID is invalid, re-input ...");
                }
            }
            while (_LaptopId == 0);

            Laptop laptop = new Laptop() { LaptopId = _LaptopId };
            laptop = lbl.GetLaptop(laptop);

            if (laptop.Status == Laptop.LaptopStatus.ID_NOT_FOUND)
            {
                Console.WriteLine("Not found");
            }
            else
            {
                DisplayLaptopInfo(laptop);
            }

        }

        private static void DisplayLaptopInfo(Laptop laptop)
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("==================================");
            Console.WriteLine("|        Laptop information       |");
            Console.WriteLine("==================================");
            Console.WriteLine(" ID             : " + laptop.LaptopId);
            Console.WriteLine(" Brand          : " + laptop.BrandName);
            Console.WriteLine(" Name           : " + laptop.Name);
            Console.WriteLine(" Price          : " + laptop.Price);
            Console.WriteLine(" CPU            : " + laptop.Cpu);
            Console.WriteLine(" RAM            : " + laptop.Ram);
            Console.WriteLine(" Hard Disk      : " + laptop.HardDisk);
            Console.WriteLine(" Monitor        : " + laptop.Monitor);
            Console.WriteLine(" Graphics Card  : " + laptop.GraphicsCard);
            Console.WriteLine(" Jack           : " + laptop.Jack);
            Console.WriteLine(" OS             : " + laptop.Os);
            Console.WriteLine(" Battery        : " + laptop.Battery);
            Console.WriteLine(" Weight         : " + laptop.Weight);
            Console.WriteLine(" Warranty Period: " + laptop.WarrantyPeriod);

            string status = "";
            if (laptop.Status == Laptop.LaptopStatus.ACTIVE)
            {
                status = "Active";
            }
            else if (laptop.Status == Laptop.LaptopStatus.NOT_ACTIVE)
            {
                status = "Inactive";
            }
            Console.WriteLine(" Status         : " + status);
            Console.ReadKey();

        }

        private static void SearchMenu()
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
                Console.WriteLine("|4. Back to \"Sale Menu\"        |");
                Console.WriteLine("==================================");
                Console.Write("# YOUR CHOICE: ");
                choice = CheckChoice(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        SearchId();
                        break;
                    case 2:
                        SearchName();
                        break;
                    case 3:
                        // Console.Clear();
                        // Console.WriteLine("==================================");
                        // Console.WriteLine(" SEARCH PRICE:");
                        Console.ReadLine();
                        break;
                    case 4:
                        SaleMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid! Please input 1 - 4");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        break;
                }
            } while (choice != 4);
        }

        private static void AccountantMenu()
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

        private static void SaleMenu()
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
                Console.WriteLine("3. LOGOUT");
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
                        Login();
                        break;
                    default:
                        Console.WriteLine("Invalid! Please input 1 - 3");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        break;
                }
            } while (choice != 3);
        }

        private static string InputUserName(StaffBL sbl)
        {
            string ErrorMessage;
            string userName;
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

            return userName;
        }

        private static string InputPassword(StaffBL sbl)
        {
            string Password;
            string ErrorMessage;

            do
            {
                Console.Write("Password: ");
                Password = HidePassword();
                sbl.ValidatePassword(Password, out ErrorMessage);

                if (ErrorMessage != null)
                {
                    Console.WriteLine("\n" + ErrorMessage);
                }
                Console.WriteLine();
            } while (sbl.ValidatePassword(Password, out ErrorMessage) == false);

            return Password;
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

        private static string HidePassword()
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
