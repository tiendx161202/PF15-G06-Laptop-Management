using System;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DbHelper
    {
        private static MySqlConnection connection;
        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = "server=localhost;userid=vtca;password=vtcacademy;port=3306;database=LaptopM;"
                };
            }
            return connection;
        }
        
        public static void CloseConnection()
        {
            if (connection != null) connection.Close();
        }

        public static void OpenConnection()
        {
            if (connection != null) connection.Open();
        }

        private DbHelper() { }
    }
}
