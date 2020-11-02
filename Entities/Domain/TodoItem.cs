using Core.Entities;

namespace Entities.Domain
{
    public class TodoItem : BaseEntity
    {
        public TodoItem()
        {
            Type = typeof(TodoItem).Name;
        }
        public bool isSelected { get; set; } = false;
        public string Content { get; set; }
    }
}
