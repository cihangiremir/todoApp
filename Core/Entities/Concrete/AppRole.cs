namespace Core.Entities.Concrete
{
    public class AppRole : BaseEntity
    {
        public AppRole()
        {
            Type = typeof(AppRole).Name;
        }
        public string Name { get; set; }
    }
}
