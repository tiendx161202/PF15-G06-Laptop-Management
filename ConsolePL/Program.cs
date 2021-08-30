using System;
using Persistance;
using BL;


namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("User Name: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string pass = GetPassword();
            Console.WriteLine();



            //valid username password here
            Staff staff = new Staff() { UserName = userName, Password = pass };
            StaffBL sbl = new StaffBL();
            staff  = sbl.Login(staff);

            if (staff == null)
            {
                Console.WriteLine("Can't Login");
            }
            else
            {
                Console.WriteLine("Wellcome to System...");
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
