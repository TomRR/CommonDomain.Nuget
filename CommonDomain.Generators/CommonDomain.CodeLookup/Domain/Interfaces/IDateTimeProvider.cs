namespace CommonDomain.CodeLookup.Domain.Interfaces;

public partial interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}