using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
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
    public partial class frmCustomerPart : _Forms
    {
        public int IDCutomer;
        public delegate void Signal(string si);
        public Signal Send;
        public frmCustomerPart()
        {
            InitializeComponent();
        }

        private void frmCustomerPart_Load(object sender, EventArgs e)
        {
            loadCustomer();
            cbCustomer.EditValue = IDCutomer;
            loadGrvData();
        }
        private void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        void loadGrvData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetCustomerPart", "A", new string[] { "@ID" }, new object[] { IDCutomer });
            grdData.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Close();
        }
        void SaveData()
        {
            if (lstDelete.Count > 0)
                CustomerPartBO.Instance.Delete(lstDelete);
            for (int i = 0; i < grvData.RowCount; i++)
            {
                CustomerPartModel model = new CustomerPartModel();
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if(ID>0)
                {
                    model =(CustomerPartModel) CustomerPartBO.Instance.FindByPK(ID);
                }
                model.PartCode = TextUtils.ToString(grvData.GetRowCellValue(i, colPartCode));
                model.PartName = TextUtils.ToString(grvData.GetRowCellValue(i, colPartName));
                model.CustomerID = TextUtils.ToInt(cbCustomer.EditValue);
                model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i,colAdd));
                if (model.ID > 0)
                    CustomerPartBO.Instance.Update(model);
                else
                    CustomerPartBO.Instance.Insert(model);
            }
        }
        ArrayList lstDelete = new ArrayList();

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colPartCode));
            if (MessageBox.Show($"Bạn có muốn xóa bộ phận {name }", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                lstDelete.Add(ID);
            }
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            int stt = 1;
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && info.Column == colAdd)
            {
                int STT;
                DataTable dt = (DataTable)grdData.DataSource;

                // khi click STT tự động tăng
                if (dt.Rows.Count == 0)
                {
                    STT = 1;
                }
                else
                {
                    STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
                }
                DataRow dtrow = dt.NewRow();
                dtrow["STT"] = STT;
                dt.Rows.Add(dtrow);
                grdData.DataSource = dt;

            }
        }

        private void frmCustomerPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            loadGrvData();
        }
    }
}
