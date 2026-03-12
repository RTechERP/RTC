using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections;
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
    public partial class frmSettingFolder : Form
    {
        public frmSettingFolder()
        {
            InitializeComponent();
        }

        private void treeData_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo info = treeData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == coladd && e.Y < 50)
            {
                treeData.Nodes.Add();
            }
        }
        ArrayList lstDelete = new ArrayList();
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)treeData.DataSource;
            dt.AcceptChanges();
           if(lstDelete.Count >0)
            ProjectTreeFolderBO.Instance.Delete(lstDelete);
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProjectTreeFolderModel model = new ProjectTreeFolderModel();
                int ID = TextUtils.ToInt(dt.Rows[i]["ID"]);
                if (ID > 0)
                    model = (ProjectTreeFolderModel)ProjectTreeFolderBO.Instance.FindByPK(ID);
                model.FolderName = TextUtils.ToString(dt.Rows[i]["FolderName"]);
                model.ParentID = TextUtils.ToInt(dt.Rows[i]["ParentID"]);
                if (model.ID > 0)
                    ProjectTreeFolderBO.Instance.Update(model);
                else
                    ProjectTreeFolderBO.Instance.Insert(model);
            }
            if(MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)==DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }    
        }

        private void frmSettingFolder_Load(object sender, EventArgs e)
        {
            loadFolder();
        }

        void loadFolder()
        {
            DataTable dt = TextUtils.Select($"Select * from ProjectTreeFolder");
            treeData.DataSource = dt;
        }

        private void thêmThưMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeData.AllNodesCount == 0) return;
            treeData.FocusedNode.Nodes.Add();
            treeData.ExpandAll();
        }

        private void btndele_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (treeData.AllNodesCount == 0) return;
                int id = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDfolder));
                lstDelete.Add(id);
                treeData.DeleteSelectedNodes();
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (treeData.AllNodesCount == 0) return;
            treeData.FocusedNode.Nodes.Add();
            treeData.ExpandAll();
        }
    }
}
