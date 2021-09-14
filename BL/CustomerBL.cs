using System;
using System.Collections.Generic;
using Persistance;
using DAL;

namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL cdal = new CustomerDAL();
        public Customer GetCustomerById(Customer customer)
        {
            return cdal.GetCustomerById(customer);
        }

        public int? AddCustomer(Customer customer)
        {
            return cdal.AddCustomer(customer);
        }

        
    }
}