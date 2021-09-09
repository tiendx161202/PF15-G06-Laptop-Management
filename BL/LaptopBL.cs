using System;
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
    }
}