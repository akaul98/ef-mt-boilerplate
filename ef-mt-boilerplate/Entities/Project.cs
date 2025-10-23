namespace ef_mt_boilerplate.Entities
{
    public class Project : ITenantEntity
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public int OwnerUserId { get; set; }
        public User Owner { get; set; }
        public int TenantId { get; set; }  // ITenantEntity
        public Tenant Tenant { get; set; }
    }
}
