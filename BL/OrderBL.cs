using System;
using System.Collections.Generic;
using Persistance;
using DAL;

namespace BL
{
    public class OrderBL
    {
        private OrderBL odl =new OrderBL();
        public bool CreateOrder(Order order)
        {
            bool result = odl.CreateOrder(order);
            return result;
        }
    }
}