using Parva.Application.Services.TreeService;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parva.Utility.Tools;

namespace Parva.Utility.WinForm 
{
    public partial class DistrictManager : Form
    {
        private ITreeService<District> _treeService;
        
        public DistrictManager(ITreeService<District> treeService)
        {
            InitializeComponent();            

            this.districtTreeView.parvaTreeService = treeService;
            this.districtTreeView.parvaImageList = treeImageList;
            this.districtTreeView.NewNodeEvent += DistrictTreeView_NewNodeEvent;
             

            _treeService = treeService;
        }

        private void DistrictTreeView_NewNodeEvent(object sender, EventArgs e)
        {
            var node = sender as TreeNode;
            if (node == null) return;

            var district = node.Tag as District;

            this.districtTreeView.SwitchNodeDetail("district");
            this.districtTreeView.SetNodeDetail("district", district);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.districtTreeView.RegisterDetailView("district", new DistrictDetail());
            base.OnLoad(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var modifiedlist = this.districtTreeView.GetModifiedData();

            this._treeService.SaveChanges(modifiedlist.AsQueryable());
            this.districtTreeView.AcceptChange();
        }

        private void districtTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            var district = e.Node.Tag as District;
                  
            this.districtTreeView.SwitchNodeDetail("district");            
            this.districtTreeView.SetNodeDetail("district", district);
        }

        private void districtTreeView_ContextMenuEvent(object sender, EventArgs e)
        {
            var tn = (TreeView)sender;
            if (tn == null || tn.SelectedNode == null)
            {
                var menu = this.districtTreeView.GetMenu();
                menu.Items[1].Enabled = false;
            }
            else
            {
                var menu = this.districtTreeView.GetMenu();
                menu.Items[1].Enabled = true;
            }
            //this.districtTreeView.SetMenu(null);
        }
       
    }
}
