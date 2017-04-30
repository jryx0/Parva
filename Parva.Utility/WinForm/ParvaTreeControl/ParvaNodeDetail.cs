using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parva.Domain.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Parva.Utility.WinForm 
{
    public partial class ParvaNodeDetail<T> : Form where T : TreeBase<T>, new()
    {
        public ParvaTreeControl<T> ParentTreeView;
        protected T _currentDetail;
        public event EventHandler NodeDetailMsgEvent;
        public String ParvaNodeName;

        public ParvaNodeDetail()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="tag"></param>
        public virtual void InitNodeDetail(T tag)  
        {
            throw new Exception();
        }

        public virtual ContextMenuStrip GetMenu()
        {
            return null;
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveModify()
        {
            return false;
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveChanges(List<T> ChangeList)
        {
            return false;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="argData"></param>
        public virtual void SetData (T argData)   
        {
             
        }
        /// <summary>
        /// 是否允许修改节点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool CanModifyNode (T t)  
        {     
            return true;
        }
        /// <summary>
        /// 树节点文本修改通知
        /// </summary>
        /// <param name="v"></param>
        public virtual void LabelChange(string v)
        {
            
        }

        public void NodeDetailMessageHandler(object sender, EventArgs e)
        {
            NodeDetailMsgEvent?.Invoke(sender, e);
        }
        /// <summary>
        /// 获取从根到当前节点路径
        /// </summary>
        /// <param name="t"></param>
        /// <param name="expression">t中的字段</param>
        /// <param name="separator">分割附</param>
        /// <returns></returns>
        public virtual string GetNodePath(T t, Expression<Func<T, string>> expression, string separator)
        {           
            var path = expression.Compile().Invoke(t);
            if (t.Parent != null)
                path = GetNodePath(t.Parent, expression, separator) + separator + path;

            return path;
        }
        /// <summary>
        /// 是否允许修改树
        /// </summary>
        /// <returns></returns>
        public virtual bool AllowEdit()
        {
            return true;
        }


        public virtual void NodeDetailOperation(object sender, EventArgs e)
        {

        }

    }
}
