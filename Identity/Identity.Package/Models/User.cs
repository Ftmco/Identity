namespace Identity.Entity.User;

public record User
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string UserName { get; set; }

    public bool IsActive { get; set; }

    public string Email { get; set; }

    public string MobileNo { get; set; }

    public DateTime RegisterDate { get; set; }
}

public record GetAllUsers(ActionStatus Status,IEnumerable<User> Users);

public record GetUser(ActionStatus Status,User User);

public enum ActionStatus
{
    Success = 0,
    Exception = 1
}