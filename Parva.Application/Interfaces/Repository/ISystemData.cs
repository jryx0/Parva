using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parva.Application.Interfaces.Repository
{
    interface ISystemData<T>
    {
        DataSet ExecuteDataset(string commandText);
        int ExecuteNonQuery(string commandText);

        T Mapor();
    }
}
