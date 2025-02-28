using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Payments;
using System;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Payments.SendEmailAfterPayment;

[method: JsonConstructor]
public class SendEmailAfterPaymentCommand(Guid id, PaymentId paymentId) : InternalCommandBase<Unit>(id)
{
    public PaymentId PaymentId { get; } = paymentId;
}