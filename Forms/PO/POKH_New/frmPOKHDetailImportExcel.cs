using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPOKHDetailImportExcel : _Forms
    {
        public int idPOKH;
        public DataTable dtClone;
        DateTime start;
        DataSet ds;
        public frmPOKHDetailImportExcel()
        {
            InitializeComponent();
        }

        private void frmPOKHDetailImportExcel_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo("POKH.xlsx");
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start("POKH.xlsx");
                }
                else
                {
                    MessageBox.Show("file doesn't exist", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                if (table.ToString().Contains('.')) continue;
                tableList.Add(table.ToString());
            }

            return tableList;
        }

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;


                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
                {
                    var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    var sw = new Stopwatch();
                    sw.Start();


                    IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

                    var openTiming = sw.ElapsedMilliseconds;

                    ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = false,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false
                        }
                    });


                    var tablenames = GetTablenames(ds.Tables);

                    cboSheet.DataSource = tablenames;

                    if (tablenames.Count > 0) cboSheet.SelectedIndex = 0;

                    btnSave.Enabled = true;
                    var tablename = cboSheet.SelectedItem.ToString();

                    grdData.DataSource = ds; // dataset
                    grdData.DataMember = tablename;
                    grvData.BestFitColumns();

                }

            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                try
                {

                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
                    {
                        var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                        var sw = new Stopwatch();
                        sw.Start();

                        IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

                        var openTiming = sw.ElapsedMilliseconds;

                        ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            UseColumnDataType = false,
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = false
                            }
                        });

                        var tablenames = GetTablenames(ds.Tables);

                        cboSheet.DataSource = tablenames;

                        if (tablenames.Count > 0)
                            cboSheet.SelectedIndex = 0;

                        btnSave.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    btnBrowse.Text = ofd.FileName;
                    cboSheet.DataSource = null;
                    cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);

                    cboSheet_SelectionChangeCommitted(null, null);
                }
            }
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grvData.Columns.Clear();
            try
            {
                var tablename = cboSheet.SelectedItem.ToString();

                grdData.DataSource = ds;
                grdData.DataMember = tablename;
            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
                grdData.DataSource = null;
            }
            if (grdData.DataSource == null)
            {
                try
                {
                    DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                    grdData.DataSource = dt;
                    grvData.PopulateColumns();
                    grvData.BestFitColumns();
                    grdData.Focus();
                }
                catch (Exception ex)
                {
                    TextUtils.ShowError(ex);
                    grdData.DataSource = null;
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                enableControl(true);
                return;
            };
            this.DialogResult = DialogResult.OK;
            //MessageBox.Show($"Chu thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            this.Close();
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!CheckValidate())
            {
                e.Cancel = true;
                return;
            }
            UpdatePOKHDetail();

        }

        private void UpdatePOKHDetail()
        {
            List<string> nameGroup = new List<string>();
            Dictionary<int, string> valuePG = new Dictionary<int, string>();
            int childCOunt = 0;
            int childCount = 0;

            for (int i = 3; i < grvData.RowCount; i++)
            {
                DataRow row = dtClone.NewRow();

                // Kiểm tra có tồn tại nhóm hay không
                string nG = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                if (!nameGroup.Contains(nG))
                {
                    var idPG = SQLHelper<ProductGroupModel>.FindByAttribute("ProductGroupName", nG).FirstOrDefault();
                    if (idPG != null) valuePG.Add(TextUtils.ToInt(idPG.ID), nG);
                    else
                    {
                        MessageBox.Show($"Tên nhóm [{nG}] không tồn tại vui lòng kiểm tra lại dữ liệu!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    nameGroup.Add(nG);
                }

                // Kiểm tra nếu có tồn tại dữ liệu trong ProductSaleModel lấy id
                string valueToFind = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                var key = valuePG.FirstOrDefault(x => x.Value == valueToFind).Key;

                var ex = new Expression("ProductNewCode", TextUtils.ToString(grvData.GetRowCellValue(i, "F3")));
                var ex1 = new Expression("ProductGroupID", key);
                var ex2 = new Expression("ProductCode", TextUtils.ToString(grvData.GetRowCellValue(i, "F5")));
                //var ex3 = new Expression("ProductName", TextUtils.ToString(grvData.GetRowCellValue(i, "F6")));
                var idDetail = SQLHelper<ProductSaleModel>.FindByExpression(ex.And(ex1).And(ex2));
                int productsaleID = 0;
                if (idDetail.Count() <= 0)
                {
                    ProductSaleModel ps = new ProductSaleModel();
                    ps.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
                    ps.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                    productsaleID = SQLHelper<ProductSaleModel>.Insert(ps).ID;
                }
                else productsaleID = idDetail.FirstOrDefault().ID;

                // kiểm tra lưu dữ liệu node cha con 
                string checkTT = TextUtils.ToString(grvData.GetRowCellValue(i, "F1"));
                if (!checkTT.Contains("."))
                {
                    //row["STT"] = TextUtils.ToInt(grvData.GetRowCellValue(i, "F2"));
                    //childCOunt = 0;
                    row["STT"] = TextUtils.ToInt(grvData.GetRowCellValue(i, "F2"));
                    row["TT"] = checkTT;
                    childCount = 0;
                }
                else
                {
                    //childCOunt++;
                    //string TT = "." + checkTT.Split('.')[1];
                    //row["STT"] = childCOunt;
                    //row["TT"] = TT;

                    childCount++;
                    string[] parts = checkTT.Split('.');
                    int level = parts.Length;

                    row["STT"] = childCount;
                    row["TT"] = checkTT;
                }

                // gán các giá trị còn lại trong bảng exel vào bảng
                row["ProductID"] = productsaleID; // lấy ProductsaleID
                row["ProductNewCode"] = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")); // lấy mã nội bộ 
                row["ProductCode"] = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")); // lấy mã sản phẩm
                row["ProductName"] = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")); // lấy tên sản phẩm
                row["GuestCode"] = TextUtils.ToString(grvData.GetRowCellValue(i, "F7")); //Mã theo khách
                row["Maker"] = TextUtils.ToString(grvData.GetRowCellValue(i, "F9")); // lấy hãng
                row["Unit"] = TextUtils.ToString(grvData.GetRowCellValue(i, "F10")); // lấy đơn vị tính 
                //row["NetUnitPrice"] = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F14")); // lấy đơn giá 
                row["Qty"] = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11")); // lấy đơn giá 
                row["UnitPrice"] = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F14")); // lấy đơn giá 

                dtClone.Rows.Add(row);
            }
        }

        private bool CheckValidate()
        {
            string pattern = @"^[^àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴ]+$";
            Regex regex = new Regex(pattern);
            Regex regexStt = new Regex(@"^-?[\d\.]+$");

            List<string> listStt = new List<string>();
            for (int i = 3; i < grvData.RowCount; i++)
            {
                string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                if (string.IsNullOrEmpty(stt)) continue;
                if (!stt.Contains(".")) continue;
                if (!regexStt.IsMatch(stt)) continue;
                stt = stt.Substring(0, stt.LastIndexOf("."));

                if (listStt.Contains(stt)) continue;
                listStt.Add(stt);
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Minimum = 1;
                if (grvData.RowCount == 0)
                {
                    MessageBox.Show(String.Format("Bạn chưa chọn đường dẫn file hoặc tên sheet. Vui lòng chọn và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    progressBar1.Maximum = grvData.RowCount - 1;
                    txtRate.Text = "";
                    start = DateTime.Now;
                    enableControl(false);
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }
    }

}
