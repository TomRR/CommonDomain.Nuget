namespace CommonDomain.Sample.CodeLookups.Domain.Ids;

public readonly record struct AssetId : IEntityId<AssetId>
{
    private AssetId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    public static AssetId CreateUniqueId() => new AssetId(Guid.NewGuid());
    
    public static AssetId Create(Guid value) => new AssetId(value);
}