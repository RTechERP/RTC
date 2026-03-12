using BMS.Business;
using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using BMS.Business;
using BMS.BO;
using DevExpress.XtraPrinting;
using System.Diagnostics;


namespace BMS
{
    public partial class frmKPICriteria : _Forms
    {
        public string deName;
        public List<KPICriteriaModel> list = new List<KPICriteriaModel>();
        public frmKPICriteria()
        {
            InitializeComponent();
        }

        private void frmKPICriteria_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            txtQuarter.Value = (int)((DateTime.Now.Month + 2) / 3);
            txtYear.Text = DateTime.Now.Year.ToString();
            LoadData();
        }
        void LoadData()
        {
            //Expression ex1 = new Expression("IsDeleted", 1, "<>");
            //Expression ex2 = new Expression("KPICriteriaQuater", TextUtils.ToInt(txtQuarter.Value));
            //Expression ex3 = new Expression("KPICriteriaYear", TextUtils.ToInt(txtYear.Value));
            //List<KPICriteriaModel> list = SQLHelper<KPICriteriaModel>.FindByExpression(ex1.And(ex2.And(ex3)));
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPICriteria", "A",
                new string[] { "@KPICriteriaQuater", "@KPICriteriaYear", "@Keyword" },
                new object[] { txtQuarter.Value, txtYear.Value, txtFilterText.Text.Trim() });
            grdData.DataSource = dt;
            LoadDatails();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCriteriaDetail frm = new frmCriteriaDetail();
            frm.txtQuarter.Value = txtQuarter.Value;
            frm.txtYear.Value = txtYear.Value;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void LoadDatails()
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colCriteriaID));
            List<KPICriteriaDetailModel> list = SQLHelper<KPICriteriaDetailModel>.FindByAttribute("KPICriteriaID", ID);
            grdDetail.DataSource = list;

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colCriteriaID));
            if (ID == 0) return;
            KPICriteriaModel model = SQLHelper<KPICriteriaModel>.FindByID(ID);
            frmCriteriaDetail frm = new frmCriteriaDetail();
            frm._KPICriteria = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = focusedRowHandle;
                LoadDatails();
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

            int[] selectedRows = grvData.GetSelectedRows();

            if (MessageBox.Show($"Bạn có thực sự muốn xóa danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                foreach (int row in selectedRows)
                {
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(row, colCriteriaID));
                    if (ID <= 0) continue;
                    //string currencyCode = TextUtils.ToString(grvData.GetRowCellValue(row, colCriteriaID));

                    KPICriteriaModel model = SQLHelper<KPICriteriaModel>.FindByID(ID);
                    if (model.ID > 0)
                    {
                        model.IsDeleted = true;
                        SQLHelper<KPICriteriaModel>.Update(model);

                    }
                }
                LoadData();
            }

        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDatails();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtQuarter_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    LoadData();
            //}
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            List<int> lsID = new List<int>();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colCriteriaID));
                lsID.Add(id);
            }

            int year = TextUtils.ToInt(txtYear.Value);
            int quarter = TextUtils.ToInt(txtQuarter.Value);
            frmCopyKPI frm = new frmCopyKPI();
            frm.lsID = lsID;
            frm.year = year;
            frm.quarter = quarter;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            };
        }
    }
}
