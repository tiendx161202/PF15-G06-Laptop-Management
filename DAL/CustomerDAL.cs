using System;
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

        public Customer GetCustomerByPhone(Customer cus)
        {
            lock (connection)
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                MySqlCommand command = connection.CreateCommand();

                query = @"SELECT * FROM Customers WHERE CustomerPhone = @Phone;";
                command.Parameters.AddWithValue("@Phone", cus.CustomerPhone);
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

        public Customer GetCustomerById(Customer cus)
        {
            lock (connection)
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
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

        private Customer GetCustomer(MySqlDataReader reader)
        {
            Customer cus = new Customer();
            cus.CustomerId = reader.GetInt32("Customerid");
            cus.CustomerName = reader.GetString("CustomerName");
            cus.CustomerPhone = reader.GetString("CustomerPhone");
            cus.CustomerAddress = reader.GetString("CustomerAddress");
            return cus;
        }

        public bool AddCustomer(Customer cus)
        {
            bool result = true;
            Customer check_custommer = GetCustomerByPhone(cus);
            if (check_custommer != null)
            {
                return UpdateCustomer(cus);
            }
            else
            {
                lock (connection)
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    MySqlCommand cmd = new MySqlCommand("p_createCustomer", connection);

                    try
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", cus.CustomerName);
                        cmd.Parameters["@Name"].Direction = System.Data.ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@Phone", cus.CustomerPhone);
                        cmd.Parameters["@Phone"].Direction = System.Data.ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@Address", cus.CustomerAddress);
                        cmd.Parameters["@Address"].Direction = System.Data.ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@Customerid", MySqlDbType.Int32);
                        cmd.Parameters["@Customerid"].Direction = System.Data.ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                    }
                    catch
                    {
                        result = false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return result;
                }
            }
        }

        public bool UpdateCustomer(Customer cus)
        {
            bool result = true;
            lock (connection)
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();

                try
                {
                    query = @"UPDATE Customers SET Customername = @Name, CustomerAddress = @Address WHERE customerphone = @Phone;";
                    command.Parameters.AddWithValue("@Name", cus.CustomerName);
                    command.Parameters.AddWithValue("@Address", cus.CustomerAddress);
                    command.Parameters.AddWithValue("@Phone", cus.CustomerPhone);
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
                catch
                {
                    result = false;
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