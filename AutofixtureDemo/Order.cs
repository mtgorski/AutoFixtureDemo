using System;
using System.Collections.Generic;

namespace AutofixtureDemo
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Address ShippingAddress { get; set; }

        public List<Product> Products { get; set;}
        public bool CreditOrder { get; set; }
        public Address BillingAddress { get; set; }
    }
}
