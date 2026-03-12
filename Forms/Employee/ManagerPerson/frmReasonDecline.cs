using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

namespace BMS
{
    public partial class frmReasonDecline : _Forms
    {
        public int ID;
        public string tablename;

        public List<EmployeeDeclineApprove> listDecline = new List<EmployeeDeclineApprove>();

        public frmReasonDecline()
        {
            InitializeComponent();
        }

        private void frmReasonDecline_Load(object sender, EventArgs e)
        {

        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReasonDecline.Text))
            {
                MessageBox.Show("Vui lòng nhập Lý do!!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //string TableName = tablename;
            //int id = ID;

            string sql = "";
            foreach (var item in listDecline)
            {
                string fieldName = tablename == "EmployeeNighShift" ? "IsApprovedTBP" : "DecilineApprove";
                sql += $"UPDATE {item.TableName} SET {fieldName} = 2, ReasonDeciline = N'{txtReasonDecline.Text.Trim()}' WHERE ID = {item.ID}\n";
            }
            

            try
            {
                TextUtils.ExcuteSQL(sql);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Thông báo");
            }
        }
    }
}