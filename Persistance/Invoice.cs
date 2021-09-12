using System;
using System.Collections.Generic;

namespace Persistance
{
    public static class InvoiceStatus
    {
        public const int CREATE_NEW_INVOICE = 1;
    }

    public class Invoice
    {
        public int InvoiceId { set; get; }
        public DateTime InvoiceDate { set; get; }
        public Customer InvoiceCustomer { set; get; }
        public int Status { set; get; }
        public List<Laptop> LaptopList { set; get; }
        public Laptop this[int index]
        {
            get
            {
                if (LaptopList == null || LaptopList.Count == 0 || index < 0 || LaptopList.Count < index) return null;
                return LaptopList[index];
            }
            set
            {
                if (LaptopList == null) LaptopList = new List<Laptop>();
                LaptopList.Add(value);
            }
        }

        public Invoice()
        {
            LaptopList = new List<Laptop>();
        }

        public override bool Equals(object obj)
        {
            if (obj is Invoice)
            {
                return ((Invoice)obj).InvoiceId.Equals(InvoiceId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return InvoiceId.GetHashCode();
        }

    }
}