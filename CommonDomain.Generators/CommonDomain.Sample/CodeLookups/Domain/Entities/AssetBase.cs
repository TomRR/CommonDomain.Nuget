using CommonDomain.Sample.CodeLookups.Domain.AggreagteRoots;
using CommonDomain.Sample.CodeLookups.Domain.Ids;

namespace CommonDomain.Sample.CodeLookups.Domain.Entities;

public sealed partial class Asset : AggregateRoot<AssetId>;