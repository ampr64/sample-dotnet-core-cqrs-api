using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Emails;
using SampleProject.Domain.Payments;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Payments.SendEmailAfterPayment;

public class SendEmailAfterPaymentCommandHandler(
    IEmailSender emailSender,
    IPaymentRepository paymentRepository) : ICommandHandler<SendEmailAfterPaymentCommand>
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;

    public async Task Handle(SendEmailAfterPaymentCommand request, CancellationToken cancellationToken)
    {
        // Logic of preparing an email. This is only mock.
        var emailMessage = new EmailMessage("from@email.com", "to@email.com", "content");

        await _emailSender.SendEmailAsync(emailMessage);

        var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);

        payment.MarkEmailNotificationIsSent();
    }
}