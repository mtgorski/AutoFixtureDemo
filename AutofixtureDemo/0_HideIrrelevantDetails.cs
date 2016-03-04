using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ploeh.AutoFixture;

namespace AutofixtureDemo
{
    [TestClass]
    public class HideIrrelevantDetails
    {
        private ShippingService _shippingService;
        public ShippingService ShippingService => _shippingService ?? (_shippingService = new ShippingService());

        [TestMethod]
        public void GivenInvalidState_WhenShip_ReturnInvalidResponse()
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                ShippingAddress = new Address
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
        public void Improved_GivenInvalidState_WhenShip_ReturnInvalidResponse()
        {
            var fixture = new Fixture();
            var order = fixture.Create<Order>();
            order.ShippingAddress.State = "Ohio";
            Debug.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));

            var response = ShippingService.ShipOrder(order);
            Assert.IsFalse(response.IsValidRequest);
            Assert.AreEqual(
                string.Format("{0} is not a valid state abbreviation.", order.ShippingAddress.State),
                response.ErrorMessage);
        }

    }
}
