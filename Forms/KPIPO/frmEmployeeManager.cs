using BMS.Business;
using BMS.Model;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
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
    public partial class frmEmployeeManager : _Forms
    {
        public frmEmployeeManager()
        {
            InitializeComponent();
        }

        private void frmPosition_Load(object sender, EventArgs e)
        {
            loadGroup();
            loadGrvData();
        }


        void loadGroup()
        {
            DataTable dt = TextUtils.Select("Select * From GroupSales");
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DisplayMember = "GroupSalesName";
            cbGroup.Properties.DataSource = dt;

        }
        void loadGrvData()
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetEmployeeManager", new string[] { "@group" }, new object[] { TextUtils.ToInt(cbGroup.EditValue) });
            tlGroupSale.DataSource = ds.Tables[0];
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddGroupSalesUser frm = new frmAddGroupSalesUser();
            frm.group = TextUtils.ToInt(cbGroup.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrvData();
            }
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(tlGroupSale.FocusedNode.GetValue(colIDGroup));
            GroupSalesUserModel model = (GroupSalesUserModel)GroupSalesUserBO.Instance.FindByPK(ID);
            frmAddGroupSalesUser frm = new frmAddGroupSalesUser();
            frm.SalesUserModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrvData();
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(tlGroupSale.FocusedNode.GetValue(colIDGroup));
            if (MessageBox.Show("Bạn có thực sự muốn xóa không!", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                GroupSalesUserBO.Instance.Delete(ID);
                tlGroupSale.DeleteSelectedNodes();
                TextUtils.ExcuteSQL($"Update GroupSalesUser set ParentID=0 where ParentID={ID}");
            }
            loadGrvData();
        }
        private void btnUserType_Click(object sender, EventArgs e)
        {
            frmPosition1 frm = new frmPosition1();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrvData();
            }
        }
        private void cbGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadGrvData();
        }

        private void tlGroupSale_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            int color = TextUtils.ToInt(tlGroupSale.GetRowCellValue(e.Node, colParentID));
            if (color == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(192, 192, 192);
            }
        }

        private void aDDLeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddLeader frm = new frmAddLeader();
            frm.ID = TextUtils.ToInt(cbGroup.EditValue);
            frm.IDuser = TextUtils.ToInt(tlGroupSale.FocusedNode.GetValue(colIDGroup));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrvData();
            }
        }

        private void tlGroupSale_DragDrop(object sender, DragEventArgs e)
        {
            DataTable dt = (DataTable)tlGroupSale.DataSource;
        }

        private void tlGroupSale_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        int indexOld;

 
        private void tlGroupSale_MouseDown(object sender, MouseEventArgs e)
        {
            if (ckEdit.Checked)
            {
                TreeListHitInfo info = tlGroupSale.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Node == null) return;
                indexOld = info.Node.Id;
                TreeListNode node = info.Node;
                if (indexOld != -1)
                {
                    tlGroupSale.DoDragDropNode(node, DragDropEffects.Move);
                }
            }
        }
        private void tlGroupSale_MouseUp(object sender, MouseEventArgs e)
        {
           
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupSalesUserModel model = new GroupSalesUserModel();
            foreach (TreeListNode item in tlGroupSale.Nodes)
            {
                int ID = TextUtils.ToInt(item.GetValue(colIDGroup));
                if (ID > 0)
                {
                    model = (GroupSalesUserModel)GroupSalesUserBO.Instance.FindByPK(ID);
                }
                model.ParentID = TextUtils.ToInt(item.GetValue(colParentID));
                foreach (TreeListNode child in item.Nodes)
                {
                    GroupSalesUserModel mdchid = new GroupSalesUserModel();
                    int IDchid = TextUtils.ToInt( child.GetValue(colIDGroup));
                    if (IDchid > 0)
                    {
                        mdchid = (GroupSalesUserModel)GroupSalesUserBO.Instance.FindByPK(IDchid);
                    }
                    mdchid.ParentID = TextUtils.ToInt(child.GetValue(colParentID));
                    foreach (TreeListNode child1 in child.Nodes)
                    {
                        GroupSalesUserModel mdchil1 = new GroupSalesUserModel();
                        int IDchid1 = TextUtils.ToInt(child1.GetValue(colIDGroup));
                        if (IDchid1 > 0)
                        {
                            mdchil1 = (GroupSalesUserModel)GroupSalesUserBO.Instance.FindByPK(IDchid1);
                        }
                        mdchil1.ParentID = TextUtils.ToInt(child1.GetValue(colParentID));
                        if (mdchil1.ID > 0)
                        {
                            GroupSalesUserBO.Instance.Update(mdchil1);
                        }
                    }
                    if (mdchid.ID > 0)
                    {
                        GroupSalesUserBO.Instance.Update(mdchid);
                    }
                }
                if (model.ID > 0)
                {
                    GroupSalesUserBO.Instance.Update(model);
                }
            }
            ckEdit.Checked = false;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if(ckEdit.Checked)
            {
                tlGroupSale.AllowDrop = true;
            }    
        }
    }
}
