using System;
using BMS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using BMS.Business;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using System.Text.RegularExpressions;

namespace BMS
{
    public partial class frmSetProjectMachinePrice : _Forms
    {
        public DateTime d;
        public int IDProject, RowNumber, ProjectMachinePriceID, IDEmployee, ProjectMachineID, IDCustomer;
        public decimal AmountSpent1, AmountSpent2, AmountSpent3;
        public int PLV1, PLV2;
        string NameNode, CodeGroup, NameGroup;
        bool DeleteTab;
        // Khai báo các biến để tính toán
        decimal ExpectationsArise, BonusStaffTech, BonusStaffSale, Customer111,
               GuarantyCost, VAT, ProjectCost, TotalProjectCost, Profit,
               ProfitPercent;

        public DataTable dt = new DataTable();
        DataTable dataSet = new DataTable();
        string ct1 = "Chi tiết tiêu chuẩn".ToLower();
        string ct2 = "Chi tiết gia công".ToLower();
        string ct3 = "Phụ kiện".ToLower();
        public frmSetProjectMachinePrice()
        {
            InitializeComponent();
        }

        private void frmSetProjectMachinePrice_Load(object sender, EventArgs e)
        {
            dtpCreateDate.Value = d;
            LoadCustomer();
            LoadProject();
            LoadEmployee();
            LoadData();
        }

        private void LoadData()
        {
            dataSet = TextUtils.GetDataTableFromSP("spGetProjectMachinePriceDetail", new string[] { "@ID" }, new object[] { ProjectMachineID });
            if (dataSet.Rows.Count > 0)
            {
                var groupedData = dataSet.AsEnumerable().GroupBy(row => row.Field<string>("NameGroup"));

                foreach (var group in groupedData)
                {
                    DataTable groupTable = group.CopyToDataTable();
                    XtraTabPage tabPage = new XtraTabPage();
                    tabPage.Text = group.Key;
                    tabPage.Controls.Add(CloneGridControl(grdData, groupTable));
                    tabPage.Controls.Add(ClonePanel(panelParent, groupTable.Rows[0][$"{colCodeGroup.FieldName}"].ToString(), group.Key));
                    xtraTabControl1.TabPages.Insert(xtraTabControl1.TabPages.Count - 2, tabPage);
                }
                xtraTabControl1.TabPages.RemoveAt(xtraTabControl1.TabPages.IndexOf(addTabPage) - 1);
                xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 2;
            }
            else
            {
                LoadContentPrice(dataSet, grdData);
            }
        }

        private void frmSetProjectMachinePrice_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void LoadEmployee()
        {
            List<EmployeeModel> ls = SQLHelper<EmployeeModel>.FindAll();
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = ls;
            cboEmployee.EditValue = IDEmployee;
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(cboProject.EditValue);
            var CusID = SQLHelper<ProjectModel>.FindByID(ID).CustomerID;
            cboCustomer.EditValue = TextUtils.ToInt(CusID);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == addTabPage)
            {
                XtraTabPage tabPage = new XtraTabPage();
                tabPage.Text = "Nhóm mới";
                DataTable dt = TextUtils.GetDataTableFromSP("spGetProjectMachinePriceDetail", new string[] { "@ID" }, new object[] { -1 });
                tabPage.Controls.Add(CloneGridControl(grdData, dt));
                tabPage.Controls.Add(ClonePanel(panelParent, "", ""));
                xtraTabControl1.TabPages.Insert(xtraTabControl1.TabPages.Count - 1, tabPage);
                xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 2;

            }
        }

