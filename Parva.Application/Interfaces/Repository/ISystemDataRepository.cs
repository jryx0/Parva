using Parva.Domain.Core;
using System.Data;

namespace Parva.Application.Interfaces.Repository
{
    public interface ISystemDataRepository
    {
        DataSet ExecuteDataset(string sql);        
        IDataReader ExecuteReader(string sql);
        object ExecuteScalar(string sql);

        int ExecuteNonQuery(string sql, IDbDataParameter[] parameters = null);

        void BeginTran();
        void Commit();
        void Rollback();
    }
}