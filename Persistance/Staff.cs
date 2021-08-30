using System;

namespace Persistance
{
    public class Staff
    {
        public int StaffId {set; get;}
        public string UserName {set; get;}
        public string Password {set; get;}
        public string Name {set; get;}
        public string Phone {set; get;}
        public string Email {set; get;}
        public int Role {set; get;}

        public int ROLE_SALE = 1;
        public int ROLE_ACCOUNTANT = 2;

        

    }
}
