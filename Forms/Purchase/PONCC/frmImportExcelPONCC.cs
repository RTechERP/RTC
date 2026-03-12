using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmImportExcelPONCC : _Forms
    {
        DataSet ds;
        public frmImportExcelPONCC()
        {
            InitializeComponent();
        }

        private void frmImportExcelPONCC_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                //if (!validate()) return;
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
                    //start = DateTime.Now;
                    //enableControl(false);
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdatePartlist();
        }

        void UpdatePartlist()
        {
            try
            {
                //bool isUpdatePartList = isUpdate.Checked;
                //int versionId = TextUtils.ToInt(cboVersion.EditValue);


                //var exp1 = new Expression("ProjectID", var.ProjectID);
                //var exp2 = new Expression("ProjectTypeID", var.ProjectTypeID);
                //var exp3 = new Expression("ProjectPartListVersionID", versionId);
                //var exp4 = new Expression("IsDeleted", 1, "<>");
                //var partlists = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
                //if (partlists.Count > 0 && !(chkIsProblem.Checked || isUpdatePartList))
                //{
                //    DialogResult dialog = MessageBox.Show("Danh mục vật tư đã tồn tại.\nBạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (dialog != DialogResult.Yes) return;

                //    string idText = string.Join(",", partlists.Select(x => x.ID));
                //    string sql = $"UPDATE dbo.ProjectPartList SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE ID IN ({idText})";
                //    //SQLHelper<ProjectPartListModel>.DeleteListModel(partlists);

                //    TextUtils.ExcuteSQL(sql);
                //}

                ////NT.Huy update lay ra list cac parent TT
                //var listParentTT = new List<string>();
                //foreach (var item in partlists)
                //{
                //    if (!item.TT.Contains(".")) continue;
                //    string parentTt = item.TT.Substring(0, item.TT.LastIndexOf(".")).Trim();
                //    listParentTT.Add(parentTt);
                //}

                //Regex regex = new Regex(@"^-?[\d\.]+$");
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

                    string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();

                    //if (string.IsNullOrEmpty(stt)) continue;
                    //if (!regex.IsMatch(stt)) continue;
                    //if (isUpdatePartList && partlists.Any(x => x.TT == stt)) continue;
                    //if (partlists.Any(x => x.TT == stt)) continue;

                    PONCCHistoryModel po = new PONCCHistoryModel();
                    po.RequestDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F1"));
                    po.CompanyText = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                    po.BillCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                    po.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                    po.DeliveryDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F5"));
                    po.CodeNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                    po.NameNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
                    po.ProductNewCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F8"));
                    po.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, "F9"));
                    po.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F10"));
                    po.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11"));
                    po.UnitPriceVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F12"));
                    po.QtyRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F13"));
                    po.QuantityReturn = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F14"));
                    po.QuantityRemain = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F15"));
                    po.StatusText = TextUtils.ToString(grvData.GetRowCellValue(i, "F16"));
                    po.FullName = TextUtils.ToString(grvData.GetRowCellValue(i, "F17"));
                    po.NCCNew = TextUtils.ToBoolean(grvData.GetRowCellValue(i, "F18"));
                    po.DeptSupplier = TextUtils.ToBoolean(grvData.GetRowCellValue(i, "F19"));
                    po.FeeShip = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F20"));
                    po.PriceSale = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F21"));
                    po.DeadlineDelivery = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F22"));
                    po.POCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F23"));
                    po.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F24"));
                    po.TotalMoneyChangePO = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F25"));
                    po.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F26"));
                    po.ProjectCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F27"));
                    po.PriceHistory = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F28"));
                    po.SupplierVoucher = TextUtils.ToString(grvData.GetRowCellValue(i, "F29"));
                    po.BiddingPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F30"));
                    po.TotalQuantityLast = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F31"));
                    po.MinQuantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F32"));
                    po.ProductCodeOfSupplier = TextUtils.ToString(grvData.GetRowCellValue(i, "F33"));
                    po.CurrencyName = TextUtils.ToString(grvData.GetRowCellValue(i, "F34"));
                    po.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, "F35"));
                    po.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F36"));
                    po.CurrencyRate = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F37"));

                    if (po.ID > 0)
                    {
                        SQLHelper<PONCCHistoryModel>.Update(po);
                    }
                    else
                    {
                        SQLHelper<PONCCHistoryModel>.Insert(po);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grvData.Columns.Clear();
            try
            {
                var tablename = cboSheet.SelectedItem.ToString();

                grdData.DataSource = ds; // dataset
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
            MessageBox.Show($"Cập nhật thành công!", "Thông báo");
            //enableControl(true);
            this.Close();
        }
    }
}
