using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BMS;
using BMS.Model;
using Newtonsoft.Json;
using BaseBusiness.DTO;
using System.Net.Http;

namespace Forms.Technical.KPI
{
	public partial class frmChooseKPIEmployee:_Forms
	{
        public bool IsChooseMulti = true;
        public List<int> LstID = new List<int>();
        public int UserTeamID = 0;
        public Action<Task> saveEvent;
        public int KPIEmployeeTeamID = 0;
        HttpClient client = new HttpClient();

        //string _host = "https://localhost:44365/api";
        //string _url = "http://10.20.29.65:8088/rerpapi/api";
        string _url = Global.HostKPITeam;
        public frmChooseKPIEmployee()
		{
            InitializeComponent();
		}

        private async void frmChooseKPIEmployee_Load(object sender, EventArgs e)
        {
            await loadDepartment();
            await loadEmployee();
            ckbCode.CheckedChanged += CkbCode_CheckedChanged;
        }
        private void CkbCode_CheckedChanged(object sender, EventArgs e)
        {
            //if (e.Column != colIsSelection) return;
            grvDetail.CloseEditor();
            bool isSelection = TextUtils.ToBoolean(grvDetail.GetFocusedRowCellValue(colIsSelection));
            int ID = TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(colID));
            if (isSelection)
            {
                if (!LstID.Contains(ID))
                    LstID.Add(ID);
            }
            else
            {
                LstID.Remove(ID);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
             if (LstID == null || LstID.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                //ndnhat 29/05/2025
                var payload = LstID.Select(id => new
                {
                    ID = 0,
                    KPIEmployeeTeamID = KPIEmployeeTeamID,
                    EmployeeID = id,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Global.AppUserName
                }).ToList();
                string urlpost = $"{_url}/KPIEmployeeTeamLink/savedata";
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(urlpost, content);

                if (response.IsSuccessStatusCode)
                {
                    saveEvent.Invoke(Task.CompletedTask);
                    this.Close();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi server ({response.StatusCode}):\n{error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối tới máy chủ:\n{ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            await loadEmployee();
        }
        async Task loadEmployee()
        {
            int dID = TextUtils.ToInt(cboDepartment.EditValue);

            try
            {
                //var response = await client.GetStringAsync($"{_url}/KPIEmployeeTeam/getemployeeinteam?departmentID={dID}&kpiEmployeeTeamID={KPIEmployeeTeamID}");
                var response = await client.GetStringAsync($"{_url}/kpiemployeeteam/get-employee-in-team?departmentID={dID}&kpiEmployeeTeamID={KPIEmployeeTeamID}"); //TN.Binh update  14/10/25
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<EmployeeDTO>>>(response);
                if (apiResponse != null && apiResponse.status == 1)
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Code", typeof(string));
                    dt.Columns.Add("FullName", typeof(string));
                    dt.Columns.Add("DepartmentName", typeof(string));
                    dt.Columns.Add("IsSelect", typeof(bool));
                    foreach (var item in apiResponse.data)
                    {
                        dt.Rows.Add(item.ID, item.Code, item.FullName, item.DepartmentName, false);
                    }
                    grdDetail.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n {ex.ToString()}","Thông báo");
            }
        }
        async Task loadDepartment()
        {
            try
            {
                //var response = await client.GetStringAsync($"{_url}/Department/getall");
                var response = await client.GetStringAsync($"{_url}/department/get-all"); //TN.Binh update  14/10/25
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<DepartmentModel>>>(response);
                if (apiResponse != null && apiResponse.status == 1)
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Name", typeof(string));
                    foreach (var item in apiResponse.data)
                    {
                        dt.Rows.Add(item.ID, item.Name);
                    }
                    DataRow row = dt.NewRow();
                    row["ID"] = 0;
                    row["Name"] = "--Tất cả các phòng--";
                    dt.Rows.InsertAt(row, 0);
                    cboDepartment.Properties.DataSource = dt;
                    cboDepartment.Properties.DisplayMember = "Name";
                    cboDepartment.Properties.ValueMember = "ID";
                    cboDepartment.EditValue = Global.DepartmentID < 0 ? 0 : Global.DepartmentID;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu.");
                }
            }
            catch (Exception)
            {
            }
        }

        private void frmChooseKPIEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            LstID.Clear();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvDetail.RowCount; i++)
            {
                grvDetail.SetRowCellValue(i, colIsSelection, true);

                int id = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colID));
                if (!LstID.Contains(id))
                {
                    LstID.Add(id);
                }
            }
        }

        private void bntCancelSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvDetail.RowCount; i++)
            {
                grvDetail.SetRowCellValue(i, colIsSelection, false);

                int id = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colID));
                LstID.Remove(id);
            }
        }
    }
}