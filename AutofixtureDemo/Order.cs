using System.Collections.Generic;

namespace AutofixtureDemo
{
    public class Order
    {
        public Address CustomerAddress { get; set; }

        public List<Product> Products { get; set;}
    }
}
