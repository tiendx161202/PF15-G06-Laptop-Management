using System;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class LaptopDAL
    {
        MySqlConnection connection = DBConfiguration.GetConnection();

        public Laptop GetLaptopById(Laptop laptop)
        {
            lock (connection)
            {
                try
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Laptops INNER JOIN Brands ON laptops.BrandId = brands.BrandId WHERE Laptops.LaptopId = @id;";
                    // command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", laptop.LaptopId);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read() == true)
                    {
                        laptop = GetDataLaptop(reader);
                    }
                    else
                    {
                        // laptop.Status = Laptop.LaptopStatus.NOT_FOUND;
                        laptop = null;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    laptop = null;
                    Console.WriteLine(ex.Message);
                    // laptop.Status = Laptop.LaptopStatus.EXCEPTION;
                }
                finally
                {
                    connection.Close();
                }
            }

            return laptop;
        }

        public Laptop GetDataLaptop(MySqlDataReader reader)
        {
            Laptop laptop = new Laptop();
            laptop.LaptopId = reader.GetInt32("LaptopId");
            laptop.BrandId = reader.GetInt32("BrandId");
            laptop.BrandName = reader.GetString("Brandname");
            laptop.Name = reader.GetString("LaptopName");
            laptop.Price = reader.GetDecimal("Price");
            laptop.Ram = reader.GetString("RAM");
            laptop.HardDisk = reader.GetString("HardDisk");
            laptop.Cpu = reader.GetString("Cpu");
            laptop.Monitor = reader.GetString("Monitor");
            laptop.GraphicsCard = reader.GetString("GraphicsCard");
            laptop.Jack = reader.GetString("Jack");
            laptop.Os = reader.GetString("Os");
            laptop.Battery = reader.GetString("Battery");
            laptop.Weight = reader.GetString("Weight");
            laptop.WarrantyPeriod = reader.GetString("WarrantyPeriod");
            laptop.Stock = reader.GetInt32("Stock");
            laptop.Quanity = null;

            if (laptop.Stock > 0)
            {
                laptop.Status = Laptop.LaptopStatus.ACTIVE;
            }
            else if (laptop.Stock == 0)
            {
                laptop.Status = Laptop.LaptopStatus.NOT_ACTIVE;
            }
            else
            {
                laptop.Status = Laptop.LaptopStatus.NOT_FOUND;
            }

            return laptop;
        }

    }
}