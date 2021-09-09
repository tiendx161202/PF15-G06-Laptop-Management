using System;

namespace Persistance
{
    public class Laptop
    {
        public int LaptopId { set; get; }
        public int BrandId { set; get; }
        public string BrandName { set; get; }
        public string Name { set; get; }
        public int Price { set; get; }
        public string Cpu { set; get; }
        public string Ram { set; get; }
        public string HardDisk { set; get; }
        public string Monitor { set; get; }
        public string GraphicsCard { set; get; }
        public string Jack { set; get; }
        public string Os { set; get; }
        public string Battery { set; get; }
        public string Weight { set; get; }
        public string WarrantyPeriod { set; get; }
        public int Stock { set; get; }
        public int Status { set; get; }
        public int minPrice { set; get; }
        public int maxPrice { set; get; }

        public static class LaptopStatus
        {
            public const int NOT_ACTIVE = 0;
            public const int ACTIVE = 1;
            public const int EXCEPTION = -2;
            public const int NOT_FOUND = -1;

        }
        public override bool Equals(object obj)
        {
            if (obj is Laptop)
            {
                return ((Laptop)obj).LaptopId.Equals(LaptopId);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return LaptopId.GetHashCode();
        }
    }
}
