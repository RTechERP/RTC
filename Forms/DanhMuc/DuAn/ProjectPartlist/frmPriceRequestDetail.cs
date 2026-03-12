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
    public partial class frmPriceRequestDetail : _Forms
    {
        public int statusPurchaseRequest = 0;
        public frmPriceRequestDetail()
        {
            InitializeComponent();
        }

        private void frmPriceRequestDetail_Load(object sender, EventArgs e)
        {
            //dtpDeadlinePriceRequest.Value = new DateTime(DateTime.Now.AddMonths(+1).Year, DateTime.Now.AddMonths(+1).Month, DateTime.Now.AddMonths(+1).Day);
            dtpDeadlinePriceRequest.Value = DateTime.Now;
            if (statusPurchaseRequest == 3 || statusPurchaseRequest == 5)
            {
                dtpDeadlinePriceRequest.Value = DateTime.Now;
            }

            if (!txtNote.Visible)
            {
                tablePanel1.SetColumnSpan(dtpDeadlinePriceRequest, 2);
                tablePanel1.Size = new Size(tablePanel1.Width, 29);
                this.Size = new Size(this.Width, 200);
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmPriceRequestDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }


        bool CheckValidate()
        {
            DateTime deadline = dtpDeadlinePriceRequest.Value;
            DateTime dateNow = DateTime.Now;

            double timeSpan = (deadline.Date - dateNow.Date).TotalDays + 1;
            if (dateNow.Hour < 15)
            {
                if (timeSpan < 2)
                {
                    MessageBox.Show("Deadline tối thiếu là 2 ngày từ ngày hiện tại!", "Thông báo");
                    return false;
                }
            }
            else if (timeSpan < 3)
            {
                MessageBox.Show("Yêu cầu từ sau 15h nên ngày Deadline sẽ bắt đầu tính từ ngày hôm sau và tối thiểu là 2 ngày!", "Thông báo");
                return false;
            }

            if (deadline.DayOfWeek == DayOfWeek.Sunday || deadline.DayOfWeek == DayOfWeek.Saturday)
            {
                MessageBox.Show("Deadline phải là ngày làm việc (T2 - T6)!", "Thông báo");
                return false;
            }

            int coutWeekday = 0;
            for (int i = 0; i < timeSpan; i++)
            {
                DateTime dateValue = dateNow.Date.AddDays(i);
                if (dateValue.DayOfWeek == DayOfWeek.Sunday || dateValue.DayOfWeek == DayOfWeek.Saturday)
                {
                    coutWeekday++;
                }
            }

            if (coutWeekday > 0)
            {
                DialogResult dialog = MessageBox.Show($"Deadline sẽ không tính Thứ 7 và Chủ nhật.\nBạn có chắc muốn chọn Deadline là ngày [{deadline.ToString("dd/MM/yyyy")}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return dialog == DialogResult.Yes;
            }

            return true;
        }
    }
}
