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
    public partial class frmPosition : _Forms
    {
        public frmPosition()
        {
            InitializeComponent();
        }
        private void frmPosition_Load(object sender, EventArgs e)
        {
            loadGrv();

            for (int i = 1; i <= 10; i++)
            {
                ToolStripMenuItem item1 = new ToolStripMenuItem();
                ToolStripMenuItem item2 = new ToolStripMenuItem();
                item1.Text = $"Mức độ ưu tiên {i}";
                item1.Tag = i;
                item1.Click += Item1_Click;

                item2.Text = $"Mức độ ưu tiên {i}";
                item2.Tag = i;
                item2.Click += Item2_Click;

                contextMenuStrip1.Items.Add(item1);
                contextMenuStrip2.Items.Add(item2);
            }


            ToolStripMenuItem item3 = new ToolStripMenuItem();
            item3.Text = "Khác";
            item3.Tag = contextMenuStrip1.Items.Count + 1;
            //item3.Click += Item3_Click;

            ToolStripMenuItem item4 = new ToolStripMenuItem();
            item4.Text = "Khác";
            item4.Tag = contextMenuStrip2.Items.Count + 1;
            //item4.Click += Item4_Click;

            ToolStripDropDownItem subItem1;
            ToolStripDropDownItem subItem2;
            for (int i = 11; i <= 20; i++)
            {
                subItem1 = new ToolStripMenuItem();
                subItem2 = new ToolStripMenuItem();
                subItem1.Text = $"Mức độ ưu tiên {i}";
                subItem1.Tag = i;
                subItem1.Click += Item1_Click;

                subItem2.Text = $"Mức độ ưu tiên {i}";
                subItem2.Tag = i;
                subItem2.Click += Item2_Click;

                item3.DropDownItems.Add(subItem1);
                item4.DropDownItems.Add(subItem2);
            }

            contextMenuStrip1.Items.Add(item3);
            contextMenuStrip2.Items.Add(item4);
        }

        private void Item2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            List<int> listID = new List<int>();
            int[] rowSelected = grvChucVuNB.GetSelectedRows();
            foreach (int row in rowSelected)
            {
                int id = TextUtils.ToInt(grvChucVuNB.GetRowCellValue(row, colIDChucVuNB));
                listID.Add(id);
            }
            if (listID.Count <= 0)
            {
                MessageBox.Show("Bạn chưa chọn chức vụ! Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            string idNB = string.Join(",", listID);
            string sql = $"UPDATE dbo.EmployeeChucVu SET PriorityOrder = {item.Tag} WHERE ID IN({idNB})";

            try
            {
                TextUtils.ExcuteSQL(sql);
                loadGrv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Item1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            List<int> listID = new List<int>();
            int[] rowSelected = grvChucVuHD.GetSelectedRows();

            foreach (int row in rowSelected)
            {
                int id = TextUtils.ToInt(grvChucVuHD.GetRowCellValue(row, colIDChucVuHD));
                listID.Add(id);
            }
            if (listID.Count <= 0)
            {
                MessageBox.Show("Bạn chưa chọn chức vụ! Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            string idHD = string.Join(",", listID);
            string sql = $"UPDATE dbo.EmployeeChucVuHD SET PriorityOrder = {item.Tag} WHERE ID IN({idHD})";

            try
            {
                TextUtils.ExcuteSQL(sql);
                loadGrv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void loadGrv()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeChucVu", new string[] { }, new object[] { });

            //DataTable dt1 = TextUtils.Select("Select * from EmployeeChucVuHD Order");
            //DataTable dt2 = TextUtils.Select("Select * from EmployeeChucVu");

            grdChucVuHD.DataSource = dataSet.Tables[1];
            grdChucVuNB.DataSource = dataSet.Tables[0];
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmPositionDetail frm = new frmPositionDetail();
            frm.HD = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrv();
            }
        }
        private void btnNewNB_Click(object sender, EventArgs e)
        {
            frmPositionDetail frm = new frmPositionDetail();
            frm.NB = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrv();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvChucVuHD.GetFocusedRowCellValue(colIDChucVuHD));
            if (id == 0) return;

            // _rownIndex = grvData.FocusedRowHandle;

            EmployeeChucVuHDModel model = (EmployeeChucVuHDModel)EmployeeChucVuHDBO.Instance.FindByPK(id);

            frmPositionDetail frm = new frmPositionDetail();
            frm.HD = true;
            frm.ModelHD = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrv();
            }
        }

        private void btnEditNB_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvChucVuNB.GetFocusedRowCellValue(colIDChucVuNB));
            if (id == 0) return;

            // _rownIndex = grvData.FocusedRowHandle;

            EmployeeChucVuModel model = (EmployeeChucVuModel)EmployeeChucVuBO.Instance.FindByPK(id);

            frmPositionDetail frm = new frmPositionDetail();
            frm.NB = true;
            frm.ModelNB = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrv();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvChucVuHD.GetFocusedRowCellValue(colIDChucVuHD));
            if (id == 0) return;
            string name = grvChucVuHD.GetFocusedRowCellValue(colNameChucVuHD).ToString();
            DialogResult result = MessageBox.Show($"Bạn có thực sự muốn xoá chức vụ {name} không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                EmployeeChucVuHDBO.Instance.Delete(id);
                loadGrv();
            }
        }

        private void btnDeleteNB_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvChucVuNB.GetFocusedRowCellValue(colIDChucVuNB));
            if (id == 0) return;
            string name = grvChucVuNB.GetFocusedRowCellValue(colNameChucVuNB).ToString();
            DialogResult result = MessageBox.Show($"Bạn có thực sự muốn xoá chức vụ {name} không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                EmployeeChucVuBO.Instance.Delete(id);
                loadGrv();
            }
        }
        private void grvChucVuHD_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grvChucVuNB_DoubleClick(object sender, EventArgs e)
        {
            btnEditNB_Click(null, null);
        }

        private void btnIsBusinessCostHD_Click(object sender, EventArgs e)
        {
            UpdateIsBusinessCostHD(true);
        }

        private void btnNotBusinessCostHD_Click(object sender, EventArgs e)
        {
            UpdateIsBusinessCostHD(false);
        }

        private void btnIsBusinessCost_Click(object sender, EventArgs e)
        {
            UpdateIsBusinessCostNB(true);
        }


        private void btnNotBusinessCost_Click(object sender, EventArgs e)
        {
            UpdateIsBusinessCostNB(false);
        }

        void UpdateIsBusinessCostHD(bool isBusinessCost)
        {
            List<int> listID = new List<int>();
            int[] rowSelected = grvChucVuHD.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn chức vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            foreach (int row in rowSelected)
            {
                int id = TextUtils.ToInt(grvChucVuHD.GetRowCellValue(row, "ID"));
                if (id <= 0) continue;
                listID.Add(id);
            }

            string idText = string.Join(",", listID);
            //string sql = $"UPDATE dbo.EmployeeChucVuHD SET IsBusinessCost = 'True' WHERE ID IN({idHD})";

            var myDict = new Dictionary<string, object>()
            {
                {"IsBusinessCost", isBusinessCost ? 1:0},
                {"UpdatedBy", Global.AppUserName},
                {"UpdatedDate", DateTime.Now},
            };
            SQLHelper<EmployeeChucVuHDModel>.UpdateFields(myDict, new Utils.Expression("ID", idText, "IN"));
            loadGrv();
        }


        void UpdateIsBusinessCostNB(bool isBusinessCost)
        {
            List<int> listID = new List<int>();
            int[] rowSelected = grvChucVuNB.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn chức vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            foreach (int row in rowSelected)
            {
                int id = TextUtils.ToInt(grvChucVuNB.GetRowCellValue(row, "ID"));
                if (id <= 0) continue;
                listID.Add(id);
            }

            string idText = string.Join(",", listID);
            //string sql = $"UPDATE dbo.EmployeeChucVuHD SET IsBusinessCost = 'True' WHERE ID IN({idHD})";

            var myDict = new Dictionary<string, object>()
            {
                {"IsBusinessCost", isBusinessCost ? 1:0},
                {"UpdatedBy", Global.AppUserName},
                {"UpdatedDate", DateTime.Now},
            };
            SQLHelper<EmployeeChucVuModel>.UpdateFields(myDict, new Utils.Expression("ID", idText, "IN"));
            loadGrv();
            
        }

        
    }
}
