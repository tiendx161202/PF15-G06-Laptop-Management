using System;
using Xunit;
using DAL;
using Persistance;

namespace DALTest
{
    public class StaffDALTest
    {
        private const int LOGIN_SALE = 1;
        private const int LOGIN_ACCOUNTANT = 2;
        private const int LOGIN_FAIL = 0;

        private StaffDAL sdal = new StaffDAL();

        [Theory]
        [InlineData("Giang1111", "Giang123@", LOGIN_SALE)]
        [InlineData("Tien2222", "Tien123@", LOGIN_ACCOUNTANT)]
        [InlineData("Tien9122", "Tien123@", LOGIN_FAIL)]
        [InlineData("Giang1111", "giang1", LOGIN_FAIL)]
        [InlineData("", "Tien123@", LOGIN_FAIL)]
        [InlineData("Giang1111", "", LOGIN_FAIL)]
        private void LoginTest(string UN, string PW, int expected)
        {
            Staff staff1 = new Staff() { UserName = UN, Password = PW };
            int result = sdal.Login(staff1).Role;
            Assert.True(expected == result);
        }

        private LaptopDAL ldal = new LaptopDAL();
        private const int FOUND = 1;
        private const int NOT_FOUND = -1;
        private const int EXCEPTION = -2;
        private const int OUT_OF_STOCK = 0;

        [Theory]
        [InlineData(3, FOUND)]
        [InlineData(2, FOUND)]
        [InlineData(100, NOT_FOUND)]
        [InlineData(-10, NOT_FOUND)]
        [InlineData(50, NOT_FOUND)]
        private void SearchIdTest(int id, int expected)
        {
            Laptop laptop1 = new Laptop() { LaptopId = id };
            int result = ldal.GetLaptop(laptop1).Status;
            Assert.True(expected == result);
        }



        // private LaptopsDAL lsdal = new LaptopsDAL();
        // [Theory]
        // [InlineData("asus", FOUND)]
        // [InlineData("leno vo", FOUND)]
        // [InlineData("", NOT_FOUND)]
        // [InlineData("giang", NOT_FOUND)]
        // [InlineData("UDP", NOT_FOUND)]
        // private void SearchNameTest(string name)
        // {
        //     bool = lsdal.GetLaptops(LaptopFilter.FILTER_BY_LAPTOP_NAME, laptop1).

        //     Laptop laptop1 = new Laptop() { Name = name };
        //     // lsdal.GetLaptops(LaptopFilter.FILTER_BY_LAPTOP_NAME, laptop1);
        //     Assert.True(lsdal.GetLaptops(LaptopFilter.FILTER_BY_LAPTOP_NAME, laptop1) != null);
        // }

    }


}
