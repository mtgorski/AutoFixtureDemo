using System.Linq;

namespace AutofixtureDemo
{
    public class ShippingService
    {
        public ShipOrderResponse ShipOrder(Order order)
        {
            var response = new ShipOrderResponse();
            if (order.CustomerAddress.State.Length != 2)
            {
                response.ErrorMessage = $"{order.CustomerAddress.State} is not a valid state abbreviation.";
                return response;
            }
            response.NumberShipped = order.Products.Sum(x => x.Quantity);
            if (order.CreditOrder && order.BillingAddress.CustomerName == "Matt")
            {
                response.ShippingIsFree = true;
            }

            return response;
        }
    }
}
