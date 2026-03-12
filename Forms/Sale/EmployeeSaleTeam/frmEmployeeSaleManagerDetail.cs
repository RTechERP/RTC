using BMS;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Sale.EmployeeSaleManager
{
    public partial class frmEmployeeSaleManagerDetail : _Forms
    {
        public EmployeeTeamSaleModel EmployeeTeamSale = new EmployeeTeamSaleModel();
        public Func<EmployeeTeamSaleModel, Task> SaveEvent;
        public frmEmployeeSaleManagerDetail()
        {
            InitializeComponent();
        }
        void LoadTeams()
        {
            DataTable dt = TextUtils.GetTable("spGetEmployeeTeamSale");
            cboTeamSale.Properties.DataSource = dt;
            cboTeamSale.Properties.ValueMember = "ID";
            cboTeamSale.Properties.DisplayMember = "Name";
        }

        void LoadData()
        {
            txtSTT.Text = EmployeeTeamSale.STT.ToString();
            txtCode.Text = EmployeeTeamSale.Code;
            txtName.Text = EmployeeTeamSale.Name;
            cboTeamSale.EditValue = EmployeeTeamSale.ParentID;
        }

        bool ValidateData()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                Lib.ShowError("Hãy nhập mã team!");
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                Lib.ShowError("Hãy nhập tên team!");
                return false;
            }
            if (txtSTT.Text.Trim().Length > 0)
            {
                if (!int.TryParse(txtSTT.Text.Trim(), out int i))
                {
                    Lib.ShowError("STT không hợp lệ!");
                    return false;
                }
            }
                var existsTeam = SQLHelper<EmployeeTeamSaleModel>.FindByExpression(
                new Expression(EmployeeTeamSaleModel_Enum.Code, txtCode.Text)
                .And(new Expression(EmployeeTeamSaleModel_Enum.ID, EmployeeTeamSale.ID, "<>")))
                .FirstOrDefault();

            if (existsTeam != null)
            {
                Lib.ShowError($"Mã team [{existsTeam.Code}] đã tồn tại !");
                return false;
            }
            return true;
        }
        bool SaveData()
        {
            if (!ValidateData()) return false;

            EmployeeTeamSale.STT = TextUtils.ToInt(txtSTT.Text.Trim());
            EmployeeTeamSale.Code = txtCode.Text.Trim();
            EmployeeTeamSale.Name = txtName.Text.Trim();
            EmployeeTeamSale.ParentID = TextUtils.ToInt(cboTeamSale.EditValue);

            if (EmployeeTeamSale.ID > 0)
            {
                SQLHelper<EmployeeTeamSaleModel>.Update(EmployeeTeamSale);
                EmployeeTeamSale.UpdatedBy = Global.AppUserName;
                EmployeeTeamSale.UpdatedDate = DateTime.Now;
                
            }
            else
            {
                EmployeeTeamSale.ID = SQLHelper<EmployeeTeamSaleModel>.Insert(EmployeeTeamSale).ID;
                EmployeeTeamSale.CreatedBy = Global.AppUserName;
                EmployeeTeamSale.CreatedDate = DateTime.Now;
            }

            SaveEvent?.Invoke(EmployeeTeamSale);
            return true;
        }


        private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData()) this.Close();
        }

        private void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                LoadTeams();
                EmployeeTeamSale = new EmployeeTeamSaleModel();
                LoadData();
            }
        }

        private void frmEmployeeSaleManagerDetail_Load(object sender, EventArgs e)
        {
            LoadTeams();
            LoadData();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtSTT_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cboTeamSale_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}