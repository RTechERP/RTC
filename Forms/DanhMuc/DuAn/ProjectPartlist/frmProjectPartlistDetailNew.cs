using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;

namespace BMS
{
    public partial class frmProjectPartlistDetailNew : _Forms
    {
        //public int projectId = 0;
        //public int versionId = 0;
        //public int projectSolutionId = 0;

        public class Variable
        {
            public int ProjectID { get; set; }
            public int VersionID { get; set; }
            public int ProjectSolutionID { get; set; }
            public bool IsPriceCheck { get; set; }
        }


        public ProjectPartListModel partList = new ProjectPartListModel();
        public Variable var = new Variable();
        public ProjectPartListModel partListOld = new ProjectPartListModel();

        string productName = "";


        public bool isUpdate = false;
        public frmProjectPartlistDetailNew()
        {
            InitializeComponent();
        }

        private void frmProjectPartlistDetailNew_Load(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load..."))
            {
                LoadProject();
                LoadVersion();
                LoadUnit();
                LoadSupplier();
                LoadCurrency();
                LoadEmployee();

                LoadSuggestion();
                LoadData();
            }

        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DataSource = list;

            cboProject.EditValue = var.ProjectID;
        }


        void LoadVersion()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartListVersion", "a", new string[] { "@ProjectSolutionID" }, new object[] { var.ProjectSolutionID });
            cboVersion.Properties.DisplayMember = "Code";
            cboVersion.Properties.ValueMember = "ID";
            cboVersion.Properties.DataSource = dt;

            cboVersion.EditValue = var.VersionID;
        }

        void LoadUnit()
        {
            var unitCounts = SQLHelper<UnitCountModel>.FindAll().Select(x => new { UnitName = x.UnitName }).Distinct().ToList();
            //list = list.Select(c => { c.UnitName = c.UnitName.ToUpper().Trim(); return c; }).ToList();
            //list.ForEach(x => x.UnitName = x.UnitName.ToUpper().Trim());

            cboUnit.Properties.DisplayMember = "UnitName";
            cboUnit.Properties.ValueMember = "UnitName";
            cboUnit.Properties.DataSource = unitCounts;
        }

        void LoadSupplier()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboNCC.Properties.DisplayMember = "CodeNCC";
            cboNCC.Properties.ValueMember = "ID";
            cboNCC.Properties.DataSource = list;

