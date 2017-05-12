using Parva.Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BigData.JW.Models;

namespace BigData.JW.Infrastructure.Implementation.Repository.SystemData.Mapping
{
    public class ItemDataFormatObject : IBaseObject<ItemDataFormat>
    {
        public ItemDataFormatObject()
        {
            Name = "ItemDataFormatObject";
        }

        public string DeleteSql
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ItemDataFormat Entity { get; set; }

        public string InsertSql
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name { set; get; }

        public string SelectSql
        {
            get
            {
                return @"SELECT Id,
                               ParentId,
                               ColInfoId,
                               ColIndex,       
                               Seq,
                               Status
                          FROM ItemDataFormat";
            }
        }

        public string UpdateSql
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbDataParameter[] getParameter()
        {
            throw new NotImplementedException();
        }

        public IQueryable<ItemDataFormat> Mapor(IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
