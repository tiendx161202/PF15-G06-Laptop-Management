using System;
using Xunit;
using DAL;
using Persistance;

namespace DALTest
{
    public class StaffDALTest
    {
        private StaffDAL dal = new StaffDAL();
        // [Fact]
        [Theory]
        [InlineData("Giang1111", "Giang123@", 1)]
        [InlineData("Tien2222", "Tien123@", 2)]
        [InlineData("Tien9122", "Tien123@", 0)]
        [InlineData("Giang1111", "giang1", 0)]
        [InlineData("", "Tien123@", 0)]
        [InlineData("Giang1111", "", 0)]
        public void LoginTest(string UN, string PW, int expected)
        {
            Staff staff1 = new Staff() {UserName = UN, Password = PW};
            int result = dal.Login(staff1).Role;
            Assert.True(expected == result);
        }
    }
}
