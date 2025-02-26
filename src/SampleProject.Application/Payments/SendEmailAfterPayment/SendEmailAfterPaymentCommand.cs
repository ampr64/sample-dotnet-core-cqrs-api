using System;
using MediatR;
using Newtonsoft.Json;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Payments;

namespace SampleProject.Application.Payments.SendEmailAfterPayment
{
    [method: JsonConstructor]
    public class SendEmailAfterPaymentCommand(Guid id, PaymentId paymentId) : InternalCommandBase<Unit>(id)
    {
        public PaymentId PaymentId { get; } = paymentId;
    }
}