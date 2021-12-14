using Identity.Entity.User;

namespace Identity.Entity.Page;

public record RolePages
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid PageId { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    //Navigation Property
    //Relationships

    public virtual Page Page { get; set; }

    public virtual Role Role { get; set; }
}