        GridView viewFocus = null;
        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            viewFocus = sender as GridView;
        }

        private void btnDeleteTab_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có chắc chăn muốn xóa bảng [{xtraTabControl1.SelectedTabPage.Text}] không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (xtraTabControl1.TabPages.Count > 0)
                {
                    var currentTab = xtraTabControl1.SelectedTabPage;

                    for (int i = 0; i < viewFocus.RowCount; i++)
                    {
                        int DeleteValue = TextUtils.ToInt(viewFocus.GetRowCellValue(i, colID));
                        SQLHelper<ProjectMachinePriceDetailModel>.DeleteModelByID(DeleteValue);
                    }

                    if (currentTab != null)
                    {
                        xtraTabControl1.TabPages.Remove(currentTab);
                        xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 2;
                    }
                }
            }
        }

        private void btnAddNewTab_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = addTabPage;
            xtraTabControl1_SelectedPageChanged(null, null);
        }

        Panel ClonePanel(Panel template, string CodeGroup, string NameGroup)
        {
            Panel panel = new Panel
            {
                Size = template.Size,
                Location = template.Location,
                Dock = template.Dock,
                BackColor = template.BackColor,
                Name = template.Name

            };
            panel.SendToBack();
            foreach (Control control in template.Controls)
            {
                if (control is Label label)
                {
                    Label newLabel = new Label
                    {
                        Text = label.Text,
                        Size = label.Size,
                        Location = label.Location,
                        Font = label.Font,
                        ForeColor = label.ForeColor,
                        BackColor = label.BackColor,
                        Name = label.Name,
                        TextAlign = label.TextAlign
                    };
                    panel.Controls.Add(newLabel);
                }
                else if (control is TextBox textBox)
                {
                    TextBox newTextBox = new TextBox
                    {
                        Size = textBox.Size,
                        Location = textBox.Location,
                        Font = textBox.Font,
                        ForeColor = textBox.ForeColor,
                        BackColor = textBox.BackColor,
                        Name = textBox.Name
                    };
                    panel.Controls.Add(newTextBox);
                    if (newTextBox.Name == $"{txtNameGroup.Name}")
                    {
                        newTextBox.Text = NameGroup;
                        newTextBox.TextChanged += txtNameGroup_TextChanged;
                    }
                    else
                    {
                        newTextBox.Text = CodeGroup;
                    }
                }
            }

            return panel;
        }

        GridControl CloneGridControl(GridControl template, DataTable dt)
        {
            GridControl gridControl = new GridControl();
            GridView gridView = new GridView(gridControl);

            gridView.OptionsBehavior.Editable = true;
            gridView.OptionsBehavior.ReadOnly = true;

            gridControl.MainView = gridView;
            gridControl.Dock = template.Dock;

            LoadContentPrice(dt, gridControl);
            gridControl.DataSource = dt;

            gridControl.ContextMenuStrip = template.ContextMenuStrip;

            gridView.Assign(template.MainView as GridView, false);
            gridView.CellValueChanged += grvData_CellValueChanged;
            gridView.RowCellStyle += grvData_RowCellStyle;
            gridView.ShowingEditor += grvData_ShowingEditor;
            gridView.FocusedRowChanged += grvData_FocusedRowChanged;

            return gridControl;
        }

        private void txtNameGroup_TextChanged(object sender, EventArgs e)
        {
            TextBox txtNameGroup = sender as TextBox;
            var TabSelect = xtraTabControl1.SelectedTabPage;
            TabSelect.Text = txtNameGroup.Text;
        }

        private void LoadProject()
        {
            List<ProjectModel> ls = SQLHelper<ProjectModel>.FindAll();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.DataSource = ls;
            cboProject.EditValue = IDProject;

        }

        private void LoadCustomer()
        {
            List<CustomerModel> ls = SQLHelper<CustomerModel>.FindAll();
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.DataSource = ls;
            cboCustomer.EditValue = IDCustomer;
        }

        private void btnDelete1_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            string ContentPrice = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colContentPrice));
            if (MessageBox.Show(string.Format($"Bạn có chắc chắn muốn xóa [{ContentPrice}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
            }
        }

        private void LoadContentPrice(DataTable dataSet, GridControl grdData)
        {
            if (dataSet.Rows.Count <= 0)
            {
                dataSet.AcceptChanges();
                for (int i = 0; i < 12; i++)
                {
                    DataRow dtrow = dataSet.NewRow();
                    dtrow["STT"] = i + 1;
                    dtrow["DependentObject"] = "RTC";
                    switch (i)
                    {
                        case 0:
                            dtrow["ContentPrice"] = "Chi phí vật tư tiêu chuẩn";
                            break;
                        case 1:
                            dtrow["ContentPrice"] = "Chi phí vật tư điện";
                            break;
                        case 2:
                            dtrow["ContentPrice"] = "Chi phí gia công";
                            break;
                        case 3:
                            dtrow["ContentPrice"] = "Dự trù phát sinh";
                            break;
                        case 4:
                            dtrow["ContentPrice"] = "Thưởng nhân viên kỹ thuật";
                            break;
                        case 5:
                            dtrow["ContentPrice"] = "Thưởng nhân viên sales";
                            break;
                        case 6:
                            dtrow["ContentPrice"] = "111 khách hàng";
                            break;
                        case 7:
                            dtrow["ContentPrice"] = "Chi phí dự trù bảo hành";
                            break;
                        case 8:
                            dtrow["ContentPrice"] = "Thuế TNDN 20%";
                            break;
                        case 9:
                            dtrow["ContentPrice"] = "Chi phí vật tư dự án";
                            break;
                        case 10:
                            dtrow["ContentPrice"] = "Tổng giá trị đầu ra vật tư dự án";
                            break;
                        case 11:
                            dtrow["ContentPrice"] = "Lợi nhuận";
                            break;
                    }
                    dataSet.Rows.Add(dtrow);
                }
                grdData.DataSource = dataSet;
            }
        }

        private bool _isHandlingCellValueChanged = false;

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView grvData = sender as GridView;
            if (_isHandlingCellValueChanged) return; // Ngăn vòng lặp vô hạn

            _isHandlingCellValueChanged = true; // Bắt đầu xử lý sự kiện

            try
            {
                AmountSpent1 = TextUtils.ToDecimal(grvData.GetRowCellValue(0, $"{colAmountSpent.FieldName}"));
                AmountSpent2 = TextUtils.ToDecimal(grvData.GetRowCellValue(1, $"{colAmountSpent.FieldName}"));
                AmountSpent3 = TextUtils.ToDecimal(grvData.GetRowCellValue(2, $"{colAmountSpent.FieldName}"));
                grvData.SetRowCellValue(0, $"{colCoefficient.FieldName}", AmountSpent1);
                grvData.SetRowCellValue(1, $"{colCoefficient.FieldName}", AmountSpent2);
                grvData.SetRowCellValue(2, $"{colCoefficient.FieldName}", AmountSpent3);
                decimal TotalAmountSpent = AmountSpent1 + AmountSpent2 + AmountSpent3;

                // Chi phí dự trù
                ExpectationsArise = TextUtils.ToDecimal(grvData.GetRowCellValue(3, $"{colCoefficient.FieldName}")) * TotalAmountSpent / 100;
                grvData.SetRowCellValue(3, $"{colAmountSpent.FieldName}", ExpectationsArise);

                if (e.RowHandle == 11 && e.Column.FieldName == $"{colCoefficient.FieldName}")
                {
                    decimal TotalCoef = 0;
                    // lặp qua 4 trường 
                    for (int i = 4; i < 8; i++)
                    {
                        TotalCoef = TotalCoef + TextUtils.ToDecimal(grvData.GetRowCellValue(i, $"{colCoefficient.FieldName}"));
                    }
                    ProfitPercent = TextUtils.ToDecimal(grvData.GetRowCellValue(11, $"{colCoefficient.FieldName}"));
                    try
                    {
                        TotalProjectCost = 80 * (TotalAmountSpent + ExpectationsArise) / (80 - ProfitPercent - 80 * TotalCoef / 100);
                    }
                    catch
                    {
                        TotalProjectCost = 0;
                    }
                    grvData.SetRowCellValue(10, $"{colCoefficient.FieldName}", TotalProjectCost);
                    grvData.SetRowCellValue(10, $"{colAmountSpent.FieldName}", TotalProjectCost);
                    UpdateCal(TotalProjectCost, grvData);
                }
                else
                {
                    TotalProjectCost = TextUtils.ToDecimal(grvData.GetRowCellValue(10, $"{colCoefficient.FieldName}"));
                    grvData.SetRowCellValue(10, $"{colCoefficient.FieldName}", TotalProjectCost);
                    grvData.SetRowCellValue(10, $"{colAmountSpent.FieldName}", TotalProjectCost);
                    UpdateCal(TotalProjectCost, grvData);
                    try
                    {
                        ProfitPercent = (Profit / Math.Ceiling(TotalProjectCost)) * 100;
                    }
                    catch
                    {
                        ProfitPercent = 0;
                    }
                    grvData.SetRowCellValue(11, $"{colCoefficient.FieldName}", ProfitPercent);
                }
            }
            finally
            {
                _isHandlingCellValueChanged = false;
            }
        }

        private void UpdateCal(decimal value, GridView grvData)
        {
            // Thưởng nhân viên kỹ thuật 
            BonusStaffTech = TextUtils.ToDecimal(grvData.GetRowCellValue(4, $"{colCoefficient.FieldName}")) * value / 100;
            grvData.SetRowCellValue(4, $"{colAmountSpent.FieldName}", BonusStaffTech);

            // Thưởng nhân viên sale
            BonusStaffSale = TextUtils.ToDecimal(grvData.GetRowCellValue(5, $"{colCoefficient.FieldName}")) * value / 100;
            grvData.SetRowCellValue(5, $"{colAmountSpent.FieldName}", BonusStaffSale);

            // 111 khách hàng
            Customer111 = TextUtils.ToDecimal(grvData.GetRowCellValue(6, $"{colCoefficient.FieldName}")) * Math.Ceiling(value) / 100;
            grvData.SetRowCellValue(6, $"{colAmountSpent.FieldName}", Customer111);

            // Chi phí dự trù bảo hành
            GuarantyCost = TextUtils.ToDecimal(grvData.GetRowCellValue(7, $"{colCoefficient.FieldName}")) * value / 100;
            grvData.SetRowCellValue(7, $"{colAmountSpent.FieldName}", GuarantyCost);

            // Thuế TNDN
            decimal TotalAll = AmountSpent1 + AmountSpent2 + AmountSpent3 + ExpectationsArise + BonusStaffTech + BonusStaffSale + Customer111 + GuarantyCost;
            VAT = ((Math.Ceiling(value) - TotalAll) * 20) / 100;
            grvData.SetRowCellValue(8, $"{colAmountSpent.FieldName}", VAT);

            // Chi phí vật tư dự án 
            ProjectCost = TotalAll + VAT;
            grvData.SetRowCellValue(9, $"{colAmountSpent.FieldName}", ProjectCost);

            // Lợi nhuận
            Profit = Math.Ceiling(value) - ProjectCost;
            grvData.SetRowCellValue(11, $"{colAmountSpent.FieldName}", Profit);
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView grvData = sender as GridView;
            if (e.Column.FieldName == $"{colAmountSpent.FieldName}")
            {
                int cellValue = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, $"{colSTT.FieldName}"));
                if (cellValue == 1 || cellValue == 2 || cellValue == 3)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
                if (cellValue == 10 || cellValue == 12)
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.ForeColor = Color.Black;
                }
                if (cellValue == 11)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            if (e.Column.FieldName == $"{colCoefficient.FieldName}")
            {
                int cellValue = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, $"{colSTT.FieldName}"));
                if (cellValue == 4 || cellValue == 5 || cellValue == 6 || cellValue == 7 || cellValue == 8 || cellValue == 12)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            if (e.Column.FieldName == $"{colContentPrice.FieldName}")
            {
                int cellValue = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, $"{colSTT.FieldName}"));

                if (cellValue == 10 || cellValue == 11 || cellValue == 12)
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void btnCloseAndSave_Click(object sender, EventArgs e)
        {
            if (SaveDataTab())
            {
                this.Close();
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            DeleteTab = true;
            if (SaveDataTab())
            {
                grdData.DataSource = null;
                dataSet.Clear();
                LoadContentPrice(dataSet, grdData);
                ProjectMachineID = -1;
                cboEmployee.EditValue = -1;
                cboProject.EditValue = -1;
                cboCustomer.EditValue = -1;
                if (xtraTabControl1.TabPages.Count > 1)
                {
                    for (int i = xtraTabControl1.TabPages.Count - 1; i >= 0; i--)
                    {
                        XtraTabPage tabPage = xtraTabControl1.TabPages[i];
                        if (tabPage != addTabPage)
                        {
                            xtraTabControl1.TabPages.RemoveAt(i);
                        }
                    }
                }

            }
        }

        private bool SaveDataTab()
        {
            ProjectMachinePriceModel pm = new ProjectMachinePriceModel();
            ProjectMachinePriceDetailModel pmd = new ProjectMachinePriceDetailModel();


            if (TextUtils.ToInt(cboProject.EditValue) > 0 && TextUtils.ToInt(cboEmployee.EditValue) > 0)
            {
                if (ProjectMachineID > 0)
                {
                    pm = SQLHelper<ProjectMachinePriceModel>.FindByID(ProjectMachineID);
                    pm.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                    pm.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                    pm.DatePrice = dtpCreateDate.Value;

                    SQLHelper<ProjectMachinePriceModel>.Update(pm);
                }
                else
                {
                    pm.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                    pm.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                    pm.DatePrice = dtpCreateDate.Value;
                    pm.ID = SQLHelper<ProjectMachinePriceModel>.Insert(pm).ID;

                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin dự án và nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (xtraTabControl1.TabPages.Count > 0)
            {
                foreach (XtraTabPage tabPage in xtraTabControl1.TabPages)
                {
                    foreach (Control control in tabPage.Controls)
                    {
                        if (control is System.Windows.Forms.Panel panel)
                        {
                            foreach (Control control1 in panel.Controls)
                            {
                                if (control1 is System.Windows.Forms.TextBox textBox)
                                {
                                    if (textBox.Name == $"{txtCodeGroup.Name}")
                                    {
                                        if (textBox.Text == "")
                                        {
                                            MessageBox.Show($"Vui lòng điền mã nhóm tab {xtraTabControl1.TabIndex}", "Thông báo", MessageBoxButtons.OK);
                                            return false;
                                        }
                                        CodeGroup = textBox.Text;
                                        
                                    }
                                    if (textBox.Name == $"{txtNameGroup.Name}")
                                    {
                                        if (textBox.Text == "")
                                        {
                                            MessageBox.Show($"Vui lòng điền tên nhóm tab {xtraTabControl1.TabIndex}", "Thông báo", MessageBoxButtons.OK);
                                            return false;
                                        }
                                        NameGroup = textBox.Text;
                                    }
                                }
                            }
                        }
                    }

                    var checkNameGroup = SQLHelper<ProjectMachinePriceDetailModel>.FindByAttribute("NameGroup", $"{NameGroup}");
                    foreach (Control control in tabPage.Controls)
                    {
                        if (control is DevExpress.XtraGrid.GridControl gridControl)
                        {
                            var gridView = gridControl.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                            if (gridView != null)
                            {
                                for (int i = 0; i < gridView.RowCount; i++)
                                {
                                    int detailID = TextUtils.ToInt(gridView.GetRowCellValue(i, $"{colID.FieldName}"));
                                    pmd = SQLHelper<ProjectMachinePriceDetailModel>.FindByID(detailID);
                                    if (i == 0 || i == 2)
                                    {
                                        pmd.ProjectVersionID = PLV1;
                                    }
                                    else if (i == 1)
                                    {
                                        pmd.ProjectVersionID = PLV2;
                                    }
                                    else pmd.ProjectVersionID = 0;

                                    pmd.CodeGroup = CodeGroup;
                                    pmd.NameGroup = NameGroup;
                                    pmd.STT = TextUtils.ToInt(gridView.GetRowCellValue(i, $"{colSTT.FieldName}"));
                                    pmd.ContentPrice = TextUtils.ToString(gridView.GetRowCellValue(i, $"{colContentPrice.FieldName}"));
                                    pmd.AmountSpent = TextUtils.ToDecimal(gridView.GetRowCellValue(i, $"{colAmountSpent.FieldName}"));
                                    pmd.DependentObject = TextUtils.ToString(gridView.GetRowCellValue(i, $"{colDependentObject.FieldName}"));
                                    pmd.EstimateCost = TextUtils.ToDecimal(gridView.GetRowCellValue(i, $"{colEstimateCost.FieldName}"));
                                    pmd.Coefficient = TextUtils.ToDecimal(gridView.GetRowCellValue(i, $"{colCoefficient.FieldName}"));
                                    pmd.Note = TextUtils.ToString(gridView.GetRowCellValue(i, $"{colNote.FieldName}"));
                                    bool checkChar = TextUtils.IsVietNamese(pmd.CodeGroup);
                                    if (checkChar == false)
                                    {
                                        MessageBox.Show($"Mã [{pmd.CodeGroup}] cụm [{pmd.NameGroup}] không được chứa ký tự tiếng Việt.\nVui lòng kiểm tra lại!.", TextUtils.Caption);
                                        return false;
                                    }

                                    if (ProjectMachineID > 0 && checkNameGroup.Count > 0)
                                    {
                                        pmd.ProjectMachinePriceID = ProjectMachineID;
                                        SQLHelper<ProjectMachinePriceDetailModel>.Update(pmd);
                                    }
                                    else
                                    {
                                        var checkCode = SQLHelper<ProjectMachinePriceDetailModel>.FindByAttribute("CodeGroup", TextUtils.ToString(pmd.CodeGroup));
                                        if (checkCode.Count > 0)
                                        {
                                            MessageBox.Show($"Mã [{pmd.CodeGroup}] nhóm [{pmd.NameGroup}] đã tồn tại \n\r Vui lòng nhập lại mã", "Thông báo", MessageBoxButtons.OK);
                                            return false;
                                        }
                                        pmd.ProjectMachinePriceID = pm.ID;
                                        SQLHelper<ProjectMachinePriceDetailModel>.Insert(pmd);
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        

        private void btnProjectPartList_Click(object sender, EventArgs e)
        {
            XtraTabPage selectedTab = xtraTabControl1.SelectedTabPage;

            // Kiểm tra nếu tab có chứa DataGridView
            if (selectedTab != null)
            {
                // Duyệt qua tất cả các control trong tab để tìm DataGridView
                foreach (Control control in selectedTab.Controls)
                {
                    if (control is DevExpress.XtraGrid.GridControl gridControl)
                    {
                        var gridView = gridControl.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                        RowNumber = gridView.FocusedRowHandle;
                        IDProject = TextUtils.ToInt(cboProject.EditValue);
                        if (IDProject <= 0)
                        {
                            MessageBox.Show("Chưa có dự án được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        ProjectModel project = SQLHelper<ProjectModel>.FindByID(IDProject);
                        frmProjectPartList_New frm = new frmProjectPartList_New(true);
                        frm.rowNumber = RowNumber;
                        frm.project = project;
                        //ct = content
                        frm.ct1 = ct1;
                        frm.ct2 = ct2;
                        frm.ct3 = ct3;
                        frm.ShowDialog();
                        if (frm.DialogResult == DialogResult.OK)
                        {
                            PLV1 = frm.PLV1;
                            PLV2 = frm.PLV2;
                            if (RowNumber == 0 || RowNumber == 2)
                            {
                                AmountSpent1 = frm.AmountSpent1;
                                gridView.SetRowCellValue(0, $"{colAmountSpent.FieldName}", AmountSpent1);
                                gridView.SetRowCellValue(0, $"{colCoefficient.FieldName}", AmountSpent1);

                                AmountSpent3 = frm.AmountSpent3;
                                gridView.SetRowCellValue(2, $"{colAmountSpent.FieldName}", AmountSpent3);
                                gridView.SetRowCellValue(2, $"{colCoefficient.FieldName}", AmountSpent3);


                            }
                            if (RowNumber == 1)
                            {
                                AmountSpent2 = frm.AmountSpent2;
                                gridView.SetRowCellValue(1, $"{colAmountSpent.FieldName}", AmountSpent2);
                                gridView.SetRowCellValue(1, $"{colCoefficient.FieldName}", AmountSpent2);
                            }
                            NameNode = frm.NameNode;
                        }
                    }
                    else if (control is System.Windows.Forms.Panel panel)
                    {
                        foreach (Control control1 in panel.Controls)
                        {
                            if (control1 is System.Windows.Forms.TextBox txtName)
                            {
                                if (!string.IsNullOrEmpty(NameNode) && txtName.Name == $"{txtNameGroup.Name}")
                                {
                                    txtName.Text = NameNode;
                                }
                            }
                        }
                    }
                }
            }

        }

        #region Chọn ô không được sửa
        private void grvData_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            // Kiểm tra nếu ô hiện tại là ô bạn muốn thiết lập readonly
            if (view.FocusedColumn.FieldName == $"{colCoefficient.FieldName}" && (view.FocusedRowHandle == 0 || view.FocusedRowHandle == 1 || view.FocusedRowHandle == 2))
            {
                // Ngăn không cho chỉnh sửa ô
                e.Cancel = true;
            }
        }

        #endregion
    }
}
