using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Package.Models;

public record ApiModel(int Code, bool Status, string Title, string Message, object Result);

public record ApiRequest(string Data);