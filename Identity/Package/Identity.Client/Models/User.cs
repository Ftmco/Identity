namespace Identity.Client.Models;

public record User
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime RegisterDate { get; set; }

    public static User CreateUser(UserModel user)
    {
        return new()
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email,
            FullName = user.FullName,
            IsActive = user.IsActive,
            MobileNo = user.MobileNo,
            RegisterDate = DateTime.Parse(user.RegisterDate)
        };
    }
}