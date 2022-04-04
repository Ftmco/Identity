namespace Identity.DataBase.Entity;

public record User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string FullName { get; set; }

    public string Email { get; set; }

    public string MobileNo { get; set; }

    [Required]
    public string ActiveCode { get; set; }

    [Required]
    public bool IsActvie { get; set; }

    [Required]
    public DateTime RegisterDate { get; set; }

    //Navigation Proeprty
    //Relationships

    public virtual ICollection<Session> Sessions { get; set; }
}