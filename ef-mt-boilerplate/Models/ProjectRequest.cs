using System.ComponentModel.DataAnnotations;

public class ProjectRequest
{
    [Required]
    public string TenantId { get; set; }
    
    // Add Password if you want to check credentials
    // public string Password { get; set; }
}