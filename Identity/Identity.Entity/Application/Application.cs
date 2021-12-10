using Identity.Entity.User;

namespace Identity.Entity.Application;

public record Application
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Image { get; set; }

    [Required]
    public string ApiKey { get; set; }
        
    [Required]
    public string Password { get; set; }

    [Required]
    public Guid OwnerId { get; set; }


    //Navigation Property
    //Relationships
    public virtual ICollection<ApplicationUsers> ApplicationUsers { get; set; }

    public virtual ICollection<Page.Page> Page { get; set; }
}
