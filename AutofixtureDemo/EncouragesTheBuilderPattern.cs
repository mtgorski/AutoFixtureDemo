using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofixtureDemo
{
    [TestClass]
    public class EncouragesTheBuilderPattern
    {
        public ShippingService ShippingService => new ShippingService();

        [TestMethod]
        public void GivenSpecialCustomerAndSpecialProductAndCreditCardOrder_WhenShipOrder_ReturnFreeShipping()
        {
            var order = OrderTemplate.GetValidOrder();
            order.CustomerAddress.CustomerName = "Matt";
            order.CreditOrder = true;
            order.BillingAddress = new Address
            {
                CustomerName = "Matt"
                //...
            }; 
            order.Products.Add(new Product {Name = "Computer", Quantity = 1});

            var response = ShippingService.ShipOrder(order);

            Assert.IsTrue(response.ShippingIsFree);
        }

        [TestMethod]
        public void Improved()
        {
            var fixture = new OrderFixture();
            var order = fixture.UseValidOrder()
                .UseCreditOrder()
                .UseSpecialCustomer()
                .UseSpecialItem()
                .Build();

            var response = ShippingService.ShipOrder(order);

            Assert.IsTrue(response.ShippingIsFree);
        }
    }
}
