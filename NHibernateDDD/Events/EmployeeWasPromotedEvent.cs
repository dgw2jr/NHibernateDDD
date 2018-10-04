using MediatR;

namespace NHibernateDDD.Events
{
    public class EmployeeWasPromotedEvent : INotification
    {
        public Employee Employee { get; set; }
    }
}