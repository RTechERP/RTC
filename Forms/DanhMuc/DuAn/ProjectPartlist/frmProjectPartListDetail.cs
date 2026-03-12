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
using BMS;
using BMS.Business;
using System.Collections;
using System.IO;
using BMS.Utils;

namespace Forms.DanhMuc.DuAn
{
    public partial class frmProjectPartListDetail : _Forms
    {
        public int ProjectID;
        public ProjectPartListModel model = new ProjectPartListModel();
        public List<int> LstID = new List<int>();

        public int projectSolutionId = 0;
        public frmProjectPartListDetail()
        {
            InitializeComponent();
        }

        private void frmProjectPartListDetail_Load(object sender, EventArgs e)
        {
            cbProjectList.EditValue = ProjectID;
            cbStatus.SelectedIndex = 0;
            loadProject();
            loadPersonManager();
            loadPartListType();
            LoadVersion();
            loadData();
        }

        void loadData()
        {
            if (model != null)
            {
                txtSTT.Text = TextUtils.ToString(model.TT);
                txtGroup.Text = TextUtils.ToString(model.GroupMaterial);
                txtProductCode.Text = TextUtils.ToString(model.ProductCode);
                txtModel.Text = TextUtils.ToString(model.Model);
                txtManufacturer.Text = TextUtils.ToString(model.Manufacturer);
                txtUnit.Text = TextUtils.ToString(model.Unit);
                txtQtyMin.EditValue = model.QtyMin;
                txtQtyFull.EditValue = model.QtyFull;
                txtQtyReturn.EditValue = model.QtyReturned;
                txtPrice.EditValue = model.Price;
                txtAmount.EditValue = model.Amount;
                txtVAT.Text = TextUtils.ToString(model.Note1);
                txtLeadTime.Text = TextUtils.ToString(model.LeadTime);
                dtpDateReturn.EditValue = model.ExpectedReturnDate;
                txtNote.Text = TextUtils.ToString(model.Note);
                cbPersonManager.EditValue = TextUtils.ToInt(model.EmployeeID);
                txtNCC.Text = model.NCC;
                dtpDateRequest.EditValue = model.RequestDate;
                cboPartListType.EditValue = model.ProjectPartListTypeID;
                dtpOrderDate.EditValue = model.OrderDate;
                dtpReturnDate.EditValue = model.ReturnDate;
                txtOrderCode.Text = model.OrderCode;
                txtPriceOrder.EditValue = model.PriceOrder;
                txtTotalPriceOrder.EditValue = model.TotalPriceOrder;
                int status = TextUtils.ToInt(model.Status);
                if (status == -1)
                {
                    cbStatus.SelectedIndex = 0;
                }
                else if (status == 0)
                {
                    cbStatus.SelectedIndex = 3;
                }
                else if (status == 1)
                {
                    cbStatus.SelectedIndex = 2;
                }
                else
                {
                    cbStatus.SelectedIndex = 1;
                }

                if (model.IsApprovedTBP == true && !Global.IsAdmin)
                {
                    cbProjectList.Enabled = false;
                    cboPartListType.Enabled = false;
                    txtGroup.Enabled = false;
                    txtProductCode.Enabled = false;
                    txtModel.Enabled = false;
                    cbPersonManager.Enabled = false;
                    txtManufacturer.Enabled = false;
                    txtUnit.Enabled = false;
                    txtQtyMin.Enabled = false;
                    txtQtyFull.Enabled = false;
                    txtNCC.Enabled = false;
                }
            }
        }
        private void loadPartListType()
        {
            List<ProjectPartListTypeModel> listType = SQLHelper<ProjectPartListTypeModel>.FindAll();
            cboPartListType.Properties.DataSource = listType;
            cboPartListType.Properties.ValueMember = "ID";
            cboPartListType.Properties.DisplayMember = "Name";
        }

        void loadPersonManager()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Employee");
            cbPersonManager.Properties.DataSource = dt;
            cbPersonManager.Properties.DisplayMember = "FullName";
            cbPersonManager.Properties.ValueMember = "ID";
        }

