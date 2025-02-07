﻿
namespace Shared.DDD
{
    public abstract class Entity<T> : IEntity
    {
        public T Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get ; set; }
        public DateTime? LastModifiedAt { get ; set ; }
    }
}
