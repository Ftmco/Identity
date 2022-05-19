namespace Identity.DataBase.Entity;

public record Setting
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public short CodeLength { get; set; } = 4;

    [Required]
    public Guid DefaultRole { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }

    //Relationships
    //Navigation Property

    public virtual Application Application { get; set; }
}