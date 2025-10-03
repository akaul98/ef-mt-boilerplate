namespace ef_mt_boilerplate.models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int TenantId { get; set; }

        public Tenant Tenant { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
