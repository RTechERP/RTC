using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using static BMS.frmRequestBuyDetail;
using Forms.Classes;

namespace BMS
{
    public partial class frmQuotationKHData : _Forms
    {
        DataTable dtProduct = new DataTable();
        public ListID _listID; // tạo ra ListID để chuyền giữa 2 form    
        public delegate void SendListID(List<string> ID, string group, DataTable dtproduct);
        public SendListID sendListID;

        public frmQuotationKHData()
        {
            InitializeComponent();
        }

        private void frmTargetVisit_Load(object sender, EventArgs e)
        {
            try
            {
               
                cGlobVar.LockEvents = true;
                loadcbPO();
                cbIsPO.SelectedIndex = 0;
                loadUser();
                loadCustomer();
                txtPageNumber.Text = "1";
                loadQuotationData();
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
            
        }

        #region Methods

        private void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Customer where IsDeleted <> 1 ORDER BY CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        private void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName,Code+'-'+FullName AS UserInfo FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
        }
        void loadcbPO()
        {
            DataTable dt = TextUtils.Select($"SELECT q.*,c.CustomerName FROM QuotationKH q Inner join Customer c on c.ID=q.CustomerID");
            cbQuotation.Properties.DisplayMember = "QuotationCode";
            cbQuotation.Properties.ValueMember = "ID";
            cbQuotation.Properties.DataSource = dt;

        }
        DataSet oDataSet;
        DataTable dtGrv;
        private void loadQuotationData()
        {
             dtGrv = TextUtils.LoadDataFromSP("spGetQuotationKHData", "A"
                , new string[] { "@FilterText", "@CustomerID", "@UserID", "@PageSize", "@PageNumber", "@QuotationID", "@IsPO" }
                , new object[] { txtFilterText.Text, TextUtils.ToInt(cbCustomer.EditValue),TextUtils.ToInt(cbUser.EditValue),
                    txtPageSize.Value, TextUtils.ToInt(txtPageNumber.Text),TextUtils.ToInt(cbQuotation.EditValue),cbIsPO.SelectedIndex });
            grdData.DataSource = dtGrv;
            if (dtGrv.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dtGrv.Rows[0]["TotalPage"]);
        }
        #endregion

        #region Button Events

        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadQuotationData();
        }
        #endregion

      

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadQuotationData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadQuotationData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadQuotationData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadQuotationData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadQuotationData();
        }
        List<string> lstProduct = new List<string>();
        List<string> lstID = new List<string>();
        string group;
        private void btnSave_Click(object sender, EventArgs e)
        {
            lstProduct.Clear();
            string  code;
            string ID;
            int[] selectedRowHandles = grvData.GetSelectedRows();
            foreach (int item in selectedRowHandles)
            {
                code = TextUtils.ToString(grvData.GetRowCellValue(item, colProductNewCode));
                ID = TextUtils.ToString(grvData.GetRowCellValue(item, colID));
                if (ID != "" && code != "")
                {
                    lstProduct.Add(code);
                    lstID.Add(ID);
                }
            }
            genProductCode(lstID);
            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// Send data product được chọn
        /// </summary>
        /// <param name="lstcode"></param>
        void genProductCode(List<string> lstcode)
        {
            if (lstcode.Count == 0) return;
            string code = string.Join(",", lstcode);
            string result = lstcode.Aggregate((total, part) => total + "'" + part + "'" + ",");
            result = result.TrimEnd(',');
            result = result.Replace(lstcode[0], $"'{lstcode[0]}',");
            DataRow[] dtr = dtGrv.Select($"ID in ({result})");
            DataTable dtresult= dtr.CopyToDataTable();
            this.sendListID?.Invoke(lstProduct, group, dtresult);
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadQuotationData();
        }

        private void cbPO_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadQuotationData();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadQuotationData();
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadQuotationData();
        }

        private void cbIsPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadQuotationData();
        }
    }
}


