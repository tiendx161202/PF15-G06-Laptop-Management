using System;
using System.Collections.Generic;
using Persistance;
using DAL;

namespace BL
{
    public class InvoiceBL
    {
        private InvoiceDAL idal = new InvoiceDAL(); 

        // public int CreateInvoice(Invoice invoice)
        public bool CreateInvoice(Invoice invoice)
        {
            // int result = idal.CreateInvoice(invoice);
            bool result = idal.CreateInvoice(invoice);

            return result;
        }
    }
}