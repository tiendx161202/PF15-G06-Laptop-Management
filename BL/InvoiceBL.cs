using System;
using Persistance;
using DAL;

namespace BL
{
    public class InvoiceBL
    {
        private InvoiceDAL idal = new InvoiceDAL(); 


        public Invoice GetInvoiceById(Invoice invoice)
        {
            return idal.GetInvoiceById(invoice);
        }

        public bool CreateInvoice(Invoice invoice, out Invoice invoice1)
        {
            bool result;
            return result = idal.CreateNewInvoice(invoice, out invoice1);
        }
    }
}