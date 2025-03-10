using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Payments;
using System;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Payments.SendEmailAfterPayment;

[method: JsonConstructor]
public record SendEmailAfterPaymentCommand(Guid Id, PaymentId PaymentId)
    : InternalCommandBase(Id);