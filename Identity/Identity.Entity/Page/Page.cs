namespace Identity.Entity.Page;

public record Page
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Path { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }

    //Navigation Property
    //Relationships

    public virtual Application.Application Application { get; set; }

    public virtual ICollection<RolePages> RolePages { get; set; }
}
