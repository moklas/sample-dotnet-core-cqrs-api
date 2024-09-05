using System;

namespace SampleProject.Domain.Customers.Orders
{
    public class TooLowValueException : Exception
    {
        public string Details { get; }
        public TooLowValueException(string message, string details) : base(message)
        {
            this.Details = details;
        }
    }
}