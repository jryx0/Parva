using Parva.Application;
using Parva.Application.Interfaces.Repository;
using Parva.Domain.Models;
using Parva.Infrastructure.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BigData.JW
{
    public class AppInit
    {
        public static void BootStrap()
        {
            Assembly.LoadFrom(@"BigData.JW.dll");
            AppEngine.Init(new SimpleInjectorContiainer());
            // Parva.Application.AppEngine.Container.Resgister<IFormOpener, FormOpener>(Parva.Application.Core.IoC.Lifecycle.Singleton);

            AppEngine.Container.Config();
            
            SyncDataBase();
            GlobalEnviroment.InitEnviroment();
        }

        private static void SyncDataBase()
        {
            var repo = AppEngine.Container.GetInstance<IEFRepository<BaseDataType>>();
            var bst = repo.FindById(1);
        }
    }
}
