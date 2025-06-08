namespace CommonDomain.CodeLookup.Domain.Ids;

public readonly record struct TransactionId : IEntityId<TransactionId>
{
    private TransactionId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static TransactionId CreateUniqueId() => new TransactionId(Guid.NewGuid());
    
    public static TransactionId Create(Guid value) => new TransactionId(value);
}