            cboNCCFinal.Properties.DisplayMember = "CodeNCC";
            cboNCCFinal.Properties.ValueMember = "ID";
            cboNCCFinal.Properties.DataSource = list;
        }

        void LoadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();

            cboCurrencyQuote.Properties.DisplayMember = "Code";
            cboCurrencyQuote.Properties.ValueMember = "Code";
            cboCurrencyQuote.Properties.DataSource = list;

            cboCurrencyPurchase.Properties.DisplayMember = "Code";
            cboCurrencyPurchase.Properties.ValueMember = "Code";
            cboCurrencyPurchase.Properties.DataSource = list;
        }

        void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
        }


        void LoadSuggestion()
        {
            DataTable dt = TextUtils.GetTable("spGetProjectPartlistSuggest");

            if (dt != null)
            {
                AutoCompleteStringCollection suggestionNames = new AutoCompleteStringCollection();
                AutoCompleteStringCollection suggestionMakes = new AutoCompleteStringCollection();

                foreach (DataRow row in dt.Rows)
                {
                    suggestionNames.Add(TextUtils.ToString(row["ProductName"]));
                    suggestionMakes.Add(TextUtils.ToString(row["Maker"]));
                }

                txtGroupMaterial.AutoCompleteCustomSource = suggestionNames;
                txtManufacturer.AutoCompleteCustomSource = suggestionMakes;
            }
        }

        void LoadData()
        {
            if (partList.ID > 0)
            {
                cboProject.EditValue = partList.ProjectID;
                cboVersion.EditValue = partList.ProjectPartListVersionID;
                txtSTT.Value = TextUtils.ToInt(partList.STT);
                chkIsDeleted.Checked = TextUtils.ToBoolean(partList.IsDeleted);

                partListOld = new ProjectPartListModel()
                {
                    //TT = partList.TT,
                    ProductCode = partList.ProductCode,
                    SpecialCode = partList.SpecialCode,
                    GroupMaterial = partList.GroupMaterial,
                    Manufacturer = partList.Manufacturer,
                    Model = partList.Model,
                    QtyMin = TextUtils.ToDecimal(TextUtils.ToDecimal(partList.QtyMin).ToString("n2")),
                    QtyFull = TextUtils.ToDecimal(TextUtils.ToDecimal(partList.QtyFull).ToString("n2")),
                    Unit = TextUtils.ToString(partList.Unit).ToUpper().Trim(),
                    EmployeeID = partList.EmployeeID,
                    IsProblem = partList.IsProblem,
                    ReasonProblem = partList.ReasonProblem,
                    Note = partList.Note,
                };
            }
            else
            {
                txtSTT.Value = GetSTT();
            }
            //Thông tin chung
            txtTT.Text = partList.TT;
            txtProductCode.Text = partList.ProductCode;
            txtSpecialCode.Text = partList.SpecialCode;
            txtGroupMaterial.Text = partList.GroupMaterial;
            txtManufacturer.Text = partList.Manufacturer;
            txtModel.Text = partList.Model;
            txtQtyMin.Value = TextUtils.ToDecimal(partList.QtyMin);
            txtQtyFull.Value = TextUtils.ToDecimal(partList.QtyFull);
            cboUnit.EditValue = TextUtils.ToString(partList.Unit).ToUpper().Trim();
            cboEmployee.EditValue = partList.EmployeeID;
            chkIsProblem.Checked = partList.IsProblem == true;
            txtReasonProblem.Text = partList.ReasonProblem;
            txtNote.Text = partList.Note;

            //Thông tin báo giá
            cboNCC.EditValue = partList.SupplierSaleID;//
            txtNCC.Text = partList.NCC;
            txtPrice.Value = TextUtils.ToInt(partList.Price);
            txtAmount.Value = TextUtils.ToDecimal(partList.Amount);
            cboCurrencyQuote.EditValue = partList.UnitMoney;//
            txtLeadTime.Text = partList.LeadTime;

            //Thông tin mua
            txtOrderCode.Text = partList.OrderCode;
            cboNCCFinal.EditValue = partList.SupplierSaleID;//
            txtNCCFinal.Text = partList.NCCFinal;
            txtPriceOrder.Value = TextUtils.ToDecimal(partList.PriceOrder);
            txtTotalPriceOrder.Value = TextUtils.ToDecimal(partList.TotalPriceOrder);
            cboCurrencyPurchase.EditValue = partList.UnitMoney;//
            txtLeadTimePurchase.Text = partList.LeadTime;//
            dtpRequestDate.EditValue = partList.RequestDate;
            dtpExpectedReturnDate.EditValue = partList.ExpectedReturnDate;
            dtpOrderDate.EditValue = partList.OrderDate;
            dtpReturnDate.EditValue = partList.RequestDate;
            cboStatus.SelectedIndex = TextUtils.ToInt(partList.Status);
            txtQuality.Text = partList.Quality;


            if (chkIsProblem.Checked && !Global.IsAdmin)
            {
                txtQtyMin.Enabled = true;
                txtQtyFull.Enabled = true;
                //cboUnit.Enabled = true;
                //cboNCC.Enabled = true;
                //txtNCC.Enabled = true;
                //txtPrice.Enabled = true;
                //txtAmount.Enabled = true;
                //cboCurrencyQuote.Enabled = true;
                //txtLeadTime.Enabled = true;
            }
            else if (partList.IsApprovedTBP == true && !Global.IsAdmin)
            {
                txtQtyMin.Enabled = false;
                txtQtyFull.Enabled = false;
                //cboUnit.Enabled = false;
                //cboNCC.Enabled = false;
                //txtNCC.Enabled = false;
                //txtPrice.Enabled = false;
                //txtAmount.Enabled = false;
                //cboCurrencyQuote.Enabled = false;
                //txtLeadTime.Enabled = false;
            }

            if (!(Global.IsAdmin && Global.EmployeeID <= 0))
            {
                btnSave.Enabled = btnSaveAndNew.Enabled = !(partList.IsApprovedTBP == true || partList.IsApprovedTBPNewCode == true);
            }
        }

        int GetSTT()
        {
            int versionID = TextUtils.ToInt(cboVersion.EditValue);
            var listPartlists = SQLHelper<ProjectPartListModel>.FindByAttribute("ProjectPartListVersionID", versionID);
            int stt = listPartlists.Count <= 0 ? 1 : listPartlists.Max(x => TextUtils.ToInt(x.STT)) + 1;
            return stt;
        }

        bool SaveData()
        {
            try
            {
                if (!CheckValidate()) return false;
                //Thông tin chung
                partList.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                partList.ProjectPartListVersionID = TextUtils.ToInt(cboVersion.EditValue);
                partList.TT = txtTT.Text.Trim();
                partList.ProductCode = txtProductCode.Text.Trim();
                partList.SpecialCode = txtSpecialCode.Text.Trim();
                partList.GroupMaterial = txtGroupMaterial.Text.Trim();
                partList.Manufacturer = txtManufacturer.Text.Trim();
                partList.Model = txtModel.Text.Trim();
                partList.QtyMin = txtQtyMin.Value;
                partList.QtyFull = txtQtyFull.Value;
                partList.Unit = TextUtils.ToString(cboUnit.EditValue);
                partList.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                partList.IsProblem = chkIsProblem.Checked;
                partList.ReasonProblem = txtReasonProblem.Text.Trim();
                partList.Note = txtNote.Text.Trim();

                //Thông tin báo giá
                partList.SupplierSaleID = TextUtils.ToInt(cboNCC.EditValue);//
                partList.NCC = txtNCC.Text.Trim();
                partList.Price = txtPrice.Value;
                partList.Amount = txtAmount.Value;
                partList.UnitMoney = TextUtils.ToString(cboCurrencyQuote.EditValue);//
                partList.LeadTime = txtLeadTime.Text.Trim();

                //Thông tin mua
                partList.OrderCode = txtOrderCode.Text.Trim();
                partList.SuplierSaleFinalID = TextUtils.ToInt(cboNCCFinal.EditValue);//
                partList.NCCFinal = txtNCCFinal.Text.Trim();
                partList.PriceOrder = txtPriceOrder.Value;
                partList.TotalPriceOrder = txtTotalPriceOrder.Value;
                partList.UnitMoney = TextUtils.ToString(cboCurrencyPurchase.EditValue);//
                partList.LeadTime = txtLeadTimePurchase.Text.Trim();//
                partList.RequestDate = TextUtils.ToDate4(dtpRequestDate.EditValue);
                partList.ExpectedReturnDate = TextUtils.ToDate4(dtpExpectedReturnDate.EditValue);
                partList.OrderDate = TextUtils.ToDate4(dtpOrderDate.EditValue);
                partList.RequestDate = TextUtils.ToDate4(dtpReturnDate.EditValue);
                partList.Status = cboStatus.SelectedIndex;
                partList.Quality = txtQuality.Text.Trim();
                partList.IsDeleted = chkIsDeleted.Checked;

                DataRowView dataVerion = (DataRowView)cboVersion.GetSelectedDataRow();
                //partList.ProjectTypeID = 0;
                partList.ProjectTypeID = TextUtils.ToInt(dataVerion["ProjectTypeID"]);
                var type = (DataRowView)cboVersion.GetSelectedDataRow();
                if (type != null) partList.ProjectTypeID = TextUtils.ToInt(type["ProjectTypeID"]);

                partList.ParentID = GetParentID(partList.TT, TextUtils.ToInt(partList.ProjectTypeID), TextUtils.ToInt(partList.ProjectPartListVersionID));
                partList.STT = TextUtils.ToInt(txtSTT.Value);

                if (/*chkIsProblem.Checked || */partList.ID <= 0)
                {
                    partList.IsApprovedPurchase = false;
                    partList.IsApprovedTBP = false;
                    SQLHelper<ProjectPartListModel>.Insert(partList);
                }
                else
                {
                    var partlistProblem = SQLHelper<ProjectPartListModel>.FindByID(partList.ID);
                    if (chkIsProblem.Checked && !TextUtils.ToBoolean(partlistProblem.IsProblem))
                    {
                        partList.IsApprovedPurchase = false;
                        partList.IsApprovedTBP = false;

                        //Update thông tin yc báo giá
                        partList.StatusPriceRequest = 0;
                        partList.DatePriceRequest = null;
                        partList.DeadlinePriceRequest = null;

                        //Update thông tin yc mua
                        partList.IsApprovedPurchase = false;
                        partList.RequestDate = null;
                        partList.ExpectedReturnDate = null;
                        partList.Status = 2;

                        SQLHelper<ProjectPartListModel>.Insert(partList);
                    }
                    else
                    {

                        SQLHelper<ProjectPartListModel>.Update(partList);

                        UpdateRequestQuote(partList.ID);
                    }
                }
                isUpdate = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }

        bool CheckValidate()
        {
            //check đã có yc báo giá chưa
            var expR1 = new Expression(ProjectPartlistPriceRequestModel_Enum.ProjectPartListID, partList.ID);
            var expR2 = new Expression(ProjectPartlistPriceRequestModel_Enum.IsDeleted, 1, "<>");
            var expR3 = new Expression(ProjectPartlistPriceRequestModel_Enum.IsCommercialProduct, 1, "<>");
            var priceRequest = SQLHelper<ProjectPartlistPriceRequestModel>.FindByExpression(expR1.And(expR2).And(expR3)).FirstOrDefault();
            priceRequest = priceRequest ?? new ProjectPartlistPriceRequestModel();
            if (priceRequest.ProjectPartListID > 0)
            {
                if (priceRequest.IsCheckPrice == true)
                {
                    MessageBox.Show($"Thiết bị mã [{txtProductCode.Text.Trim()}] đang được check giá. Bạn không thể sửa.\nVui lòng liên hệ nhân viên mua hàng", "Thông báo");
                    return false;
                }

                if (priceRequest.StatusRequest == 2)
                {
                    MessageBox.Show($"Thiết bị mã [{txtProductCode.Text.Trim()}] đã báo giá. Bạn không thể sửa.\nVui lòng liên hệ nhân viên mua hàng", "Thông báo");
                    return false;
                }

                if (priceRequest.StatusRequest == 3)
                {
                    MessageBox.Show($"Thiết bị mã [{txtProductCode.Text.Trim()}] đã hoàn thành báo giá. Bạn không thể sửa.\nVui lòng liên hệ nhân viên mua hàng", "Thông báo");
                    return false;
                }
            }

            //check đã có yc mua hàng chưa
            if (partList.ID > 0)
            {
                var ex1 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.ProjectPartListID, partList.ID);
                var ex2 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.IsDeleted, 0);
                var ex3 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.StatusRequest, 2, "<>");
                var purchaseRequestDeletes = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByExpression(ex1.And(ex2));
                var purchaseRequestCancels = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByExpression(ex1.And(ex3));

                if (purchaseRequestDeletes.Count > 0 && purchaseRequestCancels.Count > 0)
                {
                    MessageBox.Show($"Thiết bị mã [{txtProductCode.Text.Trim()}] đã yêu cầu mua. Bạn không thể sửa.\nVui lòng liên hệ nhân viên mua hàng hoặc PM để hủy YÊU CẦU MUA HÀNG trước", "Thông báo");
                    return false;
                }
            }

            //string pattern = @"^[a-zA-Z0-9_!@#$%^()&*\-\s]+$";
            string pattern = @"^[^àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴ]+$";
            Regex regex = new Regex(pattern);

            int versionID = TextUtils.ToInt(cboVersion.EditValue);
            string tt = txtTT.Text.Trim();
            if (TextUtils.ToInt(cboProject.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Dự án!", "Thông báo");
                return false;
            }

            if (versionID <= 0)
            {
                MessageBox.Show("Vui lòng nhập Phiên bản!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(tt))
            {
                MessageBox.Show("Vui lòng nhập TT!", "Thông báo");
                return false;
            }
            else
            {
                var exp1 = new Expression("ProjectPartListVersionID", versionID);
                var exp2 = new Expression("TT", tt);
                var exp3 = new Expression("ID", partList.ID, "<>");
                var exp4 = new Expression("IsDeleted", 1, "<>");
                var exp5 = new Expression("IsProblem", 1, "<>");
                var partlists = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5));
                if (partlists.Count > 0 && !chkIsProblem.Checked)
                {
                    MessageBox.Show($"TT [{tt}] đã tồn tại.\nVui lòng kiểm tra lại!", "Thông báo");
                    return false;
                }

                //var exp6 = new Expression(ProjectPartListModel_Enum.SpecialCode, txtSpecialCode.Text.Trim());
                //var specialCodes = SQLHelper<ProjectPartListModel>.FindByExpression(exp6.And(exp4));
                //if (specialCodes.Count > 0 && !string.IsNullOrWhiteSpace(txtSpecialCode.Text.Trim()))
                //{
                //    MessageBox.Show($"Mã đặc biệt [{txtSpecialCode.Text}] đã tồn tại!", "Thông báo");
                //    return false;
                //}

                if (!string.IsNullOrWhiteSpace(txtSpecialCode.Text))
                {
                    var exp6 = new Expression(ProjectPartListModel_Enum.SpecialCode, txtSpecialCode.Text.Trim());
                    var exp7 = new Expression(ProjectPartListModel_Enum.ID, partList.ID, "<>");
                    var specialCodes = SQLHelper<ProjectPartListModel>.FindByExpression(exp6.And(exp4).And(exp3));
                    if (specialCodes.Count > 0)
                    {
                        MessageBox.Show($"Mã đặc biệt [{txtSpecialCode.Text}] đã tồn tại!", "Thông báo");
                        return false;
                    }
                }

            }

            var expIsDeleted = new Expression("IsDeleted", 1, "<>");
            var expParentID = new Expression("ParentID", partList.ID);
            var listChilds = SQLHelper<ProjectPartListModel>.FindByExpression(expIsDeleted.And(expParentID));
            if (listChilds.Count <= 0)
            {
                if (string.IsNullOrEmpty(txtProductCode.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Mã thiết bị!", "Thông báo");
                    return false;
                }
                else
                {
                    bool isCheck = regex.IsMatch(txtProductCode.Text.Trim());
                    if (!isCheck)
                    {
                        MessageBox.Show("Mã thiết bị không được chứa ký tự tiếng Việt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(txtGroupMaterial.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Tên thiết bị!", "Thông báo");
                    return false;
                }

                if (chkIsProblem.Checked && string.IsNullOrEmpty(txtReasonProblem.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Lý do phát sinh!", "Thông báo");
                    return false;
                }

                if (string.IsNullOrEmpty(txtManufacturer.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Hãng!", "Thông báo");
                    return false;
                }

                if (string.IsNullOrEmpty(cboUnit.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Đơn vị!", "Thông báo");
                    return false;
                }

            }



            //NTA B - update 13/08/2025
            var expProductCode = new Expression("ProductCode", txtProductCode.Text.Trim());
            var checkIsDeleted = new Expression("IsDeleted", 1, "<>");
            var productSale = SQLHelper<ProductSaleModel>.FindByExpression(expProductCode.And(checkIsDeleted)).FirstOrDefault();

            if (productSale != null && productSale.IsFix)
            {
                List<string> errors = new List<string>();

                string productCode = txtProductCode.Text.Trim();
                string productNameInput = txtGroupMaterial.Text.Trim();
                string makerInput = txtManufacturer.Text.Trim();
                string unitInput = TextUtils.ToString(cboUnit.EditValue).Trim();

                string productNameSale = productSale.ProductName ?? "";
                string makerSale = productSale.Maker ?? "";
                string unitSale = productSale.Unit ?? "";

                if (TextUtils.ConvertUnicode(productNameInput.ToLower(), 1) != TextUtils.ConvertUnicode(productNameSale.ToLower(), 1))
                {
                    errors.Add($"\nTên thiết bị (tích xanh: [{productNameSale}], hiện tại: [{productNameInput}])");
                }
                if (TextUtils.ConvertUnicode(makerInput.ToLower(), 1) != TextUtils.ConvertUnicode(makerSale.ToLower(), 1))
                {
                    errors.Add($"\nHãng sản xuất (tích xanh: [{makerSale}], hiện tại: [{makerInput}])");
                }
                if (TextUtils.ConvertUnicode(unitInput.ToLower(), 1) != TextUtils.ConvertUnicode(unitSale.ToLower(), 1))
                {
                    errors.Add($"\nĐơn vị (tích xanh: [{unitSale}], hiện tại: [{unitInput}])");
                }

                if (errors.Any())
                {
                    string message =
                        $"Mã thiết bị {productCode} đã có TÍCH XANH.\n" +
                        $"Các trường không khớp:\n {string.Join("\n", errors)}\n\n" +
                        "Bạn muốn:\n" +
                        "- Sử dụng dữ liệu tích xanh từ hệ thống (Yes)\n" +
                        "- Lưu dữ liệu hiện tại (No)";

                    var result = MessageBox.Show(
                        message,
                        "Cảnh báo dữ liệu cố định",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        txtGroupMaterial.Text = productSale.ProductName ?? "";
                        txtManufacturer.Text = productSale.Maker ?? "";
                        cboUnit.EditValue = productSale.Unit ?? "";
                        //return true;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return false;
                    }
                }

            }
            return true;
        }


        int GetParentID(string tt, int typeId, int versionId)
        {
            int parentId = 0;
            if (tt.Contains("."))
            {
                string ttParent = tt.Substring(0, tt.LastIndexOf('.')).Trim();
                //var exp1 = new Expression("ProjectID", TextUtils.ToInt(cboProject.EditValue));
                //var exp2 = new Expression("ProjectTypeID", typeId);
                var exp3 = new Expression("TT", ttParent);
                var exp4 = new Expression("ProjectPartListVersionID", versionId);
                var exp5 = new Expression("IsDeleted", 1, "<>");
                //var exp6 = new Expression("ProjectTypeID", projectTypeID);
                var checkParent = SQLHelper<ProjectPartListModel>.FindByExpression(exp3.And(exp4).And(exp5)).FirstOrDefault();
                if (checkParent != null) parentId = checkParent.ID;
            }

            return parentId;
        }


        void UpdateRequestQuote(int projectPartlistID)
        {

            ProjectPartListModel partListNew = new ProjectPartListModel()
            {
                //TT = txtTT.Text.Trim(),
                ProductCode = txtProductCode.Text.Trim(),
                SpecialCode = txtSpecialCode.Text.Trim(),
                GroupMaterial = txtGroupMaterial.Text.Trim(),
                Manufacturer = txtManufacturer.Text.Trim(),
                Model = txtModel.Text.Trim(),
                QtyMin = TextUtils.ToDecimal(txtQtyMin.Value.ToString("n2")),
                QtyFull = TextUtils.ToDecimal(txtQtyFull.Value.ToString("n2")),
                Unit = TextUtils.ToString(cboUnit.EditValue).Trim().ToUpper(),
                EmployeeID = TextUtils.ToInt(cboEmployee.EditValue),
                IsProblem = chkIsProblem.Checked,
                ReasonProblem = txtReasonProblem.Text.Trim(),
                Note = txtNote.Text.Trim(),
            };


            //Check có thay đổi không?
            var resultCompare = TextUtils.DeepEquals(partListOld, partListNew);
            bool equal = TextUtils.ToBoolean(resultCompare.GetType().GetProperty("equal").GetValue(resultCompare));

            if (!equal)
            {
                List<string> propertys = (List<string>)resultCompare.GetType().GetProperty("property").GetValue(resultCompare);

                propertys = propertys.Where(p => !p.Contains("TT")).ToList();

                if (propertys.Count <= 0) return;
            }

            List<ProjectPartlistPriceRequestModel> listQuotes = SQLHelper<ProjectPartlistPriceRequestModel>.FindByAttribute("ProjectPartListID", projectPartlistID);
            if (listQuotes.Count > 0)
            {
                string sql = $"UPDATE dbo.ProjectPartlistPriceRequest SET IsDeleted = 1," +
                            $"UpdatedBy = '{Global.LoginName}'," +
                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                            $"WHERE ProjectPartListID = {projectPartlistID}";

                TextUtils.ExcuteSQL(sql);


                DateTime dateNow = DateTime.Now;
                TimeSpan timeSpan = new TimeSpan(15, 30, 0);
                TimeSpan timeSpanNow = new TimeSpan(dateNow.Hour, dateNow.Minute, 0);
                ProjectPartListModel partList = SQLHelper<ProjectPartListModel>.FindByID(projectPartlistID);
                if (partList == null || partList.ID <= 0) return;
                if (!partList.DeadlinePriceRequest.HasValue || !partList.DatePriceRequest.HasValue) return;

                partList.StatusPriceRequest = 1;
                //partList.DeadlinePriceRequest = frm.dtpDeadlinePriceRequest.Value;

                double totalDay = (dateNow.Date - partList.DeadlinePriceRequest.Value.Date).TotalDays;
                double totalDayOld = (partList.DeadlinePriceRequest.Value.Date - partList.DatePriceRequest.Value.Date).TotalDays;
                if (totalDay > 0)
                {
                    partList.DeadlinePriceRequest = partList.DeadlinePriceRequest.Value.AddDays(totalDay + totalDayOld);
                }

                if (timeSpanNow > timeSpan)
                {
                    partList.DeadlinePriceRequest = partList.DeadlinePriceRequest.Value.AddDays(+1);
                }

                //if (partList.DatePriceRequest.Value.Date <= dateNow.Date)
                //{

                //}

                if (partList.DeadlinePriceRequest.Value.DayOfWeek == DayOfWeek.Saturday)
                {
                    partList.DeadlinePriceRequest = partList.DeadlinePriceRequest.Value.AddDays(+2);
                }
                else if (partList.DeadlinePriceRequest.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    partList.DeadlinePriceRequest = partList.DeadlinePriceRequest.Value.AddDays(+1);
                }


                partList.DatePriceRequest = DateTime.Now;
                SQLHelper<ProjectPartListModel>.Update(partList);
                ProjectPartlistPriceRequestModel priceRequest = new ProjectPartlistPriceRequestModel();
                priceRequest.ProjectPartListID = projectPartlistID;
                priceRequest.EmployeeID = Global.EmployeeID;
                priceRequest.ProductCode = partList.ProductCode;
                priceRequest.ProductName = partList.GroupMaterial;
                priceRequest.StatusRequest = 1;
                priceRequest.DateRequest = DateTime.Now;
                priceRequest.Deadline = partList.DeadlinePriceRequest;
                priceRequest.Quantity = partList.QtyFull;

                SQLHelper<ProjectPartlistPriceRequestModel>.Insert(priceRequest);
            }


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                partList = new ProjectPartListModel();
                LoadData();
            }
        }

        private void cboNCC_EditValueChanged(object sender, EventArgs e)
        {
            SupplierSaleModel supplier = (SupplierSaleModel)cboNCC.GetSelectedDataRow();
            txtNCC.Text = supplier == null ? "" : supplier.NameNCC;
            txtNCC.ReadOnly = supplier != null;
        }

        private void cboNCCFinal_EditValueChanged(object sender, EventArgs e)
        {
            SupplierSaleModel supplier = (SupplierSaleModel)cboNCCFinal.GetSelectedDataRow();
            txtNCCFinal.Text = supplier == null ? "" : supplier.NameNCC;
            txtNCCFinal.ReadOnly = supplier != null;
        }

        private void frmProjectPartlistDetailNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmProjectPartlistDetailNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void chkIsProblem_CheckedChanged(object sender, EventArgs e)
        {
            //if ()
            //{
            //    txtQtyMin.Enabled = !(partList.IsApprovedTBP && chkIsProblem.Checked);
            //    txtQtyFull.Enabled = !(partList.IsApprovedTBP && chkIsProblem.Checked);
            //    cboUnit.Enabled = !(partList.IsApprovedTBP && chkIsProblem.Checked);
            //    cboNCC.Enabled = !(partList.IsApprovedTBP && chkIsProblem.Checked);
            //    txtNCC.Enabled = !(partList.IsApprovedTBP && chkIsProblem.Checked);
            //    txtPrice.Enabled = !(partList.IsApprovedTBP && chkIsProblem.Checked);
            //    txtAmount.Enabled = !(partList.IsApprovedTBP && !chkIsProblem.Checked);
            //    cboCurrencyQuote.Enabled = !(partList.IsApprovedTBP && !chkIsProblem.Checked);
            //    txtLeadTime.Enabled = !(partList.IsApprovedTBP && !chkIsProblem.Checked);
            //}

            if (chkIsProblem.Checked && !Global.IsAdmin)
            {
                txtQtyMin.Enabled = true;
                txtQtyFull.Enabled = true;
                //cboUnit.Enabled = true;
                //cboNCC.Enabled = true;
                //txtNCC.Enabled = true;
                //txtPrice.Enabled = true;
                //txtAmount.Enabled = true;
                //cboCurrencyQuote.Enabled = true;
                //txtLeadTime.Enabled = true;
            }
            else if (partList.IsApprovedTBP == true && !Global.IsAdmin)
            {
                txtQtyMin.Enabled = false;
                txtQtyFull.Enabled = false;
                //cboUnit.Enabled = false;
                //cboNCC.Enabled = false;
                //txtNCC.Enabled = false;
                //txtPrice.Enabled = false;
                //txtAmount.Enabled = false;
                //cboCurrencyQuote.Enabled = false;
                //txtLeadTime.Enabled = false;
            }

        }

        private void cboVersion_EditValueChanged(object sender, EventArgs e)
        {
            txtSTT.Value = GetSTT();
        }
    }
}
