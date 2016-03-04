using System;
using System.Linq;
using System.Linq.Expressions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;
using Ploeh.AutoFixture.Kernel;

namespace AutofixtureDemo
{
    public class OrderFixture
    {
        private readonly Fixture _fixture;
        private IPostprocessComposer<Address> _addressComposer;
        private IPostprocessComposer<Order> _orderComposer;


        public OrderFixture()
        {
            _fixture = new Fixture();
            _orderComposer = _fixture.Build<Order>();
            _addressComposer = _fixture.Build<Address>();
        }

        public OrderFixture UseValidOrder()
        {
            _addressComposer = _addressComposer.WithStateAbbreviationFor(x => x.State);
            return this;
        }

        public Order Build()
        {
            return _orderComposer.With(x => x.ShippingAddress, _addressComposer.Create())
                .With(x => x.BillingAddress, _addressComposer.Create())
                .Create();
        }

        public OrderFixture UseCreditOrder()
        {
            _orderComposer = _orderComposer.With(x => x.CreditOrder, true);
            return this;
        }

        public OrderFixture UseSpecialCustomer()
        {
            _addressComposer = _addressComposer.With(x => x.CustomerName, "Matt");

            return this;
        }

        public OrderFixture UseSpecialItem()
        {
            var specialItem = new Product
            {
                Name = "Computer",
                Quantity = 1
            };
            
            _orderComposer = _orderComposer.Do(
                order =>
                {
                    order.Products = _fixture.CreateMany<Product>().ToList();
                    order.Products.Add(specialItem);
                });
            return this;
        }
    }

    public static class Extensions
    {
        public static IPostprocessComposer<T> WithStateAbbreviationFor<T>(this IPostprocessComposer<T> composer,
            Expression<Func<T, string>> propertyPicker)
        {
            var states = new string[] { "OH", "NY", "PA" };
            var index = new Int32SequenceGenerator().Create() % 3;

            return composer.With(propertyPicker, states[index]);
        }

        
    }
}
