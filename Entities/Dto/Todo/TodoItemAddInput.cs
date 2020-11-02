namespace Entities.Dto.Todo
{
    public class TodoItemAddInput
    {
        public bool isSelected { get; set; } = false;
        public string Content { get; set; }
    }
}
