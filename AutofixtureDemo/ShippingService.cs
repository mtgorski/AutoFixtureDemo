using System;
using System.Collections.Generic;
using System.Linq;

namespace AutofixtureDemo
{
    public class ShippingService
    {
        private readonly Dictionary<Guid, Order> _orders = new Dictionary<Guid, Order>(); 

        public ShipOrderResponse ShipOrder(Order order)
        {
            var response = new ShipOrderResponse();

            if (order.ShippingAddress.State.Length != 2)
            {
                response.ErrorMessage = $"{order.ShippingAddress.State} is not a valid state abbreviation.";
                return response;
            }

            _orders[order.OrderId] = order;

            response.NumberShipped = order.Products.Sum(x => x.Quantity);

            if (order.CreditOrder && order.BillingAddress.CustomerName == "Matt")
            {
                response.ShippingIsFree = true;
            }

            return response;
        }

        public Order GetOrder(Guid orderId)
        {
            return _orders[orderId];
        }
    }
}
