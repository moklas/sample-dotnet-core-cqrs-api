using System.Collections.Generic;
using AutoFixture;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers.Rules;
using Xunit;

namespace SampleProject.UnitTests.Customers.Rules
{
    public class OrderMustHaveAtLeastOneProductRuleTests
    {
        Fixture _fixture;
        public OrderMustHaveAtLeastOneProductRuleTests() => _fixture = new Fixture();

        [Fact]
        public void IsBroken_WithEmptyProductData_ShouldBreakRule()
        {
            // Arrange
            var rule = new OrderMustHaveAtLeastOneProductRule(new List<OrderProductData>());

            // Act
            var result = rule.IsBroken();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsBroken_WithExistingProductData_ShouldNotBreakRule()
        {
            // Arrange
            var orderProductData = _fixture.Create<OrderProductData>();
            var productDataList = new List<OrderProductData> { orderProductData };
            var rule = new OrderMustHaveAtLeastOneProductRule(productDataList);

            // Act
            var result = rule.IsBroken();

            // Assert
            Assert.False(result);
        }
    }
}