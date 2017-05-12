using Parva.Application.Core;
using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BigData.JW.Services
{
    public class BigDataMasterDetailService<TMaster, TDetail> where TMaster : BaseEntity where TDetail : BaseEntity
    {
        protected IBaseObjectService<TMaster> _masterService;
        protected IBaseObjectService<TDetail> _detailService;

        public BigDataMasterDetailService(IBaseObjectService<TMaster> masterService, IBaseObjectService<TDetail> detailService)
        {
            _masterService = masterService;
            _detailService = detailService;
            _detailService._systemRepo = masterService._systemRepo;
        }

        public IQueryable<TMaster> Find(Expression<Func<TMaster, bool>> masterCondition, Expression<Func<TMaster, IQueryable<TDetail>>> express, Expression<Func<TDetail, int?>> detailCondition)
        {
            var entities = _masterService.Find(x => masterCondition.Compile().Invoke(x));

            if (express != null)
            {
                var Ids = entities.Select(x => x.Id);
                var detaillist = _detailService.FindByParentId(Ids);
                if (detaillist != null)
                    entities.ToList().ForEach(x =>
                                        {
                                            var list = express.Compile().Invoke(x);
                                            list = detaillist.Where(y => x.Id == detailCondition.Compile().Invoke(y));
                                        });
            }

            return entities;
        }
    }
}
