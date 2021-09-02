using System;
using Persistance;
using DAL;
using System.Text.RegularExpressions;

namespace BL
{
    public class LaptopBL
    {
        private LaptopDAL dal = new LaptopDAL();

        public Laptop GetLaptop(Laptop laptop)
        {
            return dal.GetLaptop(laptop);
        }
    }
}