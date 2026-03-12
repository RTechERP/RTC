using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmProjectPriority : _Forms
    {
        public int projectID;
        List<TreeListNode> listSelectedNode = new List<TreeListNode>();

        public List<ProjectPriorityLinkModel> listPriorities = new List<ProjectPriorityLinkModel>();


        public frmProjectPriority()
        {
            InitializeComponent();
        }

        private void frmProjectPriority_Load(object sender, EventArgs e)
        {
            //if (projectID == 0)
            //{
            //    btnSave.Visible = false;
            //}
            //else
            //{
            //    btnSave.Visible = true;
            //}
            LoadData();

        }

        private void LoadProjectPriority()
        {
            if (projectID != 0)
            {
                var listProjectPrioLink = SQLHelper<ProjectPriorityLinkModel>.FindByAttribute("ProjectID", projectID);
                if (listProjectPrioLink.Count == 0) return;
                foreach (var item in listProjectPrioLink)
                {
                    int id = item.ProjectPriorityID;
                    TreeListNode node = TrListData.FindNodeByFieldValue("ID", id);
                    if (node != null)
                    {
                        node.Checked = true;
                    }
                }

            }
        }

        private void LoadData()
        {
            List<ProjectPriorityModel> listProjectPriority = SQLHelper<ProjectPriorityModel>.FindAll();
            TrListData.DataSource = listProjectPriority;
            TrListData.ExpandAll();
            LoadProjectPriority();
            txtTotalScore.Text = getTotalPriority().ToString();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listSelectedNode = TrListData.GetAllCheckedNodes();
            frmProjectPriorityDetail frm = new frmProjectPriorityDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                foreach (var node in listSelectedNode)
                {
                    node.Checked = true;
                }
                TrListData.ExpandAll();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            listSelectedNode = TrListData.GetAllCheckedNodes();

            int id = TextUtils.ToInt(TrListData.GetFocusedRowCellValue(colID));
            ProjectPriorityModel model = SQLHelper<ProjectPriorityModel>.FindByID(id);
            frmProjectPriorityDetail frm = new frmProjectPriorityDetail();
            frm.model = model;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                foreach (var node in listSelectedNode)
                {
                    node.Checked = true;
                }
                TrListData.ExpandAll();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {



            // Creates a new instance of the XtraMessageBoxArgs class.
            //XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            //args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel, DialogResult.Retry };
            //args.Caption = "Xóa bản ghi";
            //args.Text = $"Bạn muốn xóa các thành phần thuộc [{priorityCode}] hay chỉ xóa [{priorityCode}] ?";
            //args.Showing += showMessageBoxDelete;  
            //if (parentID == 0)
            //{
            //    XtraMessageBox.Show(args);
            //}


            //if (MessageBox.Show($"Bạn có thực sự muốn xóa [{priorityCode}] không?", "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //{
            //    ProjectPriorityBO.Instance.Delete(id);
            //    LoadData();

            //}

        }



        //private void showMessageBoxDelete(object sender, XtraMessageShowingArgs e)
        //{
        //    string priorityCode = TextUtils.ToString(TrListData.GetFocusedRowCellValue(colCode));
        //    e.Buttons[DialogResult.OK].Text = "Xóa tất cả thành phần";
        //    e.Buttons[DialogResult.OK].Width = 150;

        //    e.Buttons[DialogResult.Cancel].Text = "Cancel";
        //    e.Buttons[DialogResult.Retry].Text = $"Chỉ xóa [{priorityCode}]";
        //}
        private void btnDeleteOnly_Click(object sender, EventArgs e)
        {
            listSelectedNode = TrListData.GetAllCheckedNodes();
            int id = TextUtils.ToInt(TrListData.GetFocusedRowCellValue(colID));
            string priorityCode = TextUtils.ToString(TrListData.GetFocusedRowCellValue(colCode));
            if (MessageBox.Show($"Bạn có thực sự muốn xóa [{priorityCode}] không?", "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ProjectPriorityBO.Instance.Delete(id);
                LoadData();
                foreach (var node in listSelectedNode)
                {
                    node.Checked = true;
                }
                TrListData.ExpandAll();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            listSelectedNode = TrListData.GetAllCheckedNodes();
            int id = TextUtils.ToInt(TrListData.GetFocusedRowCellValue(colID));
            string priorityCode = TextUtils.ToString(TrListData.GetFocusedRowCellValue(colCode));
            List<ProjectPriorityModel> listPrio = SQLHelper<ProjectPriorityModel>.FindByAttribute("ParentID", id);
            string strDelete = "";
            foreach (var item in listPrio)
            {
                strDelete += $"Delete from ProjectPriority where id = {item.ID};";
            }


            if (MessageBox.Show($"Bạn có thực sự muốn xóa tất cả bản ghi thuộc [{priorityCode}] không?", "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                strDelete += $"Delete from ProjectPriority where id = {id};";
                TextUtils.ExcuteSQL(strDelete);
                LoadData();
                foreach (var node in listSelectedNode)
                {
                    node.Checked = true;
                }
                TrListData.ExpandAll();
            }
        }

        private void TrListData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            int parentID = TextUtils.ToInt(TrListData.GetFocusedRowCellValue(colParentID));
            if (parentID != 0)
            {
                btnDeleteAll.Enabled = false;
            }
            else
            {
                btnDeleteAll.Enabled = true;
            }
        }
        public decimal getTotalPriority()
        {
            decimal totalPriority = 0;
            foreach (var node in TrListData.GetAllCheckedNodes())
            {
                totalPriority += TextUtils.ToDecimal(node[colPriority]);
            }
            return totalPriority;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (getTotalPriority() > 5)
            {
                MessageBox.Show("Tổng số điểm ưu tiên không được vượt quá 5!", "Thông báo");
                return;
            }

            var selectedNodes = TrListData.GetAllCheckedNodes();
            DialogResult dialog = MessageBox.Show($"Bạn có thực sự muốn lưu thay đổi không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (TreeListNode node in selectedNodes)
                {
                    ProjectPriorityLinkModel priority = new ProjectPriorityLinkModel();
                    priority.ProjectPriorityID = TextUtils.ToInt(TrListData.GetRowCellValue(node, colID));

                    listPriorities.Add(priority);
                }

                this.DialogResult = DialogResult.OK;
            }

            // if (MessageBox.Show($"Bạn có thực sự muốn lưu thay đổi không?", "Lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            // {
            //     return;
            // }

            // if (getTotalPriority() > 5)
            // {
            //     MessageBox.Show("Tổng số điểm ưu tiên không được vượt quá 5!", "Thông báo");
            //     return;

            // }
            // var listProjectPriority = SQLHelper<ProjectPriorityLinkModel>.FindByAttribute("ProjectID", projectID);

            // //list ProjectPriorityID before save
            // List<int> listOldID = new List<int>();
            // foreach (var item in listProjectPriority)
            // {
            //     listOldID.Add(item.ProjectPriorityID);
            // }

            // //list ProjectPriorityID has selected in treelist
            // List<int> listNewID = new List<int>();
            // var selectedNodes = TrListData.GetAllCheckedNodes();
            // foreach (var item in selectedNodes)
            // {
            //     listNewID.Add(TextUtils.ToInt(item[colID]));
            // }

            // string strDelete = "";
            // string strInsert = "";
            // var project = SQLHelper<ProjectModel>.FindByID(projectID);
            //// if (project == null) return;
            // //project.Priotity = getTotalPriority();
            // ProjectBO.Instance.Update(project);

            // //foreach(var node in TrListData.GetNodeList())
            // //{
            // //    int nodeID = TextUtils.ToInt(node[colID]);
            // //    int indexCheck = selectedNodes.IndexOf(node);
            // //    if(indexCheck > 0)
            // //    {

            // //    }
            // //}

            // //Delete old Object has not selected in tree list
            // IEnumerable<int> listDeleteID = listOldID.Except(listNewID);
            // strDelete = $"DELETE FROM ProjectPriorityLink WHERE ProjectID = {projectID} AND ProjectPriorityID IN ({string.Join(",", listDeleteID)})";
            // if(listDeleteID.Count() > 0)
            // {
            //     TextUtils.ExcuteSQL(strDelete);
            // }

            // //Insert new object has selected
            // IEnumerable<int> listInsertID = listNewID.Except(listOldID);
            // foreach (var i in listInsertID)
            // {
            //     strInsert += $"INSERT INTO ProjectPriorityLink (ProjectID, ProjectPriorityID) Values ({projectID}, {i});";
            // }
            // if(listInsertID.Count() > 0)
            // {
            //     TextUtils.ExcuteSQL(strInsert);
            // }
            // this.DialogResult = DialogResult.OK;
        }

        private void TrListData_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            txtTotalScore.Text = getTotalPriority().ToString();
            var currentNode = e.Node as TreeListNode;
            int id = TextUtils.ToInt(currentNode[colID]);
            if (TextUtils.ToInt(currentNode[colParentID]) != 0) return;
            foreach (var node in TrListData.GetNodeList())
            {
                if (TextUtils.ToInt(node[colParentID]) == id)
                {
                    if (currentNode.CheckState == CheckState.Unchecked)
                    {
                        node.Checked = false;
                    }
                    else
                    {
                        node.Checked = true;
                    }
                }
            }
            txtTotalScore.Text = getTotalPriority().ToString();
        }

        private void TrListData_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            if (e.Node.CheckState == CheckState.Checked)
            {
                e.Appearance.BackColor = Color.LightSteelBlue;
            }
        }

        private void TrListData_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colRate)
            {
                decimal rate = TextUtils.ToDecimal(e.Value) * 100;
                e.DisplayText = $"{rate}%";
            }
        }

        //private void TrListData_SelectionChanged(object sender, EventArgs e)
        //{
        //    var currentNode = TrListData.FocusedNode;
        //    if(currentNode.IsSelected == true)
        //    {
        //        currentNode.CheckState = CheckState.Checked ;
        //    }
        //    //if (TextUtils.ToInt(currentNode[colParentID]) != 0) return;
        //    //var childList = currentNode.TreeList.Nodes.Where(p => p.ParentNode == currentNode);
        //    //if (currentNode.IsSelected == true)
        //    //{
        //    //    foreach (var node in childList)
        //    //    {
        //    //        node.Checked = true;
        //    //    }
        //    //}

        //}
    }
}