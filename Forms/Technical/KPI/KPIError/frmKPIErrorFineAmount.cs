using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BMS.Model;

using DevExpress.XtraGrid.Views.Grid;

namespace BMS
{
	public partial class frmKPIErrorFineAmount: _Forms
	{
        public int KpiErrorID;

        public Action SaveEvent;

        bool isSaved = false;
        public frmKPIErrorFineAmount()
		{
            InitializeComponent();
		}
		void loadCboKPIError()
        {

            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIError", "A", new string[] { "@Keyword","@TypeID","@DepartmentID" }, new object[] {"",0,0});
            cboKPIError.Properties.DataSource =dt;
            cboKPIError.Properties.DisplayMember = "Name";
            cboKPIError.Properties.ValueMember = "ID";
            cboKPIError.EditValue = KpiErrorID;
        }
        void insertData()
        {
            for (int i = 1; i <= 6; i++)
            {
                SQLHelper<KPIErrorFineAmountModel>.Insert(new KPIErrorFineAmountModel
                {
                    KPIErrorID = KpiErrorID,
                    QuantityError = i,
                    TotalMoneyError = 0
                });
            }
        }
        void loadData()
        {
            if(cboKPIError.EditValue == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("QuantityError", typeof(int));
                dt.Columns.Add("TotalMoneyError", typeof(int));
                dt.Columns.Add("Note", typeof(string));

                for (int i = 1; i <= 6; i++)
                {
                    DataRow row = dt.NewRow();
                    row["QuantityError"] = i;
                    row["TotalMoneyError"] = 0;
                    row["Note"] = "";
                    dt.Rows.Add(row);
                }
                grdData.DataSource = dt;
            }
            else
            {
                DataTable dt = SQLHelper<KPIErrorFineAmountModel>.LoadDataFromSP("spGetKPIErrorFineAmount",new string[] {"@KPIErrorID"},new object[] {KpiErrorID} );
                if (dt.Rows.Count <= 0)
                {
                    insertData();
                    dt = SQLHelper<KPIErrorFineAmountModel>.LoadDataFromSP("spGetKPIErrorFineAmount", new string[] { "@KPIErrorID" }, new object[] { KpiErrorID });
                }

                grdData.DataSource = dt;

            }
        }
        private void frmKPIErrorFineAmount_Load(object sender, EventArgs e)
        {
            loadCboKPIError();
            loadData();
        }
        private void cboKPIError_EditValueChanged(object sender, EventArgs e)
        {
            KpiErrorID = TextUtils.ToInt(cboKPIError.EditValue);
            loadData();
        }

        private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            isSaved = true;
            grvData.CloseEditor();
            grvData.UpdateCurrentRow();
            SaveAllData();
            this.Close();
        }
        DataTable createData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("QuantityError", typeof(int));
            dt.Columns.Add("TotalMoneyError", typeof(int));
            dt.Columns.Add("Note", typeof(string));
            for (int i = 1; i <= 6; i++)
            {
                DataRow row = dt.NewRow();
                row["QuantityError"] = i;
                row["TotalMoneyError"] = 0;
                row["Note"] = "";
                dt.Rows.Add(row);
            }
            return dt;
        }
        private void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            isSaved = true;
            grvData.CloseEditor();
            grvData.UpdateCurrentRow();
            SaveAllData();
            grdData.DataSource = null;
            cboKPIError.EditValue = null;        
            grdData.DataSource = createData();
        }

        private void SaveAllData()
        {
            DataTable dt = (DataTable)grdData.DataSource;
            if (dt == null || dt.Rows.Count == 0)
                return;

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Modified) // Chỉ lưu nếu có thay đổi
                {
                    int id = TextUtils.ToInt(row["ID"]);
                    int totalMoneyError = TextUtils.ToInt(row["TotalMoneyError"]);
                    string note = TextUtils.ToString(row["Note"]);

                    Dictionary<string, object> dic = new Dictionary<string, object>()
                        {
                            { "TotalMoneyError", totalMoneyError },
                            { "Note", note }
                        };

                    var result = SQLHelper<KPIErrorFineAmountModel>.UpdateFieldsByID(dic, id);

                    if (!result.IsSuccess)
                    {
                        KPIErrorModel kPIErrorModel = SQLHelper<KPIErrorModel>.FindByID(id);
                        MessageBox.Show("Lỗi khi lưu đánh giá cho" + kPIErrorModel.Code + ": " + result.ErrorText, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            SaveEvent.Invoke();
        }
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string fieldName = e.Column.FieldName;
            if (fieldName != "TotalMoneyError" && fieldName != "Note")
                return;

            GridView view = sender as GridView;
            object newValue = e.Value;       
            int[] selectedRowHandles = view.GetSelectedRows();
            foreach (int rowHandle in selectedRowHandles)
            {
                if (rowHandle >= 0)
                {
                    DataRow dataRow = view.GetDataRow(rowHandle);
                    if (dataRow != null)
                    {
                        dataRow[fieldName] = newValue; 
                    }
                }
            }
            view.ClearSelection();
        }
    }
}