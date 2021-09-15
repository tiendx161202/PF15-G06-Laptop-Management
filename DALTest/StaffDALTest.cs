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
            int result;

            Staff staff1 = new Staff() { UserName = _UserName, Password = _Password };
            staff1 = sdal.Login(staff1);
            if (staff1 == null)
            {
                result = 0;
            }
            else
            {
                result = staff1.Role;
            }
            Assert.True(result == expected);
        }

    }


}
