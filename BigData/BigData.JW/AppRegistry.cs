using BigData.JW.Services;
using BigData.JW.Infrastructure.Implementation.Repository.SystemData.Mapping;
using BigData.JW.Models;
using Parva.Application;
using Parva.Application.Core;
using Parva.Application.Core.IoC;
using Parva.Application.Services.MasterDetail;
using Parva.Application.Services.TreeService;

namespace BigData.JW
{
    public class AppRegistry : IIoCRegistry
    {
        public void Register()
        {
            AppEngine.Container.Resgister(typeof(IBaseObject<CompareItem>), typeof(CompareItemObject));
            AppEngine.Container.Resgister(typeof(ITreeService<CompareItem>), typeof(TreeService<CompareItem>));
            AppEngine.Container.Resgister(typeof(IBaseObjectService<CompareItem>), typeof(BaseObjectService<CompareItem>));

            AppEngine.Container.Resgister(typeof(IBaseObject<ItemFormat>), typeof(ItemFormatObject));
            AppEngine.Container.Resgister(typeof(IBaseObjectService<ItemFormat>), typeof(BaseObjectService<ItemFormat>));


            AppEngine.Container.Resgister(typeof(IBaseObject<ItemDataFormat>), typeof(ItemDataFormatObject));
            AppEngine.Container.Resgister(typeof(IBaseObjectService<ItemDataFormat>), typeof(BaseObjectService<ItemDataFormat>));

            AppEngine.Container.Resgister(typeof(BigDataMasterDetailService<ItemFormat, ItemDataFormat>));
            
        }
    }
}