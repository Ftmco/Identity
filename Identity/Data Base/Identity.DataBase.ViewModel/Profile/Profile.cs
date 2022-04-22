using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DataBase.ViewModel;

public record ProfileViewModel(Guid ProfileId, Guid UserId, string FirstName, string LastName,UserViewModel User, IEnumerable<FileViewModel> Images);

public record UpdateProfile(string FirstName,string LastName,Guid? FileId,string? FileToken);

public record FileViewModel(Guid FileId, string FileToken);

public record ProfileResponse(ProfileStatus Status,ProfileViewModel? Profile);

public enum ProfileStatus
{
    Success = 0,
    NotFound = 1,
    Exception = 2,
    NotAuthorized = 3
}