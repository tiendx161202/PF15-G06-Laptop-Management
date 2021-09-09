using System;
using Persistance;
using BL;
using System.Collections.Generic;
using ConsoleTables;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            Login();
        }

        private static int IntputMaxPrice(LaptopBL lbl, int _minPrice)
        {
            int _maxPrice = 0;
            bool pass = true;
            string ErrorMessage = string.Empty;

            do
            {
                Console.Write("\nInput max price: ");
                string sprice = Console.ReadLine();
                if (string.IsNullOrEmpty(sprice) || string.IsNullOrWhiteSpace(sprice))
                {
                    Console.WriteLine(" Max price should not be empty or whitespace!");
                    continue;
                }
                else
                {
                    _maxPrice = Convert.ToInt32(sprice);
                }

                pass = lbl.ValidateMaxPrice(_minPrice, _maxPrice, out ErrorMessage);

                if (ErrorMessage != null)
                {
                    Console.WriteLine(ErrorMessage);
                }

            } while (!pass);

            return _maxPrice;
        }

        private static int IntputMinPrice(LaptopBL lbl)
        {
            int _minPrice = 0;
            bool pass = true;
            string ErrorMessage = string.Empty;

            do
            {
                Console.Write("\nInput min price: ");
                string sprice = Console.ReadLine();
                if (string.IsNullOrEmpty(sprice) || string.IsNullOrWhiteSpace(sprice))
                {
                    Console.WriteLine(" Min price should not be empty or whitespace!");
                    continue;
                }
                else
                {
                    _minPrice = Convert.ToInt32(sprice);
                }

                pass = lbl.ValidateMinPrice(_minPrice, out ErrorMessage);

                if (ErrorMessage != null)
                {
                    Console.WriteLine(ErrorMessage);
                }

            } while (!pass);

            return _minPrice;
        }

        private static void SearchPrice()
        {
            int _minPrice = 0;
            int _maxPrice = 0;

            LaptopBL lbl = new LaptopBL();
            List<Laptop> LaptopList = new List<Laptop>();

            _minPrice = IntputMinPrice(lbl);
            _maxPrice = IntputMaxPrice(lbl, _minPrice);

            Laptop laptop = new Laptop() { minPrice = _minPrice, maxPrice = _maxPrice };
            LaptopList = lbl.GetLaptopByPrice(laptop);

            Console.Clear();
            Console.WriteLine("\nResult for price range Min: {0} - Max: {1}\n", laptop.minPrice, laptop.maxPrice);
            var table = new ConsoleTable("ID", "NAME", "CPU", "RAM", "PRICE");

            foreach (Laptop lt in LaptopList)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8; // Display Vietnamese language
                table.AddRow(lt.LaptopId.ToString(), lt.Name, lt.Cpu, lt.Ram, lt.Price.ToString());

                //Console.WriteLine(LaptopList.IndexOf(lt));
                //DisplayLaptopInfo(lt);
            }
            table.Write(ConsoleTables.Format.Alternative);
            Console.ReadKey();
        }

        private static void ShowAll()
        {
            LaptopBL lbl = new LaptopBL();
            Laptop laptop = new Laptop() { };
            List<Laptop> LaptopList = new List<Laptop>();

            LaptopList = lbl.GetAllLaptop(laptop);

            Console.Clear();
            Console.WriteLine("\nALL LAPTOP\n");
            var table = new ConsoleTable("ID", "NAME", "CPU", "RAM", "PRICE");

            foreach (Laptop lt in LaptopList)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8; // Display Vietnamese language
                // string sts;
                // if (lt.Status == Laptop.LaptopStatus.ACTIVE)
                // {
                //     sts = "ACTIVE";
                // }
                // else
                // {
                //     sts = "NOT ACTIVE";
                // }
                table.AddRow(lt.LaptopId.ToString(), lt.Name, lt.Cpu, lt.Ram, lt.Price.ToString());
            }
            table.Write(ConsoleTables.Format.Alternative);
            Console.ReadKey();
        }

        private static string InputNameSearch(LaptopBL lbl)
        {
            string ErrorMessage;
            string Name;

            do
            {
                Console.Write("\nName: ");
                Name = Console.ReadLine();
                lbl.ValidateName(Name, out ErrorMessage);

                if (ErrorMessage != null)
                {
                    Console.WriteLine(ErrorMessage);
                }
            }
            while (lbl.ValidateName(Name, out ErrorMessage) == false);

            return Name;
        }

        private static void SearchName()
        {
            string _name = string.Empty;
            LaptopBL lbl = new LaptopBL();
            List<Laptop> LaptopList = new List<Laptop>();

            try
            {
                Console.Write("Input your search: ");
                _name = InputNameSearch(lbl);

                Laptop laptop = new Laptop();

                // if (_name != null)
                laptop.Name = _name;
                Console.WriteLine("{0}", laptop.Name);

                LaptopList = lbl.GetLaptopByName(laptop);

                Console.Clear();
                Console.WriteLine("\nResult for: \"{0}\"\n", _name);
                var table = new ConsoleTable("ID", "NAME", "CPU", "RAM", "PRICE");

                foreach (Laptop lt in LaptopList)
                {
                    Console.OutputEncoding = System.Text.Encoding.UTF8; // Display Vietnamese language
                    table.AddRow(lt.LaptopId.ToString(), lt.Name, lt.Cpu, lt.Ram, lt.Price.ToString());
                }
                table.Write(ConsoleTables.Format.Alternative);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            Console.ReadKey();
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

            if (laptop.Status == Laptop.LaptopStatus.NOT_FOUND)
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

            // string status = "";
            // if (laptop.Status == Laptop.LaptopStatus.ACTIVE)
            // {
            //     status = "Active";
            // }
            // else if (laptop.Status == Laptop.LaptopStatus.NOT_ACTIVE)
            // {
            //     status = "Inactive";
            // }
            // Console.WriteLine(" Status         : " + status);
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
                Console.WriteLine("|4. SHOW ALL                     |");
                Console.WriteLine("|5. Back to \"Sale Menu\"          |");
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
                        SearchPrice();
                        break;
                    case 4:
                        ShowAll();
                        break;
                    case 5:
                        SaleMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid! Please input 1 - 5");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        break;
                }
            } while (choice != 5);
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
