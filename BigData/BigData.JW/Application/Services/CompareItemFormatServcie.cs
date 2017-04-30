using BigData.JW.Models;
using Parva.Application.Services.MasterDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parva.Application.Core;
using System.Data;

namespace BigData.JW.Services
{
    public class CompareItemFormatServcie : MasterDetailService<CompareItem>
    {
        public CompareItemFormatServcie(IBaseObjectService<CompareItem> masterService) : base(masterService)
        {

        }

        public override IQueryable<TDetail> GetDetail<TDetail>(string detailName)
        {
            throw new NotImplementedException();
        }

        public override DataTable GetDetailTable(string detailName)
        {
            throw new NotImplementedException();
        }

        public override void IncludeDetail(CompareItem entity, string detailName)
        {
            throw new NotImplementedException();
        }

        public override void IncludeDetail(IQueryable<CompareItem> entities, string detailName)
        {
            throw new NotImplementedException();
        }

        protected override void SaveDetailChanges(List<CompareItem> changeList, string key)
        {
            throw new NotImplementedException();
        }
    }
}
