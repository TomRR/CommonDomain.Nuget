using CommonDomain.Sample.CodeLookups.Domain.Ids;

namespace CommonDomain.Sample.CodeLookups.Domain.Entities;

public abstract partial class Entity<TId> where TId : IEntityId<TId>
{
    protected TId Id { get; init; }
    protected Entity(TId id)
    {
        ArgumentNullException.ThrowIfNull(id);
        Id = id;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object? obj) => Equals(obj as Entity<TId>);
    private bool Equals(Entity<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other.GetType() != GetType()) return false;

        return Id.Equals(other.Id);
    }
}