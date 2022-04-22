using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Abstraction;

public interface IFastAccountAction : IAsyncDisposable
{
    Task<LoginStatus> FastLoginAsync(FastLogin fastLogin);

    Task<LoginResponse> ActivationAsync(Activation activation);
}
