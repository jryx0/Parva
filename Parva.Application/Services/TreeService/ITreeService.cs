using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Parva.Application.Services.TreeService
{
    public interface ITreeService<T> where T : TreeBase<T>
    {
        IQueryable<T> GetTree(Expression<Func<T, bool>> expression);

        T GetTreeNode(int Id);
        T GetTreeById(int Id);
        IQueryable<T> GetChild(int Id);
        T GetParent(int id);

       void  SaveChanges(IQueryable<T> savingList);
    }
}
