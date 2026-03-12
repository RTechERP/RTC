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
using Forms.DanhMuc.DuAn;

namespace BMS
{
    public partial class frmProjectType : _Forms
    {
        public frmProjectType()
        {
            InitializeComponent();
        }

        private void frmGroupFile_Load(object sender, EventArgs e)
        {
            loadProjectType();
        }

        #region Methods
        /// <summary>
        /// load SALE
        /// </summary>
        private void loadProjectType()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectType", "A", new string[] {"@FilterText"}, new object[] {txtFilterText.Text.Trim()});
            tlData.DataSource = dt;
            tlData.ExpandAll();
        }

        /// <summary>
        /// Load folder
        /// </summary>
        void loadFolderName()
        {
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("sp_GetProjectTypeTreeFolder", "A", new string[] { "@ProjectTypeID" }, new object[] { id });
            tlFolderName.DataSource = dt;
            tlFolderName.ExpandAll();
        }
        #endregion



        #region Button Events
        /// <summary>
        /// click button add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProjectTypeModel model = new ProjectTypeModel();
            frmProjectTypeDetail frm = new frmProjectTypeDetail();
            frm.projectType = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectType();
            }
        }

        /// <summary>
        /// click button edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = tlData.GetFocusedRow();
            int ID = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            ProjectTypeModel model = (ProjectTypeModel)ProjectTypeBO.Instance.FindByPK(ID);
            frmProjectTypeDetail frm = new frmProjectTypeDetail();
            frm.projectType = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectType();
                //tlData.SetFocusedNode(focusedRowHandle);
            }
        }
        /// <summary>
        /// click button delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = tlData.FocusedNode;
            int ID = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            string projectTypeName = TextUtils.ToString(tlData.GetFocusedRowCellValue(colProjectTypeName));
            if (ID == 0) return;

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa kiểu dự án : [{0}] không?", projectTypeName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProjectTypeBO.Instance.Delete(ID);
                tlData.DeleteSelectedNodes();
                //tlData.Focus = focusedRowHandle;
            }
        }
        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadProjectType();
        }
        #endregion

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void tlData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            loadFolderName();
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            //var focusnode = tlData.focusRo
            ProjectTreeFolderModel model = new ProjectTreeFolderModel();
            frmProjectTreeFolder frm = new frmProjectTreeFolder();
            frm.model = model;
            frm.idFolder = TextUtils.ToInt(tlFolderName.GetFocusedRowCellValue(colIDFolder));

            frm.projectTypeID = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectType();
            }
        }

        private void btnEditFolder_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = tlFolderName.GetFocusedRow();
            int ID = TextUtils.ToInt(tlFolderName.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            ProjectTreeFolderModel model = (ProjectTreeFolderModel)ProjectTreeFolderBO.Instance.FindByPK(ID);
            frmProjectTreeFolder frm = new frmProjectTreeFolder();
            frm.model = model;
            frm.projectTypeID = model.ProjectTypeID;

            
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectType();
                
                //tlData.SetFocusedNode(focusedRowHandle);
            }
        }

        private void tlFolderName_DoubleClick(object sender, EventArgs e)
        {
            btnEditFolder_Click(null, null);
        }

        private void tlFolderName_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    tlFolderName.PopupMenuShowing += tlFolderName_PopupMenuShowing;
            //}
        }

        private void tlFolderName_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            contextMenuStrip1.Show(tlData, e.Point);
        }

        private void btnDeleteFolder_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = tlData.FocusedNode;
            int ID = TextUtils.ToInt(tlFolderName.GetFocusedRowCellValue(colID));
            int parentID = TextUtils.ToInt(tlFolderName.GetFocusedRowCellValue(colParentIDFolder));
            string folderName = TextUtils.ToString(tlFolderName.GetFocusedRowCellValue(colFolderName));
            if (ID == 0) return;

            if (MessageBox.Show(string.Format("Dữ liệu trong folder [{0}] sẽ bị mất. \nBạn có chắc muốn xóa không ?", folderName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (parentID == 0)
                {
                    DataTable dt = TextUtils.Select($"select * from ProjectTreeFolder where ID = {ID} or ParentID = {ID}");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProjectTreeFolderBO.Instance.Delete(TextUtils.ToInt(dt.Rows[i]["ID"]));
                        tlFolderName.DeleteSelectedNodes();
                    }
                }
                else
                {
                    ProjectTreeFolderBO.Instance.Delete(ID);
                    tlFolderName.DeleteSelectedNodes();
                }
                //tlData.Focus = focusedRowHandle;
            }
        }
    }
}


