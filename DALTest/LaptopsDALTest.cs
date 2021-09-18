using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;

namespace DALTest
{
    public class LaptopsDALTest
    {
        private LaptopsDAL lsdal = new LaptopsDAL();

        [Theory]
        [InlineData("Asus", 3)]
        [InlineData("leno vo", 0)]
        [InlineData("gbhghgh", 0)]
        [InlineData("1235234234", 0)]
        [InlineData("apple", 3)]
        [InlineData("lenovo", 3)]
        [InlineData("Acer", 4)]
        [InlineData("Dell", 4)]
        [InlineData("MSI", 3)]
        [InlineData("HP", 3)]
        [InlineData("LG", 2)]
        [InlineData("", 25)]
        private void GetNameTest(string _name, int expected)
        {
            Laptop laptop1 = new Laptop() { Name = _name };
            List<Laptop> list = lsdal.GetLaptops(LaptopFilter.FILTER_BY_LAPTOP_NAME, laptop1);

            if (expected == 0)
            {
                Assert.True(list == null);
            }
            else
            {
                Assert.True(list != null);
                Assert.True(list.Count == expected);

                foreach (Laptop lt in list)
                {
                    Assert.Contains(_name.ToLower(), lt.Name.ToLower());
                }
            }
        }

        [Theory]
        [InlineData(3000000, 19000000, 6)]
        [InlineData(30000000, 19000000, 0)]
        [InlineData(-1000, 10000, 0)]
        [InlineData(12000000, 5000000, 0)]
        [InlineData(60000000, 90000000, 1)]
        [InlineData(1000000, 10000000, 0)]
        [InlineData(1000000, 100000000, 25)]
        private void GetPriceTest(decimal _min, decimal _max, int expected)
        {
            Laptop laptop1 = new Laptop() { minPrice = _min, maxPrice = _max };
            List<Laptop> list = lsdal.GetLaptops(LaptopFilter.FILTER_BY_LAPTOP_PRICE, laptop1);

            if (expected == 0)
            {
                Assert.True(list == null);
            }
            else
            {
                Assert.True(list != null);
                Assert.True(list.Count == expected);

                foreach (Laptop lt in list)
                {
                    Assert.True(_min <= lt.Price && lt.Price <= _max);
                }
            }
        }

    }
}