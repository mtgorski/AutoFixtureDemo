using System.CodeDom;

namespace AutofixtureDemo
{
    public class ShipOrderResponse
    {
        public bool IsValidRequest { get; set; }

        public string ErrorMessage { get; set; }

        public int NumberShipped { get; set; }
        public bool ShippingIsFree { get; set; }
    }
}