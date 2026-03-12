using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmListTestDetail : _Forms
    {
        public ExamListTestModel listtest = new ExamListTestModel();
        public frmListTestDetail()
        {
            InitializeComponent();
        }

        private void frmListTestDetail_Load(object sender, EventArgs e)
        {
            LoadcboTypeTest();
            cboTypeTest.EditValue = 1;
            LoadData();
        }

        void LoadcboTypeTest()
        {
            DataTable dtType = TextUtils.Select("Select * from ExamTypeTest");
            cboTypeTest.Properties.DataSource = dtType;
            cboTypeTest.Properties.DisplayMember = "TypeName";
            cboTypeTest.Properties.ValueMember = "ID";
        }
        void LoadData()
        {
            if (listtest.ID > 0)
            {
                cboTypeTest.EditValue = listtest.ExamTypeTestID;
                txtTime.EditValue = listtest.TestTime;
                txtNote.Text = listtest.Note;
                txtCode.Text = listtest.CodeTest;
                txtName.Text = listtest.NameTest;
            }
        }
        bool ValidateForm()
        {
            //if (txtName.Text.Trim() == "")
            //{
            //    MessageBox.Show(string.Format("Vui lòng nhập tên bài kiểm tra! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập Mã đề thi!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                //DataTable data = TextUtils.Select($"SELECT ID,CodeTest FROM ExamListTest WHERE CodeTest = '{txtCode.Text.Trim()}' AND ExamCategoryID = {listtest.ExamCategoryID} AND ID <> {listtest.ID}");
                //if (data.Rows.Count > 0)
                //{
                //    MessageBox.Show("Mã bài kiểm tra đã tồn tại. Vui lòng nhập mã bài kiểm tra khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}
                //else 
            }

            if (cboTypeTest.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập Loại đề thi!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtTime.Text.Trim() == "" || TextUtils.ToInt(txtTime.Text.Trim()) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập Thời gian làm bài!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;
            listtest.NameTest = txtName.Text.Trim();
            listtest.CodeTest = txtCode.Text.Trim();
            listtest.ExamTypeTestID = TextUtils.ToInt(cboTypeTest.EditValue);
            listtest.TestTime = TextUtils.ToInt(txtTime.Text.Trim());
            listtest.Note = txtNote.Text.Trim();
            if (listtest.ID > 0)
            {
                ExamListTestBO.Instance.Update(listtest);
            }
            else
                ExamListTestBO.Instance.Insert(listtest);
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSave_New_Click(object sender, EventArgs e)

        {
            if (saveData())
            {
                txtCode.Text = "";
                txtName.Text = "";
                txtNote.Text = "";
                txtTime.Text = "0";
                listtest.ID = 0;
            }
        }

        private void frmListTestDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnAddTypeTest_Click(object sender, EventArgs e)
        {
            frmTypeTest frm = new frmTypeTest();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadcboTypeTest();
            }
        }
    }
}
