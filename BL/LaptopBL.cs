using System;
using Persistance;
using DAL;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BL
{
    public class LaptopBL
    {

        private LaptopDAL dal = new LaptopDAL();
        private LaptopsDAL sdal = new LaptopsDAL();

        public Laptop GetLaptop(Laptop laptop)
        {
            return dal.GetLaptopById(laptop);
        }

        public List<Laptop> GetLaptopByPrice(Laptop laptop)
        {
            return sdal.GetLaptops(LaptopFilter.FILTER_BY_LAPTOP_PRICE, laptop);
        }

        public List<Laptop> GetAllLaptop(Laptop laptop)
        {
            return sdal.GetLaptops(LaptopFilter.GET_ALL, laptop);
        }

        public List<Laptop> GetLaptopByName(Laptop laptop)
        {
            return sdal.GetLaptops(LaptopFilter.FILTER_BY_LAPTOP_NAME, laptop);
        }

        public bool ValidateName(string name, out string ErrorMessage)
        {
            var input = name;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(input))
            {
                // throw new Exception("Password should not be empty");
                ErrorMessage = "User Name should not be empty and whitespace";
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateMinPrice(string s_minPrice, out decimal d_minPrice, out string ErrorMessage)
        {
            Regex regexNumber = new Regex("^[0-9]+$");
            ErrorMessage = string.Empty;
            d_minPrice = decimal.Zero;

            if (string.IsNullOrEmpty(s_minPrice) || string.IsNullOrWhiteSpace(s_minPrice))
            {
                ErrorMessage = " Min price should not be empty or whitespace!";
                return false;
            }
            if (!regexNumber.IsMatch(s_minPrice))
            {
                ErrorMessage = "Min Price must be a number";
                return false;
            }

            d_minPrice = Convert.ToDecimal(s_minPrice);

            if (d_minPrice < 0)
            {
                ErrorMessage = "Min Price should not be negative number";
                return false;
            }

            return true;
        }

        public bool ValidateMaxPrice(decimal _minPrice, string s_maxPrice, out decimal d_maxPrice, out string ErrorMessage)
        {
            Regex regexNumber = new Regex("^[0-9]+$");
            ErrorMessage = string.Empty;

            d_maxPrice = decimal.Zero;

            if (string.IsNullOrEmpty(s_maxPrice) || string.IsNullOrWhiteSpace(s_maxPrice))
            {
                ErrorMessage = " Min price should not be empty or whitespace!";
                return false;
            }
            if (!regexNumber.IsMatch(s_maxPrice))
            {
                ErrorMessage = "Min Price must be a number";
                return false;
            }

            d_maxPrice = Convert.ToDecimal(s_maxPrice);

            if (d_maxPrice <= 0)
            {
                ErrorMessage = "Max Price should not be negative number";
                return false;
            }
            else if (d_maxPrice < _minPrice)
            {
                ErrorMessage = "Max Price should not be less than Min Price";
                return false;
            }
            return true;
        }

    }
}