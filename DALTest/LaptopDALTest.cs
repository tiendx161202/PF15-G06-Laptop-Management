using Xunit;
using DAL;
using Persistance;

namespace DALTest
{
    public class LaptopDALTest
    {
        private LaptopDAL ldal = new LaptopDAL();
        private const bool MATCH = true;
        private const bool NOT_MATCH = false;

        [Theory]
        [InlineData(3, MATCH)]
        [InlineData(2, MATCH)]
        [InlineData(100, NOT_MATCH)]
        [InlineData(-10, NOT_MATCH)]
        [InlineData(50, NOT_MATCH)]
        [InlineData(4, MATCH)]
        [InlineData(5, MATCH)]
        [InlineData(6, MATCH)]
        [InlineData(7, MATCH)]
        [InlineData(8, MATCH)]
        [InlineData(9, MATCH)]
        [InlineData(10, MATCH)]
        [InlineData(11, MATCH)]
        [InlineData(12, MATCH)]
        [InlineData(13, MATCH)]
        [InlineData(14, MATCH)]
        [InlineData(15, MATCH)]
        [InlineData(16, MATCH)]

        private void SearchIdTest(int _id, bool expected)
        {
            Laptop result = new Laptop() { LaptopId = _id };
            result = ldal.GetLaptopById(result);

            if (expected)
            {
                Assert.True(result != null);
                Assert.True(result.LaptopId == _id);
            }
            else
            {
                Assert.True(result == null);
            }

        }

    }
}
