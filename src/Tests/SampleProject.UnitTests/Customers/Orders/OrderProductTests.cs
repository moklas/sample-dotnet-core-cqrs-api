using System.Collections.Generic;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using SampleProject.Domain.SharedKernel;
using Xunit;

namespace SampleProject.UnitTests.Customers.Orders
{
    public class OrderProductTests
    {
        [Fact]
        public void ChangeQuantity_WithValidValue_ShouldCalculateValueCorrectly()
        {
            // Arrange
            var moneyValue = new MoneyValue((decimal)12.0, "EUR");
            var productId = new ProductId(new System.Guid());
            var productPriceData = new ProductPriceData(productId, moneyValue);
            var conversionRates = new List<ConversionRate> { new("USD", "EUR", (decimal)1.0) };
            var orderProduct = OrderProduct.CreateForProduct(productPriceData, 3, "EUR", conversionRates);

            // Act
            orderProduct.ChangeQuantity(productPriceData, 7, conversionRates);

            // Assert
            Assert.Equal((decimal)84, orderProduct.ValueInEUR.Value);
        }

        [Fact]
        public void ChangeQuantity_WithNegativeQuantity_ShouldThrowException()
        {
            // Arrange
            var moneyValue = new MoneyValue((decimal)12.0, "EUR");
            var productId = new ProductId(new System.Guid());
            var productPriceData = new ProductPriceData(productId, moneyValue);
            var conversionRates = new List<ConversionRate> { new("USD", "EUR", (decimal)1.0) };
            var orderProduct = OrderProduct.CreateForProduct(productPriceData, 3, "EUR", conversionRates);

            // Act & Assert
            Assert.Throws<TooLowValueException>(() => orderProduct.ChangeQuantity(productPriceData, -7, conversionRates));
        }
    }
}