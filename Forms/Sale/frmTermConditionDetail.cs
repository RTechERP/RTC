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
    public partial class frmTermConditionDetail : _Forms
    {
        public TermConditionModel term = new TermConditionModel();
        public frmTermConditionDetail()
        {
            InitializeComponent();
        }

        private void frmTermConditionDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        void LoadData()
        {
            cboType.SelectedIndex = term.Type;
            txtSTT.Value = term.STT;
            txtTermCode.Text = term.TermCode;
            txtDescriptionVietnamese.Text = term.DescriptionVietnamese;
            txtDescriptionEnglish.Text = term.DescriptionEnglish;
        }


        bool SaveData()
        {
            term.Type = cboType.SelectedIndex;
            term.STT = (int)txtSTT.Value;
            term.TermCode = txtTermCode.Text.Trim();
            term.DescriptionVietnamese = txtDescriptionVietnamese.Text.Trim();
            term.DescriptionEnglish = txtDescriptionEnglish.Text.Trim();
            if (term.ID <= 0)
            {
                SQLHelper<TermConditionModel>.Insert(term);
            }
            else
            {
                SQLHelper<TermConditionModel>.Update(term);
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

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                term = new TermConditionModel();
                LoadData();
            }
        }
    }
}
