namespace ef_mt_boilerplate.Entities
{
    public class Tenant
    {
        public int TenantId { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
