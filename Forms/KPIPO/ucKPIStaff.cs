using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using Forms.Classes;

namespace Forms.KPI_PO
{
    public partial class ucKPIStaff : UserControl
    {
        public SearchLookUpEdit cbUser { get; set; }
        public GridControl grvPerformance { get; set; }
        public GridControl grvReport { get; set; }
        public int NewAccountQty;
        public Decimal sumACCP;
        public Decimal TotalSale;
        public DataTable dtMonth;
        public CheckedComboBoxEdit cbNote;
        public System.Windows.Forms.ComboBox cbMonth { get; set; }
        public System.Windows.Forms.NumericUpDown nbrYear { get; set; }
        public string Position;
        public int Groupsale;
        public SimpleButton btnNote;
        public CheckBox ckHide;
        public SimpleButton btnReload;
        public ToolStripButton btnSave;
        public System.Windows.Forms.ComboBox cbQuy;
        public Button btnExcel;
        public DevExpress.XtraGrid.Views.Grid.GridView grvKPIStaff; // excel HistoryProductivityIndex
        public List<int> LstMainGroupCount = new List<int>(); // số lượng maingroup

        public ucKPIStaff()
        {
            InitializeComponent();

        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }
        decimal thang0 = 0, thang1 = 0, index = 0, thang2 = 0;
        decimal avg0 = 0;

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
                loadMainIndex();
                grvData.ExpandAllGroups();
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
        }
        private void btnNote_Click(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            //  loadMainIndex();
            grvData.ExpandAllGroups();

        }
        private void cbQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadMainIndex();

        }
        private void nbrYear_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadMainIndex();
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            MyLib.ExportExcelGrid(grvData);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            SaveData();
            loadMainIndex();
            grvData.ExpandAllGroups();
        }
        private void ucKPIStaff_Load(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
                checktime();
                btnNote.Click += new EventHandler(btnNote_Click);
                btnReload.Click += new EventHandler(btnReload_Click);
                btnSave.Click += new EventHandler(btnSave_Click);
                //btnExcel.Click += new EventHandler(btnExcel_Click);
                nbrYear.ValueChanged += new EventHandler(nbrYear_ValueChanged);
                cbQuy.SelectedIndexChanged += new EventHandler(cbQuy_SelectedIndexChanged);
                loadMainIndex();
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
          

        }
        DataSet ds = new DataSet();
        decimal tongG = 0, tongR = 0, tongA = 0;

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (e.Column == colMainIndex)
            {
                string ID = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colID));
                if (!ckHide.Checked && arr.Contains(ID))
                {
                    e.Appearance.BackColor = Color.FromArgb(139, 125, 107);
                }
            }
        }
        string group;
        List<int> lstNote = new List<int>();
        string arr;

        void loadMainIndex()
        {
            
            try
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tính toán..."))
                {
                    group = TextUtils.ToString(TextUtils.ExcuteScalar($"Select GroupSalesCode From GroupSales where ID ={Groupsale}"));

                    if (cbUser.EditValue == null) return;
                    DataTable dtuser = TextUtils.Select($"Select s.SaleUserTypeCode,g.Note,g.GroupSalesID From GroupSalesUser g Inner join SaleUserType s on g.SaleUserTypeID=s.ID where UserID={cbUser.EditValue}");
                    string main = TextUtils.ToString(TextUtils.ExcuteScalar($"Select MainIndexID From GroupSales where ID={Groupsale}"));
                    arr = dtuser.Rows[0]["Note"].ToString();
                    if (TextUtils.ToString(dtuser.Rows[0]["SaleUserTypeCode"]) == cConsts.Staff)
                    {
                        if (TextUtils.ToInt(dtuser.Rows[0]["GroupSalesID"]) == 5)

                            ds = TextUtils.LoadDataSetFromSP("spGetDataKPIPuschase", new string[] { "@Quy", "@UserID", "@year" }, new object[] { cbMonth.SelectedIndex, cbUser.EditValue, nbrYear.Value });
                        else
                            ds = TextUtils.LoadDataSetFromSP("spGetProductivityIndexUpdate", new string[] { "@Quy", "@UserID", "@year", "@Main" }, new object[] { cbMonth.SelectedIndex, cbUser.EditValue, nbrYear.Value, main });

                    }
                    else if (TextUtils.ToString(dtuser.Rows[0]["SaleUserTypeCode"]) == cConsts.Leader || TextUtils.ToString(dtuser.Rows[0]["SaleUserTypeCode"]) == cConsts.LeaderTeam || TextUtils.ToString(dtuser.Rows[0]["SaleUserTypeCode"]) == cConsts.Admin)
                    {
                        ds = TextUtils.LoadDataSetFromSP("spGetProductivityIndexLeaderUpdate", new string[] { "@Quy", "@UserID", "@year", "@position", "@Main" }, new object[] { cbMonth.SelectedIndex, cbUser.EditValue, nbrYear.Value, Position, main });
                    }
                   
                        grdData.DataSource = ds.Tables[1];
                        grvKPIStaff = grvData;
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            LstMainGroupCount.Add(TextUtils.ToInt(ds.Tables[1].Rows[j]["MainGroup"]));
                        }
                        LstMainGroupCount = LstMainGroupCount.Distinct().ToList();
                        //
                        List<string> a = new List<string>();
                        if (ckHide.Checked)
                        {
                            DataRow[] drr = ds.Tables[1].Select($"ID in ({arr})");
                            for (int i = 0; i < drr.Length; i++)
                            {
                                drr[i].Delete();
                                ds.Tables[1].AcceptChanges();

                            }
                        }
                        dtMonth = ds.Tables[0];
                        grbthang1.Caption = "Tháng " + ds.Tables[0].Rows[0]["MonthReport"];
                        grbthang2.Caption = "Tháng " + ds.Tables[0].Rows[1]["MonthReport"];
                        grbthang3.Caption = "Tháng " + ds.Tables[0].Rows[2]["MonthReport"];
                        grvData.ExpandAllGroups();

                        if (group == cConsts.Base)
                        {
                            //tông po và sale
                            colMGroup.GroupIndex = 0;
                            grvData.ExpandAllGroups();
                            for (int j = 0; j < 3; j++)
                            {
                                tongG = tongR = 0;
                                for (int i = 0; i < 6; i++)
                                {
                                    tongG += TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Goal" + j]);
                                    tongR += TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Result" + j]);
                                }
                                ds.Tables[1].Rows[6]["Goal" + j] = tongG;
                                ds.Tables[1].Rows[6]["Result" + j] = tongR;

                                tongG = tongR = 0;
                                for (int i = 7; i < 13; i++)
                                {
                                    tongG += TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Goal" + j]);
                                    tongR += TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Result" + j]);
                                }
                                ds.Tables[1].Rows[13]["Goal" + j] = tongG;
                                ds.Tables[1].Rows[13]["Result" + j] = tongR;
                            }
                        }

                        //tổng quý
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            tongG = tongR = 0;
                            for (int j = 0; j < 3; j++)
                            {
                                tongG += TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Goal" + j]);
                                tongR += TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Result" + j]);

                                decimal result = TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Result" + j]);
                                decimal goal = TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Goal" + j]);
                                decimal per = TextUtils.ToDecimal(ds.Tables[1].Rows[i]["PercentIndex"]);
                                if (goal > 0 && result > 0 && per > 0)
                                    ds.Tables[1].Rows[i]["ACCP" + j] = result / goal * per;

                            }
                            if (group == cConsts.Purchase)
                            {
                                ds.Tables[1].Rows[i]["Goal"] = tongG / 3;
                                ds.Tables[1].Rows[i]["Result"] = tongR / 3;
                                repositoryItemTextEdit2.MaskSettings.MaskExpression = "p";
                                repositoryItemTextEdit2.MaskSettings.UseMaskAsDisplayFormat = true;
                            }
                            else
                            {
                                ds.Tables[1].Rows[i]["Goal"] = tongG;
                                ds.Tables[1].Rows[i]["Result"] = tongR;
                            }
                        }

                        calcuACCP();
                   
                }
            }
            catch (Exception ex)
            { 

            }
        }

        /// <summary>
        /// 
        /// </summary>
        void calcuACCP()
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                decimal G = TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Goal"]);
                decimal R = TextUtils.ToDecimal(ds.Tables[1].Rows[i]["Result"]);
                decimal Percent = TextUtils.ToDecimal(ds.Tables[1].Rows[i]["PercentIndex"]);
                if (G > 0 && R > 0 && Percent > 0)
                    ds.Tables[1].Rows[i]["ACCP"] = R / G * Percent;
            }
            if (group == cConsts.Purchase)
            {

            }
            else
            {
                NewAccountQty = TextUtils.ToInt(ds.Tables[1].Rows[22]["Result"]);//lấy tỏng new acc
                TotalSale = TextUtils.ToDecimal(ds.Tables[1].Rows[6]["Result"]);//lấy tổng sale
            }
            sumACCP = TextUtils.ToDecimal(colACCP.SummaryItem.SummaryValue);

        }
        string month;
        int quy;
        void checktime()
        {

            if (0 < DateTime.Now.Month && DateTime.Now.Month < 4)
            {
                month = "1,2,3";
                quy = 0;
            }
            if (3 < DateTime.Now.Month && DateTime.Now.Month < 7)
            {
                month = "4,5,6";
                quy = 1;
            }
            if (6 < DateTime.Now.Month && DateTime.Now.Month < 10)
            {
                month = "7,8,9";
                quy = 2;
            }
            if (9 < DateTime.Now.Month && DateTime.Now.Month < 13)
            {
                month = "10,11,12";
                quy = 3;
            }
        }
        void SaveData()
        {
            if (quy == cbQuy.SelectedIndex && nbrYear.Value == DateTime.Now.Year)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colIDHistory));
                    HistoryKPISaleModel model = new HistoryKPISaleModel();
                    if (ID > 0)
                    {
                        model = (HistoryKPISaleModel)HistoryKPISaleBO.Instance.FindByPK(ID);
                    }
                    model.MainIndexID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                    model.UserID = TextUtils.ToInt(cbUser.EditValue);
                    model.Quy = TextUtils.ToInt(quy);
                    model.Year = TextUtils.ToInt(nbrYear.Value);
                    model.Goal0 = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colGoal0));
                    model.Goal1 = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colGoal1));
                    model.Goal2 = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colGoal2));
                    model.Result0 = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colResult0));
                    model.Result1 = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colResult1));
                    model.Result2 = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colResult2));
                    model.ACCP0 = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colACCP0));
                    model.ACCP1 = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colACCP1));
                    model.ACCP2 = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colACCP2));
                    model.Goal = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colGoal));
                    model.Result = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colResult));
                    model.ACCP = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colACCP));
                    model.PercentIndex = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPercentIndex));
                    if (model.ID > 0)
                        HistoryKPISaleBO.Instance.Update(model);
                    else
                        HistoryKPISaleBO.Instance.Insert(model);
                }
            }
        }
    }
}
