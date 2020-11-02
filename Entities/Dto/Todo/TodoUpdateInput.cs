using System;
using System.Collections.Generic;

namespace Entities.Dto.Todo
{
    public class TodoUpdateInput
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public IList<TodoItemUpdateInput> TodoItems { get; set; } = new List<TodoItemUpdateInput>();
    }
}
