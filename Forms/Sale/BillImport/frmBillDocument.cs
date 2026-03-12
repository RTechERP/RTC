using BaseBusiness.DTO;
using BMS.Model;
using BMS.Utils;
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
    public partial class frmBillDocument : _Forms
    {
        public int billType = 0; //0: Phiếu nhập; 1:Phiếu xuất
        public frmBillDocument()
        {
            InitializeComponent();
        }

        private void frmBillDocument_Load(object sender, EventArgs e)
        {
            this.Text += billType == 0 ? " NHẬP" : " XUẤT";
            dtpDateReceiver.EditValue = DateTime.Now;
            cboDocumentType.SelectedIndex = 0;

            LoadBillCode();
            LoadData();
        }

        void LoadBillCode()
        {
            if (billType == 0)
            {
                List<BillImportModel> list = SQLHelper<BillImportModel>.FindAll().OrderByDescending(x => x.CreatedDate).ToList();
                cboBillCode.Properties.DisplayMember = "BillImportCode";
                cboBillCode.Properties.ValueMember = "ID";
                cboBillCode.Properties.DataSource = list;

                gridColumn2.Visible = true;
                gridColumn1.Visible = false;
            }
            else
            {
                List<BillExportModel> list = SQLHelper<BillExportModel>.FindAll().OrderByDescending(x => x.CreatedDate).ToList();
                cboBillCode.Properties.DisplayMember = "Code";
                cboBillCode.Properties.ValueMember = "ID";
                cboBillCode.Properties.DataSource = list;

                gridColumn2.Visible = false;
                gridColumn1.Visible = true;
            }


        }
        void LoadData()
        {
            int billId = TextUtils.ToInt(cboBillCode.EditValue);
            List<BillImportDocumentDTO> list = SQLHelper<BillImportDocumentDTO>.ProcedureToList("spGetBillDocument", 
                                                                        new string[] { "@BillType", "@BillID" }, 
                                                                        new object[] { billType, billId });
            grdData.DataSource = list;
        }


        bool CheckValidate()
        {
            if (TextUtils.ToInt(cboBillCode.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Mã phiếu!", "Thông báo");
                return false;
            }

            if (cboDocumentType.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Loại!", "Thông báo");
                return false;
            }

            var exp1 = new Expression("DocumentType", cboDocumentType.SelectedIndex);
            var exp2 = new Expression("BillImportID", TextUtils.ToInt(cboBillCode.EditValue));
            if (billType == 0)
            {
                var documents = SQLHelper<BillImportDocumentModel>.FindByExpression(exp1.And(exp2));
                if (documents.Count > 0)
                {
                    MessageBox.Show($"Loại hồ sơ [{cboDocumentType.Text}] đã tồn tại ở phiếu [{cboBillCode.Text}]!", "Thông báo");
                    return false;
                }
            }
            else
            {
                exp2 = new Expression("BillExportID", TextUtils.ToInt(cboBillCode.EditValue));
                var documents = SQLHelper<BillExportDocumentModel>.FindByExpression(exp1.And(exp2));
                if (documents.Count > 0)
                {
                    MessageBox.Show($"Loại hồ sơ [{cboDocumentType.Text}] đã tồn tại ở phiếu [{cboBillCode.Text}]!", "Thông báo");
                    return false;
                }
            }
            return true;
        }

        bool SaveData()
        {
            if (!CheckValidate())
            {
                return false;
            }
            if (billType == 0)
            {
                BillImportDocumentModel document = new BillImportDocumentModel();
                document.BillImportID = TextUtils.ToInt(cboBillCode.EditValue);
                document.DocumentType = cboDocumentType.SelectedIndex;
                document.DateReceiver = TextUtils.ToDate4(dtpDateReceiver.EditValue);
                document.Reason = txtReason.Text.Trim();
                document.StatusDocument = chkStatusDocument.Checked;

                SQLHelper<BillImportDocumentModel>.Insert(document);
            }
            else
            {
                BillExportDocumentModel document = new BillExportDocumentModel();
                document.BillExportID = TextUtils.ToInt(cboBillCode.EditValue);
                document.DocumentType = cboDocumentType.SelectedIndex;
                document.DateReceiver = TextUtils.ToDate4(dtpDateReceiver.EditValue);
                document.Reason = txtReason.Text.Trim();
                document.StatusDocument = chkStatusDocument.Checked;

                SQLHelper<BillExportDocumentModel>.Insert(document);
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                LoadData();
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                LoadData();
            }
        }
    }
}
