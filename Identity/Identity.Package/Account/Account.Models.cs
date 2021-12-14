using Identity.Package.Application;
using Identity.Package.Service;

namespace Identity.Package.Account;

#region -- Login --

public record Login(string UserName, string Password);

public record LoginResponse(Session Session) : ApiResponseBase;

public record Session(string Key, string Value, DateTime ExpireDate);

#endregion

#region -- SignUp --

public record SignUp(string UserName, string Password, string FullName, string Email, string MobileNo);

public record SignUpResponse(User User) : ApiResponseBase;

#endregion