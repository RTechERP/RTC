using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraPrinting;
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
    public partial class frmRulePay : _Forms
    {
        public frmRulePay()
        {
            InitializeComponent();
        }
        private void frmRulePay_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            DataTable dt= TextUtils.Select("select * from RulePay");
            grdData.DataSource = dt;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmRulePayDetail frm = new frmRulePayDetail();
            if(frm.ShowDialog()==DialogResult.OK)
            {
                loadData();
            }    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id < 0) return;
            RulePayModel model = new RulePayModel();
            model.ID = id;
            model.Code =TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            model.Note = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNote));
            frmRulePayDetail frm = new frmRulePayDetail();
            frm.model = model;
            if(frm.ShowDialog()==DialogResult.OK)
            {
                loadData();
            }    

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (id < 0) return;
            if (MessageBox.Show($"Bạn có chắc muốn xoá điều khoản có mã {code}.", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                RulePayBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNote));
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/TTLL_NhanVien-{code}-{name}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                grvData.ClearSelection();
            }
        }

     
    }
}
