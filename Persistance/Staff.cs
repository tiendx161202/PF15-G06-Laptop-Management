using System;

namespace Persistance
{
    public class Staff
    {
        public int StaffId { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int Role { set; get; }

        public int ROLE_SALE = 1;
        public int ROLE_ACCOUNTANT = 2;
        public int FAIL_LOGIN = 0;
        public int EXCEPTION = -1;

        public override bool Equals(object obj)
        {
            if (obj is Staff)
            {
                return ((Staff)obj).StaffId.Equals(StaffId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return StaffId.GetHashCode();
        }

    }
}
