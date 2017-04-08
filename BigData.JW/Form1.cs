using Parva.Application;
using Parva.Domain.Models;
using Parva.Utility.WinForm;
using System;
using System.Windows.Forms;

namespace BigData.JW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var repo = AppEngine.Container.GetInstance<IBaseObjectService<BaseDataType>>();

            //BaseDataType bdt = new BaseDataType();

            //bdt.BaseTypeName = "test";

            //repo.Insert(bdt);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var MDS = AppEngine.Container.GetInstance<IMasterDetailService<BaseDataType>>();
            //var list = MDS.GetMaster(x => x.Status);

            //var dlg = AppEngine.Container.GetInstance<BaseDataManagement>();
            //var dlg = AppEngine.Container.GetInstance<MasterDetailView<BaseDataType>>();
           
            var dlg = AppEngine.Container.GetInstance<BaseDataView>();
            if (dlg.ShowDialog() == DialogResult.Cancel)
            {

            }
        }
    }
}
