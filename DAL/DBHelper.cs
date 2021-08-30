using System;
using MySql.Data;

namespace DAL
{
    public class DBHelper
    {
        public class DBHelper
        {
            private static MySqlconnection connection;
            public static MySqlconnection GetConnection()
            {
                if (connection == null)
                {
                   connection = new MySqlConnection
                {
                    ConnectionString = "server=localhost;user id=vtca;password=vtcacademy;port=3306;database=LoginDB;"
                };
                }
            }
        }
    }
}
