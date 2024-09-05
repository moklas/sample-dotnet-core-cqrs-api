using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using SampleProject.Application.Customers.RegisterCustomer;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;
using Shouldly;
using Xunit;

namespace SampleProject.UnitTests.Application.Customers.RegisterCustomer
{
    public class RegisterCustomerCommandHandlerTests
    {
        [Fact]
        public async Task CreateCustomer_WhenValidCustomerDetails_ShouldReturnCustomerId()
        {
            // Arrange
            var rule = Substitute.For<ICustomerUniquenessChecker>();
            rule.IsUnique("m@k.se").ReturnsForAnyArgs(true);
            var repository = Substitute.For<ICustomerRepository>();
            var uow = Substitute.For<IUnitOfWork>();

            var command = new RegisterCustomerCommand("seldh", "srgähsrgho");

            // Act
            var sut = new RegisterCustomerCommandHandler(repository, rule, uow);
            var result = await sut.CreateCustomer(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}