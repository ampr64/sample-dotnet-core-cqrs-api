using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders.Events;
using SampleProject.Domain.Payments;

namespace SampleProject.Application.Orders.PlaceCustomerOrder;

public class OrderPlacedDomainEventHandler(IPaymentRepository paymentRepository) : INotificationHandler<OrderPlacedEvent>
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;

    public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        var newPayment = new Payment(notification.OrderId);

        await _paymentRepository.AddAsync(newPayment);
    }
}