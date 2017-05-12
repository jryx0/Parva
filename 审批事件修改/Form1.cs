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
            string sql = "";
            if(comboBox1.SelectedValue == null)
            {//
                return;
            }
            else
            {
                sql = @"
                        SELECT     c.appUniqueCode, c.applierID, a.ID, b.ID AS 项目编号, a.contractNo 合同编号 , a.contractName 合同名称, a.contractType 合同类型, b.orgName 项目名称
                        FROM         zjsj_iems.dbo.iect_Contract AS a INNER JOIN
                                              zjsj_iems.dbo.iecm_Org AS b ON a.orgID = b.ID INNER JOIN
                                              zjsj_iems.dbo.iecm_FlowData AS c ON a.ID = c.businessID
                         where b.ID = '@ID'
                          ORDER BY b.orgName, a.contractType";

                sql = sql.Replace("@ID", comboBox1.SelectedValue.ToString());
                //sql = @"select ID, contractNo, contractName, contractType from zjsj_iems.dbo.iect_Contract where orgID = '" + comboBox1.SelectedValue.ToString() + "' order by contractType";
            }

            var ds = sqlserver.ExecuteDataset(sql);
            if (ds == null)
                return;

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Visible = false;


            //var ds = sqlserver.ExecuteDataset(@"
            //             declare @orgName varchar(256), @contractNo varchar(32)
            //             declare @orgID varchar(100), @bizID varchar(100), @applierID varchar(100)
            //             set @orgName = '武汉市南四环项目'
            //             set @contractNo = 'FBHT-NSH002'
            //             set @orgID = (select ID from zjsj_iems_test.dbo.iecm_Org where orgName = @orgName and orgType = '4')
            //             set @bizID = (select ID from zjsj_iems_test.dbo.iect_Contract where contractType = 'P3' and contractNo = @contractNo and orgID = @orgID)
            //             set @applierID = (select applierID from zjsj_iems_test.dbo.iecm_FlowData where businessID = @bizID)

            //             select top 1  [dataHistoryID]       
            //                                   ,[f_title]
            //                                   ,[f_docFlowType]
            //                                   ,[pr_f_psyijian]      
            //                                   ,[f_iewfFlowApplierID]
            //                                   ,[f_iewfFlowDepartmentID]     
            //                                   ,[pr_f_spyijian]      
            //                                   ,[f_businessParam] from iectSubCt_business_1Hist where f_iewfFlowApplierID = @applierID  order by DATALENGTH([pr_f_spyijian]) desc  ");



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ds = sqlserver.ExecuteDataset("select ID, orgName from zjsj_iems_test.dbo.iecm_Org where orgType = '4'");

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "orgName";
            comboBox1.ValueMember = "ID";
            comboBox2.Items.Add(new { ID = "P1", Name = "建造合同" });
            comboBox2.Items.Add(new { ID = "P3", Name = "分包合同" });
            comboBox2.Items.Add(new { ID = "P5", Name = "采购合同" });
            comboBox2.Items.Add(new { ID = "P6", Name = "租赁合同" });
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";      
        }
    }
}
