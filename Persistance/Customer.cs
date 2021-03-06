using System;

namespace Persistance
{
    public class Customer
    {
        public int? CustomerId {set; get;}
        public string CustomerName {set;get;}
        public string CustomerAddress {set;get;}
        public string CustomerPhone{set; get;}

        // Phương thức nàu trả về true true nếu obj là Boolean và có 
        // cùng giá trị với trường hợp này , nếu ko,false
        public override bool Equals(object obj)
        {
            if(obj is Customer)
            {
                return ((Customer)obj).CustomerId.Equals(CustomerId);
            }
            return false;
        }

        // Hàm Băm 
        public override int GetHashCode()
        {
            return CustomerId.GetHashCode();
        }
    }
}