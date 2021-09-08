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
        private void LoginTest(string _UserName, string _Password, int expected)
        {
            Staff staff1 = new Staff() { UserName = _UserName, Password = _Password };
            int result = sdal.Login(staff1).Role;
            Assert.True(result == expected);
        }

    }



}
