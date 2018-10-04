using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernateDDD.Events;

namespace NHibernateDDD.Console
{
    internal class WriteToConsoleEmployeeWasPromotedNotificationHandler : INotificationHandler<EmployeeWasPromotedEvent>
    {
        public Task Handle(EmployeeWasPromotedEvent notification, CancellationToken cancellationToken)
        {
            System.Console.WriteLine($"{notification.Employee.Name} was promoted to {notification.Employee.EmploymentRole.GetType().Name}!");

            return Task.CompletedTask;
        }
    }
}
