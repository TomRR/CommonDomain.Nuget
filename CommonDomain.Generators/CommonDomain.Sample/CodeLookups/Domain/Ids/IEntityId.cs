     namespace CommonDomain.Sample.CodeLookups.Domain.Ids;

    public interface IEntityId<TId> where TId : IEntityId<TId>
    {
        Guid Value { get; }
        public static abstract TId CreateUniqueId();
        
        public static abstract TId Create(Guid value);
    }