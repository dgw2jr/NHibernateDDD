using System;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;

namespace NHibernateDDD
{
    public class TestListener : IPostLoadEventListener, IPostUpdateEventListener, IPostInsertEventListener
    {
        private readonly TextWriter _writer;

        public TestListener(TextWriter writer)
        {
            _writer = writer;
        }

        public void OnPostInsert(PostInsertEvent @event)
        {
            _writer.Write("Inserted!");
        }

        public Task OnPostInsertAsync(PostInsertEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void OnPostLoad(PostLoadEvent @event)
        {
            _writer.Write(@event.Entity.ToString());
        }

        public void OnPostUpdate(PostUpdateEvent @event)
        {
            _writer.Write("Updated!");
        }

        public Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}