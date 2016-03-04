using System.Collections.Generic;

namespace AutofixtureDemo
{
    public class OrderTemplate
    {
        public static Order GetValidOrder()
        {
            return new Order
            {
                ShippingAddress = new Address
                {
                    CustomerName = "Matt G.",
                    State = "OH"
                },
                BillingAddress = new Address
                {
                    CustomerName = "Matt G.",
                    State = "OH"
                },
                Products = new List<Product>
                {
                    new Product
                    {
                        Quantity = 2
                    },
                    new Product
                    {
                        Quantity = 3
                    }
                }
            };
        }
    }
}
