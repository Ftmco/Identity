namespace Identity.DataBase.Entity;

public record Profile
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    //Navigation Property
    //Relationships

    public virtual ICollection<ProfileImage> ProfileImages { get; set; }
}
