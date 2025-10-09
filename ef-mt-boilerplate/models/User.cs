namespace ef_mt_boilerplate.Models
{
    public class User
    {
        public int UserId { get; set; }         // Primary key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
    }
}
