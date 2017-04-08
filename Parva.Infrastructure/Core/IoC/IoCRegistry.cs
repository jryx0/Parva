using Parva.Application;
using Parva.Application.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parva.Infrastructure.Core.IoC
{
    public class IoCRegistry : IIoCRegistry
    {
        public void Register()
        {
            //AppEngine.Init(new SimpleInjectorContiainer());
        }
    }
}
