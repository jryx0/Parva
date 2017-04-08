using Parva.Domain.Core;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Parva.Application.Services.MasterDetail
{
    public interface IMasterDetailService<TMaster> where TMaster : BaseEntity
    {
        string Name { get; }

        Dictionary<string, Type> GetDetailInfo();
        ParvaAttribute GetCustomAttribute(string Name);

        IQueryable<TMaster> GetMaster(Expression<Func<TMaster, bool>> predict, bool bIncludeDetails = true); 
        void IncludeDetail(IQueryable<TMaster> entities, string detailName);
        void IncludeDetail(TMaster entity, string detailName);
        IQueryable<TDetail> GetDetail<TDetail>(string detailName);

        DataTable GetMasterTable();
        DataTable GetDetailTable(string detailName);
        void SaveChanges(List<TMaster> changedData);
    }

    /* 
       object GetDetail(Type _type);
       object[] GetDetail();
       object[] GetDetail(Expression<Func<TMaster, bool>> predict);


       List<TMaster> MapMaster(DataRowCollection rows);
       List<object> MapDetail(Type _type, DataRowCollection rows);
       Dictionary<string ,Type> GetDetailInfo();

       DataTable GetMasterDS(bool bIncludeDetails = true);
       DataTable GetMasterDS(Expression<Func<TMaster, bool>> predict, bool bIncludeDetails = true);

       DataTable GetDetailDS(string detailName);
       DataTable GetDetailDS(string detailName, Expression<Func<TMaster, bool>> predict);
       */
}
