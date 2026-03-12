using BMS.Business;
using BMS.Model;
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
    public partial class frmRulePayDetail : _Forms
    {
       public RulePayModel model = new RulePayModel();
        public frmRulePayDetail()
        {
            InitializeComponent();
        }

        private void frmRulePayDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            txtCode.Text = model.Code;
            txtNote.Text = model.Note;
        }
        bool save()
        { 
            if(txtCode.Text.Trim()=="")
            {
                MessageBox.Show("Ô mã khônng được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                DataTable dt = TextUtils.Select($"select top 1* from RulePay where Code='{txtCode.Text.Trim()}'");
                
                if(model.ID<=0 & dt.Rows.Count>0)
                {
                    MessageBox.Show("Mã này đã được sử dụng vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }    
            }
            model.Code = txtCode.Text;
            model.Note = txtNote.Text;
            if(model.ID>0)
            {
                RulePayBO.Instance.Update(model);
            }
            else
            {
                RulePayBO.Instance.Insert(model);
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(save())
            {
                this.DialogResult = DialogResult.OK;
            }    
        }

        private void btnSave_New_Click(object sender, EventArgs e)
        {
            if (save())
            {
                txtCode.Text = "";
                txtNote.Text = "";
                model = new RulePayModel();
            }
        }

        private void frmRulePayDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmRulePayDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
