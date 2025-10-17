using System.ComponentModel.DataAnnotations;

public class ProjectResponse
{
    public string status { get; set; }
    public string message { get; set; }
    public string projectId { get; set; }
    public string projectName { get; set; }

}