using System;
using MySql.Data.MySqlClient;
using System.IO;

namespace DAL
{
    public class DBConfiguration
    {
        private static MySqlConnection connection {get; set;}

        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                using (FileStream fileStream = File.OpenRead("ConnectionFile.txt"))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        connection = new MySqlConnection
                        {
                            ConnectionString = reader.ReadLine()
                        };
                    }
                }
            }
            return connection;
        }

    }
}

