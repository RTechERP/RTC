using BMS;
using BMS.Model;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.DanhMuc.DuAn.ProjectPartlist
{
    public partial class frmViewProjectPartlist : _Forms
    {
        string _productCode = string.Empty;
        public ProjectPartListVersionModel project = new ProjectPartListVersionModel();

        public frmViewProjectPartlist(string productCode)
        {
            InitializeComponent();
            _productCode = productCode;

        }

        private void frmViewProjectPartlist_Load(object sender, EventArgs e)
        {
            txtKeyword.Text = _productCode;
            loadData();
            loadDataDB();
        }
        void loadData()
        {
            grdData.DataSource = null;
            grdHistoryPrice.DataSource = null;

            string keyWord = txtKeyword.Text.ToString();
           // int projectID = TextUtils.ToInt(cboProject.EditValue);


            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load..."))
            {
                DataTable data = TextUtils.LoadDataFromSP("spGetProductInventoryByKeyword", "table1",
                                        new string[] { "Keyword", "ProductCode"},
                                        new object[] { keyWord, _productCode});
                grdData.DataSource = data;

                DataTable dt = TextUtils.GetDataTableFromSP("spGetHistoryPricePartlist_Khanh",
                                                    new string[] { "@Keyword", "@ProductCode"},
                                                    new object[] { keyWord, _productCode});
                grdHistoryPrice.DataSource = dt;
            }
        }

        void loadDataDB()
        {
            treeListData.DataSource = null;

            int version = -1;
            int partListTypeID = 0;
            //var isFocusGP = grvData.IsFocusedView;
            //var isFocusPO = grvDataPO.IsFocusedView;

            //int versionId = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            //int versionId = 0;

            //version = 19;
            int projectID = 0;
            int isDelete = 0;
            int isApprovedTBP = - 1;
            int isApprovedPur = - 1;
            //int type = TextUtils.ToInt(cboPartListType.EditValue);

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load..."))
            {
               DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartList_Khanh", "A",
                                        new string[] { "@ProjectID", "@PartListTypeID", "@IsDeleted", "@Keyword", "@IsApprovedTBP", "@IsApprovedPurchase", "@ProjectPartListVersionID" },
                                        new object[] { projectID, partListTypeID, isDelete, txtKeyword.Text.Trim(), isApprovedTBP, isApprovedPur, version });

                if (!dt.Columns.Contains("StatusVersionText"))
                {
                    dt.Columns.Add("StatusVersionText", typeof(string));
                }

                foreach (DataRow row in dt.Rows)
                {
                    int val = TextUtils.ToInt(row["StatusVersion"]);

                    if (val == 1)
                        row["StatusVersionText"] = "Giải pháp";
                    else if (val == 2)
                        row["StatusVersionText"] = "PO";
                    else
                        row["StatusVersionText"] = "";
                }
                if (dt.Rows.Count > 0)
                {
                    //CalculateWork(dt);
                    //CalculateAllWork(dt);
                }

                //treeListData.BeginUpdate();
                //treeListData.LockReloadNodes();
                //Lib.LockEvents = true;
                //treeListData.DataSource = null;
                treeListData.DataSource = dt;

                //Lib.LockEvents = false;
                //treeListData.UnlockReloadNodes();
                //treeListData.EndUpdate();

                //treeListData.Refresh();
                treeListData.ParentFieldName = "ParentID";
                treeListData.KeyFieldName = "ID";
                treeListData.ExpandAll();
                //popupMenu1.HidePopup();

                //treeListData.FocusedNode = treeListData.Nodes.LastNode;

                CalculatorData();
            }
        }

        private void treeListData_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.HasChildren) return;
            bool isNewCode = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsNewCode));
            if (!isNewCode) return;

            if (e.Column != colGroupMaterial && e.Column != colNameProductCode && e.Column != colManufacturer && e.Column != colNameUnit) return;

            //check tên thiết bị
            if (e.Column == colGroupMaterial)
            {
                int totalSame = TextUtils.ToInt(treeListData.GetRowCellValue(e.Node, colIsSameProductName));
                if (totalSame == 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }
            }

            //check mã thiết bị
            if (e.Column == colNameProductCode)
            {
                int totalSame = TextUtils.ToInt(treeListData.GetRowCellValue(e.Node, colIsSameProductCode));
                if (totalSame == 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }


                //bool isFix = TextUtils.ToBoolean(treeListData.GetRowCellValue(e.Node, colIsFix));
                //if (isFix)
                //{
                //    e.Appearance.BackColor = Color.DeepPink;
                //}
            }

            //check hãng
            if (e.Column == colManufacturer)
            {
                int totalSame = TextUtils.ToInt(treeListData.GetRowCellValue(e.Node, colIsSameMaker));
                if (totalSame == 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }
            }

            //check đơn vị
            if (e.Column == colNameUnit)
            {
                int totalSame = TextUtils.ToInt(treeListData.GetRowCellValue(e.Node, colIsSameUnit));
                if (totalSame == 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }
            }


        }
        private void CalculatorData()
        {
            List<TreeListNode> lst = treeListData.GetNodeList();
            for (int i = lst.Count - 1; i >= 0; i--)
            {
                TreeListNode node = lst[i];
                if (!node.HasChildren) continue;

                node.SetValue(colIsNewCode.FieldName, false);
                node.SetValue(colIsApprovedTBPNewCode.FieldName, false);

                decimal totalAmount = 0;
                decimal totalAmountQuote = 0;
                decimal totalAmountPurchase = 0;
                decimal totalPriceExchangePurchase = 0;
                decimal totalPriceExchangeQuote = 0;


                foreach (TreeListNode nodeChild in node.Nodes)
                {
                    totalAmount += TextUtils.ToDecimal(nodeChild["Amount"]);
                    totalAmountQuote += TextUtils.ToDecimal(nodeChild["TotalPriceQuote"]);
                    totalAmountPurchase += TextUtils.ToDecimal(nodeChild["TotalPricePurchase"]);
                    totalPriceExchangePurchase += TextUtils.ToDecimal(nodeChild[colTotalPriceExchangePurchase.FieldName]);
                    totalPriceExchangeQuote += TextUtils.ToDecimal(nodeChild[colTotalPriceExchangeQuote.FieldName]);
                }

                if (node.HasChildren) node[colPrice.FieldName] = 0;

                node["Amount"] = totalAmount;
                node["TotalPriceQuote"] = totalAmountQuote;
                node["TotalPricePurchase"] = totalAmountPurchase;
                node[colTotalPriceExchangePurchase.FieldName] = totalPriceExchangePurchase;
                node[colTotalPriceExchangeQuote.FieldName] = totalPriceExchangeQuote;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

     
    }
}