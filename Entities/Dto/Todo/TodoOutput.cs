using System;
using System.Collections.Generic;

namespace Entities.Dto.Todo
{
    public class TodoOutput
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public IList<TodoItemOutput> TodoItems { get; set; } = new List<TodoItemOutput>();
    }
}
