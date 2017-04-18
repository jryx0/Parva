using Parva.Application.Core.IoC;
using Parva.Application;
using Parva.Infrastructure.Implementations.Repository.SystemData;
using Parva.Application.Interfaces.Repository;
using Parva.Application.Core;
using Parva.Domain.Models;
using Parva.Infrastructure.Implementations.Repository.SystemData.Mapping;


namespace Parva.Infrastructure.Implementations.Repository
{
    public class SystemDataRegistry : IIoCRegistry
    {
        public void Register()
        {
            AppEngine.Container.Resgister<MySqlite>();
            AppEngine.Container.Resgister<ISystemDataRepository, SystemDataRepository>();

            AppEngine.Container.Resgister(typeof(Application.Core.IBaseObject<BaseDataType>), typeof(BaseTypeObject));
            AppEngine.Container.Resgister(typeof(Application.Core.IBaseObject<DataValue>), typeof(DataValueObject));

            AppEngine.Container.Resgister(typeof(Application.Core.IBaseObject<District>), typeof(DistrictObject));


        }
    }
}
