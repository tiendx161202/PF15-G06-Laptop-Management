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
        public bool CreateInvoice(Invoice invoice, out Invoice invoice1)
        {
            // int result = idal.CreateInvoice(invoice);
            bool result = idal.CreateNewInvoice(invoice, out invoice1);

            return result;
        }
    }
}