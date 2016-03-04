using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace AutofixtureDemo
{
    [TestClass]
    public class AvoidHardCoding
    {
        private ShippingService _shippingService;

        public ShippingService ShippingService => _shippingService ?? (_shippingService = new ShippingService());

        [TestMethod]
        public void GivenValidOrder_WhenShip_RespondWithNumberShipped()
        {
            var order = OrderTemplate.GetValidOrder();

            var response = ShippingService.ShipOrder(order);

            Assert.AreEqual(5, response.NumberShipped);
        }

        [TestMethod]
        public void Improved()
        {
            var fixture = new OrderFixture();
            var order = fixture
                .UseValidOrder()
                .Build();

            var response = ShippingService.ShipOrder(order);

            Assert.AreEqual(order.Products.Sum(x => x.Quantity), response.NumberShipped);
        }

        [TestMethod]
        public void GivenOrderShipped_WhenLookupOrder_ReturnTheShippedOrder()
        {
            var order = OrderTemplate.GetValidOrder();
            ShippingService.ShipOrder(order);

            var actualOrder = ShippingService.GetOrder(order.OrderId);

            Assert.AreEqual("Matt G.", actualOrder.ShippingAddress.CustomerName);
            // etc.
        }

        [TestMethod]
        public void Improved_GivenOrderShipped_WhenLookupOrder_ReturnTheShippedOrder()
        {
            var fixture = new Fixture();
            var expectedOrder = fixture.Create<Order>();
            expectedOrder.ShippingAddress.State = "OH";
            ShippingService.ShipOrder(expectedOrder);

            var actualOrder = ShippingService.GetOrder(expectedOrder.OrderId);

            Assert.AreEqual(expectedOrder.ShippingAddress.CustomerName, actualOrder.ShippingAddress.CustomerName);
            // etc.
        }
    }

    
}
