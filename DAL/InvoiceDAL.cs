using System;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{

    public class InvoiceDAL
    {
        private MySqlConnection connection = DBConfiguration.GetConnection();

        public bool CreateNewInvoice(Invoice invoice, out Invoice invoice1)
        // public int CreateInvoice(Invoice invoice)
        {
            invoice1 = null;
            // foreach (Laptop lt in invoice.LaptopList)
            // Console.WriteLine(lt.Quanity);

            // Console.ReadKey();
            // int result = 0;
            bool result = true;
            if (invoice == null || invoice.LaptopList == null || invoice.LaptopList.Count == 0)
            {
                return false;
                // return -1;
            }

            lock (connection)
            {
                MySqlCommand command = connection.CreateCommand();
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.Connection = connection;

                // Lock table to only this session write
                command.CommandText = "LOCK TABLES Customers WRITE, Invoices WRITE, InvoiceDetails WRITE, Laptops WRITE;";
                command.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                command.Transaction = trans;
                // reader = null;

                if (invoice.InvoiceCustomer == null || invoice.InvoiceCustomer.CustomerName == null || invoice.InvoiceCustomer.CustomerName == "")
                {
                    // if customer is null, set default id = 1
                    invoice.InvoiceCustomer = new Customer() { CustomerId = 1 };
                }

                try
                {
                    if (invoice.InvoiceCustomer == null || invoice.InvoiceCustomer.CustomerId == null)
                    {
                       throw new Exception("Can't find Customer!");
                    }

                    // Insert Order
                    command = new MySqlCommand ("p_createInvoice", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SaleId", invoice.InvoiceSale.StaffId);
                    command.Parameters["@SaleId"].Direction = System.Data.ParameterDirection.Input;
                    command.Parameters.AddWithValue("@CustomerId", invoice.InvoiceCustomer.CustomerId);
                    command.Parameters["@CustomerId"].Direction = System.Data.ParameterDirection.Input;
                    command.Parameters.AddWithValue("@Datetime", invoice.InvoiceDate);
                    command.Parameters["@Datetime"].Direction = System.Data.ParameterDirection.Input;
                    command.Parameters.AddWithValue("@Status", InvoiceStatus.CREATE_NEW_INVOICE);
                    command.Parameters["@Status"].Direction = System.Data.ParameterDirection.Input;
                    command.Parameters.AddWithValue("@InvoiceNo", MySqlDbType.Int32);
                    command.Parameters["@InvoiceNo"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    invoice.InvoiceNo = (int) command.Parameters["@InvoiceNo"].Value;

                    // result = invoice.InvoiceNo;

                    foreach (var laptop in invoice.LaptopList)
                    {
                        if (laptop.LaptopId == null)
                        {
                            throw new Exception("Not Exists Laptop");
                        }
                        if (laptop.Stock <= 0)
                        {
                            throw new Exception("Not enough stock");
                        }

                        // Insert To InvoiceDetails
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = @"INSERT INTO InvoiceDetails(invoiceno, laptopid, quanity, price) VALUES (@InvNo, @LapID, @quanity, (@quanity*@price));";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@InvNo", invoice.InvoiceNo);
                        command.Parameters.AddWithValue("@LapID", laptop.LaptopId);
                        command.Parameters.AddWithValue("@quanity", laptop.Quanity);
                        command.Parameters.AddWithValue("@Price", laptop.Price);
                        command.ExecuteNonQuery();

                        // Update stock in Laptop
                        command.CommandText = @"UPDATE Laptops SET Stock = Stock - @quanity WHERE LaptopId = @LapID;";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@quanity", laptop.Quanity);
                        command.Parameters.AddWithValue("@LapID", laptop.LaptopId);
                        command.ExecuteNonQuery();
                    }
                    trans.Commit();
                    result = true;
                    // result = 10;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    result = false;
                    // result = -100;
                    try
                    {
                        trans.Rollback();
                    }
                    catch
                    { }
                }
                finally
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "UNLOCK TABLES;";
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            invoice1 = invoice;
            return result;
        }

    }
}