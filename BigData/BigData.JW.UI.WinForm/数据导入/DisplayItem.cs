using BigData.JW.Models;
using Parva.Domain.Core;
using Parva.Utility.WinForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BigData.JW.UI.WinForm
{
    public partial class DisplayItem : ParvaNodeDetail<CompareItem>
    {
        private bool _ValidForm;
        public DisplayItem()
        {
            InitializeComponent();
            tbName.Validating += Item_Validating;
            _ValidForm = true;
        }

        private void Item_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                errorProvider1.SetError(tbName, "请输入项目名称");
                _ValidForm = false;
            }
        }

        private void DisplayItem_Load(object sender, EventArgs e)
        {
            List<ComboItem> cItem = new List<ComboItem>();
            cItem.Add(new ComboItem { Name = "根目录", DataType = ItemType.Root });

            cItem.Add(new ComboItem { Name = "源项目", DataType = ItemType.Source });
            cItem.Add(new ComboItem { Name = "源数据", DataType = ItemType.SourceItem });

            cItem.Add(new ComboItem { Name = "比对项目", DataType = ItemType.Compare });
            cItem.Add(new ComboItem { Name = "比对数据", DataType = ItemType.CompareItem });

            cItem.Add(new ComboItem { Name = "关联数据", DataType = ItemType.Folder });
            cItem.Add(new ComboItem { Name = "数据", DataType = ItemType.Data });



            this.cbItemType.DataSource = cItem;
            this.cbItemType.DisplayMember = "Name";
            this.cbItemType.ValueMember = "DataType";          
        }


        public override void InitNodeDetail (CompareItem t)
        {
            SetData(t);            
        }
        public override ContextMenuStrip GetMenu()
        {
            return this.cmsItemDetail;
        }

        public override void SetData (CompareItem item)
        {             
            if (item == null) return;

            this.tbName.Text = item.Name;
            this.tbShortName.Text = item.ItemName; //short name
            this.cbItemType.SelectedValue = item.DataType;
           
            this.tbPath.Text = item.Path;
            this.tbTableName.Text = item.TableName;
            this.tbSeq.Text = item.Seq.ToString();
            this.tbDescrible.Text = item.Describle;
            this.cbStatus.Checked = item.Status;

            base._currentDetail = item;
        }
        public override void LabelChange(string v)
        {
            this.tbName.Text = v;
        }

        public override bool SaveModify()
        {
            if (!_currentDetail.HasModified && _currentDetail.ModifyStatus != BaseEntityStatus.NewEntity)
                return true;
            else if (_currentDetail.ModifyStatus == BaseEntityStatus.Unchanged)
                _currentDetail.ModifyStatus = BaseEntityStatus.Modefied;

            _currentDetail.HasModified = false;
            if (_currentDetail.Original == null)
            {
                _currentDetail.Original = new CompareItem();
                _currentDetail.Original.Id = _currentDetail.Id;
                _currentDetail.Original.Name = _currentDetail.Name;
                _currentDetail.Original.ParentId = _currentDetail.ParentId;
                _currentDetail.Original.ItemName = _currentDetail.ItemName;
                _currentDetail.Original.DataType = _currentDetail.DataType;
                _currentDetail.Original.Path = _currentDetail.Path;
                _currentDetail.Original.TableName = _currentDetail.TableName;
                _currentDetail.Original.Describle = _currentDetail.Describle;
                _currentDetail.Original.Seq = _currentDetail.Seq;
                _currentDetail.Original.Status = _currentDetail.Status;
            }

           

            _currentDetail.Name = tbName.Text;

            if (tbShortName.Text.Length == 0)
                _currentDetail.ItemName = tbName.Text;
            else 
                _currentDetail.ItemName = tbShortName.Text;

            if (this.tbPath.Text.Length == 0)
                _currentDetail.Path = tbName.Text;
            else _currentDetail.Path = tbPath.Text;

            if (tbTableName.Text.Length == 0)
                _currentDetail.TableName = tbName.Text;
            else 
                _currentDetail.TableName = tbTableName.Text;

            if (cbItemType.SelectedValue != null)
                _currentDetail.DataType = (ItemType)cbItemType.SelectedValue;
            _currentDetail.Describle = tbDescrible.Text;
            int seq = 0;
            int.TryParse(tbSeq.Text, out seq);
            _currentDetail.Seq = seq;
            _currentDetail.Status = cbStatus.Checked;
            _currentDetail.LastModifiedDate = System.DateTime.Now;


            if (!ValidateChecking()) return false;


            return true;
        }
 
        public CompareItem GetData()
        {
            var item = new CompareItem();
            if(_currentDetail!= null)
            { 
                item.Id = _currentDetail.Id;
                item.ParentId = _currentDetail.ParentId;
                item.Parent = _currentDetail.Parent;                
            }
            
            item.Name = tbName.Text;
            item.ItemName = tbShortName.Text;
            if (cbItemType.SelectedValue != null)
                item.DataType = (ItemType)cbItemType.SelectedValue;
            item.Path = tbPath.Text;
            item.TableName = tbTableName.Text;
            item.Describle = tbDescrible.Text;
            int seq = 0;
            int.TryParse(tbSeq.Text, out seq);
            item.Seq = seq;
            item.Status = cbStatus.Checked;

            return item;
        }


        public bool ValidateChecking()
        {
            if (cbItemType.SelectedIndex < 0)
            {
                MessageBox.Show("未选择项目类型！");
                return false;
            }

            if (!_ValidForm)
            {
                MessageBox.Show("输入错误，请检查！");
                return false;
            }

            //var path = GlobalEnviroment.WorkDir +   
            //    GetNodePath(_currentDetail, x => x.Path, "\\");

            return true;
        }


        private void NewItemMenuItem_Click(object sender, EventArgs e)
        {
            ParvaEventArg pe = new ParvaEventArg();
            pe.ArgType = ParvaTreeViewEnum.NewNode;
                         
            base.NodeDetailMessageHandler(sender, pe);
        }

        private void DelItemStripMenuItem_Click(object sender, EventArgs e)
        {
            ParvaEventArg pe = new ParvaEventArg();
            pe.ArgType = ParvaTreeViewEnum.DeleteNode;

            base.NodeDetailMessageHandler(sender, pe);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {

        }

        private void ItemChanged(object sender, EventArgs e)
        {
            if (_currentDetail != null)
                _currentDetail.HasModified = true;
        }

        private void ModifyChanged(object sender, EventArgs e)
        {
            if (_currentDetail != null)
                _currentDetail.HasModified = true;

            //ParvaEventArg pe = new ParvaEventArg();
            //pe.ArgType = ParvaTreeViewEnum.ModifyNode;
            //var item = new CompareItem();
            //item.Name = tbName.Text;
            //item.Status = cbStatus.Checked;
            //pe.ArgData = item;

            //base.NodeDetailMessageHandler(sender, pe);

        }


        public class ComboItem
        {
            public string Name { set; get; }
            public ItemType DataType { set; get; }
        }
    }

    
}
