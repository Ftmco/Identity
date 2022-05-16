namespace Identity.DataBase.Entity;

public record Role
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Title { get; set; }

    //Navigation Proeprty
    //Relationships

    public virtual ICollection<RolesUsers> RolesUsers { get; set; }
}
