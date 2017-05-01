using Parva.Application.Core;
using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Parva.Domain.Models;
using Parva.Application.Interfaces.Repository;
using System.Data;
using System.Linq.Expressions;

namespace Parva.Application.Services.MasterDetail
{
    public abstract class  MasterDetailService<T> : IMasterDetailService<T> where T : BaseEntity
    {
        protected IBaseObjectService<T> _masterService;
        protected Dictionary<string, Type> _detailServices;
        protected Dictionary<string, ParvaAttribute> _attributes;

        public string Name
        {
            get
            {
                return _masterService.Name;
            } 
        }

        public MasterDetailService(IBaseObjectService<T> masterService)
        {
            this._masterService = masterService;
            this._detailServices = new Dictionary<string, Type>();
            this._attributes = new Dictionary<string, ParvaAttribute>();

            InitDetailInfo();
        }

        #region DetailInfo
        private void InitDetailInfo()
        {
            var typenames = typeof(T).GetProperties()
              .Where(x =>
              {
                  var p = x.PropertyType.GetInterfaces().Where(y => y.Name == typeof(ICollection<>).Name).FirstOrDefault();
                  if (p == null)
                      return false;
                  else
                      return true;
              });

            foreach (var _type in typenames)
            {
                if (_type.PropertyType.IsGenericType)
                {
                    var args = _type.PropertyType.GetGenericArguments();
                    if (args != null && args.Length > 0)
                    {
                        //var servicetype = typeof(IBaseObjectService<>).MakeGenericType(args[0]);
                        _detailServices[_type.Name] = args[0];

                        foreach (ParvaAttribute attr in _type.GetCustomAttributes(typeof(ParvaAttribute), true))
                        {
                            _attributes[_type.Name] = attr;
                            break;
                        }

                        continue;
                    }
                }

               
            }
        }
        public Dictionary<string, Type> GetDetailInfo()
        {
            return _detailServices;
        }
        public ParvaAttribute GetCustomAttribute(string Name)
        {
            if (this._attributes.Keys.Contains(Name))
                return this._attributes[Name];
            else return null;
        }
        #endregion       

        public virtual IQueryable<T> GetMaster(Expression<Func<T, bool>> predict, bool bIncludeDetails = true)
        {
            var _masterlist = _masterService.Find(x => predict.Compile().Invoke(x)).OrderBy(x => x.Seq);

            if (bIncludeDetails && _detailServices!= null)
                foreach (var d in _detailServices)
                {                    
                    IncludeDetail(_masterlist, d.Key);
                }

            return _masterlist;
        }

        public DataTable GetMasterTable()
        {
            return _masterService.GetDataTable();
        }
        public abstract DataTable GetDetailTable(string detailName); 

        public abstract void IncludeDetail(IQueryable<T> entities, string detailName);
        public abstract void IncludeDetail(T entity, string detailName);         
        public abstract IQueryable<TDetail> GetDetail<TDetail>(string detailName) where TDetail : BaseEntity;
        protected abstract void SaveDetailChanges(List<T> changeList, string key);

        public virtual void SaveChanges(List<T> changeList)
        {
            if (changeList == null || changeList.Count == 0)
                return;

            this._masterService.BeginTrans();
            try
            {
                this._masterService.SaveChanges(changeList
                                                    .Where(x => x.ModifyStatus == BaseEntityStatus.NewEntity ||
                                                                x.ModifyStatus == BaseEntityStatus.Modefied));

                foreach (var d in _detailServices)
                    SaveDetailChanges(changeList, d.Key);

                this._masterService.SaveChanges(changeList
                                                   .Where(x => x.ModifyStatus == BaseEntityStatus.Deleted));

                this._masterService.Commit();
            }
            catch (Exception ex)
            {
                this._masterService.Rollback();
            }
        }

      
    } 
}
