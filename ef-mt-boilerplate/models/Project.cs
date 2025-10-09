namespace ef_mt_boilerplate.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }

        public int OwnerUserId { get; set; }
        public User Owner { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
