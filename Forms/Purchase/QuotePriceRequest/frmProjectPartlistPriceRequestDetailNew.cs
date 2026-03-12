using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmProjectPartlistPriceRequestDetailNew : _Forms
    {
        public DataTable dt = new DataTable();
        //List<ProjectPartlistPriceRequestModel> lstData = new List<ProjectPartlistPriceRequestModel>();
        List<int> isDeleteIDs = new List<int>();

        //public bool isJobRequirement = false;
        public string noteJobRequirement = "";
        public int qty = 0;

        int _jobRequirementID = 0;
        int _projectPartlistPriceRequestTypeID = 0;

        public bool isVPP = false;
        public List<int> ListIDProductSale = new List<int>();

        public Action SaveEvent;
        public frmProjectPartlistPriceRequestDetailNew(int jobRequirementID, int projectPartlistPriceRequestTypeID)
        {
            InitializeComponent();
            _jobRequirementID = jobRequirementID;
            _projectPartlistPriceRequestTypeID = projectPartlistPriceRequestTypeID;
        }

        private void frmProjectPartlistPriceRequestDetailNew_Load(object sender, EventArgs e)
        {
            if (_jobRequirementID > 0) this.Text = "YÊU CẦU BÁO GIÁ";

            

            LoadManufacturer(); //VTN Update 17425
            LoadUnit(); //VTN Update 17425
            LoadProductSale();
            LoadProjectPartlistPriceRequestType();
            LoadData();
            LoadEmployee();

            colNote.OptionsColumn.AllowEdit = !(_jobRequirementID > 0);
            colNote.OptionsColumn.ReadOnly = (_jobRequirementID > 0);
        }

        private void LoadProductSale()
        {
            List<ProductSaleModel> lst = SQLHelper<ProductSaleModel>.FindAll();
            cboProductSale.DataSource = lst;
            cboProductSale.DisplayMember = "ProductNewCode";
            cboProductSale.ValueMember = "ProductNewCode";
        }
        private void LoadProjectPartlistPriceRequestType()
        {
            List<ProjectPartlistPriceRequestTypeModel> lst = SQLHelper<ProjectPartlistPriceRequestTypeModel>.FindAll();
            cboProjectPartlistPriceRequestType.Properties.DataSource = lst;
            cboProjectPartlistPriceRequestType.Properties.DisplayMember = "RequestTypeName";
            cboProjectPartlistPriceRequestType.Properties.ValueMember = "ID";

            cboProjectPartlistPriceRequestType.EditValue = _projectPartlistPriceRequestTypeID;
        }
        private void LoadData()
        {
            if (dt.Rows.Count <= 0)
            {
                dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPriceRequest_New", "A",
                                                new string[] { "@DateStart", "@DateEnd" },
                                                new object[] { new DateTime(2000, 1, 1), new DateTime(2000, 1, 1) });
            }

            foreach (DataRow row in dt.Rows) row["UnitCount"] = row["UnitCount"].ToString().ToUpper(); //VTN Update 17425
            foreach (var id in ListIDProductSale)
            {
                var data = SQLHelper<ProductSaleModel>.FindByID(id);
                if (data == null) continue;

                DataRow dtrow = dt.NewRow();
                dtrow["STT"] = dt.Rows.Count + 1;
                dtrow["ProductNewCode"] = data.ProductNewCode;
                dtrow["ProductCode"] = data.ProductCode;
                dtrow["ProductName"] = data.ProductName;
                dtrow["Maker"] = data.Maker;
                dtrow["UnitCount"] = data.Unit.ToUpper();
                dt.Rows.Add(dtrow);
            }
            grdData.DataSource = dt;
            //grdData.DataSource = lstData;

            if ((_jobRequirementID > 0) && dt.Rows.Count <= 0) // VTN update 18225
            {
                grvData.FocusedRowHandle = -1;
                dt.AcceptChanges();
                DataRow dtrow = dt.NewRow();
                dtrow["STT"] = grvData.RowCount + 1;
                dtrow["Quantity"] = qty;
                dtrow["Note"] = noteJobRequirement;
                dt.Rows.Add(dtrow);
            }
        }

        private void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            cboEmployee.Enabled = Global.IsAdmin;
            cboEmployee.EditValue = Global.EmployeeID;
        }

        #region VTN Update request
        void LoadManufacturer()
        {
            List<FirmModel> list = SQLHelper<FirmModel>.FindAll();
            cboMaker.DisplayMember = "FirmName";
            cboMaker.ValueMember = "FirmName";
            cboMaker.DataSource = list;
        }

        void LoadUnit()
        {
            List<string> unitsCountKTs = SQLHelper<UnitCountKTModel>.FindAll().Select(x => x.UnitCountName.ToUpper()).ToList();
            List<string> unitsCounts = SQLHelper<UnitCountModel>.FindAll().Select(x => x.UnitName.ToUpper()).ToList();

            List<string> units = unitsCountKTs.Union(unitsCounts).ToList();
            cboUnit.DataSource = units.Select(x => new { UnitName = x }).ToList();
            cboUnit.DisplayMember = "UnitName";
            cboUnit.ValueMember = "UnitName";
        }
        #endregion
        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);
                if (info.Column != null && info.Column == colAddRow && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();
                    dtrow["STT"] = grvData.RowCount + 1;
                    dt.Rows.Add(dtrow);
                }
            }
        }

        private void btnDeleteRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (grdData.DataSource == null) return;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductNewCode));
            if (MessageBox.Show(String.Format("Bạn có chắc chắn muốn xóa sản phẩm [{0}] của thông tin đề nghị không?", productCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    grvData.SetRowCellValue(i, colSTT, i + 1);
                }
                if (ID > 0 && !isDeleteIDs.Contains(ID)) isDeleteIDs.Add(ID);
            }
        }

        private void cboProductSale_EditValueChanged(object sender, EventArgs e)
        {

        }
        bool CheckValidate()
        {
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            if (employeeID <= 0)
            {
                MessageBox.Show($"Vui lòng chọn Người yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (grvData.RowCount <= 0)
            {
                MessageBox.Show($"Vui lòng tạo ít nhất một yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (TextUtils.ToInt(cboProjectPartlistPriceRequestType.EditValue) <= 0)
            {
                MessageBox.Show($"Vui lòng chọn Loại yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            for (int index = 0; index < grvData.RowCount; index++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(index, colSTT));
                string code = TextUtils.ToString(grvData.GetRowCellValue(index, colProductCode));
                string name = TextUtils.ToString(grvData.GetRowCellValue(index, colProductName));
                string maker = TextUtils.ToString(grvData.GetRowCellValue(index, colMaker));
                string unit = TextUtils.ToString(grvData.GetRowCellValue(index, colUnitCount));
                int quantity = TextUtils.ToInt(grvData.GetRowCellValue(index, colQuantity));
                DateTime? deadline = TextUtils.ToDate4(grvData.GetRowCellDisplayText(index, colDeadline).ToString());

                if (string.IsNullOrWhiteSpace(code))
                {
                    MessageBox.Show($"Vui lòng nhập Mã sản phẩm tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show($"Vui lòng nhập Tên sản phẩm tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!deadline.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Deadline sản phầm tại dòng [{stt}]!", "Thông báo");
                    return false;
                }
                else if (!CheckDeadLine(deadline.Value))
                {
                    return false;
                }

                if (quantity <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập SL yêu cầu tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(maker))
                {
                    MessageBox.Show($"Vui lòng nhập Hãng tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(unit))
                {
                    MessageBox.Show($"Vui lòng nhập ĐVT tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

            }

            return true;
        }

        bool CheckDeadLine(DateTime deadline)
        {
            //Nếu ngày yêu cầu từ sau 15h, thì bắt đầu tính từ ngày hôm sau
            //Nếu ngày yêu cầu là ngày T7 hoặc CN thì bắt đầu tính từ t2
            //Ngày deadline phải lơn hơn ngày yêu cầu từ 2 ngày trở lên
            //Và không tính T7, CN

            if (Global.IsAdmin) return true;

            TimeSpan time = new TimeSpan(15, 0, 0);
            DateTime dateRequest = DateTime.Now;
            TimeSpan timeRequest = TimeSpan.Parse(dateRequest.ToString("HH:mm:ss"));
            if (timeRequest >= time)
            {
                dateRequest = dateRequest.AddDays(+1);
            }

            if (dateRequest.DayOfWeek == DayOfWeek.Saturday)
            {
                dateRequest = dateRequest.AddDays(+1);
            }

            if (dateRequest.DayOfWeek == DayOfWeek.Sunday)
            {
                dateRequest = dateRequest.AddDays(+1);
            }

            List<DateTime> listDates = new List<DateTime>();
            double totalDays = (deadline.Date - dateRequest.Date).TotalDays;
            for (int i = 0; i <= totalDays; i++)
            {
                var date = dateRequest.AddDays(i).Date;
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                if (!listDates.Contains(date))
                {
                    listDates.Add(date);
                }
            }

            if (listDates.Count < 2)
            {
                MessageBox.Show($"Dealine phải ít nhất là 2 ngày tính từ [{dateRequest.ToString("dd/MM/yyyy")}] và KHÔNG tính Thứ 7, Chủ nhật", "Thông báo");
                return false;
            }

            return true;
        }
        private bool SaveData()
        {
            grvData.FocusedRowHandle = -1;

            if (grvData.RowCount <= 0)
            {
                MessageBox.Show($"Vui lòng chọn vào sản phẩm muốn yêu cầu báo giá!", "Thông báo");
                return false;
            }

            if (!CheckValidate()) return false;
            List<int> lstID = new List<int>();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                decimal quantityRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantity));
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                int status = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatusRequest));
                bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheckPrice));
                int empID = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeID));

                ProjectPartlistPriceRequestModel model = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                if (status == 2 || status == 3 || isCheck) continue;
                if (!(_jobRequirementID > 0))
                {
                    if (id > 0 && Global.EmployeeID != empID && !Global.IsAdmin) continue;
                    if (!lstID.Contains(id)) lstID.Add(id);

                    model = new ProjectPartlistPriceRequestModel();
                }

                model.DateRequest = TextUtils.ToDate5(dtpDateRequest.Value); // ngày yêu cầu
                model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue); // người yêu cầu
                model.Deadline = TextUtils.ToDate5(grvData.GetRowCellValue(i, colDeadline));
                model.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCode));
                model.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                model.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, colMaker));
                model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                model.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, colUnitCount));
                model.Quantity = quantityRequest;
                model.StatusRequest = 1; // yêu cầu báo giá
                                         //model.IsCommercialProduct = true; //hàng thương mại

                //if (isJobRequirement) model.IsJobRequirement = true; //Yêu cầu công việc
                //else model.IsCommercialProduct = true; //hàng thương mại

                model.NoteHR = TextUtils.ToString(grvData.GetRowCellValue(i, colNoteHR));
                model.JobRequirementID = _jobRequirementID;

                model.ProjectPartlistPriceRequestTypeID = TextUtils.ToInt(cboProjectPartlistPriceRequestType.EditValue);

                if (model.ID <= 0)
                {
                    if (model.ProjectPartlistPriceRequestTypeID != 4)
                    {
                        if (_jobRequirementID > 0 || isVPP) model.IsJobRequirement = true; //Yêu cầu công việc
                        else model.IsCommercialProduct = true; //hàng thương mại
                    }

                    model.ID = SQLHelper<ProjectPartlistPriceRequestModel>.Insert(model).ID;
                }
                else
                {
                    //model.Note = "";
                    SQLHelper<ProjectPartlistPriceRequestModel>.Update(model);
                }
                string requestNote = TextUtils.ToString(grvData.GetRowCellValue(i, colRequestNote));

                var lstRequestNote = SQLHelper<ProjectPartlistPriceRequestNoteModel>
                    .FindByExpression(new Expression("ProjectPartlistPriceRequestID", model.ID)) ?? new List<ProjectPartlistPriceRequestNoteModel>();

                ProjectPartlistPriceRequestNoteModel priceRequestNote = lstRequestNote.FirstOrDefault() ?? new ProjectPartlistPriceRequestNoteModel();

                priceRequestNote.ProjectPartlistPriceRequestID = model.ID;
                priceRequestNote.Note = requestNote;

                if (priceRequestNote.ID > 0)
                {
                    SQLHelper<ProjectPartlistPriceRequestNoteModel>.Update(priceRequestNote);
                }
                else
                {
                    SQLHelper<ProjectPartlistPriceRequestNoteModel>.Insert(priceRequestNote);
                }
            }

            if (lstID.Count <= 0 || (_jobRequirementID > 0)) return true;


            Dictionary<string, object> newDict = new Dictionary<string, object>()
            {
                {ProjectPartlistPriceRequestModel_Enum.IsDeleted.ToString(), 1},
                {ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(), Global.AppCodeName},
                {ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(), DateTime.Now}
            };
            string strID = string.Join(",", lstID);
            Expression ex1 = new Expression(ProjectPartlistPriceRequestModel_Enum.ID.ToString(), strID, "IN");
            SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(newDict, ex1);

            if (_jobRequirementID > 0 || _projectPartlistPriceRequestTypeID == 3)
            {
                SaveEvent?.Invoke();
            }

            return true;
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmProjectPartlistPriceRequestDetailNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void btnImportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmProjectPartlistPriceRequestImport frm = new frmProjectPartlistPriceRequestImport(_jobRequirementID);
            //frm.jobRequirementID = jobRequirementID
            frm.Show();
        }

        private void cboProductSale_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            SearchLookUpEdit cboData = (SearchLookUpEdit)sender;
            var gridView2 = cboData.Properties.View;
            if (gridView2 == null) return;

            int[] selectedRowHandles = gridView2.GetSelectedRows();
            if (selectedRowHandles == null || selectedRowHandles.Length == 0)
            {
                ProductSaleModel data = (ProductSaleModel)cboData.GetSelectedDataRow();
                if (data == null) return;

                // Check duplicate by ProductNewCode
                if (dt.AsEnumerable().Any(r => r["ProductNewCode"]?.ToString() == data.ProductNewCode))
                    return;

                FirmModel firm = SQLHelper<FirmModel>.FindByID(data.FirmID) ?? new FirmModel();

                grvData.SetFocusedRowCellValue(colProductNewCode, data.ProductNewCode);
                grvData.SetFocusedRowCellValue(colProductCode, data.ProductCode);
                grvData.SetFocusedRowCellValue(colProductName, data.ProductName);
                grvData.SetFocusedRowCellValue(colMaker, string.IsNullOrWhiteSpace(firm.FirmName) ? data.Maker : firm.FirmName);
                grvData.SetFocusedRowCellValue(colUnitCount, data.Unit.ToUpper());
                return;
            }

            var firstData = gridView2.GetRow(selectedRowHandles[0]) as ProductSaleModel;
            if (firstData != null)
            {
                // Check duplicate by ProductNewCode
                if (!dt.AsEnumerable().Any(r => r["ProductNewCode"]?.ToString() == firstData.ProductNewCode))
                {
                    FirmModel firm = SQLHelper<FirmModel>.FindByID(firstData.FirmID) ?? new FirmModel();
                    grvData.SetFocusedRowCellValue(colProductNewCode, firstData.ProductNewCode);
                    grvData.SetFocusedRowCellValue(colProductCode, firstData.ProductCode);
                    grvData.SetFocusedRowCellValue(colProductName, firstData.ProductName);
                    grvData.SetFocusedRowCellValue(colMaker, string.IsNullOrWhiteSpace(firm.FirmName) ? firstData.Maker : firm.FirmName);
                    grvData.SetFocusedRowCellValue(colUnitCount, firstData.Unit.ToUpper());
                }
            }

            for (int i = 1; i < selectedRowHandles.Length; i++)
            {
                var data = gridView2.GetRow(selectedRowHandles[i]) as ProductSaleModel;
                if (data == null) continue;

                // Check duplicate by ProductNewCode
                if (dt.AsEnumerable().Any(r => r["ProductNewCode"]?.ToString() == data.ProductNewCode))
                    continue;

                FirmModel firm = SQLHelper<FirmModel>.FindByID(data.FirmID) ?? new FirmModel();

                DataRow dtrow = dt.NewRow();
                dtrow["STT"] = dt.Rows.Count + 1;
                dtrow["ProductNewCode"] = data.ProductNewCode;
                dtrow["ProductCode"] = data.ProductCode;
                dtrow["ProductName"] = data.ProductName;
                dtrow["Maker"] = string.IsNullOrWhiteSpace(firm.FirmName) ? data.Maker : firm.FirmName;
                dtrow["UnitCount"] = data.Unit.ToUpper();
                dt.Rows.Add(dtrow);
            }
            grdData.DataSource = dt;
        }

        private void gridView2_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                DXMenuItem selectAllItem = new DXMenuItem("Chọn tất cả", (s, args) =>
                {
                    var gridView = sender as GridView;
                    if (gridView != null)
                    {
                        gridView.SelectAll();
                    }
                });
                selectAllItem.Shortcut = Shortcut.CtrlA; // Phím tắt: Ctrl + A

                DXMenuItem clearSelectionItem = new DXMenuItem("Bỏ chọn tất cả", (s, args) =>
                {
                    var gridView = sender as GridView;
                    if (gridView != null)
                    {
                        gridView.ClearSelection();
                    }
                });
                clearSelectionItem.Shortcut = Shortcut.CtrlX; // Phím tắt: Ctrl + D

                // Thêm các mục menu vào PopupMenu
                e.Menu.Items.Add(selectAllItem);
                e.Menu.Items.Add(clearSelectionItem);
            }
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                gridView2.SelectAll();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                gridView2.ClearSelection();
                e.Handled = true;
            }
        }
    }
}
