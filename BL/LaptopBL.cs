using Persistance;
using DAL;
using System.Collections.Generic;

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
        }

        public bool ValidateMinPrice(decimal? _minPrice, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            if (_minPrice < 0)
            {
                ErrorMessage = "Min Price should not be negative number";
                return false;
            }

            return true;
        }

        public bool ValidateMaxPrice(decimal? _minPrice, decimal? _maxPrice, out string ErrorMessage)
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