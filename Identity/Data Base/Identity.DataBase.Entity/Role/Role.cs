namespace Identity.DataBase.Entity;

public record Role
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }

    //Navigation Proeprty
    //Relationships

    public virtual ICollection<RolesUsers> RolesUsers { get; set; }

    public virtual ICollection<PagesRoles> PagesRoles { get; set; }

    public virtual Application Application { get; set; }

    public virtual ICollection<ActionsRoles> ActionsRoles { get; set; }
}
