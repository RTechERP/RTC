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
    public partial class frmWarehouseDetail : _Forms
    {
        public WarehouseModel warehouse = new WarehouseModel();
        public frmWarehouseDetail()
        {
            InitializeComponent();
        }

        private void frmWarehouseDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }
        private void loadData()
        {
            if (warehouse.ID > 0)
            {
                txtWarehouseCode.Text = warehouse.WarehouseCode;
                txtWarehouseName.Text = warehouse.WarehouseName;
            }    
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        bool SaveData()
        {
            if (!validate()) return false;
            warehouse.WarehouseCode = txtWarehouseCode.Text;
            warehouse.WarehouseName = txtWarehouseName.Text;
            if (warehouse.ID > 0)
                WarehouseBO.Instance.Update(warehouse);
            else
                WarehouseBO.Instance.Insert(warehouse);
            return true;
        }
        bool validate()
        {
            if(txtWarehouseCode.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã kho", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                var ID = TextUtils.ExcuteScalar($"EXEC spGetCheckWarehouseCode N'{txtWarehouseCode.Text}', {warehouse.ID}");
                if(TextUtils.ToInt(ID) > 0)
                {
                    MessageBox.Show(string.Format($"Mã kho {txtWarehouseCode.Text} đã tồn tại. Vui lòng thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }    
            }
            if (txtWarehouseName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên kho", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }    
            return true;
        }
    }
}
