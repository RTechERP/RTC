using BMS.Model;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectJoin : _Forms
    {
        DataTable dtProjectTypeTreeFolder;
        public frmProjectJoin()
        {
            InitializeComponent();
        }

        private void frmProjectJoin_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadData();
            
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.EditValue = Global.EmployeeID;
        }

        void LoadData()
        {
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            DateTime dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectByEmployeeID", "A",
                new string[] { "@EmployeeID", "@UserIDPriotity", "@DateStart", "@DateEnd" },
                new object[] { employeeID, Global.UserID, dateStart,dateEnd });
            grdData.DataSource = dt;

            LoadProjectTypeLink();
            LoadProjectItem();

            LoadProjectEmployee();
        }


        void LoadProjectEmployee()
        {
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectEmployeeByEmployeeID", "A",
                new string[] { "@EmployeeID" },
                new object[] { employeeID });
            gridControl1.DataSource = dt;
        }

        void LoadProjectTypeLink()
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectTypeLink", "A", new string[] { "@ProjectID" }, new object[] { id });
            tlProjectTypeMaster.DataSource = dt;
            tlProjectTypeMaster.ExpandAll();

        }

        /// <summary>
        /// Load Hạng mục công việc
        /// </summary>
        void LoadProjectItem()
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) id--;
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectItem", "A", new string[] { "@ProjectID" }, new object[] { id });
            grdProjectItem.DataSource = dt;
        }


        private void CreateDirectoryWithDatatable(DataRow row, string path)
        {
            //Trước tiên kiểm tra nó có phải cấp đầu tiên hay ko
            int id = TextUtils.ToInt(row["ID"]);
            int parentId = TextUtils.ToInt(row["ParentID"]);
            string folderName = TextUtils.ToString(row["FolderName"]);
            if (parentId <= 0)
            {
                // Nó là 1 cha
                string newPath = Path.Combine(path, folderName);
                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);

                row["Path"] = newPath;
                // Lấy toàn bộ các con của nó
                DataRow[] childFolder = row.Table.Select($"ParentID={id}");
                foreach (DataRow childRow in childFolder)
                    CreateDirectoryWithDatatable(childRow, path);
            }
            else
            {
                // Nếu nó là 1 con
                //DataRow[] parentFolder = row.Table.Select($"ID={parentId}");
                //if (parentFolder.Length > 0 && string.IsNullOrEmpty(TextUtils.ToString(parentFolder[0]["Path"])))
                //    CreateDirectoryWithDatatable(parentFolder[0], path);
                //else
                //{
                //    string newPath = Path.Combine(TextUtils.ToString(parentFolder[0]["Path"]), folderName);
                //    if (!Directory.Exists(newPath))
                //        Directory.CreateDirectory(newPath);
                //    row["Path"] = newPath;
                //    // Lấy toàn bộ các con của nó
                //    DataRow[] childFolder = row.Table.Select($"ParentID={id}");
                //    foreach (DataRow childRow in childFolder)
                //        CreateDirectoryWithDatatable(childRow, path);
                //}

                ///-------------------------------------------- phuc add new
                int gp1ID = TextUtils.ToInt(row["GP1ID"]); // lấy ID để get dữ liệu của các GP 
                DataRow[] parentFolder;
                // nếu nó là các SP thì > 0
                if (gp1ID > 0)
                {
                    parentFolder = dtProjectTypeTreeFolder.Select($"ID={parentId}");
                }
                else
                {
                    parentFolder = row.Table.Select($"ID={parentId}");
                }

                if (parentFolder.Length > 0 && string.IsNullOrEmpty(TextUtils.ToString(parentFolder[0]["Path"])))
                {
                    CreateDirectoryWithDatatable(parentFolder[0], path);
                }
                else
                {
                    string newPath = Path.Combine(TextUtils.ToString(parentFolder[0]["Path"]), folderName);
                    if (!Directory.Exists(newPath))
                        Directory.CreateDirectory(newPath);
                    row["Path"] = newPath;
                    // nếu nó là SP động
                    if (gp1ID > 0)
                    {
                        // Lấy toàn bộ các con của nó
                        DataRow[] childFolder = dtProjectTypeTreeFolder.Select($"ParentID={gp1ID}");
                        foreach (DataRow childRow in childFolder)
                        {
                            int GP1IDnew = TextUtils.ToInt(childRow["ID"]);

                            DataRow dtNewRow = dtProjectTypeTreeFolder.NewRow();
                            dtNewRow["ID"] = TextUtils.ToInt(idMaxData());
                            dtNewRow["ParentID"] = id;
                            dtNewRow["FolderName"] = TextUtils.ToString(childRow["FolderName"]);
                            dtNewRow["Path"] = TextUtils.ToString(childRow["Path"]);
                            dtNewRow["GP1ID"] = GP1IDnew;
                            dtProjectTypeTreeFolder.Rows.Add(dtNewRow);

                            CreateDirectoryWithDatatable(dtNewRow, path);
                        }
                    }
                    else
                    {
                        // Lấy toàn bộ các con của nó
                        DataRow[] childFolder = row.Table.Select($"ParentID={id}");
                        foreach (DataRow childRow in childFolder)
                            CreateDirectoryWithDatatable(childRow, path);
                    }
                }
                //-----------------------------------end
            }
        }
        private void CreateDirectoryWithDatatable(DataTable dataTable, string path)
        {
            foreach (DataRow row in dataTable.Rows)
                CreateDirectoryWithDatatable(row, path);
        }

        #region PhucLH update 28/05/2024
        private void addGPProjectTypeTreeFolder(DataTable dt)
        {
            DataTable dtNew = dt.Clone();
            // kiểm tra nếu có các SP1.. là con của folder THIETKE.Co
            bool parentIdExists = dt.AsEnumerable().Any(row => TextUtils.ToInt(row["ParentID"]) == 3);
            if (parentIdExists)
            {
                // lấy danh sách yêu cầu giải pháp GP...
                List<string> lsSp = new List<string>();
                int idMaster = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDMaster));
                DataTable dtProjectRequest = TextUtils.LoadDataFromSP("spGetProjectRequest", "A", new string[] { "@ProjectID" }, new object[] { idMaster });
                if (dtProjectRequest.Rows.Count > 0)
                {
                    foreach (DataRow rowProjectRequest in dtProjectRequest.Rows)
                    {
                        int idProjectRequest = TextUtils.ToInt(rowProjectRequest["ID"]);
                        DataTable dtProjectSolution = TextUtils.LoadDataFromSP("spGetProjectSolution", "B", new string[] { "@ProjectRequestID" }, new object[] { idProjectRequest });
                        if (dtProjectSolution.Rows.Count > 0)
                        {
                            foreach (DataRow rowProjectSolution in dtProjectSolution.Rows)
                            {
                                string codeSolution = TextUtils.ToString(rowProjectSolution["CodeSolution"]).Trim();
                                if (!string.IsNullOrEmpty(codeSolution) && !lsSp.Contains(codeSolution))
                                {
                                    // Thêm mã giải pháp vào danh sách nếu chưa tồn tại
                                    lsSp.Add(codeSolution);

                                    DataRow newRow1 = dtNew.NewRow();
                                    newRow1["ID"] = TextUtils.ToInt(idMaxData());
                                    newRow1["ParentID"] = 3; // ParentID của GP1 trong mẫu
                                    newRow1["FolderName"] = codeSolution;
                                    newRow1["GP1ID"] = 40; // id của GP1 trong mẫu
                                    dtNew.Rows.Add(newRow1);

                                    DataRow newRow2 = dtProjectTypeTreeFolder.NewRow();
                                    newRow2["ID"] = newRow1["ID"];
                                    newRow2["ParentID"] = newRow1["ParentID"];
                                    newRow2["FolderName"] = newRow1["FolderName"];
                                    newRow2["GP1ID"] = newRow1["GP1ID"];
                                    dtProjectTypeTreeFolder.Rows.Add(newRow2);
                                }
                                //
                            }
                        }
                    }
                }



                // add dữ liệu vào dt và check không được trùng
                foreach (DataRow itemRow in dtNew.Rows)
                {
                    string folderName = TextUtils.ToString(itemRow["FolderName"]);
                    if (!dt.AsEnumerable().Any(x => x.Field<string>("FolderName") == folderName))
                    {
                        DataRow rowNew = dt.NewRow();
                        rowNew["ID"] = itemRow["ID"];
                        rowNew["ParentID"] = itemRow["ParentID"];
                        rowNew["FolderName"] = folderName;
                        rowNew["GP1ID"] = itemRow["GP1ID"];
                        dt.Rows.Add(rowNew);
                    }
                }
                dtProjectTypeTreeFolder.Clear();
            }
            dtProjectTypeTreeFolder = dt.Copy();
        }

        private int idMaxData()
        {
            if (dtProjectTypeTreeFolder.Rows.Count == 0)
                return 1;

            int maxId = dtProjectTypeTreeFolder.AsEnumerable().Max(row => TextUtils.ToInt(row["ID"]));
            return maxId + 1;
        }
        #endregion

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnTreeFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var dateCreated = grvData.GetFocusedRowCellValue(colCreatedDate);
                string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProjectCode));
                int year = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colCreatedDate)).Year;

                string pathLocation = Global.PathLocationProject;
                if (Global.IsOnline)
                {
                    //pathLocation = @"\\rtctechnologydata.ddns.net\DUAN\Projects\";
                    pathLocation = @"\\113.190.234.64\DUAN\Projects\";
                }
                //pathLocation = @"\\rtctechnologydata.ddns.net\DUAN\Projects\";

                string path = $@"{pathLocation}\{year}\{code}";


                List<int> listProjectTypeID = new List<int>();
                List<string> listProjectTypeName = new List<string>();

                var nodeSelected = tlProjectTypeMaster.GetNodeList().Where(x => TextUtils.ToBoolean(x.GetValue(colSeleted)) == true);

                foreach (TreeListNode node in nodeSelected)
                {
                    int projectTypeID;
                    string projectTypeName = "";
                    if (TextUtils.ToBoolean(node.GetValue(colSeleted)))
                    {
                        projectTypeID = TextUtils.ToInt(node.GetValue(colProjectTypeID));
                        projectTypeName = TextUtils.ToString(node.GetValue(colProjectTypeMaster));

                        listProjectTypeID.Add(projectTypeID);
                        listProjectTypeName.Add(projectTypeName);
                    }

                }

                if (listProjectTypeID.Count > 0)
                {
                    for (int i = 0; i < listProjectTypeID.Count; i++)
                    {

                        DataTable dt = TextUtils.LoadDataFromSP("sp_GetProjectTypeTreeFolder", "A", new string[] { "@ProjectTypeID" }, new object[] { listProjectTypeID[i] });
                        if (dt.Rows.Count <= 0)
                            continue;
                        dt.Columns.Add("Path", typeof(string));

                        //---------------------------------------------------------------- phuc add new
                        dt.Columns.Add("GP1ID", typeof(int));
                        dtProjectTypeTreeFolder = dt.Copy();
                        addGPProjectTypeTreeFolder(dt);
                        //------------------------------------------------------------------------end

                        CreateDirectoryWithDatatable(dt, path);


                    }
                }

                try
                {
                    //Danh sách thư mục con của TaiLieuChung
                    List<ProjectTreeFolderModel> list = SQLHelper<ProjectTreeFolderModel>.FindByAttribute("ParentID", 20);
                    Directory.CreateDirectory($@"{path}\DanhMucVatTu");
                    //Directory.CreateDirectory($@"{path}\TaiLieuChung");

                    foreach (var item in list)
                    {
                        Directory.CreateDirectory($@"{path}\TaiLieuChung\{item.FolderName}");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Process.Start(path);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Chưa đăng nhập tài khoản server");
                MessageBox.Show(ex.Message);
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadProjectTypeLink();
            LoadProjectItem();
        }
    }
}
