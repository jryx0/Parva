using Parva.Application.Core;
using Parva.Application.Interfaces.Repository;
using System.Data;
using System;

namespace Parva.Infrastructure.Implementations.Repository.SystemData
{
    public class SystemDataRepository : ISystemDataRepository
    {
        private MySqlite _sqliteDB;         
        public SystemDataRepository(MySqlite sqlite)
        {
            this._sqliteDB = sqlite;           
        }

        public DataSet ExecuteDataset(string sql)
        {
            return _sqliteDB.ExecuteDataset(sql);
        }
        public IDataReader ExecuteReader(string sql)
        {
            return _sqliteDB.ExecuteReader(sql);
        }
        public int ExecuteNonQuery(string sql, IDbDataParameter[] parameters = null)
        {
            return parameters == null ?
                _sqliteDB.ExecuteNonQuery(sql) :
                _sqliteDB.ExecuteNonQuery(CommandType.Text, sql, parameters);
        }

        public void BeginTran()
        {
            _sqliteDB.BeginTran();
        }
        public void Commit()
        {
            _sqliteDB.Commit();
        }
        public void Rollback()
        {
            _sqliteDB.RollBack();
        }

        public object ExecuteScalar(string sql)
        {
            return _sqliteDB.ExecuteScalar(sql);
        }
    }
}
