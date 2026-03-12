using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmListPONCCDetail : _Forms
    {

        public bool ValuesIsSelect;
        public List<int> rowIndex = new List<int>();
        DataSet  dt;

        public frmListPONCCDetail()
        {
            InitializeComponent();
        }

        private void frmListPONCCDetail_Load(object sender, EventArgs e)
        {
            //Bên ImportSale trỏ sang.
            if (ValuesIsSelect)
            {
                grvData.OptionsSelection.MultiSelect = true;
                grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                btnXuat.Visible = true;
                btnExcel.Visible = false;
            }
            else
            {
                grvData.OptionsSelection.MultiSelect = false;
                btnXuat.Visible = false;
            }
            cbFilter.SelectedIndex = 2;
            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            loadData();
        }
        void loadData()
        {

            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            dt= TextUtils.LoadDataSetFromSP("spGetAllPONCCDetail"
               , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@Values","@Filter" }
               , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text.Trim(), ValuesIsSelect,cbFilter.SelectedIndex });
            //DataTable dt = TextUtils.Select("Exec spGetAllPONCCDetail");
           
            grdData.DataSource = dt.Tables[0];

            if (dt.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Tables[1].Rows[0]["TotalPage"]);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }




        public List<DataRow> List = new List<DataRow>();
        private void btnXuat_Click(object sender, EventArgs e)
        {

            int[] RowIndex = grvData.GetSelectedRows();
            for (int i = 0; i < RowIndex.Length; i++)
            {
                if (rowIndex.Contains(RowIndex[i])) continue;
                DataRow row = grvData.GetDataRow(i);
                List.Add(row);
                rowIndex.Add(RowIndex[i]);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == ColStatus)
            {
                if (e.Value != null && e.Value.ToString().Trim() == "0")
                {
                    e.DisplayText = "Chưa hoàn thành";
                }
                if (e.Value != null && e.Value.ToString().Trim() == "1")
                {
                    e.DisplayText = "Hoàn thành";
                }
                //e.DisplayText = e.Value.ToString().Trim() == "0" ? "Chưa hoành thành" :
                //                e.Value.ToString().Trim() == "1" ? "Hoàn thành" : "";

            }
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == ColStatus)
            {
                if (e.CellValue != null && e.CellValue.ToString().Trim() == "0")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 128);
                }
                if (e.CellValue != null && e.CellValue.ToString().Trim() == "1")
                {
                    e.Appearance.BackColor = Color.FromArgb(128, 255, 128);
                }
                
            }
        }
    }
}
