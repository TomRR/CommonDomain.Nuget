using Generators.Attributes;

namespace CommonDomain.Sample;

[Entity]
public sealed partial class Depot
{
}
[ValueObject]
public readonly partial record struct TimeStamp
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
public sealed partial class Admin
{
}