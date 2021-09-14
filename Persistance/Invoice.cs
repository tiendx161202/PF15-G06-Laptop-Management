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
        public int InvoiceNo { set; get; }
        public DateTime InvoiceDate { set; get; }
        public Customer InvoiceCustomer { set; get; }
        public Staff InvoiceSale { set; get; }
        public Staff InvoiceAccountant { set; get; }
        public int Status { set; get; }
        public List<Laptop> LaptopList { set; get; }
        public Laptop this[int index]
        {
            get
            {
                if (LaptopList == null || LaptopList.Count == 0 || index < 0 || LaptopList.Count < index)
                    return null;

                return LaptopList[index];
            }
            set
            {
                if (LaptopList == null)
                    LaptopList = new List<Laptop>();

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
                return ((Invoice)obj).InvoiceNo.Equals(InvoiceNo);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return InvoiceNo.GetHashCode();
        }

    }
}