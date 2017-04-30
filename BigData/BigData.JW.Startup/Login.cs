using Parva.Application;
using Parva.Application.Core;
using Parva.Application.Services.Account;
using Parva.Domain.Models;
using Parva.Infrastructure.Implementations.Repository.SystemData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BigData.JW.Startup
{
    public partial class Login : Form
    {
        // the flag of validate
        private bool _ValidForm;

        public Login()
        {
            InitializeComponent();

           
            tbUserName.Validating += new CancelEventHandler(ValidateTextBox);
            tbPassword.Validating += new CancelEventHandler(ValidateTextBox);
            tbUserName.Tag = 0;
            tbPassword.Tag = 1;
            lbInfo.Text = "";
        }

        #region Validating

        private void ValidateTextBox(object sender, CancelEventArgs e)
        {
            bool NameValid = true, PasswordValid = true;

            if (String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                switch (Convert.ToByte(((TextBox)sender).Tag))
                {
                    case 0:
                        errorProvider1.SetError(tbUserName, "请输入用户名");
                        
                        lbInfo.Text = "请输入用户名";
                        NameValid = false;
                        break;
                    case 1:
                        errorProvider1.SetError(tbPassword, "请输入密码");
                        PasswordValid = false;
                        
                        lbInfo.Text = "请输入密码";
                        break;
                }
            }
            else
            {
                switch (Convert.ToByte(((TextBox)sender).Tag))
                {
                    case 0:
                        errorProvider1.SetError(tbUserName, "");
                        lbInfo.Text = "";
                        break;
                    case 1:
                        errorProvider1.SetError(tbPassword, "");
                        lbInfo.Text = "";
                        break;
                }
            }
            _ValidForm = NameValid && PasswordValid;
        }

        #endregion

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!_ValidForm)
            { 

                MessageBox.Show("用户名或密码不能为空,请重新输入!");
                return;
            }

            if (GlobalEnviroment.LocalLogin)
                LocalStartup();
            else
                RemoteStartup();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {            
            this.Close();
        }      

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            lbInfo.Text = "";
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                btnLogin_Click(null, null);
                e.Handled = true;
            }
        }
        private void tbUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lbInfo.Text = "";
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                tbPassword.Focus();
                e.Handled = true;
            }
        }
   
        private void LocalStartup()
        {
            var service = AppEngine.Container.GetInstance<UserService>();
            var user = service.GetUserInfo(tbUserName.Text.ToLower(), tbPassword.Text);
            if (user == null)
            { 
                lbInfo.Text = "用户名或密码错误";
                return;
            }

            MainStartup();
            this.Close();
        }

        private void RemoteStartup()
        {
            //remote
            MainStartup();
            this.Close();
        }

        private void MainStartup()
        {
            Process p = new Process();            
            try
            { 
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "精准扶贫监督检查(大数据).exe";
                p.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
