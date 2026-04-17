namespace TaskManagement.SharedKernel
{
    public abstract class EntityBaseWithoutId : HasDomainEventsBase
    {
    }

    public abstract class EntityBase<T, TId> : HasDomainEventsBase where T : EntityBase<T, TId>
    {
        public TId Id { get; set; } = default!;
    }
}
