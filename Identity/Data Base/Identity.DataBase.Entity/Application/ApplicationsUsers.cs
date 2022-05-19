namespace Identity.DataBase.Entity;

public record ApplicationsUsers
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    //Navigation Proeprty
    //Relationships

    public virtual Application Application { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<RolesUsers> RolesUsers { get; set; }
}
