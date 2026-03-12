using BMS.Model;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmTrackingMarks : _Forms
    {
        public frmTrackingMarks()
        {
            InitializeComponent();
        }

        private void frmTrackingMarks_Load(object sender, EventArgs e)
        {
            LoadTypeDocument();
            LoadStatus();
            LoadDepartment();
            LoadEmployee();
            LoadData();


        }
        private void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
        }
        private void LoadDepartment()
        {
            List<DepartmentModel> lstD = new List<DepartmentModel>();
            lstD = SQLHelper<DepartmentModel>.FindAll();
            lstD.Insert(0, new DepartmentModel
            {
                ID = 0,
                Name = "---Tất cả---"
            });
            cboDepartment.Properties.DataSource = lstD;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }
        private void LoadStatus()
        {
            List<object> lstStatus = new List<object>()
            {
                new {ID = -1, Name = "---Tất cả---"},
                new {ID = 0, Name = "Chưa hoàn thành"},
                new {ID = 1, Name = "Hoàn thành"},
                new {ID = 2, Name = "Đã hủy"}
            };
            cboStatus.DataSource = lstStatus;
            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "ID";
        }
        private void LoadTypeDocument()
        {
            List<DocumentTypeModel> lstTD = new List<DocumentTypeModel>();
            lstTD = SQLHelper<DocumentTypeModel>.FindAll();
            lstTD.Insert(0, new DocumentTypeModel
            {
                ID = -1,
                Name = "---Tất cả---"
            });
            cboDocumentType.DataSource = lstTD;
            cboDocumentType.DisplayMember = "Name";
            cboDocumentType.ValueMember = "ID";
        }
        private void LoadData()
        {

            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59); ;
            string keyWords = txtKeyword.Text;
            int employID = TextUtils.ToInt(cboEmployee.EditValue);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int status = TextUtils.ToInt(cboStatus.SelectedValue);
            int typeDocument = TextUtils.ToInt(cboDocumentType.SelectedValue);

            DataTable dt = TextUtils.LoadDataFromSP("spGetAllTrackingMarks", "LMKTable",
                                                        new string[] { "@DateStart", "@DateEnd", "@KeyWord", "@EmployeeID", "@DepartMentID", "@Status", "@TypeDocument" },
                                                        new object[] { dateStart, dateEnd, keyWords, employID, departmentID, status, typeDocument });
            grdData.DataSource = dt;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            int isApproved = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus));

            if (isApproved == 2)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
                e.HighPriority = true;
            }
            else if (isApproved == 1)
            {
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.ForeColor = Color.Black;
                e.HighPriority = true;
            }


            /*else
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.ForeColor = Color.Black;
            }*/
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grvData.RowCount > 0)
            {
                int focusedRowHandle = grvData.FocusedRowHandle;
                int status = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colStatus));
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                string documentName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colDocumentName));
                if (status > 0)
                {
                    string _str = status == 1 ? "Hoàn thành" : (status == 2 ? "Hủy" : "");
                    MessageBox.Show($"Sổ theo dõi đóng dấu đã được {_str} không thế xóa!");
                }
                else if (SQLHelper<TrackingMarksModel>.FindByID(ID).EmployeeID != Global.EmployeeID)
                {
                    MessageBox.Show("Bạn không thể xoá đề nghị của người khác!");
                }
                else
                {
                    if (MessageBox.Show(string.Format("Bạn có muốn xóa Sổ theo dõi đóng dấu [{0}] hay không ?", documentName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SQLHelper<TrackingMarksModel>.DeleteModelByID(ID);
                        SQLHelper<TrackingMarksSealModel>.DeleteByAttribute("TrackingMartkID", ID);
                        SQLHelper<TrackingMarksTaxCompanyModel>.DeleteByAttribute("TrackingMartkID", ID);
                        SQLHelper<TrackingMarksFileModel>.DeleteByAttribute("TrackingMartkID", ID);
                        btnFind_Click(null, null);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmTrackingMarksDetails frm = new frmTrackingMarksDetails();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnFind_Click(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int trackingMarkID = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colID));
            if (trackingMarkID <= 0) return;
            TrackingMarksModel trackingMarks = SQLHelper<TrackingMarksModel>.FindByID(trackingMarkID);
            frmTrackingMarksDetails frm = new frmTrackingMarksDetails();

            frm.trackingMark = trackingMarks;
            if (trackingMarks.Status > 0)
            {
                frm.btnSave.Enabled = frm.btnSaveNew.Enabled = false;
            }
            else if (trackingMarks.EmployeeID != Global.EmployeeID)
            {
                frm.btnSave.Enabled = frm.btnSaveNew.Enabled = false;
            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnFind_Click(null, null);
                grvData.FocusedRowHandle = rowHandle;
            }
        }


        private void Approved(int Status)
        {
            int Id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (Id <= 0) return;
            string reasonCancel = "";
            //Nhập lí do hủy duyệt
            if (Status == 2)
            {
                frmTrackingMarksCancel frm = new frmTrackingMarksCancel();
                if (frm.ShowDialog() == DialogResult.OK) reasonCancel = frm.txtReasonCancel.Text;
                else return;
            }

            TrackingMarksModel tracking = SQLHelper<TrackingMarksModel>.FindByID(Id) ?? new TrackingMarksModel();
            if (tracking.ID <= 0) return;
            //if (tracking.Status > 0) return;



            //Add Log
            string statusTextOld = tracking.Status == 0 ? "chưa hoàn thành" : (tracking.Status == 1 ? "hoàn thành" : "Huỷ");
            string statusTextNew = Status == 0 ? "chưa hoàn thành" : (Status == 1 ? "hoàn thành" : "Huỷ");
            string contenLog = $"Văn bản: {tracking.DocumentName} đổi từ {statusTextOld} thành {statusTextNew}.";
            TrackingMarksLogModel log = new TrackingMarksLogModel()
            {
                TrackingMarksID = tracking.ID,
                DateLog = DateTime.Now,
                EmployeeID = Global.EmployeeID,
                ContentLog = contenLog
            };

            tracking.Status = Status;
            tracking.ReasonCancel = reasonCancel;
            tracking.SignDatedActual = tracking.ApprovedDate = DateTime.Now;
            tracking.ApprovedID = Global.EmployeeID;

            SQLHelper<TrackingMarksModel>.Update(tracking);
            SQLHelper<TrackingMarksLogModel>.Insert(log);

            btnFind_Click(null, null);


            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            string documentName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colDocumentName));

            
            if (Status == 1) //Hoàn thành
            {

                string text = $"Văn bản: {documentName} đã hoàn thành!";
                TextUtils.AddNotify("ĐĂNG KÝ ĐÓNG DẤU", text, employeeID);
            }
            else if (Status == 2)//Huỷ
            {
                string text = $"Văn bản: {documentName} đã huỷ\n" +
                    $"Lý do: {reasonCancel}";
                TextUtils.AddNotify("ĐĂNG KÝ ĐÓNG DẤU", text, employeeID);
            }
        }

        private void btnApproveDocumentHR_Click(object sender, EventArgs e)
        {
            Approved(1);
        }

        private void btnUnApproveDocumentHR_Click(object sender, EventArgs e)
        {
            Approved(2);
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"SoTheoDoiDongDau_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";

            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"SoTheoDoiDongDau_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");
                string filepath = f.FileName;

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUnComplete_Click(object sender, EventArgs e)
        {
            Approved(0);
        }

        private void btnUpdateExpectDateComplete_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;

            frmTrackingMarksExpectComplete frm = new frmTrackingMarksExpectComplete();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var myDict = new Dictionary<string, object>()
                {
                    {"ExpectDateComplete",frm.dtpExpectDateComplete.Value.ToString("yyyy-MM-dd HH:mm:ss") }

                };

                SQLHelper<TrackingMarksModel>.UpdateFieldsByID(myDict, id);
                btnFind_Click(null, null);
                grvData.FocusedRowHandle = rowHandle;

                int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
                string documentName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colDocumentName));
                string text = $"Văn bản: {documentName}\n" +
                                $"Dự kiến hoàn thành: {frm.dtpExpectDateComplete.Value.ToString("dd/MM/yyyy")}";
                TextUtils.AddNotify("ĐĂNG KÝ ĐÓNG DẤU", text, employeeID);
            }
        }
    }
}
