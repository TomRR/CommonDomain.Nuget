using CommonDomain.CodeLookup.Domain.AggreagteRoots;
using CommonDomain.CodeLookup.Domain.Ids;

namespace CommonDomain.CodeLookup.Domain.Entities;

public sealed partial class Asset : AggregateRoot<AssetId>
{
    private Asset(AssetId id) : base(id) { }
}