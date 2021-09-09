using System;

namespace Persistance
{
    public class Customer
    {
        public int? CustomerId {set; get;}
        public string CustomerName {set;get;}
        public string CustomerAddress {set;get;}
        public int CustomerPhone{set; get;}

        // Phương thức nàu trả về true true nếu obj là Booleanvà có 
        // cùng giá trịvới trường hợp này , news ko,false
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
            return base.GetHashCode();
        }
    }
}