using Parva.Application;
using Parva.Application.Interfaces.Repository;
using Parva.Domain.Models;
using Parva.Infrastructure.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BigData.JW
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BootStrap();

            Application.Run(new Form1());
        }

        private static void BootStrap()
        {
            AppEngine.Init(new SimpleInjectorContiainer());
            // Parva.Application.AppEngine.Container.Resgister<IFormOpener, FormOpener>(Parva.Application.Core.IoC.Lifecycle.Singleton);
            Parva.Application.AppEngine.Container.Config();

            //Sync Database
            SyncDataBase();
        }

        private static void SyncDataBase()
        {
            //    var repo = AppEngine.Container.GetInstance<IEFRepository<BaseDataType>>();
            //    var bst = repo.FindById(1);
        }
    }
}
