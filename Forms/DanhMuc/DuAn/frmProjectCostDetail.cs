using BMS.Business;
using BMS.Model;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;


namespace BMS
{
    public partial class frmProjectCostDetail : _Forms
    {
        public ProjectCostModel projectCost = new ProjectCostModel();

        public frmProjectCostDetail()
        {
            InitializeComponent();

        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProjectCostDetail_Load(object sender, EventArgs e)
        {
            loadProject();
            loadListCost();
            loadProductTestDetail();
        }

        #region Methods
        /// <summary>
        /// hiển thị danh sách dự án
        /// </summary>
        public void loadProject()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Project order by ID desc");
            cbProject.Properties.DisplayMember = "ProjectName";
            cbProject.Properties.ValueMember = "ID";
            cbProject.Properties.DataSource = dt;
        }

        /// <summary>
        /// hiển thị danh sách chi phí
        /// </summary>
        public void loadListCost()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ListCost order by ID desc");
            cbListCost.Properties.DisplayMember = "CostName";
            cbListCost.Properties.ValueMember = "ID";
            cbListCost.Properties.DataSource = dt;
        }

        /// <summary>
        /// load ProductTestDetail
        /// </summary>
        private void loadProductTestDetail()
        {
            if (projectCost.ID > 0)
            {
                cbProject.EditValue = projectCost.ProjectID;
                cbListCost.EditValue = projectCost.ListCostID;
                txtMoney.Text = TextUtils.ToString(projectCost.Money);
            }
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData()) this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// click button sve new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            cbProject.Text = "";
            cbListCost.Text = "";
            txtMoney.Clear();
            projectCost = new ProjectCostModel();
        }

        /// <summary>
        /// click button thêm dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewProject_Click(object sender, EventArgs e)
        {
            frmProjectDetail frm = new frmProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
            }
        }

        /// <summary>
        /// click button thêm danh sách chi phí
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewListCost_Click(object sender, EventArgs e)
        {
            frmListCostDetail frm = new frmListCostDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadListCost();
            }
        }
        #endregion

        /// <summary>
        /// hàm save
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            if (!ValidateForm()) return false;
            try
            {
                projectCost.ProjectID = TextUtils.ToInt(cbProject.EditValue);
                projectCost.ListCostID = TextUtils.ToInt(cbListCost.EditValue);
                projectCost.Money = TextUtils.ToDecimal(txtMoney.Text.Trim());
                if (projectCost.ID > 0)
                {
                    ProjectCostBO.Instance.Update(projectCost);
                }
                else
                {
                    ProjectCostBO.Instance.Insert(projectCost);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi Update", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            if (cbProject.Text== "")
            {
                MessageBox.Show("Xin vui lòng chọn dự án", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cbListCost.Text== "")
            {
                MessageBox.Show("Xin vui lòng chọn danh sách chi phí", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtMoney.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng điền số tiền", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void frmGroupFileDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// txtMoney chỉ điền được số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// txtMoney dấu phẩy khi điền 3 chữ số 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMoney_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMoney.Text))
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                long valueBefore = Int64.Parse(txtMoney.Text, System.Globalization.NumberStyles.AllowThousands);
                txtMoney.Text = String.Format(culture, "{0:N0}", valueBefore);
                txtMoney.Select(txtMoney.Text.Length, 0);
            }
        }
    }
}
