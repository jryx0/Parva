using Parva.Application.Core;
using Parva.Application.Core.IoC;
using Parva.Application.Services;
using Parva.Application.Services.Account;

namespace Parva.Application
{
    public class AppRegistry : IIoCRegistry
    {
        public void Register()
        {
            AppEngine.Container.Resgister(typeof(ICommonService<>), typeof(CommonService<>), Lifecycle.UniquePerRequest);
            AppEngine.Container.Resgister<ISeedService, DefaultSeedService>(Lifecycle.UniquePerRequest);


            AppEngine.Container.Resgister<IAccountService, AccountService>(Lifecycle.UniquePerRequest);
        }
    }
}