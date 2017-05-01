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
using Parva.Domain.Core;
using System.Linq.Expressions;
using Parva.Utility.WinForm;

namespace Parva.Utility.WinForm 
{
    public partial class ParvaTreeControl<T> : UserControl where T : TreeBase<T>, new()
    {
        public Dictionary<string, ParvaNodeDetail<T>> _treeNodeDetails;
        public ParvaNodeDetail<T> _currentNodeDetail;
        public ContextMenuStrip _currentMenu;
        protected ImageList _currentImageList;

        public List<T> ModifiedNode;
        public ITreeService<T> parvaTreeService;

        public event TreeViewEventHandler AfterSelect;
        public event EventHandler LogInfoEvent;

        public ParvaTreeControl()
        {
            _treeNodeDetails = new Dictionary<string, ParvaNodeDetail<T>>();
            _currentMenu = null;
            _currentNodeDetail = null;
            _currentImageList = null;

            ModifiedNode = new List<T>();
            InitializeComponent();
        }

        private void ParvaTreeControl_Load(object sender, EventArgs e)
        {
            SetImgList(this.parvaImgList);
            SetMenu(this.parvaTreeMenu);
            //root
            //InitTrees(0, int.MaxValue);
        }

        #region InitTreeView
        protected void InitTreeView()
        {
            //if (parvaTreeService == null) return;
            //var treelist = parvaTreeService.GetTree(x => x.Status && !String.IsNullOrEmpty(x.Name));
            //if (treelist == null) return;
            //var treeNodes = BulidTreeViewById(treelist.ToList(), 1);


            ////  MaxId = treelist.Max(x => x.Id);
            //this.DisplayTree(treeNodes, null, true);
            //this.ptreeView.Tag = treelist;
        }

        public void InitTrees(Expression<Func<T, bool>> expression, bool bShowAllNode = true)
        {
            if (parvaTreeService == null) return;
            var treelist = parvaTreeService.GetTree(x => bShowAllNode ? true : x.Status && !String.IsNullOrEmpty(x.Name));
            if (treelist == null) return;

            ptreeView.Nodes.Clear();
            var tlist = treelist.Where(x => expression.Compile().Invoke(x));
            foreach (var t in tlist)
            {
                var treeNodes = BulidTreeViewById(treelist.ToList(), t.Id, int.MaxValue);
                DisplayTree(treeNodes, t, true);
            }

            this.ptreeView.Tag = treelist;
        }

        public void InitTrees(int level, bool bShowAllNode = false)
        {
            if (parvaTreeService == null) return;
            var treelist = parvaTreeService.GetTree(x => bShowAllNode ? true : x.Status && !String.IsNullOrEmpty(x.Name));
            if (treelist == null) return;

            ptreeView.Nodes.Clear();
            var tlist = treelist.Where(x => x.ParentId == 0);
            foreach (var t in tlist)
            {
                var treeNodes = BulidTreeViewById(treelist.ToList(), t.Id, level);
                DisplayTree(treeNodes, t, true);
            }
            this.ptreeView.Tag = treelist;
        }
        public void InitTrees(int ParentId, int level, bool bShowAllNode = false)
        {
            if (parvaTreeService == null) return;
            var treelist = parvaTreeService.GetTree(x => bShowAllNode ? true : x.Status && !String.IsNullOrEmpty(x.Name));
            if (treelist == null) return;

            ptreeView.Nodes.Clear();
            var tlist = treelist.Where(x => x.ParentId == ParentId);
            foreach (var t in tlist)
            {
                var treeNodes = BulidTreeViewById(treelist.ToList(), t.Id, level);
                DisplayTree(treeNodes, t, true);
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

            //if (!root.Status)
            //    tn.ForeColor = Color.Gray;
            SetNodeColor(tn, tn.Tag as T);

            tn.SelectedImageIndex = tn.ImageIndex = GetImgIndex(root);

            root.ModifyStatus = Domain.Core.BaseEntityStatus.Unchanged;
            return tn;
        }
        protected void DisplayTree(TreeNode node, T t, bool isExpand)
        {
            this.ptreeView.Nodes.Add(node);

            if (isExpand)                            
                node.Expand();
        }

        public void RegisterDetailView(string formName, ParvaNodeDetail<T> nodeDetail)
        {
            if (!_treeNodeDetails.Keys.Contains(formName))
            {
                _treeNodeDetails[formName] = nodeDetail;

                nodeDetail.FormBorderStyle = FormBorderStyle.None;
                nodeDetail.TopLevel = false;
                nodeDetail.Dock = DockStyle.Fill;
                nodeDetail.Visible = true;
                nodeDetail.ParentTreeView = this;
                nodeDetail.InitNodeDetail(this.ptreeView.Tag as T);

                nodeDetail.NodeDetailMsgEvent += NodeDetailMsgHandle;
                nodeDetail.ParvaNodeName = formName;
                
                _currentMenu = nodeDetail.GetMenu();
            }
            // splitContainer1.Panel2.Controls.Add(_treeNodeDetails[formName]);
        }
        #endregion
        #region TreeView Event
        private void ptreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelect?.Invoke(sender, e);
        }

        private void ptreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (ptreeView.SelectedNode != null)
            {
               //保存当前数据
                _currentNodeDetail?.SaveModify();

                //将上一个选中的节点背景色还原（原先没有颜色）
                ptreeView.SelectedNode.BackColor = Color.Empty;
                //还原前景色
                //ptreeView.SelectedNode.ForeColor = Color.Black;                
                SetNodeColor(ptreeView.SelectedNode, ptreeView.SelectedNode.Tag as T);
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

                tn.SelectedNode = info.Node;

                _currentMenu?.Show(tn, e.X, e.Y);
            }
        }

