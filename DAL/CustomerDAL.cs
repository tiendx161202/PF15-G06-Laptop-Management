using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class CustomerDAL
    {
        private string query;
        private MySqlConnection connection = DBConfiguration.GetConnection();
        private MySqlDataReader reader;
        public CustomerDAL()
        {
            if (connection == null)
            {
                connection = DbHelper.GetConnection();
            }
        }

        public Customer GetCustomerById(Customer cus)
        {
            lock (connection)
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();

                query = @"SELECT * FROM Customers WHERE Customerid = @id;";
                command.Parameters.AddWithValue("@id", cus.CustomerId);
                command.CommandText = query;

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cus = GetCustomer(reader);
                }
                else
                {
                    cus = null;
                }
                reader.Close();
                connection.Close();
                return cus;
            }
        }

        // internal Customer GetCustomerById(int customerId, MySqlConnection connection)
        // {
        //     MySqlCommand command = new MySqlCommand();

        //     query = @"select customer_id, customer_name,
        //                 ifnull(customer_address, '') as customer_address
        //                 from Customers where customer_id=" + customerId + ";";
        //     Customer cus = null;
        //     reader = (new MySqlCommand(query, connection)).ExecuteReader();
        //     if (reader.Read())
        //     {
        //         cus = GetCustomer(reader);
        //     }
        //     reader.Close();
        //     return cus;
        // }

        private Customer GetCustomer(MySqlDataReader reader)
        {
            Customer cus = new Customer();
            cus.CustomerId = reader.GetInt32("Customerid");
            cus.CustomerName = reader.GetString("Name");
            cus.CustomerPhone = reader.GetString("Phone");
            cus.CustomerAddress = reader.GetString("Address");
            return cus;
        }

        public int? AddCustomer(Customer cus)
        {
            lock (connection)
            {
                int? result = null;
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                MySqlCommand cmd = new MySqlCommand("sp_createCustomer", connection);
                try
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", cus.CustomerName);
                    cmd.Parameters["@Name"].Direction = System.Data.ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@Phone", cus.CustomerPhone);
                    cmd.Parameters["@Phone"].Direction = System.Data.ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@Address", cus.CustomerAddress);
                    cmd.Parameters["@Address"].Direction = System.Data.ParameterDirection.Input;

                    cmd.Parameters.AddWithValue("@customerid", MySqlDbType.Int16);
                    cmd.Parameters["@Customerid"].Direction = System.Data.ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    result = (int) cmd.Parameters["@customerId"].Value;

                    // Console.WriteLine(result);
                    // Console.ReadKey();
                }
                catch
                {
                }
                finally
                {
                    connection.Close();
                }
                return result;
            }
        }
    }
}