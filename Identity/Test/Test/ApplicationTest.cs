
using Identity.Service.Abstraction;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Injection;

namespace Test;

public class ApplicationTest
{
    IApplicationAction? _appAction;

    ServiceInjection _serviceInjection = new();

    [SetUp]
    public void Setup()
    {
        _appAction = _serviceInjection.GetService<IApplicationAction>(typeof(IApplicationAction));
    }

    [Test]
    public void InjectionTest()
    {
        if (_appAction != null)
            Assert.Pass("Injection Success");
        else Assert.Fail("Injection Fail");
    }
}
