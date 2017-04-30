using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 审批事件修改
{
    public partial class Form1 : Form
    {
        private DAL.MyDataBase sqlserver; 
        public Form1()
        {
            sqlserver = new DAL.MyDataBase();
            sqlserver.DBConnectionString = @"Provider = SQLNCLI11; Data Source = 10.11.250.15; Initial Catalog = zjsj_oa_test; User Id = zhanghao; Password = 1qaz2wsx;";

            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
           var ds = sqlserver.ExecuteDataset(@"
                        declare @orgName varchar(256), @contractNo varchar(32)
                        declare @orgID varchar(100), @bizID varchar(100), @applierID varchar(100)
                        set @orgName = '武汉市南四环项目'
                        set @contractNo = 'FBHT-NSH002'
                        set @orgID = (select ID from zjsj_iems_test.dbo.iecm_Org where orgName = @orgName and orgType = '4')
                        set @bizID = (select ID from zjsj_iems_test.dbo.iect_Contract where contractType = 'P3' and contractNo = @contractNo and orgID = @orgID)
                        set @applierID = (select applierID from zjsj_iems_test.dbo.iecm_FlowData where businessID = @bizID)

                        select top 1  [dataHistoryID]       
                                              ,[f_title]
                                              ,[f_docFlowType]
                                              ,[pr_f_psyijian]      
                                              ,[f_iewfFlowApplierID]
                                              ,[f_iewfFlowDepartmentID]     
                                              ,[pr_f_spyijian]      
                                              ,[f_businessParam] from iectSubCt_business_1Hist where f_iewfFlowApplierID = @applierID  order by DATALENGTH([pr_f_spyijian]) desc  ");

            if(ds != null)
                dataGridView1.DataSource = ds.Tables[0];

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ds = sqlserver.ExecuteDataset("select ID, orgName from zjsj_iems_test.dbo.iecm_Org where orgType = '4'");

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "orgName";
            comboBox1.ValueMember = "ID";

            comboBox2.Items.Add(new { ID = "P3", Name = "分包合同" });
            comboBox2.Items.Add(new { ID = "P5", Name = "采购合同" });
            comboBox2.Items.Add(new { ID = "P6", Name = "租赁合同" });
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";      
        }
    }
}
