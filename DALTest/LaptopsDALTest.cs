using Xunit;
using DAL;
using Persistance;
using System.Linq;
using System.Collections.Generic;

namespace DALTest
{
    public class LaptopsDALTest
    {
        private LaptopsDAL lsdal = new LaptopsDAL();

        [Theory]
        [InlineData("asus", true)]
        [InlineData("leno vo", false)]
        [InlineData("", true)]
        [InlineData("gbhghgh", false)]
        [InlineData("1235234234", false)]
        private void GetNameTest(string _name, bool expected)
        {
            Laptop laptop1 = new Laptop() { Name = _name };
            List<Laptop> list = lsdal.GetLaptops(LaptopFilter.FILTER_BY_LAPTOP_NAME, laptop1);
            Assert.True(HasValue(list) == expected);
        }

        [Theory]
        [InlineData(3000000, 19000000, true)]
        [InlineData(30000000, 19000000, false)]
        [InlineData(-1000, 10000, false)]
        [InlineData(12000000, 5000000, false)]
        // [InlineData(3000000, 19000000, true)]
        // [InlineData(3000000, 19000000, true)]
        private void GetPriceTest(decimal _min, decimal _max, bool expected)
        {
            Laptop laptop1 = new Laptop() { minPrice = _min, maxPrice = _max };
            List<Laptop> list = lsdal.GetLaptops(LaptopFilter.FILTER_BY_LAPTOP_PRICE, laptop1);
            Assert.True(HasValue(list) == expected);

        }

        bool HasValue(List<Laptop> list)
        {
            return list.Any();
        }

    }
}