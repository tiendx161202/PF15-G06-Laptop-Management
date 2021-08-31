using System;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class StaffDAL
    {
        public Staff Login(Staff staff)
        {
            // int login = 0;
            // Console.WriteLine(staff.UserName + " - " + staff.Password);
            try
            {
                // Console.WriteLine("{0}", MD5.CreateMD5(staff.Password));
                MySqlConnection connection = DbHelper.GetConnection();
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Staffs WHERE userName ='"+
                  staff.UserName +"' and Password ='" + MD5.CreateMD5(staff.Password) + "';";

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read() == true)
                {
                    staff.StaffId = reader.GetInt32("StaffID");
                    staff.Name = reader.GetString("Name");
                    staff.Phone = reader.GetString("Phone");
                    staff.Email = reader.GetString("Email");
                    staff.Role = reader.GetInt16("Role");
                    // login = reader.GetInt32("role");
                }
                else
                {
                    staff.Role = 0;
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                staff.Role = -1;
            }
            // Console.WriteLine(login);
            return staff;
        }
    }

}