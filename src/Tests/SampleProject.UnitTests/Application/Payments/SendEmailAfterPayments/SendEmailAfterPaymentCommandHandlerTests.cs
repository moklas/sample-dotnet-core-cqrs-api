using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using NSubstitute;
using SampleProject.Application.Configuration.Emails;
using SampleProject.Application.Payments.SendEmailAfterPayment;
using SampleProject.Domain.Payments;
using SampleProject.UnitTests.SeedWork;
using Shouldly;
using Xunit;

namespace SampleProject.UnitTests.Domain.Customers.Rules
{
    public class SendEmailAfterPaymentCommandHandlerTests : TestBase
    {
        private readonly Fixture _fixture;
        public SendEmailAfterPaymentCommandHandlerTests() => _fixture = new Fixture();

        [Fact]
        public async Task SendEmailAfterPaymentCommandHandler_Handle_WhenPaymentFound_EmailNotificationShouldBeSent()
        {
            // Arrange
            var emailSender = Substitute.For<IEmailSender>();
            var paymentRepository = Substitute.For<IPaymentRepository>();

            var paymentId = _fixture.Create<PaymentId>();
            var payment = _fixture.Create<Payment>();

            paymentRepository.GetByIdAsync(paymentId).Returns(Task.FromResult(payment));
            var message = new SendEmailAfterPaymentCommand(Guid.NewGuid(), paymentId);

            // Act
            var sut = new SendEmailAfterPaymentCommandHandler(null, paymentRepository);
            await sut.Handle(message, CancellationToken.None);

            // Assert
            payment.EmailNotificationIsSent.ShouldBeTrue();
        }

    }
}