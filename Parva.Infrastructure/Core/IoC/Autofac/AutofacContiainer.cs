using Parva.Application.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Parva.Infrastructure.Core.IoC.Autofac
{
    public class AutofacContiainer : IIoCContainer
    {
        public void Config()
        {
            throw new NotImplementedException();
        }

        public T[] GetAllInstance<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public object GetInstance(Type type)
        {
            throw new NotImplementedException();
        }

        public T GetInstance<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public void Resgister(Type IService)
        {
            throw new NotImplementedException();
        }

        public void Resgister(Type IService, object instance, Lifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        public void Resgister(Type IService, Type TImpl, Lifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        public void Resgister<IService>() where IService : class
        {
            throw new NotImplementedException();
        }

        public void ResgisterAll(Type IService, Lifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        public void ResgisterProperties<IService>(Action<IService> acction) where IService : class
        {
            throw new NotImplementedException();
        }

        void IIoCContainer.Resgister<IService, TImpl>(Lifecycle lifecycle)
        {
            throw new NotImplementedException();
        }
    }
}
