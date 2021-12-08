namespace Identity.Entity.Application;

public record ApplicationUsers
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    //Navigation Property
    //Relationships

    public virtual User.User User { get; set; }

    public virtual Application Application { get; set; }
}