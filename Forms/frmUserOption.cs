using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMS.Business;
using BMS.Model;
using BMS.Utils;

namespace BMS
{
    public partial class frmUserOption : _Forms
    {

        ConfigSystemModel _oConfigSystem = new ConfigSystemModel();
        public frmUserOption()
        {
            InitializeComponent();
        }

        private void frmUserOption_Load(object sender, EventArgs e)
        {
            ArrayList arr = ConfigSystemBO.Instance.FindByExpression(new Expression("ConfigType", 2).And(new Expression("UserID", Global.UserID)));
            if (arr.Count > 0)
            {
                _oConfigSystem = (ConfigSystemModel)arr[0];
            }

            txtEmail.Text = _oConfigSystem.KeyValue1;
            txtPassword.Text = MD5.DecryptPassword(_oConfigSystem.KeyValue2);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _oConfigSystem.KeyValue1 = txtEmail.Text.Trim();
            _oConfigSystem.KeyValue2 = MD5.EncryptPassword(txtPassword.Text.Trim());
            _oConfigSystem.ConfigType = 2;
            _oConfigSystem.UserID = Global.UserID;
            _oConfigSystem.KeyName = "Email";

            if (_oConfigSystem.ID == 0)
            {
                ConfigSystemBO.Instance.Insert(_oConfigSystem);
            }
            else
            {
                ConfigSystemBO.Instance.Update(_oConfigSystem);
            }

            this.Close();
        }
    }
}
