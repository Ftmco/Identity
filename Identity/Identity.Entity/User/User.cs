using Identity.Entity.Application;

namespace Identity.Entity.User;

public record User
{ 

    [Key]
    public Guid Id { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ActiveCode { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public string Email { get; set; }

    public string MobileNo { get; set; }

    [Required]
    public DateTime RegisterDate { get; set; }

    //Navigation Property
    //Relationships

    public virtual ICollection<ApplicationUsers> ApplicationUsers { get; set; }

    public virtual ICollection<UserRoles> UserRoles { get; set; }

    public virtual ICollection<Session> Session { get; set; }
}