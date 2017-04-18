using Parva.Application.Services.TreeService;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parva.Application.Core;

namespace Parva.Application.Services
{
    public class DistrictService : TreeService<District>, IDistrictService
    {
        public DistrictService(IBaseObjectService<District> treeService) : base(treeService)
        {
        }
    }
}
