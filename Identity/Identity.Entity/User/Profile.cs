namespace Identity.Entity.User;

public record Profile
{
    [Key]
    public Guid Id { get; set; }

    public string Image { get; set; }

    [Required]
    public string Json { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }

    //Navigation Property
    //Relationships
    public virtual User User { get; set; }
}