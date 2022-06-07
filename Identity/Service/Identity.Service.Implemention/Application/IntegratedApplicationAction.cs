using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Implemention;

public class IntegratedApplicationAction : IIntegratedApplicationAction
{
    readonly IBaseCud<IntegeratedApplication,ApplicationContext>

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