        private void ptreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (_currentNodeDetail != null && !_currentNodeDetail.AllowEdit())
                return;
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
                d.Value.SaveModify();
            }
        }
        #endregion

        #region Menu
        private void NewNode_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentNodeDetail != null && !_currentNodeDetail.AllowEdit())
                return;

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
                        t.ParentId = pt.Id;
                        t.Level = pt.Level + 1;
                        t.Seq = 10 * t.Parent.Seq + 1;
                    }
                    tns = info.Node.Nodes;
                }

                if (ptreeView.ImageList != null)
                {
                    node.SelectedImageIndex = node.ImageIndex = GetImgIndex(t);
                    //node.ImageIndex = t.Level < ptreeView.ImageList.Images.Count ? t.Level : ptreeView.ImageList.Images.Count - 1;
                    //node.SelectedImageIndex = node.ImageIndex;
                }

                node.Tag = t;

                //明细窗口判断
                if (_currentNodeDetail != null && !_currentNodeDetail.CanModifyNode(t)) return;

                tns.Add(node);

                this.ptreeView.SelectedNode = node;
                this.ptreeView.LabelEdit = true;

                if (this.ptreeView.SelectedNode.IsEditing == false)
                    this.ptreeView.SelectedNode.BeginEdit();
            }
        }
        private void DelAccount_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (_currentNodeDetail != null && !_currentNodeDetail.AllowEdit())
                return;
               

            var m = ((ToolStripMenuItem)sender).GetCurrentParent();
            if (m != null)
            {
                var info = this.ptreeView.HitTest(this.ptreeView.PointToClient(m.PointToScreen(new Point(0, 0))));
                if (info.Node == null) return;

                T t = info.Node.Tag as T;
                if (MessageBox.Show("将删除节点：" + t.Name + "及其子节点", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                t.ModifyStatus = BaseEntityStatus.Deleted;

                if (!_currentNodeDetail.CanModifyNode(t)) return;

                this.ptreeView.Nodes.Remove(info.Node);
                ModifiedNode.Add(t);
            }
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
                SetNodeColor(this.ptreeView.SelectedNode, pe.ArgData as T);
                //var node = this.ptreeView.SelectedNode;
                //if (node != null)
                //{
                //    //var t = pe.ArgData as T;
                //    //if (t == null) return;

                //    //node.Text = t.Name;
                //    //if (t.Status) node.ForeColor = Color.Black;
                //    //else node.ForeColor = Color.Gray;
                //}
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

            if (_currentNodeDetail != null)
            {
                if (_currentNodeDetail.ParvaNodeName == detailName) return;

                foreach (Control c in splitContainer1.Panel2.Controls)
                    if (c.GetType().BaseType == typeof(ParvaNodeDetail<T>))
                    {
                        if (c.GetType() != this._treeNodeDetails[detailName].GetType())
                        {                          
                            splitContainer1.Panel2.Controls.Remove(c);
                            splitContainer1.Panel2.Controls.Add(this._treeNodeDetails[detailName]);
                            break;
                        }
                    }
            }
            else
                splitContainer1.Panel2.Controls.Add(this._treeNodeDetails[detailName]);
             
            _currentNodeDetail = this._treeNodeDetails[detailName];
            _currentMenu = _currentNodeDetail.GetMenu();
        }
        public void SetNodeDetail(string detailName, T argData)
        {
            if (_treeNodeDetails.Keys.Contains(detailName))
            {
                _treeNodeDetails[detailName].SetData(argData);
            }
        }

        public void LogInfo(object sender, EventArgs e)
        {
            LogInfoEvent?.Invoke(sender, e);
        }

        #region　setting
        private int GetImgIndex(T root)
        {
            if (ptreeView.ImageList == null)
                return 0;
                        
            return root.Level < ptreeView.ImageList.Images.Count ? root.Level : ptreeView.ImageList.Images.Count - 1;
        }
        private void SetMenu(ContextMenuStrip parvaTreeMenu)
        {
            this._currentMenu = parvaTreeMenu;
        }
        public void SetImgList(ImageList parvaImageList)
        {
            //this._currentImageList = parvaImageList;
            this.ptreeView.ImageList = parvaImageList;
        }
        private void SetNodeColor(TreeNode node, T t)
        {
            if (t == null ||node == null) return;

            node.Text = t.Name;
            if (t.Status) node.ForeColor = Color.Black;
            else node.ForeColor = Color.LightGray;

            //ToDo: expand
            if (t.Level < 2)
                node.Expand();

        }
        public TreeNode GetCurrentNode()
        {
            return ptreeView.SelectedNode;
        }
        public T GetCurrentData()
        {
            var node = GetCurrentNode();
            if (node == null) return null;

            return node.Tag as T;
        }
        public string GetNodePath(T t, Expression<Func<T, string>> expression, string separator)
        {
            if (_currentNodeDetail == null)
                return "";

            return _currentNodeDetail.GetNodePath(t, expression, separator);
        }
        public void RefreshDetail()
        {           
            _currentNodeDetail.SetData(GetCurrentData());
        }
        #endregion

        public List<T> GetModifiedData()
        {
            ModifiedNode.RemoveAll(x => x.ModifyStatus != BaseEntityStatus.Deleted);
            _currentNodeDetail.SaveModify();
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

        public void AcceptChange()
        {
            foreach (var t in ModifiedNode)
                t.ModifyStatus = BaseEntityStatus.Unchanged;
            ModifiedNode.Clear();
        }   
        public void SaveChanges()
        {
            if (!_currentNodeDetail.SaveChanges())
                return;
 
            this.parvaTreeService.SaveChanges(GetModifiedData().AsQueryable());
            AcceptChange();
        }
    }
}
