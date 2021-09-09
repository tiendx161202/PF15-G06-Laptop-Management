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
            return dal.GetLaptop(laptop);
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
            // var hasNumber = new Regex(@"[0-9]+");
            // var hasUpperChar = new Regex(@"[A-Z]+");
            // var hasMiniMaxChars = new Regex(@".{8,15}");
            // var hasLowerChar = new Regex(@"[a-z]+");

            // if (!hasUpperChar.IsMatch(input) && !hasLowerChar.IsMatch(input))
            // {
            // ErrorMessage = "Name should contain a letter";
            // return false;
            // }
            // else if (!hasMiniMaxChars.IsMatch(input))
            // {
            //     ErrorMessage = "Name should not be less than or greater than 12 characters";
            //     return false;
            // }
            // else if (!hasNumber.IsMatch(input))
            // {
            //     ErrorMessage = "Name should contain At least one numeric value";
            //     return false;
            // }

        }

        public bool ValidateMinPrice(int? _minPrice, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            
                if (_minPrice < 0)
                {
                    ErrorMessage = "Min Price should not be negative number";
                    return false;
                }

            return true;
        }

        public bool ValidateMaxPrice(int? _minPrice, int? _maxPrice, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

                if (_maxPrice <= 0)
                {
                    ErrorMessage = "Max Price should not be negative number";
                    return false;
                }
                else if (_maxPrice < _minPrice)
                {
                    ErrorMessage = "Max Price should not be less than Min Price";
                    return false;
                }



            return true;
        }

    }
}