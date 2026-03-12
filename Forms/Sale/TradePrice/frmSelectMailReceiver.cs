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

namespace BMS
{
    public partial class frmSelectMailReceiver : _Forms
    {
        public int receiverID = 0;
        public frmSelectMailReceiver()
        {
            InitializeComponent();
        }


        private void frmSelectMailReceiver_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboReceiver.Properties.ValueMember = "ID";
            cboReceiver.Properties.DisplayMember = "FullName";
            cboReceiver.Properties.DataSource = list;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int selectedID = TextUtils.ToInt(cboReceiver.EditValue);
            string selectedName = TextUtils.ToString(cboReceiver.Properties.GetDisplayValueByKeyValue(cboReceiver.EditValue));
            if (MessageBox.Show(string.Format($"Bạn có muốn gửi mail  tới {selectedName} không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (selectedID == 0)
                {
                    MessageBox.Show("Vui lòng chọn người nhận mail!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                receiverID = selectedID;
                this.DialogResult = DialogResult.OK;
               
            }
        }
    }
}