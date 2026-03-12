using BMS;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.DanhMuc.JobRequirement
{
    public partial class frmJobRequirementCheckPriceDetail: _Forms
    {
        public int jobRequirementID = 0;
        public JobRequirementCheckPriceModel jobRequirementCheckPriceModel = new JobRequirementCheckPriceModel();
        List<JobRequirementCheckPriceDetailModel> lstDeleteCheckPriceSupplier = new List<JobRequirementCheckPriceDetailModel>();

        public frmJobRequirementCheckPriceDetail()
        {
            InitializeComponent();
        }
        private void frmJobRequirementCheckPriceDetail_Load(object sender, EventArgs e)
        {
            LoadSuggestion();
            loadData();
            loadStatus();
            loadSupplier();
        }
        private void loadData()
        {
            if(jobRequirementCheckPriceModel.ID > 0)
            {
                txtCustomer.Text = jobRequirementCheckPriceModel.Customer;
                txtProductCode.Text = jobRequirementCheckPriceModel.ProductCode;
                txtQuantity.Text = TextUtils.ToString(jobRequirementCheckPriceModel.Quantity);
                txtUnit.Text = jobRequirementCheckPriceModel.Unit;
                dtpDeliveryDate.Value = jobRequirementCheckPriceModel.DeliveryDate.Value;
                dtpExpectedDate.Value = jobRequirementCheckPriceModel.ExpectedDate.Value;
                dtpRequestDate.Value = jobRequirementCheckPriceModel.RequestDate.Value;
                txtHRSuggestion.Text = jobRequirementCheckPriceModel.HRSuggestion;
                textNote.Text = jobRequirementCheckPriceModel.Note;

            }     
            DataTable dt = TextUtils.LoadDataFromSP("spGetJobRequirementCheckPriceDetail", "A", new string[] { "@JobRequirementID" },
                           new object[] { jobRequirementID });
            grdMaster.DataSource = dt;
        }

        void loadStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("StatusName", typeof(string));

            dt.Rows.Add(1, "Không có hóa đơn");
            dt.Rows.Add(2, "Có hóa đơn");

            cboStatusCheck.DisplayMember = "StatusName";
            cboStatusCheck.ValueMember = "ID";
            cboStatusCheck.DataSource = dt;
        }

        void loadSupplier()
        {
            DataTable dtSupplier = TextUtils.Select("SELECT NameNCC FROM SupplierSale WHERE IsDeleted <> 1");
            foreach (DataRow row in dtSupplier.Rows)
            {
                cboSupplier.Items.Add(row["NameNCC"].ToString());
            }
            grvMaster.Columns["Supplier"].ColumnEdit = cboSupplier;

        }

        void LoadSuggestion()
        {
            DataTable dtCustomerSuggest = TextUtils.Select("SELECT CustomerName FROM Customer WHERE IsDeleted <> 1");
            DataTable dtProductSuggest = TextUtils.Select("SELECT ProductName FROM ProductSale WHERE IsDeleted <> 1");
            DataTable dtUnitSuggest = TextUtils.Select("SELECT UnitName FROM UnitCount");


            if (dtCustomerSuggest != null && dtCustomerSuggest.Rows.Count > 0)
            {
                AutoCompleteStringCollection customerSource = new AutoCompleteStringCollection();
                foreach (DataRow row in dtCustomerSuggest.Rows)
                {
                    customerSource.Add(TextUtils.ToString(row["CustomerName"]));
                }
                txtCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtCustomer.AutoCompleteCustomSource = customerSource;
            }


            if (dtProductSuggest != null && dtProductSuggest.Rows.Count > 0)
            {
                AutoCompleteStringCollection productSource = new AutoCompleteStringCollection();
                foreach (DataRow row in dtProductSuggest.Rows)
                {
                    productSource.Add(TextUtils.ToString(row["ProductName"]));
                }
                txtProductCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtProductCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtProductCode.AutoCompleteCustomSource = productSource;
            }


            if (dtUnitSuggest != null && dtUnitSuggest.Rows.Count > 0)
            {
                AutoCompleteStringCollection unitSource = new AutoCompleteStringCollection();
                foreach (DataRow row in dtUnitSuggest.Rows)
                {
                    unitSource.Add(TextUtils.ToString(row["UnitName"]));
                }
                txtUnit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtUnit.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtUnit.AutoCompleteCustomSource = unitSource;
            }
        }


        private void grvMaster_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvMaster.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column == colSTTStatus && e.Y < 40)
                {
                    grvMaster.FocusedRowHandle = -1;
                    DataTable dt = (DataTable)grdMaster.DataSource;
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();

                    //int stt = dt.Rows.Count;
                    //int idMapping = TextUtils.ToInt(grvMaster.GetRowCellValue(stt - 1, colSTTStatus));
                    //dtrow["STT"] = stt + 1;
                    dt.Rows.Add(dtrow);
                    grdMaster.DataSource = dt;
                }
            }
        }

        private void btnAddAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        bool saveData()
        {
            int id = 0;
            grvMaster.CloseEditor();
            //if (!validate()) return false;

            jobRequirementCheckPriceModel.JobRequirementID = jobRequirementID;
            jobRequirementCheckPriceModel.DeliveryDate = dtpDeliveryDate.Value;
            jobRequirementCheckPriceModel.RequestDate = dtpRequestDate.Value;
            jobRequirementCheckPriceModel.ExpectedDate = dtpExpectedDate.Value;
            jobRequirementCheckPriceModel.Customer = TextUtils.ToString(txtCustomer.Text);
            jobRequirementCheckPriceModel.ProductCode = TextUtils.ToString(txtProductCode.Text);
            jobRequirementCheckPriceModel.Quantity = TextUtils.ToInt(txtQuantity.Text);
            jobRequirementCheckPriceModel.Unit = TextUtils.ToString(txtUnit.Text);
            jobRequirementCheckPriceModel.HRSuggestion = TextUtils.ToString(txtHRSuggestion.Text);
            jobRequirementCheckPriceModel.Note = TextUtils.ToString(textNote.Text);
            jobRequirementCheckPriceModel.UpdatedBy = Global.AppUserName;
            jobRequirementCheckPriceModel.UpdatedDate = DateTime.Now;
            jobRequirementCheckPriceModel.CreatedDate = jobRequirementCheckPriceModel.CreatedDate ?? DateTime.Now;
            jobRequirementCheckPriceModel.CreatedBy = Global.AppUserName;
            jobRequirementCheckPriceModel.IsDeleted = false;
            if (jobRequirementCheckPriceModel.ID > 0)
            {
                SQLHelper<JobRequirementCheckPriceModel>.Update(jobRequirementCheckPriceModel);
                id = jobRequirementCheckPriceModel.ID;
                
            }
            else
            {
                //SQLHelper<JobRequirementCheckPriceModel>.Insert(model);
                id = SQLHelper<JobRequirementCheckPriceModel>.Insert(jobRequirementCheckPriceModel).ID;

            }

            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                int detailID = TextUtils.ToInt(grvMaster.GetRowCellValue(i, gridColumn7));
                JobRequirementCheckPriceDetailModel detailModel = SQLHelper<JobRequirementCheckPriceDetailModel>.FindByID(detailID) ?? new JobRequirementCheckPriceDetailModel();

                detailModel.JobRequirementCheckPriceID = id;
                detailModel.Supplier = TextUtils.ToString(grvMaster.GetRowCellValue(i, colSupplier));
                detailModel.OfferPrice = TextUtils.ToDecimal(grvMaster.GetRowCellValue(i, colOfferPrice));
                detailModel.PurchasePrice = TextUtils.ToDecimal(grvMaster.GetRowCellValue(i, colPurchasePrice));
                detailModel.ShippingFee = TextUtils.ToDecimal(grvMaster.GetRowCellValue(i, colShippingFee));
                detailModel.TotalAmount = TextUtils.ToDecimal(grvMaster.GetRowCellValue(i, colTotalAmount));
                detailModel.LeadTime = TextUtils.ToString(grvMaster.GetRowCellValue(i, colLeadTime));
                detailModel.VAT = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colVAT));
                detailModel.Status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colStatus));
                detailModel.CreatedBy = Global.AppUserName;
                detailModel.CreatedDate = detailModel.CreatedDate ?? DateTime.Now;
                detailModel.UpdatedBy = Global.AppUserName;
                detailModel.UpdatedDate = DateTime.Now;
                detailModel.IsDeleted = false;

                if (detailModel.ID > 0)
                {
                    SQLHelper<JobRequirementCheckPriceDetailModel>.Update(detailModel);
                }
                else
                {
                    SQLHelper<JobRequirementCheckPriceDetailModel>.Insert(detailModel);
                }

            }
            foreach (var item in lstDeleteCheckPriceSupplier)
            {
                var myDict = new Dictionary<string, object>()
                        {
                            { JobRequirementCheckPriceDetailModel_Enum.IsDeleted.ToString(),true},
                            { JobRequirementCheckPriceDetailModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                            { JobRequirementCheckPriceDetailModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        };

                SQLHelper<JobRequirementCheckPriceDetailModel>.UpdateFieldsByID(myDict, item.ID);
            }
            this.DialogResult = DialogResult.OK;
            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            //string statusName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colStatus));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá dòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvMaster.DeleteSelectedRows();
                if (id <= 0) return;
                JobRequirementCheckPriceDetailModel item = SQLHelper<JobRequirementCheckPriceDetailModel>.FindByID(id);
                lstDeleteCheckPriceSupplier.Add(item);
            }
        }
    }
}
