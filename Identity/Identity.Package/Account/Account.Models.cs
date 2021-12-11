using Identity.Package.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Package.Account;

public record Login(string UserName, string Password);

public record Session(string Key, string Value, DateTime ExpireDate);