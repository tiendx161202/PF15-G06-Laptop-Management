using System;
using MySql.Data.MySqlClient;
using Persistance;
using System.Collections.Generic;

namespace DAL
{
    public static class LaptopFilter
    {
        public const int GET_ALL = 0;
        public const int FILTER_BY_LAPTOP_NAME = 1;
        public const int FILTER_BY_LAPTOP_PRICE = 2;
    }

    public class LaptopsDAL
    {
        public List<Laptop> GetLaptops(int filter, Laptop laptop)
        {
            string query = "";
            MySqlConnection connection = DbHelper.GetConnection();
            // connection.Open();
            DbHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();

            switch (filter)
            {
                case LaptopFilter.GET_ALL:
                    query = "SELECT * FROM Laptops INNER JOIN Brands ON laptops.BrandId = brands.BrandId;";
                    break;
                case LaptopFilter.FILTER_BY_LAPTOP_NAME:
                    query = "SELECT * FROM Laptops INNER JOIN Brands ON laptops.BrandId = brands.BrandId WHERE Laptops.Name LIKE CONCAT('%',@Name,'%');";
                    command.Parameters.AddWithValue("@Name", laptop.Name);
                    break;
                case LaptopFilter.FILTER_BY_LAPTOP_PRICE:
                    query = "SELECT * FROM Laptops INNER JOIN Brands ON laptops.BrandId = brands.BrandId WHERE Laptops.Price >= @Min AND Laptops.Price <= @Max;";
                    command.Parameters.AddWithValue("@Min", laptop.minPrice);
                    command.Parameters.AddWithValue("@Max", laptop.maxPrice);
                    break;
            }
            command.CommandText = query;
            return GetLaptops(command, laptop);
        }

        private List<Laptop> GetLaptops(MySqlCommand command, Laptop laptop)
        {
            List<Laptop> LaptopList = new List<Laptop>();

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    do
                    {
                        LaptopList.Add(GetData(reader));
                    } while (reader.Read());
                }
                else
                {
                    laptop.Status = Laptop.LaptopStatus.ID_NOT_FOUND;
                }
                reader.Close();
                DbHelper.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                laptop.Status = Laptop.LaptopStatus.EXCEPTION;
            }

            return LaptopList;
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

            return laptop;
        }

    }
}