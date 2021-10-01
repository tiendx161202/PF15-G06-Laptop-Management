using System;
using System.Collections.Generic;
using Persistance;
using DAL;

namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL cdal = new CustomerDAL();

        public bool UpdateCustomer(Customer customer)
        {
            return cdal.UpdateCustomer(customer);
        }

        public Customer GetCustomerById(Customer customer)
        {
            return cdal.GetCustomerById(customer);
        }

        public bool AddCustomer(Customer customer)
        {
            return cdal.AddCustomer(customer);
        }

        public Customer GetCustomerByPhone(Customer customer)
        {
            return cdal.GetCustomerByPhone(customer);
        }

        
    }
}