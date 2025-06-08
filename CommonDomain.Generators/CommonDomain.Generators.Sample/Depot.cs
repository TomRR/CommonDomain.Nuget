

using Generators.Attributes;

namespace CommonDomain.Generators.Sample;

[Entity]
public partial class Depot
{
    public Depot(DepotId id, DepotId type) : base(id)
    {
        Type = type;
    }

    public DepotId Type { get; set; }
}
[AggregateRoot]
public partial class Transaction
{
    public Transaction(TransactionId id) : base(id)
    {
    }
}
[ValueObject]
public partial record Tester
{
}