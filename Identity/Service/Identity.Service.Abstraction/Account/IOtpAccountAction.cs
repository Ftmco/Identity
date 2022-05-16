using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Abstraction;

public interface IOtpAccountAction : IAsyncDisposable
{
    Task<LoginStatus> OtpLoginAsync(FastLogin fastLogin);

    Task<LoginResponse> ActivationAsync(Activation activation);
}
