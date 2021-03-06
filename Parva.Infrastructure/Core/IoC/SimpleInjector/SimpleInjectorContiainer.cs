﻿using Parva.Application.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleInjector;
using System.Linq.Expressions;
using SimpleInjector.Lifestyles;
using System.Reflection;

namespace Parva.Infrastructure.Core.IoC
{
    public class SimpleInjectorContiainer : IIoCContainer
    {        
        private SimpleInjector.Container _container;
      

        public SimpleInjectorContiainer()
        {
            _container = new SimpleInjector.Container();          

            _container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

        }

        private Lifestyle GetLifecyle(Lifecycle lifecycle)
        {
            var life = (lifecycle == Lifecycle.Transient)
                ? Lifestyle.Transient
                : (lifecycle == Lifecycle.Singleton ? Lifestyle.Singleton
                : Lifestyle.Scoped);//  Lifestyle.Scoped);
            return life;
        }
             
        public T[] GetAllInstance<T>() where T : class
        {
            return _container.GetAllInstances<T>().ToArray<T>();
        }

        public object GetInstance(Type type)
        {
            return _container.GetInstance(type);
        }

        public T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }

        public void Resgister<IService, TImpl>(Lifecycle lifecycle = Lifecycle.Transient) where TImpl : class, IService where IService : class
        {
            _container.Register<IService, TImpl>(GetLifecyle(lifecycle));
        }

        public void Resgister(Type IService, Type TImpl, Lifecycle lifecycle)
        {
            _container.RegisterConditional(IService, TImpl, GetLifecyle(lifecycle), c => !c.Handled);
        }

        public void Resgister(Type IService, object instance, Lifecycle lifecycle)
        {
            _container.Register(IService, () => instance, GetLifecyle(lifecycle));
        }

        public void Config()
        {
            Resgister(typeof(IIoCContainer), this, Lifecycle.Singleton);

            var asslist = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var types = assembly.GetTypes().Where(t => typeof(IIoCRegistry).IsAssignableFrom(t) && t.IsClass).ToList();
                foreach (var item in types)
                {
                    var register = Activator.CreateInstance(item) as IIoCRegistry;
                    register.Register();
                }
            }

           

            _container.Verify();
        }

        public void ResgisterAll(Type IService, Lifecycle lifecycle)
        {
            _container.RegisterCollection(IService.GetType(), new[] { IService.GetType().Assembly });
        }

        public void Resgister(Type IService)
        {
            _container.Register(IService);
        }

        public void  Resgister<IService>(Lifecycle lifecycle = Lifecycle.Transient) where IService :class
        {
            _container.Register<IService>(GetLifecyle(lifecycle));
        }

       
    }
}
