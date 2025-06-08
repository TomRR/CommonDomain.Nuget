// namespace PortfolioService.Domain.TransactionAggregate;
//
// public readonly record struct DepotId : IEntityId<DepotId>
// {
//     private DepotId(Guid value)
//     {
//         Value = value;
//     }
//     public Guid Value { get; }
//     public static DepotId CreateUniqueId() => new DepotId(Guid.NewGuid());
//     
//     public static DepotId Create(Guid value) => new DepotId(value);
// }