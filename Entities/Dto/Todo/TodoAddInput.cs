using System;
using System.Collections.Generic;

namespace Entities.Dto.Todo
{
    public class TodoAddInput
    {
        public Guid UserId { get; set; }
        public IList<TodoItemAddInput> TodoItems { get; set; } = new List<TodoItemAddInput>();
    }
}
