namespace Identity.Entity.Application;

public record Setting
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }

    //Navigation Property
    //Relationships

    public virtual Application Application { get; set; }
}