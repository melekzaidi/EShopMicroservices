using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstraction;

public interface IAggregate :IEntity
{
    IReadOnlyList<IDomainEvent> domainEvents { get; }
    IDomainEvent[] ClearDomainEvent();
}
