using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmKPIError : _Forms
    {
        public int departmentID = 0;
        public string deName;
        public frmKPIError()
        {
            InitializeComponent();

        }

        private void frmKPIError_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            LoadData();
        }
        void LoadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIError", "A", new string[] { "@Keyword", "@DepartmentID" }, new object[] { txtKeyword.Text.Trim(), departmentID });
            grdData.DataSource = dt;
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmKPIErrorDetail frm = new frmKPIErrorDetail();
            frm.departmentID = departmentID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(bandedGridView1.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            KPIErrorModel model = SQLHelper<KPIErrorModel>.FindByID(ID);
            frmKPIErrorDetail frm = new frmKPIErrorDetail();
            frm.departmentID = departmentID;
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(bandedGridView1.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            KPIErrorModel model = SQLHelper<KPIErrorModel>.FindByID(ID);
            if (MessageBox.Show($"Bạn có chắc muốn xóa lỗi vi phạm với mã vi phạm là [{model.Code}] không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                model.IsDelete = true;
                SQLHelper<KPIErrorModel>.Update(model);
                LoadData();
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachLoiViPhamKPI_{DateTime.Now:ddMMyy}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, );
                string filepath = f.FileName;

                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                bandedGridView1.OptionsPrint.AutoWidth = false;
                bandedGridView1.OptionsPrint.ExpandAllDetails = false;
                bandedGridView1.OptionsPrint.PrintDetails = true;
                bandedGridView1.OptionsPrint.UsePrintStyles = true;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

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

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData();
        }

        private void btnErrorType_Click(object sender, EventArgs e)
        {
            frmKPIErrorType frm = new frmKPIErrorType();
            frm.Show();
        }

        //-------------------NdNhat-08/03/2025-------------------
        private void bandedGridView1_DoubleClick(object sender, EventArgs e)
        {
            btnKPIErrorFineAmount_Click(null, null);
        }
        void saveEventCallBack()
        {
            LoadData();
        }
        private void btnKPIErrorFineAmount_Click(object sender, EventArgs e)
        {
            
            
            frmKPIErrorFineAmount frm = new frmKPIErrorFineAmount();
            int ID = TextUtils.ToInt(bandedGridView1.GetFocusedRowCellValue(colID));
            if (ID <= 0) {
                MessageBox.Show("Vui lòng chọn lỗi vi phạm cần thiết lập tiền phạt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 
            frm.KpiErrorID = ID;
            frm.SaveEvent = saveEventCallBack;
            frm.Show();
        }
        //-------------------End NdNhat-08/03/2025-------------------
    }
}