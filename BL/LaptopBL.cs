using System;
using Persistance;
using DAL;
using System.Text.RegularExpressions;
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

        public List<Laptop> GetLaptopByName(Laptop laptop)
        {
            return sdal.GetLaptopByName(laptop);
        }
    }
}