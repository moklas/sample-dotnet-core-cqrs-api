using System;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Payments
{
    public class Payment : Entity, IAggregateRoot
    {
        public PaymentId Id { get; private set; }

        private OrderId _orderId;

        private DateTime _createDate;

        private PaymentStatus _status;

        internal bool EmailNotificationIsSent;

        private Payment()
        {
            // Only for EF.
        }

        public Payment(OrderId orderId)
        {
            this.Id = new PaymentId(Guid.NewGuid());
            this._createDate = DateTime.UtcNow;
            this._orderId = orderId;
            this._status = PaymentStatus.ToPay;
            this.EmailNotificationIsSent = false;

            this.AddDomainEvent(new PaymentCreatedEvent(this.Id, this._orderId));
        }

        public void MarkEmailNotificationIsSent()
        {
            this.EmailNotificationIsSent = true;
        }
    }
}