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
    public partial class frmMakerDetail : _Forms
    {
        public MakerModel maker = new MakerModel();
        public frmMakerDetail()
        {
            InitializeComponent();
        }

        private void frmMakerDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            txtCodeMaker.Text = maker.CodeMaker;
            txtNameMaker.Text = maker.NameMaker;

            var listMakers = SQLHelper<MakerModel>.FindAll();
            txtSTT.Value = listMakers.Count <= 0 ? 1 : listMakers.Max(x => x.STT) + 1;
        }


        
        bool SaveData()
        {
            if (!CheckValidate()) return false;

            maker.CodeMaker = txtCodeMaker.Text.Trim();
            maker.STT = TextUtils.ToInt(txtSTT.Value);
            maker.NameMaker = txtNameMaker.Text.Trim();

            if (maker.ID <= 0)
            {
                SQLHelper<MakerModel>.Insert(maker);
            }
            else
            {
                SQLHelper<MakerModel>.Update(maker);
            }
            return true;
        }

        bool CheckValidate()
        {
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
                maker = new MakerModel();
                LoadData();
            }
        }

        private void frmMakerDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
