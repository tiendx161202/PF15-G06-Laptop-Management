using System;
using Persistance;
using BL;
using System.Collections.Generic;
using ConsoleTables;

namespace ConsolePL
{
    class Program
    {
        static Staff staff = new Staff();
        static void Main(string[] args)
        {
            Login();
        }
        private static void GetByLaptopIdOrder()
        {
            DateTime now = DateTime.Now;
            int _LaptopId = 0;
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
            LaptopBL lbl1 = new LaptopBL();
            Laptop laptop = new Laptop() { LaptopId = _LaptopId };
            laptop = lbl1.GetLaptop(laptop);
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            if (laptop.Status == Laptop.LaptopStatus.NOT_FOUND)
            {
                Console.WriteLine("Not found with id {0}", laptop.LaptopId);
            }
            else
            {
                Console.WriteLine(now);
                Console.WriteLine("Laptop Name :" + laptop.Name);
                Console.WriteLine("Price : " + string.Format(info, "{0:c}", laptop.Price));
                Console.WriteLine("Insurance:" + laptop.WarrantyPeriod);
                Console.WriteLine("Status: " + laptop.Status);
                Console.WriteLine("Amount: " + laptop.Stock);
            }
            Console.ReadKey();
        }

        private static void CreateInvoice()
        {
            CustomerBL cbl = new CustomerBL();
            LaptopBL lbl = new LaptopBL();
            InvoiceBL ibl = new InvoiceBL();

            Staff i_sale = null;
            Staff i_acccountant = null;

            if (staff.Role == staff.ROLE_SALE)
            {
                i_sale = staff;
            }
            if (staff.Role == staff.ROLE_SALE)
            {
                i_acccountant = staff;
            }

            Console.Write("Cus Name: ");
            string _name = Console.ReadLine();
            Console.Write("Cus Phone: ");
            string _phone = Console.ReadLine();
            Console.Write("Cus Add: ");
            string _add = Console.ReadLine();

            Customer i_customer = new Customer() { CustomerName = _name, CustomerPhone = _phone, CustomerAddress = _add };
            i_customer.CustomerId = cbl.AddCustomer(i_customer);

            // List<Laptop> i_listLaptop = new List<Laptop>();

            Laptop i_laptop = new Laptop();
            i_laptop.LaptopId = 1;

            // i_listLaptop.Add(lbl.GetLaptop(i_laptop));

            Invoice invoice = new Invoice() { InvoiceSale = i_sale, InvoiceAccountant = i_acccountant, InvoiceCustomer = i_customer, InvoiceDate = DateTime.Now};
            invoice.LaptopList.Add(lbl.GetLaptop(i_laptop));

            Console.WriteLine(invoice.InvoiceDate);

            // int result = ibl.CreateInvoice(invoice);
            bool result = ibl.CreateInvoice(invoice);

            Console.WriteLine(result);


            if (result)
            {
                Console.WriteLine("Create Complete!");
            }
            else
            {
                Console.WriteLine("Create Fail!");

            }

            Console.ReadKey();
        }

        private static void AddCustomer()
        {
            CustomerBL cbl = new CustomerBL();
            Console.Write("Name: ");
            string _name = Console.ReadLine();
            Console.Write("Add: ");
            string _add = Console.ReadLine();
            Console.Write("Phone: ");
            string _phone = Console.ReadLine();

            Customer customer = new Customer() { CustomerName = _name, CustomerAddress = _add, CustomerPhone = _phone };
            int? _id = cbl.AddCustomer(customer);
            customer.CustomerId = _id;

            Console.WriteLine("Add customer complete, id is: {0}", _id);
            Console.ReadKey();
        }

        private static void GetCustomer()
        {
            CustomerBL cbl = new CustomerBL();
            Console.Write("Id: ");
            int id = Convert.ToInt16(Console.ReadLine());
            Customer customer = new Customer() { CustomerId = id };

            customer = cbl.GetCustomerById(customer);

            if (customer == null)
            {
                Console.WriteLine("Not Found!");
            }
            else
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine(customer.CustomerId);
                Console.WriteLine(customer.CustomerName);
                Console.WriteLine(customer.CustomerPhone);
                Console.WriteLine(customer.CustomerAddress);
            }
            Console.ReadKey();

        }

        private static int DisplayLaptopList(List<Laptop> LaptopList, Laptop laptop)
        {
            if (LaptopList == null)
            {
                return 0;
            }

            IFormatProvider info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Display Vietnamese language
            var table = new ConsoleTable("ID", "NAME", "CPU", "RAM", "PRICE");

            string FormatText(string text, int width)
            {
                return text.Length > width ? text.Substring(0, width - 3) + "..." : text;
            }

            int count = 0;
            foreach (Laptop lt in LaptopList)
            {
                ++count;
            }
            if (count == 0)
            {
                return count;
            }
            else
            {
                Console.Clear();
                foreach (Laptop lt in LaptopList)
                {
                    table.AddRow(lt.LaptopId.ToString(), FormatText(lt.Name, 30), lt.Cpu, FormatText(lt.Ram, 16), string.Format(info, "{0:c}", lt.Price));
                }
                table.Write(ConsoleTables.Format.Alternative);
            }
            return count;
        }

