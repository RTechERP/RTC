using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPartDetail : _Forms
    {
        public PartModel Part = new PartModel();
        public frmPartDetail()
        {
            InitializeComponent();
        }

        private void frmPartDetail_Load(object sender, EventArgs e)
        {
            loadPartGroup();
            loadData();
        }

        void loadPartGroup()
        {
            cboPartGroup.Properties.DisplayMember = "PartGroupName";
            cboPartGroup.Properties.ValueMember = "ID";
            cboPartGroup.Properties.DataSource = PartGroupBO.Instance.FindAll();
        }

        /// <summary>
        /// Load dữ liệu vào form
        /// </summary>
        void loadData()
        {
            txtCode.Text = Part.PartCode;
         
            txtName.Text = Part.PartName;
            txtNote.Text = Part.Description;

            cboPartGroup.EditValue = Part.PartGroupID;
        }

        /// <summary>
        /// Validate trước khi cất dữ liệu
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền mã thiết bị.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                //DataTable dt;
                //if (Part.ID > 0)
                //{
                //    int strID = Part.ID;
                //    dt = TextUtils.Select("select top 1 PartCode from Part where PartCode = '" + txtCode.Text.Trim() + "' and ID <> " + strID);
                //}
                //else
                //{
                //    dt = TextUtils.Select("select top 1 PartCode from Part where PartCode = '" + txtCode.Text.Trim() + "'");
                //}
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        MessageBox.Show("Mã thiết bị này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //        return false;
                //    }
                //}

                if (TextUtils.CheckExistTable(Part.ID, "PartCode", txtCode.Text.Trim(), "Part"))
                {
                    MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền tên vật tư/thiết bị.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Cất dữ liệu
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            if (!ValidateForm())
                return false;

            Part.PartCode = txtCode.Text.Trim();
            Part.PartName = txtName.Text.Trim();
            Part.Description = txtNote.Text.Trim();
            Part.PartGroupID = TextUtils.ToInt(cboPartGroup.EditValue);

            if (Part.ID > 0)
            {
                PartBO.Instance.Update(Part);
            }
            else
            {
                Part.ID = (int)PartBO.Instance.Insert(Part);
            }
            return true;
        } 

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
                this.DialogResult = DialogResult.OK;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                Part = new PartModel();
                loadData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPartDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnPartGroup_Click(object sender, EventArgs e)
        {
            frmPartGroup frm = new frmPartGroup();
            frm.HasDialogResult = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPartGroup();
            }
        }
    }
}
