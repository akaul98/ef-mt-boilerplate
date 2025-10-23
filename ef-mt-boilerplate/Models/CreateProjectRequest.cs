using ef_mt_boilerplate.Entities;
using System.ComponentModel.DataAnnotations;

public class CreateProjectRequest
{
    public string Title { get; set; }
    public int OwnerUserId { get; set; }
}