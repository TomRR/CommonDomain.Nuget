using CommonDomain.CodeLookup.Domain.Entities;
using CommonDomain.CodeLookup.Domain.Ids;

namespace CommonDomain.CodeLookup.Domain.AggreagteRoots;

public interface IDomainEvent { /* Marker interface, might have Timestamp etc. */ }

public abstract class AggregateRoot<TId> : Entity<TId> where TId : IEntityId<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
        // Log event addition, etc.
    }
    
    protected AggregateRoot(TId id) : base(id)
    {
    }

    protected AggregateRoot() : base(TId.CreateUniqueId()) { }
}