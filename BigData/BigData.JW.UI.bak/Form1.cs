using Parva.Application;
using Parva.Domain.Models;
using Parva.Infrastructure.Implementations.Repository.SystemData;
using Parva.Utility.WinForm;

using System;
using System.Data.SQLite;
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
            //MySqlite _sqlite = new MySqlite();
            //SQLiteDataReader reader = _sqlite.ExecuteReader("Select * from district");
            //while(reader.Read())
            //{
            //    var r = reader.GetInt32(0);
            //}


            var dlg = AppEngine.Container.GetInstance<DistrictManager>();
            if (dlg.ShowDialog() == DialogResult.Cancel)
            {

            }
             
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
