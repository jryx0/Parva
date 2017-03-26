using System;
using System.Linq.Expressions;

namespace Parva.Application.Core.IoC
{
    public interface IIoCContainer
    {
        void Resgister<IService, TImpl>(Lifecycle lifecycle) where TImpl : class, IService where IService : class;

        void Resgister(Type IService, Type TImpl, Lifecycle lifecycle);

        void Resgister(Type IService, object instance, Lifecycle lifecycle);
        void Resgister(Type IService);
        void Resgister<IService>() where IService : class;
        void ResgisterProperties<IService>(Action<IService> acction) where IService : class;
       

        void ResgisterAll(Type IService , Lifecycle lifecycle);

        void Config();

        object GetInstance(Type type);

        T GetInstance<T>() where T : class;

        T[] GetAllInstance<T>() where T : class;
    }
}