using Identity.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.ViewModels.Application;

public record GetUserViewModel
{
    public ApplicationRequest? Application { get; set; }
}

public record GetUserByToken(string Token) : GetUserViewModel;

public record GetUserByUserName(string UserName) : GetUserViewModel;

public record GetUserResponse(GetUserStatus Status,UserViewModel User);

public enum GetUserStatus
{
    Success = 0,
    ApplicationNotFound = 1,
    Exception = 2,
    UserNotFound = 3,
}