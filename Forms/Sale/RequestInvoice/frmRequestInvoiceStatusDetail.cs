using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Sale.RequestInvoice
{
    public partial class frmRequestInvoiceStatusDetail: _Forms
    {
        public RequestInvoiceStatusModel statusModel = new RequestInvoiceStatusModel();
        public frmRequestInvoiceStatusDetail()
        {
            InitializeComponent();
        }

        private void frmRequestInvoiceStatusDetail_Load(object sender, EventArgs e)
        {
            loadDetail();
        }
        private void loadDetail()
        {
            txtStatusCode.Text = statusModel.StatusCode;
            txtStatusName.Text = statusModel.StatusName;

        }
        bool CheckValidate()
        {
            return true;
        }
        bool SaveData()
        {
            if(!CheckValidate())
            {
                return false;
            }
            try
            {
                statusModel.StatusCode = txtStatusCode.Text;
                statusModel.StatusName = txtStatusName.Text;
                statusModel.IsDeleted = false;

                if (statusModel.ID > 0)
                {
                    //FirmBO.Instance.Update(oFirmModel);
                    SQLHelper<RequestInvoiceStatusModel>.Update(statusModel);
                }
                else
                {
                    //FirmBO.Instance.Insert(oFirmModel);
                    SQLHelper<RequestInvoiceStatusModel>.Insert(statusModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}r\n${ex.ToString()}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmRequestInvoiceStatusDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmRequestInvoiceStatusDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
