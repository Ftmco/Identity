namespace Identity.Client.Models;

public record User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public bool IsActvie { get; set; }

    public DateTime RegisterDate { get; set; }

}