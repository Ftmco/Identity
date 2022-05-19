namespace Identity.DataBase.Entity;

public record RolesUsers
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ApplicationsUsersId { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    //Navigation Proeprty
    //Relationships

    public virtual Role Role { get; set; }

    public virtual ApplicationsUsers ApplicationsUsers { get; set; }
}