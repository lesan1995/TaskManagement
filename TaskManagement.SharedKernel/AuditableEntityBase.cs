namespace TaskManagement.SharedKernel
{
    public class AuditableEntityBase<T, TId> : EntityBase<T, TId> where T : EntityBase<T, TId>
    {
        public string CreatedBy { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string? LastModifiedBy { get; private set; }
        public DateTime? LastModifiedAt { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public void SetCreated(string createdBy)
        {
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }
        public void SetModified(string modifiedBy)
        {
            LastModifiedBy = modifiedBy;
            LastModifiedAt = DateTime.UtcNow;
        }
        public void SoftDelete(string deletedBy)
        {
            IsDeleted = true;
            DeletedBy = deletedBy;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
