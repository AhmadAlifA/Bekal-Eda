namespace Framework.Core.Entity
{
    public class BaseEntity
    {
        public Nullable<Guid> ModifiedBy { get; set; } = default!;
        public Nullable<DateTime> ModifiedDate { get; set; } = DateTime.Now;
    }
}
