using Parva.Application.Core;
using Parva.Application.Core.IoC;
using Parva.Application.Services;
using Parva.Application.Services.Account;
using Parva.Application.Services.MasterDetail;
using Parva.Application.Services.TreeService;
using Parva.Domain.Models;

namespace Parva.Application
{
    public class AppRegistry : IIoCRegistry
    {
        public void Register()
        {
            AppEngine.Container.Resgister(typeof(IBaseObjectService<BaseDataType>), typeof(BaseObjectService<BaseDataType>) );
            AppEngine.Container.Resgister(typeof(IBaseObjectService<DataValue>), typeof(BaseObjectService<DataValue>));
            AppEngine.Container.Resgister(typeof(IMasterDetailService<BaseDataType>), typeof(BaseDataTypeService));

            //AppEngine.Container.Resgister(typeof(IBaseObjectService<User>), typeof(BaseObjectService<User>));
            //AppEngine.Container.Resgister(typeof(IBaseObjectService<Role>), typeof(BaseObjectService<Role>));

            AppEngine.Container.Resgister(typeof(UserService));

            AppEngine.Container.Resgister(typeof(ITreeService<District>), typeof(TreeService<District>));
            AppEngine.Container.Resgister(typeof(IBaseObjectService<District>), typeof(BaseObjectService<District>));
        }
    }
}