        private static void InvoiceMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("==========================================");
                Console.WriteLine("|         Order Management System         ");
                Console.WriteLine("==========================================");
                Console.WriteLine("|1.LAPTOP MANAGEMENT                      |");
                Console.WriteLine("|2.ADD CUSTOMER                           |");
                Console.WriteLine("|3.CREATE ORDER                           |");
                Console.WriteLine("|4.EXIT                                   |");
                Console.WriteLine("==========================================");
                choice = CheckChoice(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        int choisse;
                        Console.WriteLine("Laptop Management");
                        Console.WriteLine("1. Get By LaptopId");
                        Console.WriteLine("2. Get All Laptop");
                        Console.WriteLine("3. Search By LaptopName");
                        Console.WriteLine("4. Exit");
                        choisse = CheckChoice(Console.ReadLine());
                        switch (choisse)
                        {
                            case 1:
                                GetByLaptopIdOrder();
                                break;
                            case 2:
                                ShowAll();
                                break;
                            case 3:
                                SearchName();
                                break;
                            case 4:
                                SaleMenu();
                                break;
                        }

                        break;
                    case 2:
                        break;
                    case 3:
                        CreateInvoice();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;

                }

            } while (true);

        }

        private static int IntputMaxPrice(LaptopBL lbl, decimal _minPrice)
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
            decimal _minPrice = 0;
            decimal _maxPrice = 0;
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");

            LaptopBL lbl = new LaptopBL();

            _minPrice = IntputMinPrice(lbl);
            _maxPrice = IntputMaxPrice(lbl, _minPrice);

            Laptop laptop = new Laptop() { minPrice = _minPrice, maxPrice = _maxPrice };
            List<Laptop> LaptopList = lbl.GetLaptopByPrice(laptop);

            int count = DisplayLaptopList(LaptopList, laptop);

            if (count == 0)
            {
                laptop = null;
                Console.WriteLine("Not found anything in with price range Min: {0} - Max: {1}\n", string.Format(info, "{0:c}", _minPrice), string.Format(info, "{0:c}", _maxPrice));
            }
            else
            {
                Console.WriteLine("Found {0} result for price range Min: {1} - Max: {2}\n", count, string.Format(info, "{0:c}", _minPrice), string.Format(info, "{0:c}", _maxPrice));
            }

            Console.ReadKey();

        }

        private static void ShowAll()
        {
            LaptopBL lbl = new LaptopBL();
            Laptop laptop = new Laptop() { };
            List<Laptop> LaptopList = lbl.GetAllLaptop(laptop);
            // IFormatProvider info;

            Console.Clear();
            Console.WriteLine("\nALL LAPTOP\n");

            int count = DisplayLaptopList(LaptopList, laptop);

            if (count == 0)
            {
                Console.WriteLine("List laptop is empty, input laptop to your Database!");
            }
            else
            {
                Console.WriteLine("Has {0} result ", count);
            }

            Console.ReadKey();
        }

        private static string InputNameSearch(LaptopBL lbl)
        {
            string ErrorMessage;
            string Name;

            do
            {
                Console.Write("\nInput your search: ");
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

            try
            {
                _name = InputNameSearch(lbl);
                Laptop laptop = new Laptop() { Name = _name };
                List<Laptop> LaptopList = lbl.GetLaptopByName(laptop);

                int count = DisplayLaptopList(LaptopList, laptop);

                if (count == 0)
                {
                    laptop = null;
                    Console.WriteLine("Not found anything in with \"{0}\"", _name);
                }
                else
                {
                    Console.WriteLine("Has {0} result ", count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

            if (laptop == null)
            {
                Console.WriteLine("Not found with id {0}", _LaptopId);
            }
            else
            {
                DisplayLaptopInfo(laptop);
            }
            Console.ReadKey();

        }

        private static void DisplayLaptopInfo(Laptop laptop)
        {
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");

            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("==================================");
            Console.WriteLine("|        Laptop information       |");
            Console.WriteLine("==================================");
            Console.WriteLine(" ID             : " + laptop.LaptopId);
            Console.WriteLine(" Brand          : " + laptop.BrandName);
            Console.WriteLine(" Name           : " + laptop.Name);
            Console.WriteLine(String.Format(info, " Price          : {0:c}", laptop.Price));
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
                        InvoiceMenu();
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
            // Console.WriteLine(new string('=', 36));
            // Console.WriteLine("{0}");
            // Console.WriteLine(new string('=', 36));

            Console.WriteLine("=================================");
            Console.WriteLine("|       LAPTOP MANAGEMENT       |");
            Console.WriteLine("|       Login                   |");
            Console.WriteLine("=================================");

            string _UserName = InputUserName(sbl);
            string _Password = InputPassword(sbl);

            staff.UserName = _UserName; staff.Password = _Password;
            staff = sbl.Login(staff);

            if (staff == null)
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
