using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using Forms.Classes;
using Forms.DanhMuc.DuAn;
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
    public partial class frmGanttChartProjectItemGrid : _Forms
    {
        DataSet dataSet = new DataSet();
        DataTable dtProject = new DataTable();
        List<GridBand> listBand = new List<GridBand>();
        List<BandedGridColumn> listCol = new List<BandedGridColumn>();
        public DateTime createdDate = DateTime.Now;
        public int projectID = 0;

        public frmGanttChartProjectItemGrid()
        {
            InitializeComponent();
            
        }

        //public frmGanttChartProjectItemGrid(int _projectID)
        //{
        //    projectID = _projectID;
        //}

        private void frmGanttChartProjectItemGrid_Load(object sender, EventArgs e)
        {
            dtpYear.Value = createdDate;
            cboProject.EditValue = projectID;

            loadProject();
            loadData();
        }


        /// <summary>
        /// Load dự án lên combo box
        /// </summary>
        void loadProject()
        {
            dtProject = TextUtils.Select($"select p.ID, p.ProjectCode, p.ProjectName, c.CustomerCode from Project as p left join Customer as c on p.CustomerID = c.ID where Year(p.CreatedDate) = {dtpYear.Value.Year} order by ID desc");//sửa lọc theo năm
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = dtProject;  
        }


        /// <summary>
        /// Load data lên grid
        /// </summary>
        void loadData()
        {
            int id = TextUtils.ToInt(cboProject.EditValue);
            dataSet = TextUtils.LoadDataSetFromSP("[spGetTienDoCongViec]", new string[] { "@ProjectID" }, new object[] { id });
            DataTable dtMision = dataSet.Tables[0];
            grdData.DataSource = dtMision;

            DataTable dt = dataSet.Tables[1];

            int row = cboProject.Properties.GetIndexByKeyValue(id);
            string projectName = "";
            string customerName = "";
            string dateS = "";
            string dateE = "";
            if (row >= 0)
            {
                projectName = TextUtils.ToString(dtProject.Rows[row]["ProjectName"]);
                customerName = TextUtils.ToString(dtProject.Rows[row]["CustomerCode"]);
            }

            if (dt.Rows.Count >= 0)
            {
                dateS = string.IsNullOrEmpty(TextUtils.ToString(dt.Rows[0]["AllDates"])) ? "" : TextUtils.ToString(dt.Rows[0]["AllDates"]).Substring(0, 5);
                dateE = string.IsNullOrEmpty(TextUtils.ToString(dt.Rows[dt.Rows.Count - 1]["AllDates"])) ? "" : TextUtils.ToString(dt.Rows[dt.Rows.Count - 1]["AllDates"]).Substring(0, 5);
            }

            bandProjectName.Caption = projectName;
            bandCustomerName.Caption = customerName;
            bandPlanDetail.Caption = dateS + " - " + dateE;


            grvData.CellMerge += new CellMergeEventHandler(grvData_CellMerge);
            
            colSTT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            colMission.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            grvData.OptionsView.AllowCellMerge = true;

            //colGroup.GroupIndex = 0;
            grvData.OptionsBehavior.AutoExpandAllGroups = true;
        }

        /// <summary>
        /// Add cột vào Band tháng
        /// </summary>
        void addColumn()
        {
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                loadData();
                GridBand bandMonth = new GridBand();

                DataTable dtMonth = dataSet.Tables[2];
                DataTable dtAllDate = dataSet.Tables[1];

                if (listBand.Count > 0)
                {
                    foreach (GridBand item in listBand)
                    {
                        bandNote.Children.Remove(item);
                    }
                    listBand.Clear();
                }

                if (listCol.Count > 0)
                {
                    foreach (BandedGridColumn item in listCol)
                    {
                        bandMonth.Columns.Remove(item);
                    }
                    listCol.Clear();
                }

                for (int i = 0; i < dtMonth.Rows.Count; i++)
                {
                    bandMonth = new GridBand();
                    BandedGridColumn colLast = new BandedGridColumn();

                    string month = TextUtils.ToString(dtMonth.Rows[i]["monthDate"]);
                    string year = TextUtils.ToString(dtMonth.Rows[i]["yearDate"]);

                    if (string.IsNullOrEmpty(month) || string.IsNullOrEmpty(year))
                    {
                        break;
                    }
                    colLast.Visible = true;
                    colLast.MinWidth = 30;
                    colLast.Width = 30;
                    colLast.OptionsColumn.FixedWidth = true;

                    bandMonth.Caption = "Tháng " + month + "/" + year;
                    bandMonth.Name = month;
                    bandMonth.MinWidth = 120;
                    bandMonth.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    bandMonth.AppearanceHeader.Options.UseForeColor = true;
                    bandMonth.AppearanceHeader.Options.UseFont = true;

                    bandNote.Children.Add(bandMonth);
                    listBand.Add(bandMonth);

                    for (int j = 0; j < dtAllDate.Rows.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(TextUtils.ToString(dtAllDate.Rows[j]["AllDates"])))
                        {
                            DateTime date = DateTime.Parse(TextUtils.ToString(dtAllDate.Rows[j]["AllDates"]));
                            BandedGridColumn col = new BandedGridColumn();

                            col.Name = "col" + date.ToString("ddMMyy");
                            col.Caption = date.ToString("dd");
                            col.FieldName = date.ToString("dd/MM/yyyy");
                            col.Tag = date.ToString("dd/MM/yyyy");
                            col.Visible = true;
                            col.OptionsColumn.AllowMove = false;
                            col.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            col.AppearanceHeader.Options.UseFont = true;
                            col.AppearanceHeader.Options.UseForeColor = true;
                            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            col.AppearanceHeader.Options.UseTextOptions = true;
                            col.MinWidth = 30;
                            col.Width = 30;
                            col.OptionsColumn.FixedWidth = true;
                            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                            col.OptionsFilter.AllowFilter = false;
                            col.OptionsFilter.AllowAutoFilter = false;

                            listCol.Add(col);


                            if (bandMonth.Name == date.ToString("MM"))
                            {

                                bandMonth.Columns.Add(col);
                                col.BestFit();
                            }

                        }
                    }

                    if (i == dtMonth.Rows.Count - 1)
                    {
                        bandMonth.Columns.Add(colLast);
                    }
                }
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            addColumn();
        }

        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string checkValue = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, colMission));
            if ((e.Column == colSTT ||e.Column == colMission || e.Column == colFullName) && !string.IsNullOrEmpty(checkValue))
            {
                string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value1 == value2);
                e.Handled = true;
                return;
            }
            else
            {
                e.Handled = true;
                return;
            }

        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                decimal itemLate = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colItemLate));
                if (e.Column == colType)
                {
                    if (TextUtils.ToString(e.CellValue) == "Plan")
                    {
                        e.Appearance.BackColor = Color.Aqua;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }

                if (e.Column == colSTT || e.Column == colMission || e.Column == colStartDate || e.Column == colTotalDays || e.Column == colEndDate || e.Column == colType)
                {
                    return;
                }

                string strDS = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colStartDate));
                string strDE = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colEndDate));

                DateTime? ds = null;
                DateTime? de = null;
                ds = string.IsNullOrEmpty(strDS) == true ? ds : DateTime.Parse(strDS);
                de = string.IsNullOrEmpty(strDE) == true ? de : DateTime.Parse(strDE);

                if (!DateTime.TryParse(e.Column.FieldName, out DateTime dateCol))
                {
                    return;
                }

                string dayOfWeek = dateCol.DayOfWeek.ToString();
                if (dayOfWeek == "Sunday")
                {
                    e.Appearance.BackColor = Color.FromArgb(224, 224, 224);
                }

                if (dateCol >= ds && dateCol <= de)
                {
                    if (dayOfWeek == "Sunday")
                    {
                        if (TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colType)) == "Plan")
                        {
                            e.Appearance.BackColor = Color.FromArgb(224, 224, 224); ;
                            e.Appearance.BackColor2 = Color.Aqua;
                        }
                        else
                        {
                            if (itemLate != 0)
                            {
                                e.Appearance.BackColor = Color.FromArgb(224, 224, 224); ;
                                e.Appearance.BackColor2 = Color.DarkRed;
                            }
                            else
                            {
                                e.Appearance.BackColor = Color.FromArgb(224, 224, 224); ;
                                e.Appearance.BackColor2 = Color.Yellow;
                            }
                            
                        }
                    }
                    else
                    {
                        if (TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colType)) == "Plan")
                        {
                            e.Appearance.BackColor = Color.Aqua;
                        }
                        else
                        {
                            if (itemLate != 0)
                            {
                                e.Appearance.BackColor = Color.DarkRed;
                            }
                            else
                            {
                                e.Appearance.BackColor = Color.Yellow;
                            }
                           
                        }
                    }
                }
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"KeHoachDuAn_{cboProject.Text}.xlsx";
            if (f.ShowDialog()==DialogResult.OK)
            {
                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                optionsEx.CustomizeCell += OptionsEx_CustomizeCell;
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    //string filepath = $"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls";
                    string filepath = f.FileName;
                    grvData.ExportToXlsx(filepath, optionsEx);
                    
                    Process.Start(filepath);
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
                }
                grvData.ClearSelection();
            }
            
            //TextUtils.ExportExcel(grvData);
        }

        private void OptionsEx_CustomizeCell(DevExpress.Export.CustomizeCellEventArgs e)
        {

            try
            {
                e.Formatting = new XlFormattingObject
                {
                    Font = new XlCellFont
                    {
                        Size = 12,
                        Name = "Times New Roman"
                    }
                };

                if (e.DocumentRow == 0 || e.DocumentRow == 9)
                {
                    e.Formatting.Alignment = new XlCellAlignment() { HorizontalAlignment = XlHorizontalAlignment.Center, VerticalAlignment = XlVerticalAlignment.Center };
                    e.Formatting.Alignment.WrapText = true;
                    e.Formatting.Font.Bold = true;

                }

                if (e.DocumentRow > 10)
                {
                    e.Formatting.Alignment = new XlCellAlignment() { VerticalAlignment = XlVerticalAlignment.Center };
                    e.Formatting.Alignment.WrapText = true;
                }

                if (e.DocumentRow == 3 || e.DocumentRow == 5 || e.DocumentRow == 7)
                {
                    e.Formatting.Alignment = new XlCellAlignment() { HorizontalAlignment = XlHorizontalAlignment.Left, VerticalAlignment = XlVerticalAlignment.Center };
                    e.Formatting.Font.Bold = true;
                }

                if (e.ColumnFieldName != "" && e.AreaType == SheetAreaType.DataArea)
                {
                    string dateS = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colStartDate));
                    string dateE = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colEndDate));
                    string typeText = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colType));

                    decimal totalDay = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colTotalDays));
                    decimal itemLate = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colItemLate));

                    e.Formatting.Alignment = new XlCellAlignment() { VerticalAlignment = XlVerticalAlignment.Center };

                    if (totalDay <= 0)
                    {
                        e.Value = "";
                    }

                    if (e.ColumnFieldName == "StartDate" || e.ColumnFieldName == "EndDate")
                    {
                        e.Formatting.Alignment = new XlCellAlignment() { HorizontalAlignment = XlHorizontalAlignment.Center, VerticalAlignment = XlVerticalAlignment.Center };
                    }

                    if (e.ColumnFieldName == "TypeText")
                    {
                        if (typeText == "Plan")
                        {
                            e.Formatting.BackColor = Color.Aqua;
                        }
                        else if (typeText == "Actual")
                        {
                            e.Formatting.BackColor = Color.Yellow;
                        }
                        else
                        {
                            e.Formatting.BackColor = Color.White;
                        }
                    }


                    DateTime? ds = null;
                    ds = string.IsNullOrEmpty(dateS) == true ? ds : DateTime.Parse(dateS);

                    DateTime? de = null;
                    de = string.IsNullOrEmpty(dateE) == true ? de : DateTime.Parse(dateE);

                    if (!DateTime.TryParse(e.ColumnFieldName, out DateTime dateCol))
                        return;

                    string dayOfWeek = dateCol.DayOfWeek.ToString();


                    if (dayOfWeek == "Sunday")
                    {
                        e.Formatting.BackColor = Color.Silver;
                    }

                    if (dateCol >= ds && dateCol <= de)
                    {
                        if (dayOfWeek == "Sunday")
                        {
                            if (typeText == "Plan")
                            {
                                e.Formatting.BackColor = Color.DarkCyan;
                            }
                            else
                            {
                                if (itemLate == 1)
                                {
                                    e.Formatting.BackColor = Color.FromArgb(110,70,70);
                                }
                                else
                                {
                                    e.Formatting.BackColor = Color.Yellow;
                                }
                            }
                        }
                        else
                        {
                            if (typeText == "Plan")
                            {
                                e.Formatting.BackColor = Color.Aqua;
                            }
                            else
                            {
                                if (itemLate == 1)
                                {
                                    e.Formatting.BackColor = Color.DarkRed;
                                }
                                else
                                {
                                    e.Formatting.BackColor = Color.Yellow;
                                }
                                
                            }
                        }

                    }
                }
            }
            finally
            {
                e.Handled = true;
            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            addColumn();
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colTotalDays)
            {
                if (TextUtils.ToInt(e.Value) == 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        private void btnUpdateProject_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(cboProject.EditValue);
            if (ID == 0) return;
            //ProjectModel model = (ProjectModel)ProjectBO.Instance.FindByPK(ID);
            frmHangMucCongViec frm = new frmHangMucCongViec();
            ProjectModel projectModel = SQLHelper<ProjectModel>.FindByID(ID);
            frm.project = projectModel;
            //frm.Text = "HẠNG MỤC CÔNG VIỆC - " + cboProject.Text;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
                loadData();
            }
        }

        private void dtpYear_ValueChanged(object sender, EventArgs e)
        {
            loadProject();
        }
    }
}
