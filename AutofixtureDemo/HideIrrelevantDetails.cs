using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace AutofixtureDemo
{
    [TestClass]
    public class HideIrrelevantDetails
    {
        public ShippingService ShippingService => new ShippingService();

        [TestMethod]
        public void GivenInvalidState_ReturnInvalidResponse()
        {
            var order = new Order
            {
                CustomerAddress = new Address
                {
                    AddressLine1 = "43 Hillside Dr.",
                    CustomerName = "Matt G.",
                    State = "Ohio",
                    Zipcode = "44555"
                },
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "TestProduct",
                        Quantity = 2
                    }
                }
            };

            var response = ShippingService.ShipOrder(order);

            Assert.IsFalse(response.IsValidRequest);
            Assert.AreEqual("Ohio is not a valid state abbreviation.", response.ErrorMessage);
        }

        [TestMethod]
        public void Improved()
        {
            var fixture = new Fixture();
            var order = fixture.Create<Order>();
            order.CustomerAddress.State = "Ohio";

            var response = ShippingService.ShipOrder(order);
            Assert.IsFalse(response.IsValidRequest);
            Assert.AreEqual(
                string.Format("{0} is not a valid state abbreviation.", order.CustomerAddress.State),
                response.ErrorMessage);
        }
    }
}
