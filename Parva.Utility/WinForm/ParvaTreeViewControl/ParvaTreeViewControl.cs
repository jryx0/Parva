using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parva.Application.Services.TreeService;
using Parva.Domain.Models;
using Parva.Domain.Core;

namespace Parva.Utility.WinForm 
{
    public partial class ParvaTreeViewControl<T> : UserControl where T : TreeBase<T>, new()
    {
        /// <summary>
        /// 节点操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public delegate bool TreeNodeChange(T data);
        public Dictionary<string, TreeNodeChange> NodeChange;
        public delegate int TreeNodeImgIndex(T data);
        public Dictionary<string, TreeNodeImgIndex> NodeImgIndex;

        public event TreeViewEventHandler AfterSelect;
        public event EventHandler ContextMenuEvent;
        public event EventHandler NewNodeEvent;

        /// <summary>
        /// 保存属性窗口
        /// </summary>
        protected Dictionary<string, ParvaTreeNodeDetail> _treeNodeDetails;
        private ParvaTreeNodeDetail _currentNodeDetail;
        private ContextMenuStrip _currentMenu;
        public ImageList parvaImageList;

        public List<T> ModifiedNode;
        public ITreeService<T> parvaTreeService;

        public ParvaTreeViewControl()
        {
            _treeNodeDetails = new Dictionary<string, ParvaTreeNodeDetail>();
            NodeChange = new Dictionary<string, TreeNodeChange>();
            NodeImgIndex = new Dictionary<string, TreeNodeImgIndex>();
            _currentMenu = cmsTreeMenu;
            _currentNodeDetail = null;

            ModifiedNode = new List<T>();

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.parvaImageList != null)
                this.ptreeView.ImageList = this.parvaImageList;

            SetMenu(this.cmsTreeMenu);

            //root
            InitTrees(0, int.MaxValue);
            
            base.OnLoad(e);
        }

        #region InitTreeView
        protected void InitTreeView()
        {
            if (parvaTreeService == null) return;
            var treelist = parvaTreeService.GetTree(x => x.Status && !String.IsNullOrEmpty(x.Name));
            if (treelist == null) return;
            var treeNodes = BulidTreeViewById(treelist.ToList(), 1);
            

            //  MaxId = treelist.Max(x => x.Id);
            this.DisplayTree(treeNodes, 1, true);
            this.ptreeView.Tag = treelist;
        }

        public void InitTrees(int level)
        {
            if (parvaTreeService == null) return;
            var treelist = parvaTreeService.GetTree(x => x.Status && !String.IsNullOrEmpty(x.Name));
            if (treelist == null) return;

            var tlist = treelist.Where(x => x.ParentId == 0);
            foreach (var t in tlist)
            {
                var treeNodes = BulidTreeViewById(treelist.ToList(), t.Id, level);

                DisplayTree(treeNodes, 1, true);
            }

            this.ptreeView.Tag = treelist;
        }

        public void InitTrees(int ParentId, int level)
        {
            if (parvaTreeService == null) return;
            var treelist = parvaTreeService.GetTree(x => x.Status && !String.IsNullOrEmpty(x.Name));
            if (treelist == null) return;

            var tlist = treelist.Where(x => x.ParentId == ParentId);
            foreach (var t in tlist)
            {
                var treeNodes = BulidTreeViewById(treelist.ToList(), t.Id, level);
                DisplayTree(treeNodes, 1, true);
            }

            this.ptreeView.Tag = treelist;
        }

