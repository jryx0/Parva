using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BigData.JW.Startup
{
    public partial class Form1 : Form
    {
        // the flag of validate
        private bool _ValidForm;

        public Form1()
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
                MessageBox.Show("密码用户名不正确,请重新输入!");
                return;
            }

            

        }

        private void btnExit_Click(object sender, EventArgs e)
        {            
            this.Close();
        }      

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                btnLogin_Click(null, null);
                e.Handled = true;
            }
        }
        private void tbUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                tbPassword.Focus();
                e.Handled = true;
            }
        }
    }
}
