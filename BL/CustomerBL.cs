using System;
using System.Collections.Generic;
using Persistance;
using DAL;

namespace BL
{
    public class CustomerBL
    {
        private CustomerBL cbl = new CustomerBL();
        public Customer GetById(int customerId)
        {
            return cbl.GetById(customerId);
        }

        public int AddCustomer(Customer customer)
        {
            return cbl.AddCustomer(customer);
        }
    }
}