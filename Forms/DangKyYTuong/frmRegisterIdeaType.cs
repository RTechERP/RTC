using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRegisterIdeaType : _Forms
    {
        public RegisterIdeaTypeModel rit = new RegisterIdeaTypeModel();
        public frmRegisterIdeaType()
        {
            InitializeComponent();
        }

        private void frmRegisterIdeaType_Load(object sender, EventArgs e)
        {
            if (rit.ID > 0)
            {
                txtSTT.Value = TextUtils.ToDecimal(rit.STT);
                txtRegisterTypeCode.Text = rit.RegisterTypeCode;
                txtRegisterTypeName.Text = rit.RegisterTypeName;
                txtNote.Text = rit.Note;
            }
            else
            {
                try
                {
                    txtSTT.Value = SQLHelper<RegisterIdeaTypeModel>.FindByAttribute("IsDeleted", 0).Max(x => TextUtils.ToInt(x.STT)) + 1;
                }
                catch
                {
                    txtSTT.Value = 1;
                }
            }
        }

        private bool SaveData()
        {
            if (txtRegisterTypeCode.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã đề tài", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if (txtRegisterTypeName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên đề tài", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            rit.STT = TextUtils.ToInt(txtSTT.Value);
            rit.RegisterTypeCode = txtRegisterTypeCode.Text.Trim();
            rit.RegisterTypeName = txtRegisterTypeName.Text.Trim();
            rit.Note = txtNote.Text.Trim();

            if (rit.ID <= 0)
            {
                var checkRegisterCode = SQLHelper<RegisterIdeaTypeModel>.FindByAttribute("RegisterTypeCode", txtRegisterTypeCode.Text.Trim());
                if (checkRegisterCode.Count > 0)
                {
                    MessageBox.Show("Mã đề tài đã tồn tại vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                SQLHelper<RegisterIdeaTypeModel>.Insert(rit);
            }
            else SQLHelper<RegisterIdeaTypeModel>.Update(rit);
            return true;
        }

        private void Reset()
        {
            rit = new RegisterIdeaTypeModel();
            txtNote.Clear();
            txtRegisterTypeCode.Text = "";
            txtRegisterTypeName.Text = "";
            txtSTT.Value = SQLHelper<RegisterIdeaTypeModel>.FindByAttribute("IsDeleted", 0).Max(x => TextUtils.ToInt(x.STT)) + 1;
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                Reset();
            }
        }

        private void frmRegisterIdeaType_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
