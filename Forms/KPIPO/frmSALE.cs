using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace BMS
{
    public partial class frmSALE : _Forms
    {
        public int user;
        public frmSALE()
        {
            InitializeComponent();
        }

        private void frmSALE_Load(object sender, EventArgs e)
        {
            loadgroupSale();
            loadData();
            LoadCbNote();
        }
        void loadgroupSale()
        {
            DataTable dt = TextUtils.Select("Select ID,[GroupSalesName] From [GroupSales] ");
            cbGroup.Properties.DisplayMember = "GroupSalesName";
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DataSource = dt;
        }
        void loadData()
        {
             DataTable dt = TextUtils.Select($"Select g.MainIndexID,gu.GroupSalesID from GroupSalesUser gu inner join GroupSales g on g.ID=gu.GroupSalesID where gu.UserID={user}");
            cbGroup.EditValue = dt.Rows[0]["GroupSalesID"];
            cbNote.EditValue = dt.Rows[0]["MainIndexID"];

        }
        void LoadCbNote()
        {
            DataTable dt = TextUtils.Select("Select MainIndex,ID From [MainIndex]");
            cbNote.Properties.ValueMember = "ID";
            cbNote.Properties.DisplayMember = "MainIndex";
            cbNote.Properties.DataSource = dt;
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            try
            {
                GroupSalesModel model = new GroupSalesModel();
                if (cbGroup.EditValue != null)
                    model = (GroupSalesModel)GroupSalesBO.Instance.FindByPK(TextUtils.ToInt(cbGroup.EditValue));
                model.MainIndexID = TextUtils.ToString(cbNote.EditValue);
                if (model.ID > 0)
                    GroupSalesBO.Instance.Update(model);
                MessageBox.Show("Đã lưu thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        private void cbGroup_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dt = TextUtils.Select($"Select MainIndexID from GroupSales  where ID={cbGroup.EditValue}");
        }

        private void frmSALE_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}


