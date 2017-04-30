using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parva.Domain.Models;

namespace Parva.Utility.WinForm 
{
    public partial class ParvaTreeNodeDetail : Form
    {
        public TreeView parentView;
        public event EventHandler NodeDetailMsgEvent;
        public ParvaTreeNodeDetail()
        {
            if (!DesignMode)
                InitializeComponent();
            parentView = null;
        }

        public virtual void NodeDetailMessageHandler(object sender, EventArgs e)
        {
            NodeDetailMsgEvent(sender, e);
        }

        public virtual void SetData<T>(T data) { }

        public virtual void InitNodeDetail(object nodelist) { }

        public virtual void SaveChanges()
        {

        }

        public virtual void LabelChange(string Label) { }

        public virtual void RestorData() { }

        public virtual void NodeNameChanged(string name)
        {
            ParvaEventArg pe = new ParvaEventArg();
            pe.ArgType = ParvaTreeViewEnum.LabelEdit;
            pe.ArgData = name;
            NodeDetailMsgEvent(this, pe);
        }

        /// <summary>
        /// 判断是否允许对节点进行增删改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">当前节点</param>        
        /// <returns></returns>
        public virtual bool NodeChange<T>(T data)
        {
            return true;
        }

        /// <summary>
        /// 设置图标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual int ImgIndex<T>(T data)
        {
            return 0;
        }

        public virtual ContextMenuStrip GetMenu()
        {
            return null;
        }

        internal bool CanModifyNode<T>(T b) where T : TreeBase<T>, new()
        {
            throw new NotImplementedException();
        }
    }
    
}
