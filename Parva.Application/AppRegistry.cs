using Parva.Application.Core;
using Parva.Application.Core.IoC;
using Parva.Application.Services;
using Parva.Application.Services.MasterDetail;
using Parva.Domain.Models;

namespace Parva.Application
{
    public class AppRegistry : IIoCRegistry
    {
        public void Register()
        {
            //AppEngine.Container.Add(typeof(ICommonService<>), typeof(CommonService<>), Lifecycle.UniquePerRequest);
            //AppEngine.Container.Add<ISeedService, DefaultSeedService>(Lifecycle.UniquePerRequest);
            //AppEngine.Container.Add<ISettingService, SettingService>(Lifecycle.UniquePerRequest);
            //AppEngine.Container.Add<IAdminService, AdminService>(Lifecycle.UniquePerRequest);
            //AppEngine.Container.Add<IAccountService, AccountService>(Lifecycle.UniquePerRequest);

            AppEngine.Container.Resgister(typeof(IBaseObjectService<BaseDataType>), typeof(BaseObjectService<BaseDataType>));
            AppEngine.Container.Resgister(typeof(IBaseObjectService<DataValue>), typeof(BaseObjectService<DataValue>));
            AppEngine.Container.Resgister(typeof(IMasterDetailService<BaseDataType>), typeof(BaseDataTypeService));

        }
    }
}