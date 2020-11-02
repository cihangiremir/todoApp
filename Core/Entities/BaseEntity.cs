using System;

namespace Core.Entities
{
    public abstract class BaseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Type { get; protected set; }
    }
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Type { get; protected set; }
    }
}