        void loadProject()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Project");
            cbProjectList.Properties.DataSource = dt;
            cbProjectList.Properties.DisplayMember = "ProjectCode";
            cbProjectList.Properties.ValueMember = "ID";

        }

        void LoadVersion()
        {
            //int projectSolutionId = TextUtils.ToInt(grvDataSolution.GetFocusedRowCellValue("ID"));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartListVersion", "A", new string[] { "@ProjectSolutionID" }, new object[] { projectSolutionId });
            cboVersion.Properties.ValueMember = "ID";
            cboVersion.Properties.DisplayMember = "Code";
            cboVersion.Properties.DataSource = dt;
        }
        bool ValidateForm()
        {
            if (TextUtils.ToInt(TextUtils.ExcuteScalar($"Select top 1 ID FROM ProjectPartList WHERE ProductCode = '{txtProductCode.Text.Trim()}'")) > 0 && model.ID <= 0)
            {
                MessageBox.Show("Mã thiết bị đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(cbProjectList.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn dự án !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //if(TextUtils.ToInt(cboPartListType.EditValue) == 0)//Khánh update 15/09/2023
            //{
            //    MessageBox.Show("Vui lòng chọn Danh mục vật tư !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtSTT.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập STT !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtProductCode.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Mã thiết bị !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtGroup.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Tên vật tư !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            var exp1 = new Expression("ProjectID", TextUtils.ToInt(cbProjectList.EditValue));
            var exp2 = new Expression("ProjectPartListTypeID", TextUtils.ToInt(cboPartListType.EditValue));
            var exp3 = new Expression("TT", txtSTT.Text.Trim());
            var exp4 = new Expression("ID", model.ID, "<>");
            var exp5 = new Expression("IsDeleted", 1, "<>");
            var checkTT = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5));
            if (checkTT.Count > 0)
            {
                MessageBox.Show("STT đã tồn tại! Vui lòng nhập lại.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var exp6 = new Expression("SpecialCode", txtSpecialCode.Text.Trim());
            var specialCodes = SQLHelper<ProjectPartListModel>.FindByExpression(exp6.And(exp5));
            if (specialCodes.Count > 0)
            {
                MessageBox.Show($"Mã đặc biệt [{txtSpecialCode.Text}] đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //if (txtUnit.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập đơn vị !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //if (txtQtyMin.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập số lượng / 1 máy !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //if (txtQtyFull.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập số lượng tổng !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //if (txtPrice.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập đơn giá !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //if (txtLeadTime.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập LeadTime !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //if (cbStatus.Text == "")
            //{
            //    MessageBox.Show("Vui lòng chọn Tình trạng !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            return true;
        }
        bool Save()
        {
            if (!ValidateForm()) return false;
            //model.EmployeeID = LstID[i];

            model.ProjectID = TextUtils.ToInt(cbProjectList.EditValue);
            model.TT = txtSTT.Text.Trim(); //Khánh update 15/09/2023
            model.GroupMaterial = txtGroup.Text.Trim();
            model.Model = txtModel.Text.Trim();
            model.ProductCode = txtProductCode.Text.Trim();
            model.Manufacturer = txtManufacturer.Text.Trim();
            model.NCC = txtNCC.Text.Trim();
            model.Unit = txtUnit.Text.Trim();
            model.QtyMin = TextUtils.ToDecimal(txtQtyMin.EditValue);
            model.QtyFull = TextUtils.ToDecimal(txtQtyFull.EditValue);
            model.QtyReturned = TextUtils.ToDecimal(txtQtyReturn.EditValue);
            model.Price = TextUtils.ToDecimal(txtPrice.Text.Trim());
            //if (txtAmount.Text.Trim() == "" || TextUtils.ToDecimal(txtAmount.Text.Trim()) == 0)
            //{
            //decimal QtyFull = TextUtils.ToDecimal(txtQtyFull.EditValue);
            //decimal Price = TextUtils.ToDecimal(txtPrice.Text.Trim());
            //decimal TotalMoney = model.Price * model.QtyFull;
            model.Amount = model.Price * model.QtyFull;
            //}
            //else
            //{
            //    model.Amount = TextUtils.ToDecimal(txtAmount.Text.Trim());
            //}
            model.VAT = TextUtils.ToDecimal(txtVAT.Text.Trim());
            model.LeadTime = TextUtils.ToString(txtLeadTime.Text.Trim());
            model.ExpectedReturnDate = TextUtils.ToDate2(dtpDateReturn.EditValue);
            model.RequestDate = TextUtils.ToDate2(dtpDateRequest.EditValue);
            //string status = cbStatus.Text.Trim();
            model.Note = txtNote.Text.Trim();
            model.Note1 = txtVAT.Text.Trim();
            /*  Chưa đặt hàng
                Đã về
                Đã đặt hàng
                Không đặt hàng*/
            if (cbStatus.SelectedIndex == 3)
            {
                model.Status = 0;
            }
            else if (cbStatus.SelectedIndex == 2)
            {
                model.Status = 1;
            }
            else if (cbStatus.SelectedIndex == 1)
            {
                model.Status = 2;
            }
            else
            {
                model.Status = -1;
            }
            model.EmployeeID = TextUtils.ToInt(cbPersonManager.EditValue);
            //Khánh update 15/09/2023
            model.ProjectPartListTypeID = TextUtils.ToInt(cboPartListType.EditValue);
            model.OrderDate = TextUtils.ToDate4(dtpOrderDate.EditValue);
            model.ReturnDate = TextUtils.ToDate4(dtpReturnDate.EditValue);
            model.OrderCode = txtOrderCode.Text.Trim();
            string ttParent = model.TT.Substring(0, model.TT.LastIndexOf('.'));
            var exp1 = new Expression("ProjectID", TextUtils.ToInt(cbProjectList.EditValue));
            var exp2 = new Expression("ProjectPartListTypeID", TextUtils.ToInt(cboPartListType.EditValue));
            var exp3 = new Expression("TT", ttParent);
            var checkParent = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();
            if (checkParent.ID > 0)
            {
                model.ParentID = checkParent.ID;
            }
            else
            {
                MessageBox.Show("STT không đúng! Vui lòng nhập lại");
                return false;
            }
            model.PriceOrder = TextUtils.ToDecimal(txtPriceOrder.EditValue);
            model.TotalPriceOrder = TextUtils.ToDecimal(txtTotalPriceOrder.EditValue);
            model.NCCFinal = txtNCCFinal.Text.Trim();
            model.SpecialCode = txtSpecialCode.Text.Trim();

            model.ProjectPartListVersionID = 0;

            if (model.ID > 0)
            {
                ProjectPartListBO.Instance.Update(model);

                DataRowView projectSelected = (DataRowView)cbProjectList.GetSelectedDataRow();
                string projectCode = TextUtils.ToString(projectSelected["ProjectCode"]);
                //EmailSender.setEmail(projectCode, model, true);
            }
            else
            {
                ProjectPartListBO.Instance.Insert(model);
                //setEmail(ProjectID, true);
            }
            return true;
        }


        //private void setEmail(int ProjectID, ProjectPartListModel model, bool update)
        //{
        //    EmployeeSendEmailModel email = new EmployeeSendEmailModel();

        //    //if (add)
        //    //    email.Subject = $"CẬP NHẬT THÔNG TIN TIẾN ĐỘ VẬT TƯ CHO DỰ ÁN ";
        //    //else
        //    if (update)
        //        email.Subject = $"CẬP NHẬT THÔNG TIN TIẾN ĐỘ VẬT TƯ CHO SẢN PHẨM {model.ProductCode} THUỘC DỰ ÁN {ProjectCodeName(ProjectID)}";
        //    else
        //        email.Subject = $"XÓA THÔNG TIN TIẾN ĐỘ VẬT TƯ SẢN PHẨM {model.ProductCode} THUỘC DỰ ÁN {ProjectCodeName(ProjectID)}";

        //    //email.Body = $"THÔNG TIN TIẾN ĐỘ VẬT TƯ ĐÃ ĐƯỢC CẬP NHẬT: STT: {model.STT} - Tên vật tư: {model.GroupMaterial} - Mã thiết bị: {model.ProductCode} - Thông số kỹ thuật: {model.Model}" +
        //    //    $"Người phụ trách {TextUtils.ToString(cbPersonManager.EditValue)}"+
        //    //    $"- Nhà sản xuất: {model.Manufacturer} - Đơn vị: {model.Unit} - Số lượng / 1 máy: {model.QtyMin} - Số lượng tổng: {model.QtyFull} - Giá: {model.Price} - Thành tiền: {model.Amount}" +
        //    //    $"- {model.VAT}  - LeadTime: {model.LeadTime} - Ngày về dự kiến: {model.ExpectedReturnDate} - Tình trạng: {model.Status} - {model.EmployeeID} - " +
        //    //    $"Nhà cung cấp: {model.NCC} - Ngày yêu cầu: {model.RequestDate}";
        //    setEmailBody(model, true);
        //    email.Body = File.ReadAllText(tempFileName);
        //    email.EmployeeID = ProjectPartListJoiner.UpdaterID;
        //    email.Receiver = ProjectPartListJoiner.ProjectManagerID;
        //    email.EmailTo = EmployeeEmail(email.Receiver);

        //    List<int> ccIDs = ProjectPartListJoiner.LeaderIDs.Concat(ProjectPartListJoiner.ChargerIDs).ToList();
        //    ccIDs.Insert(0, ProjectPartListJoiner.UserTechnicalID);
        //    ccIDs.Insert(0, ProjectPartListJoiner.SaleID);
        //    IEnumerable<int> distinctNumbers = ccIDs.Distinct();

        //    //email.EmailCC = $"{EmployeeEmail(ProjectPartListJoiner.SaleID)}; {EmployeeEmail(ProjectPartListJoiner.UserTechnicalID)}; " +
        //    //    $"{EmailCC(ProjectPartListJoiner.LeaderIDs)} {EmailCC(ProjectPartListJoiner.ChargerIDs)}";
        //    email.EmailCC = $"{EmailCC(distinctNumbers)}";
        //    email.DateSend = DateTime.Now;
        //    email.StatusSend = 2;

        //    EmployeeSendEmailBO.Instance.Insert(email);
        //}
        //private void setEmailBody(ProjectPartListModel model, bool update)
        //{
        //    templateFileName = Path.Combine(Application.StartupPath, "mail_template.txt");
        //    tempFileName = Path.Combine(Application.StartupPath, "mail_temporary.txt");
        //    File.Copy(templateFileName, tempFileName, true);

        //    if (update)
        //    {
        //        ReplaceFileText(tempFileName, "@Heading", $"CẬP NHẬT THÔNG TIN PARTLIST PRODUCT {model.ProductCode} THUỘC DỰ ÁN {model.ProjectID}", true);
        //    }

        //    string duplicatedText = String.Concat(Enumerable.Repeat("<li>@FieldName: @FieldValue</li>", 19));
        //    ReplaceFileText(tempFileName, "<li>@FieldName: @FieldValue</li>", duplicatedText, true);
        //    var props = model.GetType().GetProperties();
        //    foreach (var item in props)
        //    {
        //        string t = item.Name;
        //        string v = TextUtils.ToString(item.GetValue(model));
        //        ReplaceFileText(tempFileName, "@FieldName", t, true);
        //        ReplaceFileText(tempFileName, "@FieldValue", v, true);
        //    }
        //}
        //private void ReplaceFileText(string filePath, string oldText, string newText, bool once)
        //{
        //    string fileContent = File.ReadAllText(filePath);

        //    if (once)
        //    {
        //        int index = fileContent.IndexOf(oldText);
        //        if (index != -1)
        //        {
        //            fileContent = fileContent.Substring(0, index) + newText + fileContent.Substring(index + oldText.Length);
        //            File.WriteAllText(filePath, fileContent);
        //            return;
        //        }
        //    }

        //    fileContent = fileContent.Replace(oldText, newText);
        //    File.WriteAllText(filePath, fileContent);
        //}
        //private string ProjectCodeName(int ProjectID)
        //{
        //    ProjectModel project = (ProjectModel)ProjectBO.Instance.FindByPK(ProjectID);
        //    return $"{project.ProjectCode} - {project.ProjectName}";
        //}
        //private string EmployeeEmail(int EmployeeID)
        //{
        //    EmployeeModel employee = (EmployeeModel)EmployeeBO.Instance.FindByPK(EmployeeID);

        //    if (employee != null)
        //    {
        //        return (String.IsNullOrEmpty(employee.EmailCongTy) ? employee.EmailCaNhan : employee.EmailCongTy) /*+ "; "*/;
        //    }
        //    return "";
        //}
        //private string EmailCC(IEnumerable<int> ccIDs)
        //{
        //    string result = "";
        //    foreach (int id in ccIDs)
        //    {
        //        ArrayList arr = EmployeeBO.Instance.FindByAttribute("UserID", id);
        //        if (arr.Count == 0)
        //        {
        //            continue;
        //        }
        //        EmployeeModel employee = (EmployeeModel)arr[0];
        //        string mail = EmployeeEmail(employee.ID);
        //        if (!String.IsNullOrEmpty(mail))
        //            result += mail + "; ";
        //    }
        //    return result;
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtQtyFull_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPrice.Text))
            {
                decimal totalMoney = TextUtils.ToDecimal(txtQtyFull.Text.Trim()) * TextUtils.ToDecimal(txtPrice.Text.Trim());
                txtAmount.Text = TextUtils.ToString(totalMoney);
            }
            else
            {
                txtAmount.Text = "0";
            }
            if (!String.IsNullOrEmpty(txtPriceOrder.Text))
            {
                decimal totalPriceOrder = TextUtils.ToDecimal(txtQtyFull.Text.Trim()) * TextUtils.ToDecimal(txtPriceOrder.Text.Trim());
                txtTotalPriceOrder.Text = totalPriceOrder.ToString();
            }
            else
            {
                txtTotalPriceOrder.Text = "0";
            }

        }
        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtQtyFull.Text))
            {
                txtQtyFull.Text = "0";
                return;
            }
            Decimal totalMoney = TextUtils.ToDecimal(txtQtyFull.Text) * TextUtils.ToDecimal(txtPrice.Text);
            txtAmount.Text = TextUtils.ToString(totalMoney);
        }



        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            frmProjectPartListTypeDetail frm = new frmProjectPartListTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPartListType();
            }
        }

        private void txtPriceOrder_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtQtyFull.Text))
            {
                txtQtyFull.Text = "0";
                return;
            }
            decimal totalPriceOrder = TextUtils.ToDecimal(txtQtyFull.Text.Trim()) * TextUtils.ToDecimal(txtPriceOrder.Text.Trim());
            txtTotalPriceOrder.Text = totalPriceOrder.ToString();
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                model = new ProjectPartListModel();
                loadData();
            }
        }

        private void txtPriceOrder_EditValueChanged(object sender, EventArgs e)
        {
            decimal totalPriceOrder = TextUtils.ToDecimal(txtQtyFull.EditValue) * TextUtils.ToDecimal(txtPriceOrder.EditValue);
            txtTotalPriceOrder.EditValue = totalPriceOrder;
        }

        private void txtQtyFull_EditValueChanged(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(txtPrice.Text))
            //{
            //    decimal totalMoney = TextUtils.ToDecimal(txtQtyFull.Text.Trim()) * TextUtils.ToDecimal(txtPrice.Text.Trim());
            //    txtAmount.Text = TextUtils.ToString(totalMoney);
            //}
            //else
            //{
            //    txtAmount.Text = "0";
            //}
            //if (!String.IsNullOrEmpty(txtPriceOrder.Text))
            //{

            //}
            //else
            //{
            //    txtTotalPriceOrder.Text = "0";
            //}

            decimal totalMoney = TextUtils.ToDecimal(txtQtyFull.EditValue) * TextUtils.ToDecimal(txtPrice.EditValue);
            txtAmount.EditValue = totalMoney;

            decimal totalPriceOrder = TextUtils.ToDecimal(txtQtyFull.EditValue) * TextUtils.ToDecimal(txtPriceOrder.EditValue);
            txtTotalPriceOrder.EditValue = totalPriceOrder;
        }

        private void txtPrice_EditValueChanged(object sender, EventArgs e)
        {
            decimal totalMoney = TextUtils.ToDecimal(txtQtyFull.EditValue) * TextUtils.ToDecimal(txtPrice.EditValue);
            txtAmount.EditValue = totalMoney;
        }

        private void frmProjectPartListDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
