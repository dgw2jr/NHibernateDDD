using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate.Event;

namespace NHibernateDDD
{
    public class TestListener : IPostLoadEventListener, IPostUpdateEventListener, IPostInsertEventListener
    {
        private readonly IMediator _mediator;

        public TestListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void OnPostInsert(PostInsertEvent @event)
        {
            DispatchEvents(@event.Entity as Entity);
        }

        public Task OnPostInsertAsync(PostInsertEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void OnPostLoad(PostLoadEvent @event)
        {
            Console.WriteLine(@event.Entity.ToString());
        }

        public void OnPostUpdate(PostUpdateEvent @event)
        {
            DispatchEvents(@event.Entity as Entity);
        }

        public Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void DispatchEvents(Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            foreach (var domainEvent in entity.DomainEvents)
            {
                _mediator.Publish(domainEvent);
            }

            entity.DomainEvents.Clear();
        }
    }
}