using System;
using Persistance;
using BL;
using System.Collections.Generic;
using ConsoleTables;
using System.Linq;

namespace ConsolePL
{
    class Program
    {
        static Staff staff = new Staff();
        static void Main(string[] args)
        {
            Login();
        }

        private static void InvoiceMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine("|        LAPTOP SHOP T&G         |");
                Console.WriteLine("|        Invoice Menu            |");
                Console.WriteLine("==================================");
                Console.WriteLine("|1. CREATE NEW INVOICE           |");
                Console.WriteLine("|2. UPDATE INVOICE               |");
                Console.WriteLine("|3. Back to \"Sale Menu\"           |");
                Console.WriteLine("==================================");
                Console.Write(" # YOUR CHOICE: ");
                choice = SupProgram.CheckChoice(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        SupProgram.CreateNewInvoice();
                        break;
                    case 2:
                        break;
                    case 3:
                        SaleMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid! Please input 1 - 3");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        break;
                }
            } while (true);

        }

        private static void SearchMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine("|        LAPTOP SHOP T&G         |");
                Console.WriteLine("|        Search Menu             |");
                Console.WriteLine("==================================");
                Console.WriteLine("|1. SEARCH ID                    |");
                Console.WriteLine("|2. SEARCH NAME                  |");
                Console.WriteLine("|3. SEARCH PRICE                 |");
                Console.WriteLine("|4. SHOW ALL                     |");
                Console.WriteLine("|5. Back to \"Sale Menu\"          |");
                Console.WriteLine("==================================");
                Console.Write("# YOUR CHOICE: ");
                choice = SupProgram.CheckChoice(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        SupProgram.SearchId();
                        break;
                    case 2:
                        SupProgram.SearchName();
                        break;
                    case 3:
                        SupProgram.SearchPrice();
                        break;
                    case 4:
                        SupProgram.ShowAll();
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
                choice = SupProgram.CheckChoice(Console.ReadLine());

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
                Console.WriteLine("|         LAPTOP SHOP T&G        |");
                Console.WriteLine("|         Sale Menu              |");
                Console.WriteLine("==================================");
                Console.WriteLine("|1. SEARCH LAPTOP                |");
                Console.WriteLine("|2. CREATE ORDER                 |");
                Console.WriteLine("|3. LOGOUT                       |");
                Console.WriteLine("==================================");
                Console.Write("# YOUR CHOICE: ");

                choice = SupProgram.CheckChoice(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        SearchMenu();
                        break;
                    case 2:
                        InvoiceMenu();
                        break;
                    case 3:
                        staff = null;
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
                staff.UserName = null; staff.Password = null;
                if (staff.Role == staff.ROLE_SALE)
                {
                    SaleMenu();
                }
                else if (staff.Role == staff.ROLE_ACCOUNTANT)
                {
                    AccountantMenu();
                }
            }

            static string InputUserName(StaffBL sbl)
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

            static string InputPassword(StaffBL sbl)
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

            static string HidePassword()
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

        internal static class SupProgram
        {
            internal static void GetByLaptopIdOrder()
            {
                DateTime now = DateTime.Now;
                int _LaptopId = 0;
                do
                {
                    Console.Write("Input your Laptop ID: ");
                    _LaptopId = SupProgram.CheckChoice(Console.ReadLine());
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

            internal static void ChoiceAfterShowList(List<Laptop> Pglist)
            {
                int idChoice;
                bool ans = false;

                do
                {
                    Console.Write("Input ID Laptop to show details: ");
                    idChoice = SupProgram.CheckChoice(Console.ReadLine());

                    if (idChoice == 0)
                    {
                        Console.WriteLine("Error ID, re input ...");
                    }
                    else if (idChoice != 0)
                    {
                        foreach (var lap in Pglist)
                        {
                            if (lap.LaptopId == idChoice)
                            {
                                ans = true;
                                break;
                            }
                            else
                            {
                                ans = false;
                            }
                        }

                        if (ans)
                        {
                            Laptop lt = new Laptop() { LaptopId = idChoice };
                            int index = Pglist.IndexOf(lt);
                            SupProgram.DisplayLaptopInfo(Pglist[index]);
                        }
                        else
                        {
                            Console.WriteLine("Error not exists in page, re input ...");
                        }
                    }
                } while (idChoice == 0);


            }

            internal static void GotoPage(List<Laptop> LaptopList, Laptop laptop, string resString)
            {
                int page = 1;
                int count = LaptopList.Count;
                double resPage = count % 10 > 0 ? count / 10 + 1 : count / 10;

                List<Laptop> Pglist = null;

                if (resPage == 1)
                {
                    Pglist = SupProgram.DisplayLaptopList(LaptopList, laptop, page, resString);
                    Console.WriteLine(" PAGE {0}", page);
                }
                else if (resPage > 1)
                {
                    do
                    {
                        Pglist = SupProgram.DisplayLaptopList(LaptopList, laptop, page, resString);
                        Console.WriteLine(" PAGE {0}", page);
                        do
                        {
                            Console.Write("Input 0 to exit next page or your page choice: ");
                            page = SupProgram.CheckChoice(Console.ReadLine());
                        } while (page > resPage || page < 0);
                    } while (page != 0);

                }
                ChoiceAfterShowList(Pglist);
            }

            internal static void CreateNewInvoice()
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

                Invoice invoice = new Invoice() { InvoiceSale = i_sale, InvoiceAccountant = i_acccountant, InvoiceCustomer = i_customer, InvoiceDate = DateTime.Now };

                // for (int i = 1; i < 23; ++i)
                // {

                Laptop i_laptop = new Laptop() { LaptopId = 25 };
                i_laptop = lbl.GetLaptop(i_laptop);
                i_laptop.Quanity = 2;
                invoice.LaptopList.Add(i_laptop);

                // }
                Invoice invoice1;
                bool result = ibl.CreateInvoice(invoice, out invoice1);

                if (result)
                {
                    Console.WriteLine("Create Complete! Your Invoice No. is " + invoice1.InvoiceNo);
                }
                else
                {
                    Console.WriteLine("Create Fail!");
                }

                // Console.WriteLine(invoice1.InvoiceCustomer.CustomerName);
                // Console.WriteLine(invoice1.InvoiceSale.Name);

                Console.ReadKey();
            }

            internal static void AddCustomer()
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

            internal static void GetCustomer()
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

            internal static List<Laptop> DisplayLaptopList(List<Laptop> LaptopList, Laptop laptop, int pageNum, string resString)
            {
                if (LaptopList == null)
                {
                    return null;
                }

                List<Laptop> PgLst = null;
                List<Laptop> GetPage(List<Laptop> PageList, int pageNum, int PageSize = 10)
                {
                    return PageList.Skip(pageNum * PageSize).Take(PageSize).ToList();
                }

                IFormatProvider info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                Console.OutputEncoding = System.Text.Encoding.UTF8; // Display Vietnamese language
                var table = new ConsoleTable("ID", "NAME", "CPU", "RAM", "PRICE");

                string FormatText(string text, int width)
                {
                    return text.Length > width ? text.Substring(0, width - 3) + "..." : text;
                }

                int count = LaptopList.Count;
                if (count == 0)
                {
                    return null;
                }
                else
                {
                    Console.Clear();
                    PgLst = GetPage(LaptopList, pageNum - 1);
                    foreach (Laptop lt in PgLst)
                    {
                        table.AddRow(lt.LaptopId.ToString(), FormatText(lt.Name, 30), lt.Cpu, FormatText(lt.Ram, 16), string.Format(info, "{0:c}", lt.Price));
                    }

                    double resPage = count % 10 > 0 ? count / 10 + 1 : count / 10;
                    Console.WriteLine("Has {0} page with {1} result " + resString, resPage, count);
                    table.Write(ConsoleTables.Format.Alternative);
                }
                return PgLst;
            }

            internal static void ShowAll()
            {
                LaptopBL lbl = new LaptopBL();
                Laptop laptop = new Laptop() { };
                List<Laptop> LaptopList = lbl.GetAllLaptop(laptop);
                // IFormatProvider info;

                Console.Clear();
                Console.WriteLine("\nALL LAPTOP\n");

                int count = LaptopList.Count;
                if (count == 0)
                {
                    Console.WriteLine("List laptop is empty, input laptop to your Database!");
                }
                else
                {
                    string resString = @"for all laptop!";
                    GotoPage(LaptopList, laptop, resString);
                }

                Console.ReadKey();
            }

            internal static void SearchPrice()
            {
                decimal _minPrice = 0;
                decimal _maxPrice = 0;
                // string ErrorMessage string.Empty;
                // bool pass = true;
                var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");

                LaptopBL lbl = new LaptopBL();

                _minPrice = IntputMinPrice(lbl);
                _maxPrice = IntputMaxPrice(lbl, _minPrice);


                Laptop laptop = new Laptop() { minPrice = _minPrice, maxPrice = _maxPrice };
                List<Laptop> LaptopList = lbl.GetLaptopByPrice(laptop);

                int count = LaptopList.Count;

                if (count == 0)
                {
                    laptop = null;
                    Console.WriteLine("Not found anything in with price range Min: {0} - Max: {1}\n", string.Format(info, "{0:c}", _minPrice), string.Format(info, "{0:c}", _maxPrice));
                }
                else
                {
                    // int pageNum = 0;
                    string resString = "for price range Min: " + _minPrice + " - Max: " + _maxPrice + " !";
                    // Console.WriteLine("Found {0} result for price range Min: {1} - Max: {2}\n", count, string.Format(info, "{0:c}", _minPrice), string.Format(info, "{0:c}", _maxPrice));
                    // DisplayLaptopList(LaptopList, laptop, pageNum, resString);
                    GotoPage(LaptopList, laptop, resString);
                }

                Console.ReadKey();

                static decimal IntputMaxPrice(LaptopBL lbl, decimal _minPrice)
                {
                    decimal d_maxPrice = decimal.Zero;
                    string s_maxPrice = string.Empty;
                    bool pass = true;
                    string ErrorMessage = string.Empty;

                    do
                    {
                        Console.Write("\nInput max price: ");
                        s_maxPrice = Console.ReadLine();

                        pass = lbl.ValidateMaxPrice(_minPrice, s_maxPrice, out d_maxPrice, out ErrorMessage);

                        if (ErrorMessage != null)
                        {
                            Console.WriteLine(ErrorMessage);
                        }

                    } while (!pass);

                    return d_maxPrice;
                }

                static decimal IntputMinPrice(LaptopBL lbl)
                {
                    decimal d_minPrice = decimal.Zero;
                    string s_minPrice;
                    bool pass = true;
                    string ErrorMessage = string.Empty;

                    do
                    {
                        Console.Write("\nInput min price: ");
                        s_minPrice = Console.ReadLine();

                        pass = lbl.ValidateMinPrice(s_minPrice, out d_minPrice, out ErrorMessage);

                        if (ErrorMessage != null)
                        {
                            Console.WriteLine(ErrorMessage);
                        }
                    } while (!pass);

                    return d_minPrice;
                }

            }

            internal static void SearchName()
            {
                string _name = string.Empty;
                LaptopBL lbl = new LaptopBL();
                string ErrorMessage;

                try
                {
                    do
                    {
                        Console.Write("\nInput your search: ");
                        _name = Console.ReadLine();
                        lbl.ValidateName(_name, out ErrorMessage);

                        if (ErrorMessage != null)
                        {
                            Console.WriteLine(ErrorMessage);
                        }
                    }
                    while (lbl.ValidateName(_name, out ErrorMessage) == false);

                    Laptop laptop = new Laptop() { Name = _name };
                    List<Laptop> LaptopList = lbl.GetLaptopByName(laptop);

                    int count = LaptopList.Count;


                    if (count == 0)
                    {
                        laptop = null;
                        Console.WriteLine("Not found anything in with \"{0}\"", _name);
                    }
                    else
                    {
                        string resString = "for \"" + _name + "\" !";
                        GotoPage(LaptopList, laptop, resString);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
            }

            internal static void SearchId()
            {
                int _LaptopId = 0;

                LaptopBL lbl = new LaptopBL();

                do
                {
                    Console.Write("Input your Laptop ID: ");
                    _LaptopId = SupProgram.CheckChoice(Console.ReadLine());
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
                    SupProgram.DisplayLaptopInfo(laptop);
                }
                Console.ReadKey();

            }

            internal static void DisplayLaptopInfo(Laptop laptop)
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

            internal static int CheckChoice(string choice)
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

}
