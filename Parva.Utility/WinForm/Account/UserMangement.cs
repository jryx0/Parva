using Parva.Application.Core;
using Parva.Application.Interfaces.Repository;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parva.Utility.WinForm.Account
{
    public partial class UserMangement : Form
    {
        private ISystemDataRepository _repo;
        public UserMangement(ISystemDataRepository repo )
        {
            this._repo = repo;
            InitializeComponent();
        }

        private void UserMangement_Load(object sender, EventArgs e)
        {




        }
    }
}
