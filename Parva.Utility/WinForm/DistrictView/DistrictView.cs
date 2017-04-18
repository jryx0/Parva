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

namespace Parva.Utility.WinForm 
{
    public partial class DistrictView : Form  
    {
        private ITreeService<District> _treeService;
        public DistrictView(ITreeService<District> treeService)
        {
            this._treeService = treeService;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {            
            this.parvaTreeView1.InitTreeView(this._treeService);             
            this.parvaTreeView1.RegisterDetailView("district", new DistrictDetail());

            base.OnLoad(e);
        }                 

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            IQueryable<TreeNode> modifiedDistrict = this.parvaTreeView1.GetSaveData();

         //   this._treeService.SaveChanges(modifiedDistrict);
        }

        private void parvaTreeView1_TreeMsgEvent(object sender, EventArgs e)
        {
            if (e == null)
                return;

            var parvaEvent = e as ParvaEventArg;
            if (parvaEvent.ArgType == ParvaTreeViewEnum.LeftMouseClick)
            {//swicth detail
                this.parvaTreeView1.SetNodeDetail("district", parvaEvent);
                this.parvaTreeView1.SwitchNodeDetail("district");
            }
            else if (parvaEvent.ArgType == ParvaTreeViewEnum.RightMouseClick)
            {                
                this.parvaTreeView1.SetMenu(null);
            }
            else if(parvaEvent.ArgType == ParvaTreeViewEnum.LabelEdit)
            {
                var district = parvaEvent.ArgData as District;
                district.Name = sender?.ToString();

                this.parvaTreeView1.SetNodeDetail("district", parvaEvent);
            }
        }

        
    }


   
}