        protected TreeNode BulidTreeViewById(List<T> treelist, int startId, int level = int.MaxValue)
        {
            var t = treelist.Find(x => x.Id == startId);
            if (t == null) return null;

            t.Parent = treelist.Find(x => x.Id == t.ParentId);
            t.Child = treelist.FindAll(x => x.ParentId == t.Id && x.Level <= level);

            var node = CreateTreeView(t);
            foreach (var c in t.Child)
                node.Nodes.Add(BulidTreeViewById(treelist, c.Id, level));

            return node;
        }
        protected TreeNode CreateTreeView(T root)
        {
            if (root == null)
                return null;

            TreeNode tn = new TreeNode();
            tn.Text = root.Name;
            tn.Tag = root;

            if (!root.Status)
                tn.ForeColor = Color.Gray;

            //tn.ImageIndex = root.Level;
            //tn.SelectedImageIndex = root.Level;
            tn.SelectedImageIndex = tn.ImageIndex = GetImgIndex(root);            

            root.ModifyStatus = Domain.Core.BaseEntityStatus.Unchanged;

            return tn;
        }
        protected void DisplayTree(TreeNode node, int Level, bool isExpand)
        {
            this.ptreeView.Nodes.Add(node);

            if (isExpand)
                node.Expand();
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
                nodeDetail.parentView = this.ptreeView;

                NodeChange[formName] = new TreeNodeChange(nodeDetail.NodeChange);
                NodeImgIndex[formName] = new TreeNodeImgIndex(nodeDetail.ImgIndex);
                
                _currentMenu = nodeDetail.GetMenu();
            }
            // splitContainer1.Panel2.Controls.Add(_treeNodeDetails[formName]);
        }
        #endregion        

        /// <summary>
        /// 接受detail事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NodeDetailMsgHandle(object sender, EventArgs e)
        {
            var pe = e as ParvaEventArg;
            if (pe == null)
                return;
            else if (pe.ArgType == ParvaTreeViewEnum.ModifyNode)
            {
                var node = this.ptreeView.SelectedNode;
                if (node != null)
                {
                    var t = pe.ArgData as T;
                    node.Text = t.Name;                    
                    if (t.Status) node.ForeColor = Color.Black;
                    else node.ForeColor = Color.Gray;
                }

            }
            else if (pe.ArgType == ParvaTreeViewEnum.NewNode)
                NewNode_ToolStripMenuItem_Click(sender, e);
            else if (pe.ArgType == ParvaTreeViewEnum.DeleteNode)
                DelAccount_ToolStripMenuItem_Click(sender, e);
        }

        public void SwitchNodeDetail(string detailName)
        {
            if (!this._treeNodeDetails.Keys.Contains(detailName))
                return;

            //if (splitContainer1.Panel2.Controls.Count == 0)
            //    splitContainer1.Panel2.Controls.Add(this._treeNodeDetails[detailName]);

            bool isNodeAdded = false;
            foreach (Control c in splitContainer1.Panel2.Controls)
                if (c.GetType().BaseType == typeof(ParvaTreeNodeDetail))
                {
                    if (c.GetType() != this._treeNodeDetails[detailName].GetType())
                    {
                        //_currentNodeDetail.SaveChanges();
                        splitContainer1.Panel2.Controls.Remove(c);
                        splitContainer1.Panel2.Controls.Add(this._treeNodeDetails[detailName]);
                        isNodeAdded = true;
                        break;
                    }
                }
            if (!isNodeAdded)
                splitContainer1.Panel2.Controls.Add(this._treeNodeDetails[detailName]);

            _currentNodeDetail?.SaveChanges();
            _currentNodeDetail = this._treeNodeDetails[detailName];
        }
        public void SetNodeDetail(string detailName, T argData)
        {
            if (_treeNodeDetails.Keys.Contains(detailName))
            {
                _treeNodeDetails[detailName].SetData(argData);
            }
        } 

        #region treeEvent
        private void ptreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelect?.Invoke(sender, e);
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

        public ContextMenuStrip GetMenu()
        {
            return _currentMenu;
        }
        public void SetMenu(ContextMenuStrip menu)
        {
          //  if (menu != null)
                _currentMenu = menu;
         //   else _currentMenu = cmsTreeMenu;
        }
        private void ptreeView_MouseUp(object sender, MouseEventArgs e)
        {
            //var tn = (TreeView)sender;
            //var info = tn.HitTest(e.X, e.Y);

            //if (info.Location == TreeViewHitTestLocations.Label)
            //{
            if (e.Button == MouseButtons.Right)
            {
                var tn = (TreeView)sender;
                var info = tn.HitTest(e.X, e.Y);

                ParvaEventArg pe = new ParvaEventArg();
                pe.EventName = "AfterSelect";
                pe.ArgType = ParvaTreeViewEnum.RightMouseClick;
                pe.ArgData = info.Node?.Tag;

                tn.SelectedNode = info.Node;
                ////Set Menu               
                if(_currentMenu == null)
                    ContextMenuEvent?.Invoke(sender, pe);

                _currentMenu?.Show(tn, e.X, e.Y);
            }
            //}
        }

        private void ptreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // if (e.Node.Nodes.Count == 0 )
            {//控制编辑级别
                this.ptreeView.LabelEdit = true;
                if (this.ptreeView.SelectedNode.IsEditing == false)
                    this.ptreeView.SelectedNode.BeginEdit();
            }
        }

        private void ptreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {            
            foreach (var d in _treeNodeDetails)
            { 
                d.Value.LabelChange(e.Label == null ? "新项目" : e.Label);
                d.Value.SaveChanges();
            }
             
        }
        #endregion

        #region MenuEvent
        private void DelAccount_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var m = ((ToolStripMenuItem)sender).GetCurrentParent();
            if (m != null)
            {
                var info = this.ptreeView.HitTest(this.ptreeView.PointToClient(m.PointToScreen(new Point(0, 0))));
                if (info.Node == null) return;

                T b = info.Node.Tag as T;
                if (MessageBox.Show("将删除节点：" + b.Name + "及其子节点", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;                

                b.ModifyStatus = BaseEntityStatus.Deleted;

                if (!CanModifyNode(b)) return;
                this.ptreeView.Nodes.Remove(info.Node);
                ModifiedNode.Add(b);                
            }
        }
        private void NewNode_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var m = ((ToolStripMenuItem)sender).GetCurrentParent();
            if (m != null)
            {
                var info = this.ptreeView.HitTest(this.ptreeView.PointToClient(m.PointToScreen(new Point(0, 0))));
                var tlist = this.ptreeView.Tag as List<T>;
                TreeNodeCollection tns = this.ptreeView.Nodes;

                T t = new T();
                //ToDo:动态更新Combobox
                tlist?.Add(t);

                t.Name = "新项目";
                t.ModifyStatus = BaseEntityStatus.NewEntity;
                t.Level = 0;
                t.Parent = null;
                t.Seq = 0;
                t.Status = true;

                TreeNode node = new TreeNode();
                node.Text = t.Name;
                if (info.Node != null)
                {
                    T pt = info.Node.Tag as T;
                    if (pt != null)
                    {                     
                        t.Parent = pt;
                        t.Level = pt.Level + 1;
                        t.Seq = t.Parent.Seq + 1;  
                    }                    
                    tns = info.Node.Nodes;
                }

                if (parvaImageList != null)
                {
                    //node.SelectedImageIndex = node.ImageIndex = GetImgIndex(t);
                    node.ImageIndex = t.Level < parvaImageList.Images.Count ? t.Level : parvaImageList.Images.Count - 1;
                    node.SelectedImageIndex = node.ImageIndex;
                }

                node.Tag = t;

                //明细窗口判断
                if (!CanModifyNode(t)) return;
               
                tns.Add(node); 
                //主窗口处理               
                NewNodeEvent?.Invoke(node, e);
                
                this.ptreeView.SelectedNode = node;
                this.ptreeView.LabelEdit = true;
                
                if (this.ptreeView.SelectedNode.IsEditing == false)
                    this.ptreeView.SelectedNode.BeginEdit();
            }
        }
        #endregion
        
        public T GetCurrentSelectData()
        {
            var tn = ptreeView.SelectedNode;
            if (tn == null) return null;

            return tn.Tag as T;
        }

        public List<T> GetModifiedData()
        {
            ModifiedNode.RemoveAll(x => x.ModifyStatus != BaseEntityStatus.Deleted);
            _currentNodeDetail.SaveChanges();
            foreach (TreeNode node in this.ptreeView.Nodes)
                GetModifiedData(node);

            return ModifiedNode;
        }
        protected void GetModifiedData(TreeNode node)
        {
            T data = node?.Tag as T;
            if (data?.ModifyStatus != BaseEntityStatus.Unchanged)
                ModifiedNode.Add(data);

            foreach (TreeNode n in node?.Nodes)
                GetModifiedData(n);
        }

        private int GetImgIndex(T t)
        {
            int index =_currentNodeDetail.ImgIndex(t);
           
            return index < parvaImageList.Images.Count ? index : parvaImageList.Images.Count - 1;
        }

        private bool CanModifyNode(T t)
        {
            bool bRet = true;
            foreach (var p in NodeChange)
                bRet = bRet && p.Value(t);

            return bRet;
        }
        public void AcceptChange()
        {
            foreach (var t in ModifiedNode)
                t.ModifyStatus = BaseEntityStatus.Unchanged;
            ModifiedNode.Clear();
        }
    }
}
