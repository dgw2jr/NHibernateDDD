using System.Collections.Generic;
using MediatR;

namespace NHibernateDDD
{
    public abstract class Entity
    {
        protected Entity()
        {
            DomainEvents = new HashSet<INotification>();
        }

        public virtual HashSet<INotification> DomainEvents { get; set; }
    }
}