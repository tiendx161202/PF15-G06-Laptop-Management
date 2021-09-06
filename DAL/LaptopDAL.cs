using System;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class LaptopDAL
    {
        public Laptop GetLaptop(Laptop laptop)
        {
            try
            {
                // SELECT * FROM Laptops INNER JOIN Brands ON laptops.BrandId = brands.BrandId WHERE Laptops.laptopId = 3;

                MySqlConnection connection = DbHelper.GetConnection();
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Laptops INNER JOIN Brands ON laptops.BrandId = brands.BrandId WHERE Laptops.LaptopId = '" +
                  laptop.LaptopId + "';";

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read() == true)
                {
                    laptop = GetData(reader);
                }
                else
                {
                    laptop.Status = Laptop.LaptopStatus.ID_NOT_FOUND;
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                laptop.Status = Laptop.LaptopStatus.EXCEPTION;
            }

            return laptop;
        }

        private Laptop GetData(MySqlDataReader reader)
        {
            Laptop laptop = new Laptop();
            laptop.LaptopId = reader.GetInt32("LaptopId");
            laptop.BrandId = reader.GetInt32("BrandId");
            laptop.BrandName = reader.GetString("Brandname");
            laptop.Name = reader.GetString("Name");
            laptop.Price = reader.GetInt32("Price");
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
                laptop.Status = Laptop.LaptopStatus.ID_NOT_FOUND;
            }

            return laptop;
        }
    }
}