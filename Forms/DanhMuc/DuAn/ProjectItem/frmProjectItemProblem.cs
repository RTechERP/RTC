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
    public partial class frmProjectItemProblem : _Forms
    {
        public ProjectItemModel projectItem = new ProjectItemModel();
        public int isUpdateProblem = 0;

        public frmProjectItemProblem()
        {
            InitializeComponent();
        }

        private void frmProjetcItemProblem_Load(object sender, EventArgs e)
        {
            this.Text += projectItem.Code;
            loadData();

            if (!isPermission())
            {
                btnCloseAndSave.Enabled = btnSave.Enabled = true;
            }
        }

        bool isPermission()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectEmployeePermisstion", "A",
                                                        new string[] { "@ProjectID", "@EmployeeID" },
                                                        new object[] { projectItem.ProjectID, Global.EmployeeID });

            int valueRow = TextUtils.ToInt(dt.Rows[0]["RowNumber"]);
            if (valueRow > 0)
            {
                return true;
            }
            else if (projectItem.UserID == Global.UserID)
            {
                return true;
            }
            else if (TextUtils.ToString(projectItem.CreatedBy).Trim() == Global.LoginName.Trim() && !string.IsNullOrWhiteSpace(projectItem.CreatedBy))
            {
                return true;
            }
            else if (Global.IsAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void loadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectItemProblem", "A", new string[] { "@ProjectItemID" }, new object[] { projectItem.ID });
            grdData.DataSource = dt;
        }

        bool saveData()
        {
            if (string.IsNullOrEmpty(txtContentProblem.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Nội dung!", "Thông báo");
                return false;
            }

            ProjectItemProblemModel model = new ProjectItemProblemModel();
            model.ProjectItemID = projectItem.ID;
            model.ContentProblem = txtContentProblem.Text.Trim();

            model.ID = (int)ProjectItemProblemBO.Instance.Insert(model);
            if (model.ID > 0)
            {
                isUpdateProblem = 1;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                loadData();
                txtContentProblem.Clear();
                txtContentProblem.Focus();
            }
        }

        private void btnCloseAndSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                //contentProblem = txtContentProblem.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmProjetcItemProblem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //contentProblem = txtContentProblem.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void frmProjetcItemProblem_FormClosed(object sender, FormClosedEventArgs e)
        {
            //contentProblem = txtContentProblem.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }


        public string SetContent()
        {
            string contentProblem = "";
            List<ProjectItemProblemModel> listProblem = SQLHelper<ProjectItemProblemModel>.FindByAttribute("ProjectItemID", projectItem.ID)
                                                                                          .OrderByDescending(x => x.CreatedDate)
                                                                                          .ToList();
            foreach (var item in listProblem)
            {
                contentProblem += item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm") + ": " + item.ContentProblem + "\n";
            }

            return contentProblem;
        }
    }
}
