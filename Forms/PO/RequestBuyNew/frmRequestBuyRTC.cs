using BMS.Business;
using BMS.Model;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections;
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
    public partial class frmRequestBuyRTC : _Forms
    {
        ArrayList ListID = new ArrayList();//List ID danh sách các RequestBuyRTC đã được PONCC.
        public frmRequestBuyRTC()
        {
            InitializeComponent();
        }

        private void frmRequestBuyRTC_Load(object sender, EventArgs e)
        {

            cbStatus.SelectedIndex = 2;
            cbTTTT.SelectedIndex = 2;
            txtFilterText.Select();
            LoadCb();
            //cbTTTT.CheckAll();
            cbTTDH.CheckAll();
            DateTime now = DateTime.Now;
            var endDate = new DateTime(now.Year, now.Month, now.Day);
            var startDate = new DateTime(now.Year, now.Month - 1, now.Day);

            //startDate.AddMonths(1).AddDays(-1);
            dtpFromDate.Value = startDate;
            dtpEndDate.Value = endDate;
            loadData();
            //treeList1.FocusedNode = treeList1.GetNodeByVisibleIndex(0);

        }

     
        void loadData()
        {/// spGetRequestBuyRTC

            //Clear
            ListID.Clear();
            List.Clear();

            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(+1);
            DataSet dts = TextUtils.LoadDataSetFromSP("spGetRequestBuyRTC", new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@TTDH", "@TT", "@TTTT" }
               , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text,
                               cbTTDH.EditValue, cbStatus.SelectedIndex,cbTTTT.SelectedIndex });

            treeList1.DataSource = dts.Tables[0];
            treeList1.ExpandAll();
            if (dts.Tables[0].Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dts.Tables[1].Rows[0]["TotalPage"]);
            treeList1.FocusedNode = treeList1.GetNodeByVisibleIndex(0);

            //Add ListID
            for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
            {
                bool values = TextUtils.ToBoolean(dts.Tables[0].Rows[i]["IsApproved_Level1"]);
                if (values)
                {
                    ListID.Add(TextUtils.ToInt(dts.Tables[0].Rows[i]["ID"]));
                }
            }
        }
        void LoadCb()

        {
 
            //TinhTrangDonHang
            DataTable dtTTDH = TextUtils.Select("Select * from RequestBuyRTCTTDH");
            cbTTDH.Properties.DataSource = dtTTDH;
            cbTTDH.Properties.DisplayMember = "Name";
            cbTTDH.Properties.ValueMember = "ID";

        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
           // treeList1.Focus();
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            _RowIndex = treeList1.GetVisibleIndexByNode(treeList1.FocusedNode);
            frmRequestBuyRTCDetail frm = new frmRequestBuyRTCDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                treeList1.FocusedNode = treeList1.GetNodeByVisibleIndex(_RowIndex);
            }

        }
        int _RowIndex = 0;
        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            _RowIndex = treeList1.GetVisibleIndexByNode(treeList1.FocusedNode);

            int ID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colIDs));


            if (ID <= 0) return;
            string PONumber = TextUtils.ToString(treeList1.GetFocusedRowCellValue(colSoPO));
            bool values = TextUtils.ToBoolean(treeList1.GetFocusedRowCellValue(colYeuCaus));
            if (values)
            {
                MessageBox.Show("Yêu cầu này đã được mua không thể sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RequestBuyRTCModel model = (RequestBuyRTCModel)RequestBuyRTCBO.Instance.FindByPK(ID);
            frmRequestBuyRTCDetail frm = new frmRequestBuyRTCDetail();
            frm.model = model;
            frm.PONumber = PONumber;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                treeList1.FocusedNode = treeList1.GetNodeByVisibleIndex(_RowIndex);
            }
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            ArrayList lstIDDelete = new ArrayList();

            int ID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colIDs));
            int ParentID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colPOKHDetailID));

            if (ID <= 0) return;
            bool values = TextUtils.ToBoolean(treeList1.GetFocusedRowCellValue(colYeuCaus));
            var childNode = treeList1.FindNodes((node) =>
            {
                return TextUtils.ToInt(node["ParentID"]) == ParentID;
            }
            );
            if (values)
            {
                MessageBox.Show("Yêu cầu nhập này đã được mua không thể xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RequestBuyRTCModel model = (RequestBuyRTCModel)RequestBuyRTCBO.Instance.FindByPK(ID);
            if (MessageBox.Show("Bạn có chắc muốn xoá yêu cầu mua này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                RequestBuyRTCBO.Instance.Delete(ID);
                RequestBuyRTCBO.Instance.DeleteByAttribute("ParentID", ParentID);
                treeList1.DeleteSelectedNodes();
                foreach (var item in childNode)
                {
                    treeList1.DeleteNode(item);
                }

            }

        }

        void approved(bool isApproved, int ID)
        {
            treeList1.FocusedNode = null;

            string sql = string.Format(@"UPDATE  RequestBuyRTC  SET IsApproved_Level1 = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
            TextUtils.ExcuteSQL(sql);
        }
        private void btnIsApproved_Click(object sender, EventArgs e)
        {

            if (treeList1.FocusedNode == null) return;

            //Các Node được chọn
            var listNodes = treeList1.FindNodes((node) =>
            {
                return node.IsSelected;
            }
            );

            foreach (var item1 in listNodes)
            {
                int id = TextUtils.ToInt(item1.GetValue(colIDs));//TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colIDs));
                int ParentID = TextUtils.ToInt(item1.GetValue(colPOKHDetailID));//TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colPOKHDetailID));
                //Find  parentnode 
                TreeListNode parentNode = treeList1.FindNode((node) =>
                {
                    return TextUtils.ToInt(node["ID"]) == id;
                }
                );
                //Find all childnode 
                var childNode = treeList1.FindNodes((node) =>
                {
                    return TextUtils.ToInt(node["ParentID"]) == ParentID;
                }
                );
                bool isApprovedParent = TextUtils.ToBoolean(item1.GetValue(colYeuCaus));//treeList1.GetFocusedRowCellValue(colYeuCaus));
                if (isApprovedParent)
                {
                    MessageBox.Show("Yêu cầu này đã được duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show(string.Format("Bạn có chắc muốn  duyệt yêu cầu này không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    treeList1.SetRowCellValue(parentNode, colYeuCaus, true);
                    approved(true, id);

                    if (childNode != null)
                    {
                        //childNode
                        foreach (TreeListNode item in childNode)
                        {
                            treeList1.SetRowCellValue(item, colYeuCaus, true);
                            int IDchild = TextUtils.ToInt(treeList1.GetRowCellValue(item, colIDs));
                            approved(true, IDchild);
                        }
                    }

                }
            }

        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode == null) return;
            int id = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colIDs));
            int ParentID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colPOKHDetailID));
            //Find  parentnode 
            TreeListNode parentNode = treeList1.FindNode((node) =>
            {
                return TextUtils.ToInt(node["ID"]) == id;
            }
            );
            //Find all childnode 
            var childNode = treeList1.FindNodes((node) =>
            {
                return TextUtils.ToInt(node["ParentID"]) == ParentID;
            }
            );
            bool isApprovedParent = TextUtils.ToBoolean(treeList1.GetFocusedRowCellValue(colYeuCaus));
            if (!isApprovedParent)
            {
                MessageBox.Show("Yêu cầu này chưa được duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có chắc muốn huỷ duyệt yêu cầu này không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                treeList1.SetRowCellValue(parentNode, colYeuCaus, false);
                approved(false, id);

                if (childNode != null)
                {
                    //childNode
                    foreach (TreeListNode item in childNode)
                    {
                        treeList1.SetRowCellValue(item, colYeuCaus, false);
                        int IDchild = TextUtils.ToInt(treeList1.GetRowCellValue(item, colIDs));
                        approved(false, IDchild);
                    }
                }


            }
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnTinhTrang_Click(object sender, EventArgs e)
        {
            frmTinhTrang frm = new frmTinhTrang();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadCb();
                cbTTDH.CheckAll();

            }
        }





        private void treeList1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colVats)
            {
                //string value = TextUtils.ToString(treeList1.GetRowCellValue(e.Node, colVats));
                e.DisplayText = TextUtils.ToDecimal(e.Value) + "%";
            }
        }
        List<RequestBuyRTCModel> List = new List<RequestBuyRTCModel>();

        /// <summary>
        /// PONCC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPONCC_Click(object sender, EventArgs e)
        {
            _RowIndex = treeList1.GetVisibleIndexByNode(treeList1.FocusedNode);
            frmPONCCDetail frm = new frmPONCCDetail();
            frm.List = List;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
            // treeList1.Focus();
            treeList1.FocusedNode = treeList1.GetNodeByVisibleIndex(_RowIndex);
            List.Clear();
            // treeList1

        }

        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {   //


            bool values = TextUtils.ToBoolean(treeList1.GetFocusedRowCellValue(colYeuCaus));
            RequestBuyRTCModel model = new RequestBuyRTCModel();
            model.ID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colIDs));
            model.ProductCode_ = TextUtils.ToString(treeList1.GetFocusedRowCellValue(colProductCodes));
            model.ProductName_ = TextUtils.ToString(treeList1.GetFocusedRowCellValue(colProductNames));
            model.Unit = TextUtils.ToString(treeList1.GetFocusedRowCellValue(colUnit));
            model.Qty = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colSoLuong));
            model.ProductID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colProductID));
            model.QtyReal = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colQtyReal));
            model.PriceSale = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colPriceSale));
            if (values)
            {
                List.Add(model); //Add model vào List nếu tích chọn
            }
            else
            {

                RequestBuyRTCModel model1 = List.Find((modelDelete) =>
                {
                    return modelDelete.ID == TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colIDs));
                });

                if (model1 != null)
                {
                    List.Remove(model1);//Xoá model khi bỏ tích chọn
                }  
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode != null && treeList1.FocusedColumn == colYeuCaus)
            {
                int id = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colIDs));

                e.Cancel = ListID.Contains(id) ? true : false;
                //if (ListID.Contains(id))
                //{
                //    e.Cancel = true;
                //}
                //else
                //{
                //    e.Cancel = false;
                //}
            }
        }

        /// <summary>
        /// Vẽ màu lên cách dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            int id = TextUtils.ToInt(e.Node.GetDisplayText(colIDs));
            //DateTime NgayYeuCauGiao = TextUtils.ToDate3(e.Node.GetValue(colNgayYeuCauGiaos));
            //DateTime NgayDatHang = TextUtils.ToDate3(e.Node.GetValue(colNgayDatHangs));
            //DateTime nowDay = DateTime.Now;

            if (e.Column == colYeuCaus)
            {
                if (ListID.Contains(id))
                {
                    e.Appearance.BackColor = Color.FromArgb(84, 255, 159); //Color.Aquamarine;//Color.FromArgb(84,255,159);
                }
            }
            if(e.Column==colNgayDatHangs)
            {
                DateTime NgayYeuCau = TextUtils.ToDate3(e.Node.GetValue(colNgayYeuCauGiaos));

                if(TextUtils.ToString(e.Node.GetValue(colNgayDatHangs))=="" && DateTime.Now.AddDays(3)>= NgayYeuCau)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 192, 192);
                }
                if(TextUtils.ToString(e.Node.GetValue(colNgayDatHangs)) == "" && DateTime.Now.Day >= NgayYeuCau.Day && DateTime.Now.Month >= NgayYeuCau.Month && DateTime.Now.Year >= NgayYeuCau.Year)
                {
                    e.Appearance.BackColor = Color.Red;
                }    
            }

        }

        private void ViewPONCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int ID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue(colIDs));

            //int PONCCID = TextUtils.ToInt(TextUtils.ExcuteScalar($"exec spGetPONCCDetailbyRequestBuyRTCID {ID}"));

            //frmPONCCDetail frm = new frmPONCCDetail();
            //PONCCModel model = (PONCCModel)PONCCBO.Instance.FindByPK(PONCCID);
            //if (model == null) return;
            //frm.oPONCC = model;
            //frm.ShowDialog();

        }

        private void pONCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPONCC_Click(null, null);
        }

      
    }
}
