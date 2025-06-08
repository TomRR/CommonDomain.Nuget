namespace CommonDomain.Sample.CodeLookups.Domain.Interfaces;

public partial interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}