namespace Identity.Entity.User;

public record UserRoles
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    //Navigation Property
    //Relationships

    public virtual Role Role { get; set; }

    public virtual User User { get; set; }
}