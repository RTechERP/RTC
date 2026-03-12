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
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace BMS
{
    public partial class frmProjectCost : _Forms
    {
        public ProjectModel project = new ProjectModel();
        DataTable dtListCost = new DataTable();
        List<int> lstIDDelete = new List<int>();

        public frmProjectCost()
        {
            InitializeComponent();
        }

        private void frmProjectCost_Load(object sender, EventArgs e)
        {
            // thay đổi tiêu đề form theo mã dự án
            this.Text = $"DANH SÁCH CHI PHÍ DỰ ÁN: {project.ProjectCode}";

            loadListCost();
            loadProjectCost();
            this.cbListCost.EditValueChanged += new EventHandler(cbListCost_EditValueChanged);
        }

        #region Methods
        /// <summary>
        /// hiển thị danh sách chi phí
        /// </summary>
        public void loadListCost()
        {
            dtListCost = TextUtils.Select("SELECT * FROM ListCost");
            cbListCost.DisplayMember = "CostName";
            cbListCost.ValueMember = "ID";
            cbListCost.DataSource = dtListCost;
        }

        /// <summary>
        /// load danh sách chi phí
        /// </summary>
        private void loadProjectCost()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectCost", "A", new string[] { "@ID" }, new object[] { project.ID });
            grdData.DataSource = dt;
        }
        #endregion

        private void cbListCost_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                grvData.Focus();
                //int ID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, "ListCostID"));
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colListCostID));
                DataRow[] dtrows = dtListCost.Select("ID = " + ID);
                if (dtrows.Length > 0)
                {
                    string costCode = TextUtils.ToString(dtrows[0]["CostCode"]);
                    grvData.SetFocusedRowCellValue(colCostCode, costCode);
                }
            }
            catch (Exception ex)
            { }
        }

        #region Button Events
        /// <summary>
        /// click button save
        /// </summary>
        /// 
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                long id = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                ProjectCostModel detail = new ProjectCostModel();
                if (id > 0)
                {
                    detail = (ProjectCostModel)ProjectCostBO.Instance.FindByPK(id);
                }
                detail.ProjectID = project.ID;
                //detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colAdd));
                detail.ListCostID = TextUtils.ToInt(grvData.GetRowCellValue(i, colListCostID));
                detail.Money = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colMoney));

                if (detail.ID > 0)
                {
                    ProjectCostBO.Instance.Update(detail);
                    foreach (int item in lstIDDelete)
                    {
                        ProjectCostBO.Instance.Delete(item);
                    }
                }
                else
                {
                    ProjectCostBO.Instance.Insert(detail);
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// click button thêm dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            grvData.AddNewRow();
            grvData.FocusedColumn = grvData.VisibleColumns[0];
        }

        /// <summary>
        /// click button xóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string costCode = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colCostCode));
            string projectCode = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colProjectCode));

            if (MessageBox.Show(String.Format("Bạn có chắc chắn muốn chi phí [{0}] của dự án [{1}] không?", costCode, projectCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                lstIDDelete.Add(ID);
            }
        }
        #endregion

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column != null && info.Column == colAdd)
                {
                    btnAdd_Click(null, null);
                }
            }
        }
    }
}
