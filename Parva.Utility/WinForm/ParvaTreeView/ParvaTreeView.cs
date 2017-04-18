using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parva.Domain.Models;
using Parva.Application.Services.TreeService;

namespace Parva.Utility.WinForm
{
    public partial class ParvaTreeView : UserControl
    {
        /// <summary>
        /// 向属性窗口发送消息
        /// </summary>
        public event EventHandler TreeMsgEvent; 
        /// <summary>
        /// 保存属性窗口
        /// </summary>
        protected Dictionary<string, ParvaTreeNodeDetail> _treeNodeDetails;
        private ParvaTreeNodeDetail _currentNodeDetail;
        private ContextMenuStrip _currentMenu;
        public ParvaTreeView()
        {
            _treeNodeDetails = new Dictionary<string, ParvaTreeNodeDetail>();
            _currentMenu = cmsTreeMenu;
            InitializeComponent();
        }

        #region TreeInit
        protected TreeNode BulidTreeViewById<T>(List<T> treelist, int startId) where T : TreeBase<T>
        {
            var t = treelist.Find(x => x.Id == startId);
            if (t == null) return null;

            t.Parent = treelist.Find(x => x.Id == t.ParentId);
            t.Child = treelist.FindAll(x => x.ParentId == t.Id);

            var node = CreateTreeView<T>(t);            
            foreach (var c in t.Child)
                node.Nodes.Add(BulidTreeViewById<T>(treelist, c.Id));

            return node;
        }

        protected TreeNode CreateTreeView<T>(T root) where T : TreeBase<T>
        {
            if (root == null)
                return null;

            TreeNode tn = new TreeNode();
            tn.Text = root.Name;
            tn.Tag = root;

            tn.ImageIndex = root.Level;
            tn.SelectedImageIndex = root.Level;

            root.ModifyStatus = Domain.Core.BaseEntityStatus.Unchanged;

            return tn;
        }
        
        protected void DisplayTree(TreeNode node, int Level, bool isExpand)
        {
            this.ptreeView.Nodes.Add(node);

            if (isExpand)
                node.Expand();
        }

        public virtual void InitTreeView<TTree>(ITreeService<TTree> _service ) where TTree : TreeBase<TTree>
        {
            var treelist = _service.GetTree(x => x.Status && !String.IsNullOrEmpty(x.Name));
            if (treelist == null) return;
            var treeNodes = BulidTreeViewById(treelist.ToList(), 1);

            this.DisplayTree(treeNodes, 1, true);
            this.ptreeView.Tag = treelist;      
        }

        public void RegisterDetailView(string formName, ParvaTreeNodeDetail nodeDetail)
        {
            if (!_treeNodeDetails.Keys.Contains(formName))
            {
                _treeNodeDetails[formName] = nodeDetail;

                nodeDetail.FormBorderStyle = FormBorderStyle.None;
                nodeDetail.TopLevel = false;
                nodeDetail.Dock = DockStyle.Fill;
                nodeDetail.Visible = true;
                nodeDetail.Parent = this;
                nodeDetail.InitNodeDetail(this.ptreeView.Tag);

                nodeDetail.NodeDetailMsgEvent += NodeDetailMsgHandle;
                TreeMsgEvent += new EventHandler(nodeDetail.NodeDetailMessageHandler);
            }
            // splitContainer1.Panel2.Controls.Add(_treeNodeDetails[formName]);
        }

        #endregion
        

        public void SetNodeDetail(string detailName, EventArgs argData)
        {
            if (_treeNodeDetails.Keys.Contains(detailName))
            {
                _treeNodeDetails[detailName].SetData(argData);
            }
        }


        public IQueryable<TreeNode> GetSaveData()
        { 

            return null;
        }

   
        public void NodeDetailMsgHandle(object sender, EventArgs e)
        {
            
        }

        public void SwitchNodeDetail(string detailName)
        {
            if (!this._treeNodeDetails.Keys.Contains(detailName))
                return;

            if(splitContainer1.Panel2.Controls.Count == 0)
                splitContainer1.Panel2.Controls.Add(this._treeNodeDetails[detailName]);

            foreach (Control c in splitContainer1.Panel2.Controls)
                if (c.GetType().BaseType == typeof(ParvaTreeNodeDetail))
                {
                    if (c.GetType() != this._treeNodeDetails[detailName].GetType())
                    {
                        _currentNodeDetail.SaveChanges();
                        splitContainer1.Panel2.Controls.Remove(c);
                        splitContainer1.Panel2.Controls.Add(this._treeNodeDetails[detailName]);
                        break;
                    }
                }

            _currentNodeDetail = this._treeNodeDetails[detailName];
        }

        #region tree event
        private void ptreeView_AfterSelect(object sender, TreeViewEventArgs e)
        { 

            ParvaEventArg pe = new ParvaEventArg();
            pe.EventName = "AfterSelect";
            pe.ArgType = ParvaTreeViewEnum.LeftMouseClick;
            pe.ArgData = ptreeView.SelectedNode?.Tag;
            
            TreeMsgEvent?.Invoke(sender, pe);
        }

        private void ptreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            ParvaEventArg pe = new ParvaEventArg();
            pe.EventName = "LabelEdit";
            pe.ArgType = ParvaTreeViewEnum.LabelEdit;
            pe.ArgData = ptreeView.SelectedNode?.Tag;

            TreeMsgEvent(e.Label == null ? "" : e.Label, pe);           
        }

        private void ptreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (ptreeView.SelectedNode != null)
            {
                //将上一个选中的节点背景色还原（原先没有颜色）
                ptreeView.SelectedNode.BackColor = Color.Empty;
                //还原前景色
                ptreeView.SelectedNode.ForeColor = Color.Black;
            }
        }

        private void ptreeView_Leave(object sender, EventArgs e)
        {
            if (ptreeView.SelectedNode != null)
            {
                ////让选中项背景色呈现红色
                //treeView1.SelectedNode.BackColor = treeView1.SelectedNode.BackColor == Color.Red ? Color.White : Color.Red;
                ////前景色为白色
                //treeView1.SelectedNode.ForeColor = treeView1.SelectedNode.ForeColor == Color.White ? Color.Black : Color.White;

                //让选中项背景色呈现红色
                ptreeView.SelectedNode.BackColor = Color.Red;
                //前景色为白色
                ptreeView.SelectedNode.ForeColor = Color.White;
            }
        }

        public void SetMenu(ContextMenuStrip menu)
        {
            if (menu != null)
                _currentMenu = menu;
            else _currentMenu = cmsTreeMenu;
        }
        private void ptreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var tn = (TreeView)sender;
                var info = tn.HitTest(e.X, e.Y);

                ParvaEventArg pe = new ParvaEventArg();
                pe.EventName = "AfterSelect";
                pe.ArgType = ParvaTreeViewEnum.RightMouseClick;
                pe.ArgData = info.Node?.Tag;

                //Set Menu
                TreeMsgEvent(sender, pe);                            
                _currentMenu?.Show(tn, e.X, e.Y);
            }
        }
        #endregion

        private void ptreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //this.ptreeView.SelectedNode.Collapse();
           // if (e.Node.Nodes.Count == 0 )
            {
                this.ptreeView.LabelEdit = true;
                if (this.ptreeView.SelectedNode.IsEditing == false)
                    this.ptreeView.SelectedNode.BeginEdit();
            }           
        }
    }
}
