using System;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class StaffDAL
    {
        MySqlConnection connection = DbHelper.GetConnection();
        public Staff Login(Staff staff)
        {
            lock (connection)
            {
                try
                {
                    // Console.WriteLine("{0}", MD5.CreateMD5(staff.Password));
                    connection.Open(); 
                    MySqlCommand command = connection.CreateCommand();

                    command.CommandText = "SELECT * FROM Staffs WHERE userName = @Name AND Password = @Pass;";
                    command.Parameters.AddWithValue("@Name", staff.UserName);
                    command.Parameters.AddWithValue("@Pass", MD5.CreateMD5(staff.Password));

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        staff.StaffId = reader.GetInt32("StaffID");
                        staff.Name = reader.GetString("Name");
                        staff.Phone = reader.GetString("Phone");
                        staff.Email = reader.GetString("Email");
                        staff.Role = reader.GetInt32("Role");
                    }
                    else
                    {
                        staff.Role = staff.FAIL_LOGIN;
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    staff.Role = staff.EXCEPTION;
                }
            }
            return staff;
        }
    }

}