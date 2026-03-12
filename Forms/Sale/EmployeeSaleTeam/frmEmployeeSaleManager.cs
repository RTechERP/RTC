using BMS;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using DevExpress.XtraTreeList.Nodes;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Data.Filtering.Helpers.SubExprHelper.ThreadHoppingFiltering;
using Action = System.Action;
using DataTable = System.Data.DataTable;

namespace Forms.Sale.EmployeeSaleManager
{
    public partial class frmEmployeeSaleManager : _Forms
    {
        frmEmployeeSaleManagerDetail frmemployeeSaleManagerDetail;
        frmEmployeeTeamSaleDetail_New frmemployeeTeamSaleDetail_New;
        List<EmployeeTeamSaleModel> _listTeam = new List<EmployeeTeamSaleModel>();
        public frmEmployeeSaleManager()
        {
            InitializeComponent();
        }
        void loadTreeView()
        {
            _listTeam = SQLHelper<EmployeeTeamSaleModel>.ProcedureToList("spGetEmployeeTeamSale",null,null);
            tlGroupSale.DataSource = _listTeam;
            tlGroupSale.ExpandAll();
        }
        void loadEmployeeSale()
        {
            TreeListNode node = tlGroupSale.FocusedNode;
            int selectedID = Convert.ToInt32(node.GetValue("ID"));
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeebyTeamSale", "A", new string[] { "@EmployeeTeamSaleID" }, new object[] { selectedID });
            grdDetails.DataSource = dt;
        }
        private void tlGroupSale_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            loadEmployeeSale();
        }
        Task SaveEventCallBack(EmployeeTeamSaleModel arg)
        {
            this.Invoke(new Action(() =>
            {
                EmployeeTeamSaleModel saleModel = _listTeam.FirstOrDefault(p => p.ID == arg.ID);
                if (saleModel == null)
                {
                    _listTeam.Add(arg);
                    tlGroupSale.DataSource = _listTeam;
                }
                else
                {
                    saleModel.STT = arg.STT;
                    saleModel.Name = arg.Name;
                    saleModel.Code = arg.Code;
                    saleModel.ParentID = arg.ParentID;
                    saleModel.UpdatedBy = arg.UpdatedBy;
                    saleModel.UpdatedDate = arg.UpdatedDate;
                    saleModel.CreatedBy = arg.CreatedBy;
                    saleModel.CreatedDate = arg.CreatedDate;

                }
                tlGroupSale.RefreshDataSource();
                tlGroupSale.ExpandAll();
            }));
            return Task.CompletedTask;
        }
        private void frmEmployeeSaleManager_Load(object sender, EventArgs e)
        {
            loadTreeView();

        }

        private void btnDeleteTeam_Click(object sender, EventArgs e)
        {
            TreeListNode node = tlGroupSale.FocusedNode;
            if (node == null) return;
            int selectedID = TextUtils.ToInt(node.GetValue("ID"));
            StringBuilder text = new StringBuilder();

            EmployeeTeamSaleModel model = SQLHelper<EmployeeTeamSaleModel>.FindByID(selectedID);
            if (model?.ParentID == 0)
            {
                text.Append("team");
            }
            else
            {
                text.Append("Chức vụ");
            }
            if (MessageBox.Show($"Bạn có chắc là muốn xóa {text} này ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Dictionary<string, object> deleted = new Dictionary<string, object>()
                {
                    { "IsDeleted", 1 }
                };
                SQLHelper<EmployeeTeamSaleModel>.UpdateFieldsByID(deleted, selectedID);
                loadTreeView();
            }
        }

        private void btnUpdateTeam_Click(object sender, EventArgs e)
        {

            if (frmemployeeSaleManagerDetail == null || frmemployeeSaleManagerDetail.IsDisposed)
            {
                TreeListNode node = tlGroupSale.FocusedNode;
                if (node == null) return;
                int selectedID = Convert.ToInt32(node.GetValue("ID"));
                EmployeeTeamSaleModel model = SQLHelper<EmployeeTeamSaleModel>.FindByID(selectedID);
                frmemployeeSaleManagerDetail = new frmEmployeeSaleManagerDetail();
                frmemployeeSaleManagerDetail.EmployeeTeamSale = model;
                frmemployeeSaleManagerDetail.SaveEvent += SaveEventCallBack;
                frmemployeeSaleManagerDetail.Show();

                frmemployeeSaleManagerDetail.FormClosed += (s, args) => frmemployeeSaleManagerDetail = null;
            }
            else
            {
                frmemployeeSaleManagerDetail.Activate();
            }
        }

        private void btnAddTeam_Click(object sender, EventArgs e)
        {
            if (frmemployeeSaleManagerDetail == null || frmemployeeSaleManagerDetail.IsDisposed)
            {
                frmemployeeSaleManagerDetail = new frmEmployeeSaleManagerDetail();
                List<EmployeeTeamSaleModel> lst = SQLHelper<EmployeeTeamSaleModel>.FindAll().Where(p => p.IsDeleted == 0).ToList();
                int maxSTT = lst.Max(p => p.STT) ?? 0;
                frmemployeeSaleManagerDetail.EmployeeTeamSale.STT = maxSTT + 1;
                frmemployeeSaleManagerDetail.SaveEvent += SaveEventCallBack;
                frmemployeeSaleManagerDetail.Show();

                frmemployeeSaleManagerDetail.FormClosed += (s, args) => frmemployeeSaleManagerDetail = null;
            }
            else
            {
                frmemployeeSaleManagerDetail.Activate();
            }

        }
        Task SaveEventCallBack1(EmployeeTeamSaleLinkModel arg)
        {
            this.Invoke(new Action(() =>
            {
           
                loadEmployeeSale();
                grdDetails.RefreshDataSource();
               
            }));
            return Task.CompletedTask;
        }
        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            if (frmemployeeTeamSaleDetail_New == null || frmemployeeTeamSaleDetail_New.IsDisposed)
            {
                int teamSelectedID = TextUtils.ToInt(tlGroupSale.FocusedNode.GetValue("ID"));
                int parentID = TextUtils.ToInt(tlGroupSale.FocusedNode.GetValue("ParentID"));

                if (parentID == 0) 
                {
                    TextUtils.ShowError("Hãy thêm nhân viên theo chức vụ (chọn một chức vụ rồi ấn thêm)!");
                    return;
                }

                frmemployeeTeamSaleDetail_New = new frmEmployeeTeamSaleDetail_New();
                frmemployeeTeamSaleDetail_New.linkModel.EmployeeTeamSaleID = teamSelectedID;

                frmemployeeTeamSaleDetail_New.SaveEvent += SaveEventCallBack1;
                frmemployeeTeamSaleDetail_New.FormClosed += (s, args) => frmemployeeTeamSaleDetail_New = null;

                frmemployeeTeamSaleDetail_New.Show();
            }
            else
            {
                frmemployeeTeamSaleDetail_New.Activate();
            }
        }


        private void btnDeleteEmp_Click(object sender, EventArgs e)
        {
            int selectedLinkID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue("LinkID"));
            if (selectedLinkID == 0) TextUtils.ShowError("Hãy chọn một nhân viên để xóa!");
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SQLHelper<EmployeeTeamSaleLinkModel>.DeleteModelByID(selectedLinkID);
                loadEmployeeSale();
            }
        }

        private void tlGroupSale_DoubleClick(object sender, EventArgs e)
        {
            btnUpdateTeam_Click(null, null);
        }
    }
}