namespace Identity.DataBase.ViewModel;

public record ProfileViewModel(Guid ProfileId, Guid UserId, string FirstName, string LastName, UserViewModel User, IEnumerable<FileViewModel> Images);

public record UpdateProfile(string FirstName, string LastName, string Email, Guid? FileId, string? FileToken);

public record FileViewModel(Guid FileId, string FileToken)
{
    public string Base64 { get; set; } = " ";
}


public record ProfileResponse(ProfileStatus Status, ProfileViewModel? Profile);

public enum ProfileStatus
{
    Success = 0,
    NotFound = 1,
    Exception = 2,
    NotAuthorized = 3
}