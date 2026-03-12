using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRequestPriceSummary : _Forms
    {
        public frmRequestPriceSummary()
        {
            InitializeComponent();
        }

        private void frmRequestPriceSummary_Load(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 0;
            //loadHang();
            //loadNCC();
            loadUser();
            loadData();
            //grvData.optionsclip
        }

        #region Methods
        void loadHang()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Manufacturer");
            repositoryItemSearchLookUpEdit2.DataSource = dt;
            repositoryItemSearchLookUpEdit2.ValueMember = "ID";
            repositoryItemSearchLookUpEdit2.DisplayMember = "ManufacturerCode";
        }
        void loadNCC()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Supplier");
            repositoryItemSearchLookUpEdit1.DataSource = dt;
            repositoryItemSearchLookUpEdit1.ValueMember = "ID";
            repositoryItemSearchLookUpEdit1.DisplayMember = "SupplierShortName";
        }
        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            cboRequestUser.Properties.DisplayMember = "FullName";
            cboRequestUser.Properties.ValueMember = "ID";
            cboRequestUser.Properties.DataSource = dt;
            //cboMonitorUser
            cboMonitorUser.Properties.DisplayMember = "FullName";
            cboMonitorUser.Properties.ValueMember = "ID";
            cboMonitorUser.Properties.DataSource = dt;
        }

        private void loadData()
        {
            try
            {
                txtPageNumber.Text = "1";
                txtTotalPage.Text = "1";

                DataSet oDataSet = loadDataSet();
                
                txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0][0]);
            }
            catch (Exception ex)
            {
            }
        }

        DataSet loadDataSet()
        {
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetRequestPriceDetailPaging"
                    , new string[] { "@PageNumber", "@PageSize", "@Status", "@RequestID", "@MonitorID", "@FilterText" }
                    , new object[] { TextUtils.ToInt(txtPageNumber.Text)
                    ,TextUtils.ToInt(txtPageSize.Value)
                    ,cboStatus.SelectedIndex
                    ,TextUtils.ToInt(cboRequestUser.EditValue)
                    ,TextUtils.ToInt(cboMonitorUser.EditValue)
                    //,chkIsImport.Checked
                    , txtFilterText.Text.Trim() });

            grdData.DataSource = oDataSet.Tables[0];

            return oDataSet;
        }
        
        #endregion

        #region Buttons Events

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle))
            //    return;

            //int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));

            //string strName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue("RequestPriceCode"));

            //if (RequestPriceDetailBO.Instance.CheckExist("RequestPriceID", strID))
            //{
            //    MessageBox.Show("Nhà cung cấp này đã phát sinh ở các nghiệp vụ khác nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}

            //if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    try
            //    {
            //        RequestPriceBO.Instance.Delete(strID);
            //        grvMaster.DeleteSelectedRows();
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
            //    }
            //}
        }

        #endregion

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadDataSet();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadDataSet();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadDataSet();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadDataSet();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colRequestPriceID));
                if (id == 0) return;
                RequestPriceModel model = (RequestPriceModel)RequestPriceBO.Instance.FindByPK(id);
              
                frmRequestPriceDetail frm = new frmRequestPriceDetail();
                //frm.dModel = model;
                if(frm.ShowDialog()==DialogResult.OK)
                {
                    loadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetMonitor_Click(object sender, EventArgs e)
        {
            frmChooseEmployee frm = new frmChooseEmployee();
            frm.IsChooseMulti = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                List<int> lstEmployeeID = frm.LstID;
                if (lstEmployeeID.Count == 0) return;

                List<int> lstRequestDetailID = new List<int>();
                int[] lstIndex = grvData.GetSelectedRows();
                for (int i = 0; i < lstIndex.Length; i++)
                {
                    lstRequestDetailID.Add(TextUtils.ToInt(grvData.GetRowCellValue(lstIndex[i], "ID")));
                }
                TextUtils.ExcuteSQL(string.Format("Update RequestPriceDetail set AskPriceID = {0} Where ID in ({1})", lstEmployeeID[0], string.Join(",", lstRequestDetailID)));
                loadData();
            }
        }

        private void btnCancelMonitor_Click(object sender, EventArgs e)
        {
            List<int> lstRequestDetailID = new List<int>();
            int[] lstIndex = grvData.GetSelectedRows();
            for (int i = 0; i < lstIndex.Length; i++)
            {
                lstRequestDetailID.Add(TextUtils.ToInt(grvData.GetRowCellValue(lstIndex[i], "ID")));
            }
            TextUtils.ExcuteSQL(string.Format("Update RequestPriceDetail set AskPriceID = {0} Where ID in ({1})", 0, string.Join(",", lstRequestDetailID)));
            loadData();
        }

        private void btnSendmailNCC_Click(object sender, EventArgs e)
        {
            List<string> lstRow = new List<string>();
            int[] lstIndex = grvData.GetSelectedRows();
            if (lstIndex.Length == 0) return;

            string htmlText = @"<html lang=""en"">
                                    <head>
                                        <meta charset = ""utf-8"" />
                                        <style>
                                          table {
                                            border-collapse: collapse;
                                          }
                                          th, td {
                                            border: 1px solid #adabab;
                                            padding: 10px;
                                            text-align: left;
                                          }
                                        </style>
                                     </head>
                                     <body>
                                        <table >
                                             <tr>
                                                <th> Tên </th>
                                                <th> Model </th>
                                                <th> Hãng </th>
                                                <th> Số lượng </th>
                                                <th> Đơn vị </th>
                                            </tr>
                                             {0}
                                        </table>
                                    </body>
                                </html>";

            string rowText = @"<tr>
                                {0}
                            </tr>";
            string cellText = @"<td>{0}</td>";

            for (int i = 0; i < lstIndex.Length; i++)
            {
                string lstCell = "";
                lstCell += string.Format(cellText, TextUtils.ToString(grvData.GetRowCellDisplayText(lstIndex[i], colName)));
                lstCell += string.Format(cellText, TextUtils.ToString(grvData.GetRowCellDisplayText(lstIndex[i], colCode)));
                lstCell += string.Format(cellText, TextUtils.ToString(grvData.GetRowCellDisplayText(lstIndex[i], colManufacturerCode)));
                lstCell += string.Format(cellText, TextUtils.ToString(grvData.GetRowCellDisplayText(lstIndex[i], colQty)));
                lstCell += string.Format(cellText, TextUtils.ToString(grvData.GetRowCellDisplayText(lstIndex[i], colUnit)));
                lstRow.Add(string.Format(rowText, lstCell));
            }
            htmlText = htmlText.Replace("{0}",string.Join("\n", lstRow));

            string sSubject = "RTC - Xin báo giá";
            string sBody = @"Dear Anh\Chị.<br> Mình là nhân viên phòng mua của công ty RTC-Technology.<br>Anh\Chị cho mình xin báo giá chính thức các mã hàng sau nhé:<br><br>" + htmlText;
            string sTo = TextUtils.ToString(grvData.GetRowCellDisplayText(lstIndex[0], colCustomerEmail));
            string cc = "";
            //List<string> attachments = new List<string>();
            //attachments.Add(@"F:\TECHNICAL\NVTHAO\Sumi.txt");

            TextUtils.ShowEmail(sSubject, sBody, sTo, cc, null);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (e.Control && e.KeyCode == Keys.C )
            //{
            //    //Clipboard.SetText(viewHanghoa.GetFocusedDisplayText());
            //    //e.Handled = true;

            //    //string selectedCellsText = TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn));
            //    //Clipboard.SetDataObject(selectedCellsText);

            //    string str;
            //    MemoryStream ms = new MemoryStream();
            //    view.ExportToHtml(ms);
            //    ms.Seek(0, SeekOrigin.Begin);
            //    StreamReader sr = new StreamReader(ms);
            //    str = sr.ReadToEnd();
            //    Clipboard.SetDataObject(str);

            //    e.Handled = true;
            //}
        }

    }
}
