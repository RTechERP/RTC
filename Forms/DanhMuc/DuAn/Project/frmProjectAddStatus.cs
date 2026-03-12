using System;
using BMS.Business;
using BMS.Model;
using BMS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Utils;

namespace BMS
{
    public partial class frmProjectAddStatus : _Forms
    {
        ProjectStatusModel prs = new ProjectStatusModel();
        int maxStt, nStt;
        string nStatus;
        public frmProjectAddStatus()
        {
            InitializeComponent();
        }

        private void frmProjectAddStatus_Load(object sender, EventArgs e)
        {
            try{maxStt = SQLHelper<ProjectStatusModel>.FindAll().Max(m => m.STT); }
            catch { maxStt = 0; }

            txtSTT.Text = Lib.ToString(maxStt + 1);
            txtSTT.Enabled = false;
            txtStatus.BackColor = Color.White;
        }

        private bool SaveData()
        {
            if(txtStatus.Text != null && txtStatus.Text != "")
            {
                var ex1 = new Expression("ID", prs.ID, "<>");
                var ex2 = new Expression("StatusName", txtStatus.Text.Trim());
                var exists = SQLHelper<ProjectStatusModel>.FindByExpression(ex1.And(ex2)).FirstOrDefault();
                if (exists != null)
                {
                    MessageBox.Show($"Trạng thái [{txtStatus.Text}] đã tồn tại]!", "Thông báo");
                    return false;
                }
                else
                {
                    nStt = Lib.ToInt(txtSTT.Text);
                    nStatus = Lib.ToString(txtStatus.Text);
                    maxStt = nStt;
                    prs.STT = nStt;
                    prs.StatusName = nStatus;
                    SQLHelper<ProjectStatusModel>.Insert(prs);
                    return true;
                }
                
            }
            else
            {
                MessageBox.Show("Vui lòng nhập trạng thái!", "Thông báo", MessageBoxButtons.OK);
            }

            return false;
        }

        private void Reset()
        {
            txtSTT.Text = Lib.ToString(maxStt + 1);
            txtStatus.Clear();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                Reset();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
            
        }
    }
}
