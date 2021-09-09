using System;
using System.Collections.Generic;

namespace Persistance
{
    public static class OrderStatus
    {
        public const int CREATE_NEW_OEDER = 1;
    }

    public class Order 
    {
        public int OrderId {set; get;}
        public DateTime OrderDate {set;get;}
        public Customer OrderCustomer {set; get;}
        public int? Status {set; get;}
        public List<Laptop> LaptopList {set;get;}
        public Laptop this[int index]
        {
            get
            {
                if(LaptopList == null || LaptopList.Count == 0 || index <0||LaptopList.Count < index) return null;
                return LaptopList[index];
            }
            set
            {
                if(LaptopList == null) LaptopList = new List<Laptop>();
                LaptopList.Add(value);
            }
        }
        public Order()
        {
            LaptopList = new List<Laptop>();

        }
        public override bool Equals(object obj)
        {
            if(obj is Order)
            {
                return ((Order)obj).OrderId.Equals(OrderId);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}