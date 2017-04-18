using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Parva.Domain.Core;

namespace Parva.Utility.WinForm
{
    public partial class DistrictDetail : ParvaTreeNodeDetail
    {
        private District _currentDistrict;
        public DistrictDetail()
        {
            InitializeComponent();
        }

        public override void NodeDetailMessageHandler(object sender, EventArgs e)
        {


        }

        public override void SetData<T>(T data)
        {
            if (data == null) return;

            var district = data as District;
            if (district == null) return;

            this.tbDistrictCode.Text = district.RegionCode;
            this.tbDistrictName.Text = district.Name;
            this.cmbParentDistrict.SelectedValue = district.ParentId == null ? -1 : district.ParentId;
            this.tbSeq.Text = district.Seq.ToString();

            _currentDistrict = district;
        }
        
        private void DistrictDetail_Load(object sender, EventArgs e)
        {

        }

        private void InitializeData(IQueryable<District> treelist)
        {
            cmbParentDistrict.DataSource = treelist.Where(x => x.Level < 2).ToList();
            cmbParentDistrict.ValueMember = "Id";
            cmbParentDistrict.DisplayMember = "Name";
            
        }

        public override void InitNodeDetail(object nodelist)
        {
            if (nodelist == null) return;
            var districtlist = nodelist as IQueryable<District>;

            InitializeData(districtlist);

        }

        public override void SaveChanges()
        {
            if (!_currentDistrict.HasModified)
                return;
            else if (_currentDistrict.ModifyStatus == Domain.Core.BaseEntityStatus.Unchanged)
                _currentDistrict.ModifyStatus = Domain.Core.BaseEntityStatus.Modefied;

            _currentDistrict.HasModified = false;
            if (_currentDistrict.Original == null)
            {
                _currentDistrict.Original = new District();
                _currentDistrict.Original.IP = _currentDistrict.IP;
                _currentDistrict.Original.Name = _currentDistrict.Name;
                _currentDistrict.Original.ParentId = _currentDistrict.ParentId;
                _currentDistrict.Original.RegionCode = _currentDistrict.RegionCode;
                _currentDistrict.Original.Seq = _currentDistrict.Seq;                 
                _currentDistrict.Original.Id = _currentDistrict.Id;
                
            }

            _currentDistrict.ParentId = Convert.ToInt32(cmbParentDistrict.SelectedValue);
            int seq = 0;
            int.TryParse(tbSeq.Text, out seq);
            _currentDistrict.Seq = seq;
            _currentDistrict.IP = tbDistrictIP.Text;
            _currentDistrict.RegionCode = tbDistrictCode.Text;
            _currentDistrict.Name = tbDistrictName.Text;
        }

        public override void LabelChange(string label)
        {
            this.tbDistrictName.Text = label;
        }

        public override void RestorData()
        {
            SetData(_currentDistrict.Original == null ? _currentDistrict : _currentDistrict.Original);
            _currentDistrict.ModifyStatus = Domain.Core.BaseEntityStatus.Unchanged;
        }

        //public override void NewNode<T>(T data)
        //{
        //    if (data == null) return;

        //    var district = data as District;
        //    if (district == null) return;

        //    cmbParentDistrict.Items.Add(district);
        //}

        public override bool NodeChange<T>(T data)
        {
            if (data == null) return true;

            bool bRet = false;
            var district = data as District;
            switch (district.ModifyStatus)
            {
                case BaseEntityStatus.NewEntity:
                    // bRet = district.Level <= 2;
                    bRet = true;
                    break;
                case BaseEntityStatus.Deleted:
                    //  bRet = district.Parent != null;
                    bRet = true;
                    break;
            }

            return bRet;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RestorData();
        }

        private void tbDistrictName_TextChanged(object sender, EventArgs e)
        {            

            if (_currentDistrict != null)
                _currentDistrict.HasModified = true;

            base.NodeNameChanged(this.tbDistrictName.Text);
        }

        private void tbDistrictCode_TextChanged(object sender, EventArgs e)
        {
            if (_currentDistrict != null)
                _currentDistrict.HasModified = true;
        }

        private void tbDistrictIP_TextChanged(object sender, EventArgs e)
        {
            if (_currentDistrict != null)
                _currentDistrict.HasModified = true;
        }
     
    }
}
