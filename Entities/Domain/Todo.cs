using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Domain
{
    public class Todo : BaseEntity
    {
        public Todo()
        {
            Type = typeof(Todo).Name;
        }
        public Guid UserId { get; set; }
        public IList<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
