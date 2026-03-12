using BaseBusiness.DTO;
using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using DevExpress.DataProcessing;
using DocumentFormat.OpenXml.Bibliography;
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using BMS.Utils;

namespace BMS
{
    public partial class frmKPIEmployeeTeamDetail : _Forms
    {
        public Action<Task> saveEvent;
        public KPIEmployeeTeamModel model = new KPIEmployeeTeamModel();
        //HttpClient client = new HttpClient();

        string _host = Global.HostKPITeam;
        string _accessToken = "";
        public frmKPIEmployeeTeamDetail()
        {
            InitializeComponent();
        }

        private async void frmKPIEmployeeTeamDetail_Load(object sender, EventArgs e)
        {
            txtYear.Value = DateTime.Now.Year;
            txtQuarter.Value = DateTime.Now.Month / 4 + 1;

            await getToken();

            LoadDepartmentGroup();
            await loadDepartment();
            await loadLeader();
            loadKPIEmployeeTeamDetail();
        }

        async Task<bool> getToken()
        {
            try
            {
                HttpClient _client = new HttpClient();
                string url = $"{_host}/home/login";

                var body = new
                {
                    LoginName = Global.LoginName,
                    PasswordHash = MD5.DecryptPassword(Global.AppPassword),
                };

                var content = new StringContent(
                    JsonConvert.SerializeObject(body),
                    Encoding.UTF8,
                    "application/json");

                var response = await _client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                //var result = JsonConvert.DeserializeObject<LoginResponse>(json);

                //if (string.IsNullOrEmpty(result?.access_token))
                //    return false;

                var jObject = JObject.Parse(json);
                _accessToken = jObject["access_token"]?.ToString();

                //_accessToken = result.access_token;
                //_client.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", _accessToken);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void loadKPIEmployeeTeamDetail()
        {

            if (model.ID > 0)
            {
                cboDepartment.EditValue = model.DepartmentID;
                cboNhom.EditValue = model.ParentID;
                txtName.Text = model.Name;
                cboLeader.EditValue = model.LeaderID;
                txtYear.Value = TextUtils.ToDecimal(model.YearValue);
                txtQuarter.Value = TextUtils.ToDecimal(model.QuarterValue);
            }
        }

        async void LoadDepartmentGroup()
        {
            try
            {
                int departmentID = TextUtils.ToInt(model.DepartmentID);
                int year = TextUtils.ToInt(model.YearValue);
                int quarter = TextUtils.ToInt(model.QuarterValue);
                HttpClient client = new HttpClient();
                //var response = await client.GetStringAsync($"{_host}/KPIEmployeeTeam/getall?yearValue={year}&quarterValue={quarter}&departmentId={departmentID}");
                var response = await client.GetStringAsync($"{_host}/kpiemployeeteam/getall?yearValue={year}&quarterValue={quarter}&departmentId={departmentID}");

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<KPIEmployeeTeamDTO>>>(response);

                if (apiResponse != null && apiResponse.status == 1 && apiResponse.data != null && apiResponse.data.Count > 0)
                {
                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("ID", typeof(int));
                    //dt.Columns.Add("Name", typeof(string));
                    //dt.Columns.Add("ParentID", typeof(int));
                    //dt.Columns.Add("LeaderName", typeof(string));
                    //foreach (var item in apiResponse.data)
                    //{
                    //    dt.Rows.Add(item.ID, item.Name, item.ParentID, item.LeaderName);
                    //}

                    //DataRow row = dt.NewRow();
                    //row["ID"] = 0;
                    //row["LeaderName"] = "--Tất cả các phòng--";
                    //dt.Rows.InsertAt(row, 0);

                    // Set the DataSource for the TreeList
                    treeListLookUpEdit1TreeList.ExpandAll();
                    cboNhom.Properties.DisplayMember = "Name";
                    cboNhom.Properties.ValueMember = "ID";
                    cboNhom.Properties.DataSource = apiResponse.data;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        async Task loadDepartment()
        {
            HttpClient client = new HttpClient();
            //var response = await client.GetStringAsync($"{_host}/Department/getall");
            var response = await client.GetStringAsync($"{_host}/department/get-all"); //TN.Binh update  14/10/25

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<DepartmentModel>>>(response);
            if (apiResponse != null && apiResponse.status == 1)
            {

                //DataTable dt = new DataTable();
                //dt.Columns.Add("ID", typeof(int));
                //dt.Columns.Add("Name", typeof(string));
                //foreach (var item in apiResponse.data)
                //{
                //    dt.Rows.Add(item.ID, item.Name);
                //}
                cboDepartment.Properties.DataSource = apiResponse.data;
                cboDepartment.Properties.ValueMember = "ID";
                cboDepartment.Properties.DisplayMember = "Name";
                cboDepartment.EditValue = Global.EmployeeID;

                if (model.ID > 0) cboDepartment.EditValue = model.DepartmentID;

            }
        }

        /// <summary>
        /// Load Leader
        /// </summary>
        async Task loadLeader()
        {
            try
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["status"] = "0";
                query["departmentid"] = "0";
                //query["keyword"] = "khoi";
                string querystring = query.ToString();

                //string url = $"{_host}/employee/employees?{querystring}";
                string url = $"{_host}/employee?{querystring}"; //TN.Binh update  14/10/25
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var response = await client.GetStringAsync(url);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<EmployeeDTO>>>(response);
                if (apiResponse != null && apiResponse.status == 1)
                {

                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("ID", typeof(int));
                    //dt.Columns.Add("Code", typeof(string));
                    //dt.Columns.Add("FullName", typeof(string));
                    //foreach (var item in apiResponse.data)
                    //{
                    //    dt.Rows.Add(item.ID, item.Code, item.FullName);
                    //}
                    cboLeader.Properties.DisplayMember = "FullName";
                    cboLeader.Properties.ValueMember = "ID";
                    cboLeader.Properties.DataSource = apiResponse.data;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        bool CheckValidate()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Team!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (TextUtils.ToInt(cboDepartment.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Phòng ban!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (TextUtils.ToInt(cboLeader.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Leader!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //if (TextUtils.ToInt(cboNhom.EditValue) <= 0)
            //{
            //    MessageBox.Show("Nhóm cha không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            return true;
        }

        private async Task<bool> SaveDataAsync()
        {
            try
            {
                if (!CheckValidate()) return false;

                //string url = $"{_host}/KPIEmployeeTeam/savedata";
                string url = $"{_host}/kpiemployeeteam/savedata"; //TN.Binh update  14/10/25
                model.Name = txtName.Text.Trim();
                model.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
                model.LeaderID = TextUtils.ToInt(cboLeader.EditValue);
                model.ParentID = TextUtils.ToInt(cboNhom.EditValue);
                model.YearValue = TextUtils.ToInt(txtYear.Value);
                model.QuarterValue = TextUtils.ToInt(txtQuarter.Value);
                model.IsDeleted = false;
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(url, content);
                var responsebody = response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(responsebody.Result);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(apiResponse.message, "Thông báo");
                    return false;
                }
                saveEvent.Invoke(Task.CompletedTask);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (await SaveDataAsync())
            {
                this.Close();
            }
            //else
            //{
            //    MessageBox.Show("lỗi tải dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private async void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (await SaveDataAsync())
            {
                //clearData();
                await loadDepartment();
                await loadLeader();
                model = new KPIEmployeeTeamModel();
                loadKPIEmployeeTeamDetail();
            }
            else
            {
                MessageBox.Show("lỗi tải dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void clearData()
        {
            cboDepartment.EditValue = null;
            cboLeader.EditValue = null;
            cboNhom.EditValue = null;
            txtName.Text = string.Empty;
            DateTime time = DateTime.Now;
            txtYear.Value = TextUtils.ToDecimal(time.Year);
            txtQuarter.Value = TextUtils.ToDecimal(time.Month / 4 + 1);
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadLeader();
        }
    }
}
