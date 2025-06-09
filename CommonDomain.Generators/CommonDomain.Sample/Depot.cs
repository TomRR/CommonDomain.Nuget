using Generators.Attributes;

namespace CommonDomain.Sample;

[AggregateRoot]
public sealed partial class Depot
{
}
[Entity]
public sealed partial class Depot2
{
}
[ValueObject]
public readonly partial record struct TimeStamp2
{
}

[AggregateRoot]
public sealed partial class Transaction
{
}
[AggregateRoot]
public sealed partial class Asset
{
}
[AggregateRoot]
public sealed partial class Admin2
{
}