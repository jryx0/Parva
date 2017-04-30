using Parva.Application.Core;
using Parva.Domain.Core;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Parva.Application.Services.TreeService
{
    public class TreeService<T> : ITreeService<T> where T : TreeBase<T>
    {
        IBaseObjectService<T> _treeService;
        public TreeService(IBaseObjectService<T> treeService)
        {
            this._treeService = treeService;
        }         

        public IQueryable<T> GetTree(Expression<Func<T, bool>> expression)
        {
           var treelist = _treeService
                .Find(x => expression.Compile().Invoke(x))
                .OrderBy(x => x.Level)
                .ThenBy(x => x.Seq);

            return treelist;                
        }

        protected IQueryable<T> BuildTree(IQueryable<T> rawlist)
        {        
            return rawlist.Where(x => x.Parent == default(T)) ;
        }

        protected T BuildTree(T t, IQueryable<T> rawlist)
        {
            if (t == null) return null;

            t.Parent = rawlist.ToList().Find(x => x.Id == t.ParentId);
            t.Child = rawlist.ToList().FindAll(x => x.ParentId == t.Id);
            return t;
        }

        public T GetTreeById(int Id)
        {
            var list = GetTree(x => x.Status).Where(x => x.Id == Id).FirstOrDefault();
            return list;
        }

        public IQueryable<T> GetChild(int Id)
        {
            return _treeService.Find(x => x.ParentId == Id);
        }
        public T GetParent(int Id)
        {
            T node = _treeService.FindById(Id);
            return node.ParentId.HasValue ? _treeService.FindById(node.ParentId.Value) : default(T);
        }
        public T GetTreeNode(int Id)
        {
            return _treeService.FindById(Id);
        }

        public void SaveChanges(IQueryable<T> savingList)
        {
            _treeService.BeginTrans();
            try
            {
                _treeService.SaveChanges(savingList.Where(x => x.ModifyStatus == BaseEntityStatus.NewEntity).OrderBy(x => x.Level));
                _treeService.SaveChanges(savingList.Where(x =>
                                                    x.ModifyStatus == BaseEntityStatus.Modefied
                                                    || x.ModifyStatus == BaseEntityStatus.Deleted));
                
                _treeService.Commit();
            }
            catch (Exception ex)
            {
                
                _treeService.Rollback();
            }
        }
    }
}
