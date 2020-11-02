using System;

namespace Entities.Dto.Todo
{
    public class TodoItemUpdateInput
    {
        public Guid Id { get; set; }
        public bool isSelected { get; set; } = false;
        public string Content { get; set; }
    }
}
