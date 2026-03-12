using BMS.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmAccountingContractLog : _Forms
    {
        public frmAccountingContractLog()
        {
            InitializeComponent();
        }

        private void frmAccountingContractLog_Load(object sender, EventArgs e)
        {
            LoadContract();
            LoadUser();
            LoadData();
        }

        void LoadContract()
        {
            List<AccountingContractModel> list = SQLHelper<AccountingContractModel>.FindAll().Where(x => x.IsDelete == false).OrderByDescending(x => x.DateInput).ToList();
            list.Insert(0, new AccountingContractModel() { ID = -1, ContractNumber = "--Tất cả--"});
            cboContract.Properties.ValueMember = "ID";
            cboContract.Properties.DisplayMember = "ContractNumber";
            cboContract.Properties.DataSource = list;
        }

        void LoadUser()
        {
            List<UsersModel> list = SQLHelper<UsersModel>.FindAll();

            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.DataSource = list;
        }

        void LoadData()
        {
            int accountingContractID = TextUtils.ToInt(cboContract.EditValue) <= 0 ? 0 : TextUtils.ToInt(cboContract.EditValue);
            int userID = TextUtils.ToInt(cboUser.EditValue);
            List<AccountingContractLogDTO> listLog = SQLHelper<AccountingContractLogDTO>.ProcedureToList("spGetAccountingContractLog",
                                                                                            new string[] { "@AccountingContractID", "@UserID" },
                                                                                            new object[] { accountingContractID, userID });

            CalulatorLog(listLog);
            grdData.DataSource = listLog;
        }


        void CalulatorLog(List<AccountingContractLogDTO> listLog)
        {
            //tính tổng tình trạng nhận hồ sơ gốc
            var summarys = grvData.Columns[colIsReceivedContractText.FieldName].Summary;
            if (summarys.Count > 0)
            {
                grvData.Columns[colIsReceivedContractText.FieldName].Summary.Clear();
            }

            var dataGet = listLog.Where(x => x.IsReceivedContract == true).ToList();
            var dataCancel = listLog.Where(x => x.IsReceivedContract == false).ToList();

            grvData.Columns[colIsReceivedContractText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colIsReceivedContractText.FieldName, $"Tổng nhận = {dataGet.Count}"));
            grvData.Columns[colIsReceivedContractText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colIsReceivedContractText.FieldName, $"Tổng huỷ = {dataCancel.Count}"));

            //tính tổng tình trạng duyệt hợp đồng
            var summarysApproved = grvData.Columns[colIsApprovedText.FieldName].Summary;
            if (summarysApproved.Count > 0)
            {
                grvData.Columns[colIsApprovedText.FieldName].Summary.Clear();
            }

            var dataApproved = listLog.Where(x => x.IsApproved == true).ToList();
            var dataUnApproved = listLog.Where(x => x.IsApproved == false).ToList();

            grvData.Columns[colIsApprovedText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colIsApprovedText.FieldName, $"Tổng duyệt = {dataApproved.Count}"));
            grvData.Columns[colIsApprovedText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colIsApprovedText.FieldName, $"Tổng huỷ = {dataUnApproved.Count}"));
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"LichSuCapNhatThongTinHopDong_{DateTime.Now.ToString("ddMMyy")}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cboContract_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboUser_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void frmAccountingContractLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
