namespace ef_mt_boilerplate.Models
{
	// This is just a "marker" interface for tenant-aware entities
	public interface ITenantEntity
	{
		int TenantId { get; set; }
	}
}
