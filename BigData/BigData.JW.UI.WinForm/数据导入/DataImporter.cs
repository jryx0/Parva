using BigData.JW.Models;
using Parva.Application;
using Parva.Application.Services.TreeService;
using Parva.Utility.WinForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BigData.JW.UI.WinForm
{
    public partial class DataImporter : Form
    {
        private string _currentName;
        private ITreeService<CompareItem> _treeService;
        public bool ItemEdit = false;
        public DataImporter(ITreeService<CompareItem> treeService)
        {
            InitializeComponent();
            _treeService = treeService;


            this.dataImpTreeView.parvaTreeService = treeService;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dataImpTreeView.SetImgList(this.treeImageList);
            this.dataImpTreeView.InitTrees(x => x.Id == 1);
            if (ItemEdit)
            {
                btnItemEdit.Visible = false;
                btnItemFormat.Visible = false;
                btnImportFile.Visible = false;
                btnDelFile.Visible = false;
                btnDataReprot.Visible = false;
                btnOpenDir.Visible = false;
                lbInfo.Visible = false;

                this.dataImpTreeView.RegisterDetailView("displayItem", new DisplayItem());
                _currentName = "displayItem";
            }
            else
            {
                btnSave.Visible = false;
                this.dataImpTreeView.RegisterDetailView("displayData", new DisplayData());

                _currentName = "displayData";
            }
            lbInfo.Text = "";


            base.OnLoad(e);
        }

        #region Button Event
        private void btnDelFile_Click(object sender, EventArgs e)
        {
            NotifyDetailOperation("DeleteFile");
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            NotifyDetailOperation("ImportFile");
        }     

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            NotifyDetailOperation("OpenDirectory");

        }

        private void btnDataReprot_Click(object sender, EventArgs e)
        {

        }

        private void btnItemFormat_Click(object sender, EventArgs e)
        {
            if (_currentName == "displayData")
            {
                btnItemFormat.Text = "显示数据";
                _currentName = "ItemFormat";
                btnSave.Visible = true;
            }
            else
            {
                btnItemFormat.Text = "编辑格式";
                _currentName = "displayData";
                btnSave.Visible = false;
            }

            this.dataImpTreeView.RegisterDetailView("ItemFormat", new DataItemFormat());
            this.dataImpTreeView.SwitchNodeDetail(_currentName);
            this.dataImpTreeView.SetNodeDetail(_currentName, dataImpTreeView.GetCurrentData());
        }

        private void btnItemEdit_Click(object sender, EventArgs e)
        {
            var dlg = AppEngine.Container.GetInstance<DataImporter>();
            dlg.ItemEdit = true;
            dlg.ShowDialog();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.dataImpTreeView.SaveChanges();
            this.Cursor = Cursors.Arrow;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        private void dataImpTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            var compareItem = e.Node.Tag as CompareItem;

            if (ItemEdit)
            {
                this.dataImpTreeView.SwitchNodeDetail("displayItem");
                this.dataImpTreeView.SetNodeDetail("displayItem", compareItem);
            }
            else
            {
                if (compareItem.DataType == ItemType.SourceItem || compareItem.DataType == ItemType.CompareItem)
                {
                    this.dataImpTreeView.SwitchNodeDetail(_currentName);
                    this.dataImpTreeView.SetNodeDetail(_currentName, compareItem);

                    lbInfo.Text = GlobalEnviroment.WorkDir + this.dataImpTreeView.GetNodePath(compareItem, x => x.Path, "\\");
                    Utility.MakeSureDirectory(lbInfo.Text + "\\");

                    //  if (compareItem.DataType == ItemType.SourceItem || compareItem.DataType == ItemType.CompareItem)
                    btnItemFormat.Enabled = btnImportFile.Enabled = btnDelFile.Enabled = true;
                    // else btnImportFile.Enabled = btnDelFile.Enabled = false;
                }
                else btnItemFormat.Enabled = btnImportFile.Enabled = btnDelFile.Enabled = false;
            }
        }

        private void NotifyDetailOperation(string OperateName)
        {
            this.dataImpTreeView.Select();

            ParvaEventArg pe = new ParvaEventArg();
            pe.ArgType = ParvaTreeViewEnum.Operation;
            pe.EventName = OperateName;

            this.dataImpTreeView._currentNodeDetail.NodeDetailOperation(this.dataImpTreeView.GetCurrentData(), pe);
        }

        private void dataImpTreeView_LogInfoEvent(object sender, EventArgs e)
        {
            lbInfo.Text = sender.ToString();
        }
    }
}
