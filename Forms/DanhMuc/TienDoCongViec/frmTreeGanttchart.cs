using BMS;
using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
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

namespace Forms.DanhMuc.DuAn
{
    public partial class frmGanttChartProjectItem : _Forms
    {
        public DataSet dataSet;
        List<TreeListBand> listBand = new List<TreeListBand>();
        List<TreeListColumn> listCol = new List<TreeListColumn>();
        public frmGanttChartProjectItem()
        {
            InitializeComponent();
        }

        private void frmTreeGanttchart_Load(object sender, EventArgs e)
        {
            loadProject();
        }

        /// <summary>
        /// Load dự án lên combo box
        /// </summary>
        void loadProject()
        {
            DataTable dt = TextUtils.Select("select ID, ProjectCode, ProjectName from Project order by ID desc");
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DataSource = dt;
        }
        private const int WM_SETREDRAW = 11;

        /// Suspends painting for the target control. Do NOT forget to call EndControlUpdate!!!
        /// </summary>
        /// <param name="control">visual control</param>
        public static void BeginControlUpdate(Control control)
        {
            Message msgSuspendUpdate = Message.Create(control.Handle, WM_SETREDRAW, IntPtr.Zero,
                  IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgSuspendUpdate);
        }

        /// <summary>
        /// Resumes painting for the target control. Intended to be called following a call to BeginControlUpdate()
        /// </summary>
        /// <param name="control">visual control</param>
        public static void EndControlUpdate(Control control)
        {
            try
            {
                // Create a C "true" boolean as an IntPtr
                IntPtr wparam = new IntPtr(1);
                Message msgResumeUpdate = Message.Create(control.Handle, WM_SETREDRAW, wparam,
                      IntPtr.Zero);

                NativeWindow window = NativeWindow.FromHandle(control.Handle);
                window.DefWndProc(ref msgResumeUpdate);
                control.Invalidate();
                control.Refresh();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Load hạng mục công việc lên treelist
        /// </summary>
        void loadProjectItem()
        {
            int id = TextUtils.ToInt(cboProject.EditValue);
            dataSet = TextUtils.LoadDataSetFromSP("spGetProjectItem", new string[] { "@ProjectID" }, new object[] { id });

            DataTable dt = dataSet.Tables[1];
            DataTable dtDate = dataSet.Tables[2];
            
            tlGanttChartProjectItem.DataSource = dt;
            //tlGanttChartProjectItem.ExpandAll();

            if (dt.Rows.Count <= 0) return;
            string customer = string.IsNullOrEmpty(TextUtils.ToString(dt.Rows[0]["CustomerCode"])) == true ? "" : TextUtils.ToString(dt.Rows[0]["CustomerCode"]);

            if (dt.Rows.Count <= 0) return;
            string projectName = string.IsNullOrEmpty(TextUtils.ToString(dt.Rows[0]["ProjectName"])) == true ? "" : TextUtils.ToString(dt.Rows[0]["ProjectName"]);

            if (dtDate.Rows.Count <= 0) return;
            string minDate = string.IsNullOrEmpty(TextUtils.ToString(dtDate.Rows[0]["AllDates"])) == true ? "" : TextUtils.ToString(dtDate.Rows[0]["AllDates"]).Substring(0, 5);
            string maxDate = string.IsNullOrEmpty(TextUtils.ToString(dtDate.Rows[dtDate.Rows.Count - 1]["AllDates"])) == true ? "" : TextUtils.ToString(dtDate.Rows[dtDate.Rows.Count - 1]["AllDates"]).Substring(0, 5);

            bandCustomerName.Caption = customer;
            bandProjectName.Caption = projectName;
            bandPlanDetail.Caption = minDate + " - " + maxDate;

            
        }

        private bool lockEvents = false;
        private void btnShow_Click(object sender, EventArgs e)
        {
            BeginControlUpdate(tlGanttChartProjectItem);
            lockEvents = true;
            AddColDate(tlGanttChartProjectItem);
            EndControlUpdate(tlGanttChartProjectItem);
        }

        /// <summary>
        /// Add cột vào band theo tháng
        /// </summary>
        /// <param name="treeList"></param>
        void AddColDate(TreeList treeList)
        {
            try
            {
                loadProjectItem();
                DataTable dtAllDate = dataSet.Tables[2];
                DataTable dtMonthDate = dataSet.Tables[3];

                DateTime minAllDate = DateTime.Now.AddDays(-1);
                DateTime maxAllDate = DateTime.Now;

                DataRow rowMinDate = dtAllDate.NewRow();
                DataRow rowMaxDate = dtAllDate.NewRow();


                if (!string.IsNullOrEmpty(TextUtils.ToString(dtAllDate.Rows[0]["AllDates"])) && !string.IsNullOrEmpty(TextUtils.ToString(dtAllDate.Rows[dtAllDate.Rows.Count - 1]["AllDates"])))
                {
                    minAllDate = DateTime.Parse(TextUtils.ToString(dtAllDate.Rows[0]["AllDates"])).AddDays(-1);
                    maxAllDate = DateTime.Parse(TextUtils.ToString(dtAllDate.Rows[dtAllDate.Rows.Count - 1]["AllDates"])).AddDays(+1);
                }
                
                rowMinDate["AllDates"] = minAllDate.ToString();
                rowMaxDate["AllDates"] = maxAllDate.ToString();
                dtAllDate.Rows.InsertAt(rowMinDate, 0);
                dtAllDate.Rows.InsertAt(rowMaxDate, dtAllDate.Rows.Count);

                TreeListBand bandMonth = new TreeListBand();
                
                if (listCol.Count > 0)
                {
                    for (int i = 0; i < listCol.Count; i++)
                    {
                        treeList.Columns.Remove(listCol[i]);

                    }
                    listCol.Clear();
                }

                if (listBand.Count > 0)
                {
                    foreach (TreeListBand item in listBand)
                    {
                        bandNote.Bands.Remove(item);
                    }
                    listBand.Clear();
                }

                for (int i = 0; i < dtMonthDate.Rows.Count; i++)
                {
                    string month = string.IsNullOrEmpty(TextUtils.ToString(dtMonthDate.Rows[i]["monthDate"])) == true ? DateTime.Now.Month.ToString() : TextUtils.ToString(dtMonthDate.Rows[i]["monthDate"]);
                    string year = string.IsNullOrEmpty(TextUtils.ToString(dtMonthDate.Rows[i]["yearDate"])) == true ? DateTime.Now.Year.ToString() : TextUtils.ToString(dtMonthDate.Rows[i]["yearDate"]);

                    bandMonth = bandNote.Bands.Add();
                    bandMonth.Name = month;
                    bandMonth.Caption = "THÁNG " + month + "/" + year;
                    bandMonth.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    bandMonth.AppearanceHeader.Options.UseFont = true;
                    bandMonth.AppearanceHeader.Options.UseForeColor = true;
                    bandMonth.RowCount = 2;
                    bandMonth.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    bandMonth.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                    listBand.Add(bandMonth);

                    for (int j = 0; j < dtAllDate.Rows.Count; j++)
                    {
                        string capt = TextUtils.ToString(dtAllDate.Rows[j]["AllDates"]);

                        if (!string.IsNullOrEmpty(capt))
                        {
                            DateTime date = DateTime.Parse(TextUtils.ToString(dtAllDate.Rows[j]["AllDates"]));
                            TreeListColumn col = treeList.Columns.Add();
                            col.Caption = capt.Substring(0, 2);
                            col.FieldName = capt.Substring(0, 10);//dd/MM/yyyy
                            col.Name = "colDate" + i;
                            col.Visible = true;
                            //col.Width = 25;
                            //col.MinWidth = 25;
                            //col.MaxWidth = 25;
                            col.OptionsColumn.AllowMove = false;
                            col.Tag = capt.Substring(0, 10);
                            col.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            col.AppearanceHeader.Options.UseFont = true;
                            col.AppearanceHeader.Options.UseForeColor = true;
                            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                            col.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
                            listCol.Add(col);

                            if (date.Month.ToString() == bandMonth.Name)
                            {
                                bandMonth.Columns.Add(col);
                            }
                        }
                    }
                }
            }
            finally
            {
                lockEvents = false;
            }

        }

        //private void GenGantValue(TreeListNodes nodes)
        //{
        //    foreach (TreeListNode node in nodes)
        //    {
        //        foreach (TreeListColumn column in _lst)
        //        {
        //            if (column == colMission || column == colStart || column == colFinish)
        //                continue;
        //            if (column.FieldName == "")
        //            {
        //                continue;
        //            }
        //            string dateS = TextUtils.ToString(node.GetValue(colStart));
        //            string dateE = TextUtils.ToString(node.GetValue(colFinish));

        //            if (string.IsNullOrEmpty(dateS) && string.IsNullOrEmpty(dateE)) return;

        //            DateTime ds = DateTime.Parse(dateS);
        //            DateTime de = DateTime.Parse(dateE);

        //            DateTime dateCol = DateTime.Parse(column.FieldName);

        //            if (dateCol >= ds && dateCol <= de)
        //            {
        //                node.SetValue(column, "DAT");
        //            }
        //        }
        //        //GenGantValue(node.Nodes);
        //    }
        //}
        private void CustomizeCellEventHandler(CustomizeCellEventArgs e)
        {
            try
            {
                DataTable dt = dataSet.Tables[1];
                var c = e.DataSourceOwner;
                e.Formatting.BackColor = Color.White;
                e.Formatting.Font.Size = 12;
                e.Formatting.Font.Name = "Times New Roman";

                if (e.DocumentRow == 0 || e.DocumentRow == 9)
                {
                    e.Formatting.Alignment = new XlCellAlignment() { HorizontalAlignment = XlHorizontalAlignment.Center, VerticalAlignment = XlVerticalAlignment.Center };
                    e.Formatting.Alignment.WrapText = true;
                    e.Formatting.Font.Bold = true;
                    
                }

                if (e.DocumentRow > 10)
                {
                    e.Formatting.Alignment = new XlCellAlignment() {VerticalAlignment = XlVerticalAlignment.Center };
                    e.Formatting.Alignment.WrapText = true;
                }

                if (e.DocumentRow == 3 || e.DocumentRow == 5 || e.DocumentRow == 7)
                {
                    e.Formatting.Alignment = new XlCellAlignment() { HorizontalAlignment = XlHorizontalAlignment.Left, VerticalAlignment = XlVerticalAlignment.Center };
                    e.Formatting.Font.Bold = true;
                }

                

                if (e.DataSourceRowIndex >= 0)
                {
                    string typeText = TextUtils.ToString(dt.Rows[2]["TypeText"]);
                }

                if (e.ColumnFieldName != "" && e.AreaType == SheetAreaType.DataArea)
                {
                    string dateS = TextUtils.ToString(tlGanttChartProjectItem.GetNodeByVisibleIndex(e.RowHandle).GetValue(colStart));
                    string dateE = TextUtils.ToString(tlGanttChartProjectItem.GetNodeByVisibleIndex(e.RowHandle).GetValue(colFinish));
                    string typeText = TextUtils.ToString(tlGanttChartProjectItem.GetNodeByVisibleIndex(e.RowHandle).GetValue(colType));

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
                        e.Formatting.BackColor = Color.DarkRed;
                    }

                    if(dateCol >= ds && dateCol <= de)
                    {
                        if (dayOfWeek == "Sunday")
                        {
                            if (typeText == "Plan")
                            {
                                e.Formatting.BackColor = Color.DeepPink;
                                //e.Formatting.BackColor2 = Color.Aqua;
                            }
                            else
                            {
                                e.Formatting.BackColor = Color.OrangeRed;
                                //e.Appearance.BackColor2 = Color.Yellow;
                            }
                        }
                        else
                        {
                            if (typeText == "Plan")
                            {
                                e.Formatting.BackColor = Color.Aqua;
                                //e.Formatting.BackColor2 = Color.Aqua;
                            }
                            else
                            {
                                e.Formatting.BackColor = Color.Yellow;
                                //e.Appearance.BackColor2 = Color.Yellow;
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
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            tlGanttChartProjectItem.ExpandAll();

            XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
            optionsEx.CustomizeCell += CustomizeCellEventHandler;
            tlGanttChartProjectItem.OptionsPrint.UsePrintStyles = true;

            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xls";
            f.FileName = $"KeHoach_{cboProject.Text}.xls";
            if (f.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    //string filepath = f.SelectedPath + $"/KeHoach_{cboProject.Text}.xls";
                    string filepath = f.FileName;
                    tlGanttChartProjectItem.ExportToXls(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

            
        }

        private void tlGanttChartProjectItem_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {

            if (lockEvents) return;
            if (e.Node == null) return;
            
            if (e.Column.FieldName == "TypeText")
            {
                if (e.Node.GetValue(colType).ToString() == "Plan")
                {
                    e.Appearance.BackColor = Color.Aqua;
                }
                else if (e.Node.GetValue(colType).ToString() == "Actual")
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                }
            }

            if (e.Column == colSTT || e.Column == colMission || e.Column == colStart || e.Column == colTotalDays || e.Column == colFinish || e.Column == colType) return;

            string dateS = TextUtils.ToString(e.Node.GetValue(colStart));
            string dateE = TextUtils.ToString(e.Node.GetValue(colFinish));
            
            if (!DateTime.TryParse(e.Column.Tag.ToString(), out DateTime dateCol))
                return;

           // DateTime dateCol = DateTime.Parse(e.Column.Tag.ToString());
            string dayOfWeek = dateCol.DayOfWeek.ToString();

            //if (string.IsNullOrEmpty(dateS) && string.IsNullOrEmpty(dateE)) return;

            DateTime? ds = null;
            ds = string.IsNullOrEmpty(dateS) == true ? ds : DateTime.Parse(dateS);
            
            DateTime? de = null;
            de = string.IsNullOrEmpty(dateE) == true ? de : DateTime.Parse(dateE);

            if (string.IsNullOrEmpty(e.Column.Tag.ToString())) return;

            if (dayOfWeek == "Sunday")
            {
                e.Appearance.BackColor = Color.DarkRed;               
            }

            if (dateCol >= ds && dateCol <= de)
            {
                if (dayOfWeek == "Sunday")
                {
                    if (e.Node.GetValue(colType).ToString() == "Plan")
                    {
                        e.Appearance.BackColor = Color.DarkRed;
                        e.Appearance.BackColor2 = Color.Aqua;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.DarkRed;
                        e.Appearance.BackColor2 = Color.Yellow;
                    }
                }
                else
                {
                    if (e.Node.GetValue(colType).ToString() == "Plan")
                    {
                        e.Appearance.BackColor = Color.Aqua;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }
            }


        }

        private void tlGanttChartProjectItem_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            //tlGanttChartProjectItem.
        }
    }
}
