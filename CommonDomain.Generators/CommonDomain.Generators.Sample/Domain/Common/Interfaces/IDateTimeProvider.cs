using System;

namespace CommonDomain.Generators.Sample.Domain.Common.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}