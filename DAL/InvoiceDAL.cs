using System;
using MySql.Data.MySqlClient;
using Persistance;
using System.Collections.Generic;

namespace DAL
{

    public class InvoiceDAL
    {
        private MySqlConnection connection = DBConfiguration.GetConnection();


        public bool ChangeQuanity(Invoice invoice)
        {
            bool result = true;
            if (invoice == null || invoice.LaptopList == null || invoice.LaptopList.Count == 0)
            {
                return false;
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
                command.CommandText = "LOCK TABLES InvoiceDetails WRITE, Laptops WRITE;";
                command.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                command.Transaction = trans;

                try
                {
                    if (invoice == null)
                    {
                        throw new Exception("Can't find invoice!");
                    }




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

            return result;
        }


        public Invoice GetInvoiceById(Invoice invoice)
        {
            lock (connection)
            {
                MySqlCommand command = connection.CreateCommand();
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                try
                {
                    if (invoice == null)
                    {
                        throw new Exception("Can't find invoice!");
                    }

                    command.CommandText = @"SELECT * FROM invoices i 
                                            INNER JOIN invoicedetails d ON i.InvoiceNo = d.InvoiceNo 
                                            INNER JOIN staffs s ON i.SaleId = s.StaffID OR i.AccountantId = s.StaffID 
                                            INNER JOIN customers c ON i.Customerid = c.Customerid
                                            INNER JOIN laptops l ON l.LaptopId = d.LaptopId
                                            INNER JOIN brands b ON b.BrandId = l.BrandId
                                            WHERE i.InvoiceNo = @invoiceNo;";
                    command.Parameters.AddWithValue("@invoiceNo", invoice.InvoiceNo);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.Read())
                    {
                        return invoice = null;
                    }
                    invoice = GetInvoice(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return invoice;
        }

        private Invoice GetInvoice(MySqlDataReader reader)
        {
            Invoice invoice = new Invoice();
            invoice.InvoiceNo = reader.GetInt32("InvoiceNo");

            invoice.InvoiceSale = new Staff() { StaffId = reader.GetInt32("SaleId") };
            if (invoice.InvoiceSale.StaffId != 0)
            {
                invoice.InvoiceSale.Name = reader.GetString("StaffName");
                invoice.InvoiceSale.Phone = reader.GetString("StaffPhone");
                invoice.InvoiceSale.Email = reader.GetString("StaffEmail");
                invoice.InvoiceSale.Role = reader.GetInt16("Role");
            }

            invoice.InvoiceAccountant = new Staff() { StaffId = reader.GetInt32("AccountantId") };
            if (invoice.InvoiceAccountant.StaffId != 0)
            {
                invoice.InvoiceAccountant.Name = reader.GetString("StaffName");
                invoice.InvoiceAccountant.Phone = reader.GetString("StaffPhone");
                invoice.InvoiceAccountant.Email = reader.GetString("StaffEmail");
                invoice.InvoiceAccountant.Role = reader.GetInt16("Role");
            }

            invoice.InvoiceCustomer = new Customer()
            {
                CustomerId = reader.GetInt32("CustomerId"),
                CustomerName = reader.GetString("CustomerName"),
                CustomerPhone = reader.GetString("CustomerPhone"),
                CustomerAddress = reader.GetString("CustomerAddress")
            };

            invoice.InvoiceDate = reader.GetDateTime("Datetime");

            LaptopDAL ldal = new LaptopDAL();
            Laptop lt = ldal.GetDataLaptop(reader);
            invoice.LaptopList.Add(lt);
            while (reader.Read())
            {
                Laptop ltt = ldal.GetDataLaptop(reader);
                invoice.LaptopList.Add(ltt);
            }
            return invoice;
        }

        public bool CreateNewInvoice(Invoice invoice, out Invoice invoice1)
        {
            invoice1 = null;
            bool result = true;
            if (invoice == null || invoice.LaptopList == null || invoice.LaptopList.Count == 0)
            {
                return false;
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
                    command = new MySqlCommand("p_createInvoice", connection);
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
                    invoice.InvoiceNo = (int)command.Parameters["@InvoiceNo"].Value;

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
