using BMS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using BMS.Business;
using BaseBusiness.DTO;
using DocumentFormat.OpenXml.Wordprocessing;
using Forms.Technical.KPI;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;


namespace BMS
{
    public partial class frmKPIEmployeeTeam : _Forms
    {
        public KPIEmployeeTeamDTO dTO = new KPIEmployeeTeamDTO();
        public string _host = Global.HostKPITeam;
        //public string _host = "https://localhost:44365/api";
        List<int> lstId = new List<int>();
        public frmKPIEmployeeTeam()
        {
            InitializeComponent();
            DateTime time = DateTime.Now;
            int year = time.Year;
            int quarter = time.Month / 4 + 1;
            txtQuarter.Value = quarter;
            txtYear.Value = year;

        }

        private async void frmKPIEmployeeTeam_Load(object sender, EventArgs e)
        {
            await loadDepartmentGroup();
            await LoadKPIEmployeeTeam();
        }

        
        async Task loadDepartmentGroup()
        {

            try
            {
                var client = new HttpClient();
                var response = await client.GetStringAsync($"{_host}/department/get-all");
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<DepartmentModel>>>(response);
                if (apiResponse != null && apiResponse.status == 1)
                {

                    DataTable dt = new DataTable();
                    //dt.Columns.Add("ID", typeof(int));
                    //dt.Columns.Add("Name", typeof(string));
                    //foreach (var item in apiResponse.data)
                    //{
                    //    dt.Rows.Add(item.ID, item.Name);
                    //}
                    //DataRow row = dt.NewRow();
                    //row["ID"] = 0;
                    //row["Name"] = "--Tất cả các phòng--";
                    //dt.Rows.InsertAt(row, 0);

                    //List<DepartmentModel>
                    cboDepartment.Properties.DataSource = apiResponse.data.OrderBy(x=>x.STT).ToList();
                    cboDepartment.Properties.DisplayMember = "Name";
                    cboDepartment.Properties.ValueMember = "ID";
                    cboDepartment.EditValue = Global.DepartmentID < 0 ? 0 : Global.DepartmentID;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu.");
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        async Task LoadKPIEmployeeTeam()
        {
            try
            {
                int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
                int year = TextUtils.ToInt(txtYear.Value);
                int quarter = TextUtils.ToInt(txtQuarter.Value);

                string url = $"{_host}/KPIEmployeeTeam/getall?yearValue={year}&quarterValue={quarter}&departmentId={departmentID}";

                var client = new HttpClient();
                var response = await client.GetStringAsync(url);

                // Deserialize into your ApiResponse<T>
                var apiResp = JsonConvert
                    .DeserializeObject<ApiResponse<List<KPIEmployeeTeamDTO>>>(response);

                if (apiResp != null && apiResp.status == 1 && apiResp.data != null)
                {
                    var list = apiResp.data;

                    // Build your DataTable from 'list'...
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("LeaderID", typeof(int));
                    dt.Columns.Add("DepartmentID", typeof(int));
                    dt.Columns.Add("ParentID", typeof(int));
                    dt.Columns.Add("LeaderName", typeof(string));
                    dt.Columns.Add("YearValue", typeof(int));
                    dt.Columns.Add("QuarterValue", typeof(int));
                    dt.Columns.Add("DepartmentName", typeof(string));

                    foreach (var item in list)
                        dt.Rows.Add(
                          item.ID,
                          item.Name,
                          item.LeaderID,
                          item.DepartmentID,
                          item.ParentID,
                          item.LeaderName,
                          item.YearValue,
                          item.QuarterValue,
                          item.DepartmentName
                        );

                    // (Optional) Insert your “--Tất cả các phòng--” row here
                    DataRow all = dt.NewRow();
                    all["ID"] = 0;
                    all["Name"] = "--Tất cả các phòng--";
                    dt.Rows.InsertAt(all, 0);

                    tlEmployeeTeam.DataSource = dt;
                    tlEmployeeTeam.ExpandAll();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private async void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            await LoadKPIEmployeeTeam();
        }

        private async void txtQuarter_ValueChanged(object sender, EventArgs e)
        {
            await LoadKPIEmployeeTeam();
        }

        private async void txtYear_ValueChanged(object sender, EventArgs e)
        {
            await LoadKPIEmployeeTeam();
        }
        frmKPIEmployeeTeamDetail frm;
        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (frm == null || frm.IsDisposed)
                {
                    frm = new frmKPIEmployeeTeamDetail();
                    frm.model = new KPIEmployeeTeamModel();
                    frm.model.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
                    frm.model.YearValue = TextUtils.ToInt(txtYear.Value);
                    frm.model.QuarterValue = TextUtils.ToInt(txtQuarter.Value);
                    frm.saveEvent += async (task) => await LoadKPIEmployeeTeam();
                    frm.FormClosed += (s, arg) => frm = null;
                    frm.Show();
                }
                else
                {
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private async void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(tlEmployeeTeam.FocusedNode.GetValue("ID"));
                if (id == 0)
                {
                    MessageBox.Show("Vui lòng chọn 1 team để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (id < 0)
                {
                    //MessageBox.Show("Vui lòng chọn 1 team để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var client = new HttpClient();
                var response = await client.GetStringAsync(_host + $"/KPIEmployeeTeam/getbyid?id={id}");
                var apiRespone = JsonConvert.DeserializeObject<ApiResponse<KPIEmployeeTeamModel>>(response);
                if (apiRespone != null && apiRespone.status == 0)
                {
                    MessageBox.Show(apiRespone.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                KPIEmployeeTeamModel team = apiRespone.data;

                if (frm == null || frm.IsDisposed)
                {
                    frm = new frmKPIEmployeeTeamDetail();
                    frm.model = team;
                    frm.saveEvent += async (task) => await LoadKPIEmployeeTeam();
                    frm.FormClosed += (s, arg) => frm = null;
                    frm.Show();
                }
                else
                {
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private async void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string url = _host + "/KPIEmployeeTeam/savedata";
            int id = TextUtils.ToInt(tlEmployeeTeam.FocusedNode.GetValue("ID"));
            KPIEmployeeTeamModel model = SQLHelper<KPIEmployeeTeamModel>.FindByID(id);
            if (id <= 0)
            {
                MessageBox.Show("Vui lòng chọn 1 team để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;

            var client = new HttpClient();
            var response = await client.GetStringAsync(_host + $"/KPIEmployeeTeam/getbyid?id={id}");
            var apiRespone = JsonConvert.DeserializeObject<ApiResponse<KPIEmployeeTeamModel>>(response);
            if (apiRespone != null && apiRespone.status == 0)
            {
                MessageBox.Show(apiRespone.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            KPIEmployeeTeamModel team = apiRespone.data;
            team.IsDeleted = true;

            var json = JsonConvert.SerializeObject(team);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseDel = await client.PostAsync(url, content);
            var r = responseDel.Content.ReadAsStringAsync();
            var apiResponseDel = JsonConvert.DeserializeObject<ApiResponse<object>>(r.Result);

            if (apiResponseDel.status == 1)
            {
                MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(apiResponseDel.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            await LoadKPIEmployeeTeam();
            await LoadEmployeeTeamLink();
        }
        frmChooseKPIEmployee frmchoose;
        //frmChooseEmployee frmchoose;
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            if (frmchoose == null || frmchoose.IsDisposed)
            {
                int masterID = TextUtils.ToInt(tlEmployeeTeam.GetFocusedRowCellValue("ID"));
                if (masterID <= 0) return;
                frmchoose = new frmChooseKPIEmployee();
                frmchoose.KPIEmployeeTeamID = masterID;
                frmchoose.saveEvent += async (task) => await LoadEmployeeTeamLink();
                frmchoose.Show();
            }
            else
            {
                frmchoose.WindowState = FormWindowState.Normal;
                frmchoose.Activate();
            }

        }
        async Task LoadEmployeeTeamLink()
        {
            try
            {
                grdEmployee.DataSource = null;
                int departmentID = 0;
                if (cboDepartment.EditValue == null) return;
                int userGroupID = TextUtils.ToInt(cboDepartment.EditValue);
                int id = TextUtils.ToInt(tlEmployeeTeam.FocusedNode.GetValue(colTreeID));
                if (id == 0) return;
                if (id < 0) departmentID = TextUtils.ToInt(tlEmployeeTeam.FocusedNode.GetValue(colDepartmentID));
                int yearvalue = TextUtils.ToInt(txtYear.Value);
                int quartervalue = TextUtils.ToInt(txtQuarter.Value);

                string url = $"{_host}/KPIEmployeeTeamLink/getall?KPIEmployeeteamID={id}&DepartmentID={departmentID}&yearValue={yearvalue}&quarterValue={quartervalue}";
                var client = new HttpClient();
                var response = await client.GetStringAsync(url);
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<KPIEmployeeTeamLinkDTO>>>(response);
                if (apiResponse != null && apiResponse.status == 1)
                {
                    List<KPIEmployeeTeamLinkDTO> employeeList = new List<KPIEmployeeTeamLinkDTO>();
                    foreach (KPIEmployeeTeamLinkDTO item in apiResponse.data)
                    {
                        employeeList.Add(item);
                    }
                    grdEmployee.DataSource = employeeList;
                    grvEmployee.ExpandAllGroups();
                }
                else if (apiResponse != null && apiResponse.status == 0)
                {
                    grdEmployee.DataSource = null;
                    return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi{ex.Message} \r\n{ex.ToString()} ", "Thông báo");
            }
        }
        private async void tlEmployeeTeam_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            await LoadEmployeeTeamLink();
        }

        private async void btnDeleteEmp_Click(object sender, EventArgs e)
        {
            //if (grvEmployee.FocusedRowHandle < 0)
            //    return;
            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            int[] arrRowIndex = grvEmployee.GetSelectedRows();
            var employeeDeleted = arrRowIndex
                    .Select(i => TextUtils.ToInt(grvEmployee.GetRowCellValue(i, colGridID)))
                    .ToList();
            if (!employeeDeleted.Any())
            {
                MessageBox.Show("Chưa chọn nhân viên nào để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            

            var payload = employeeDeleted.Select(id => new
            {
                ID = id,
                IsDeleted = true,
                UpdatedDate = DateTime.Now,
                UpdatedBy = Global.AppUserName
            }).ToList();
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //var json = JsonConvert.SerializeObject(employeeDeleted);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var response = await client.PostAsync(_host + "/KPIEmployeeTeamLink/savedata", content);

            var jsonString = response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(jsonString.Result);
            if (apiResponse.status == 1)
            {
                //MessageBox.Show(apiResponse.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadEmployeeTeamLink();
            }
            else
            {
                MessageBox.Show(apiResponse.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tlEmployeeTeam_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            //e.Menu = null;
            if (e.MenuType == DevExpress.XtraTreeList.Menu.TreeListMenuType.Node )
            {
                e.Allow = false;
            }
        }

        private void tlEmployeeTeam_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadKPIEmployeeTeam();
        }

        private void btnCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // kiểm tra trong danh sách form đang mở
            var frm = Application.OpenForms.OfType<frmCopyKPITeam>().FirstOrDefault();

            if (frm != null)
            {
                // nếu đã mở rồi thì đưa nó ra front
                frm.Activate();
                frm.BringToFront();
            }
            else
            {
                // nếu chưa mở thì tạo mới
                frm = new frmCopyKPITeam();
                frm.SaveEvent += async () => await LoadKPIEmployeeTeam();
                frm.Show();
            }
        }
    }
    public class ApiResponse<T>
    {
        public int status { get; set; }
        public T data { get; set; }
        public string message { get; set; }
    }

}
