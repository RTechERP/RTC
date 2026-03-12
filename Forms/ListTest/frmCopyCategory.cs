using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.ListTest
{
    public partial class frmCopyCategory : _Forms
    {
        public ExamCategoryModel model = new ExamCategoryModel();
        public frmCopyCategory()
        {
            InitializeComponent();
        }

        private void frmCopyCategory_Load(object sender, EventArgs e)
        {
            LoadExamCategory();
        }
        private void LoadExamCategory()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT *  FROM ExamCategory ORDER BY ID DESC");
            grdData.Properties.DataSource = dt;
            grdData.Properties.DisplayMember = "CatCode";
            grdData.Properties.ValueMember = "ID";
        }
        bool ValidateForm()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Mã kì thi không được bỏ trống. Vui lòng nhập mã kì thi! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (TextUtils.Select($"Select top 1 ID FROM ExamCategory WHERE ID <> {model.ID} AND CatCode = '{txtCode.Text.Trim()}'").Rows.Count > 0)
            {
                MessageBox.Show(string.Format("Mã kì thi đã tồn tại. Vui lòng nhập mã kì thi khác! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtName.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Tên kì thi không được bỏ trống. Vui lòng nhập tên kì thi!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }else if(oldID == 0)
            {
                MessageBox.Show(string.Format("Kì thi cũ không được bỏ trống. Vui lòng chọn kì thi cũ!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
        bool saveData()
        {

            if (!ValidateForm()) return false;
            model.CatName = txtName.Text.Trim();
            model.CatCode = txtCode.Text.Trim();
            model.Status = cbStatus.Checked;
            model.Year = TextUtils.ToInt(txtYear.Value);
            model.Quy = TextUtils.ToInt(txtQuy.Value);

            if (model.ID > 0)
            {
                ExamCategoryBO.Instance.Update(model);
            }
            else
            {
                model.ID = (int)ExamCategoryBO.Instance.Insert(model);
            }
            TextUtils.LoadDataFromSP("spCopyExamListTestByExamCategory", "a", new string[] { "@OldExamCategoryID", "@NewExamCategoryID" }, new object[] { oldID, model.ID });
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        int oldID = 0;
        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)grdData.Properties.View.GetFocusedRow();
            if (selectedRow != null)
            {
                // Update the edit value with the product name
                oldID = TextUtils.ToInt(selectedRow["ID"]);
                grdData.EditValue =TextUtils.ToString( selectedRow["CatCode"]) ;
            }
        }

        private void grdData_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}