using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parva.Application.Services.TreeService
{
    interface ITreeService<T> where T : TreeBase<T>
    {
        void  BuildTree(IQueryable<T> rawlist);
    }
}
