using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofixtureDemo
{
    [TestClass]
    public class AvoidHardCoding
    {
        public ShippingService ShippingService => new ShippingService();

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
    }

    
}
