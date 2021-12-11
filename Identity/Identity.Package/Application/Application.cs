using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Package.Application;

public record Application(string ApiKey,string Password);

public record User(Guid Id, string UserName, string FullName, string Email, string MobileNo, DateTime RegisterDate);

public record UserResponse(IEnumerable<User> Users);