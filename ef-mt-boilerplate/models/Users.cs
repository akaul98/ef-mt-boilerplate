namespace ef_mt_boilerplate.models
{
    public class Users
    
        {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string Role { get; set; }
    }
}
