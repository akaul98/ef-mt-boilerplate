using System.ComponentModel.DataAnnotations;

public class CreateProjectResponse
{
    public string Status { get; set; }
    public string Message { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public int OwnerUserId { get; set; }
    public int TenantId { get; set; }


}