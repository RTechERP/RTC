using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEmployeeBussinessVehicle : _Forms
    {
        DataTable dataVihicle = new DataTable();
        //public int bussinessID = 0;
        public object bussiness = new object();
        public object vehicleInfo = new object();
        public frmEmployeeBussinessVehicle()
        {
            InitializeComponent();
        }

        private void frmEmployeeBussinessVehicle_Load(object sender, EventArgs e)
        {
            LoadEmployeeVehicle();
            loadData();
        }

        void LoadEmployeeVehicle()
        {
            List<EmployeeVehicleBussinessModel> listVehicle = SQLHelper<EmployeeVehicleBussinessModel>.FindAll();
            listVehicle.Add(new EmployeeVehicleBussinessModel()
            {
                VehicleName = "Phương tiện khác"
            });
            cboVehicle.ValueMember = "ID";
            cboVehicle.DisplayMember = "VehicleName";
            cboVehicle.DataSource = listVehicle;
        }

        void loadData()
        {
            var type = bussiness.GetType();
            txtFullName.Text = TextUtils.ToString(type.GetProperty("FullName").GetValue(bussiness));
            txtTypeBussiness.Text = TextUtils.ToString(type.GetProperty("TypeBussiness").GetValue(bussiness));
            txtLocation.Text = TextUtils.ToString(type.GetProperty("Location").GetValue(bussiness));
            dtpDayBussiness.Value = TextUtils.ToDate5(type.GetProperty("DayBussiness").GetValue(bussiness));
            int bussinessID = TextUtils.ToInt(type.GetProperty("ID").GetValue(bussiness));

            dataVihicle.Columns.Add("ImageBillVehicle", typeof(Byte[]));
            dataVihicle = TextUtils.LoadDataFromSP("spGetBussinessVehicle", "A", new string[] { "@IDBussiness" }, new object[] { bussinessID });

            grdVeihcle.DataSource = dataVihicle;
            //for (int i = 0; i < dataVihicle.Rows.Count; i++)
            //{
            //    string imageUrl = "http://113.190.234.64:8083/api/Upload/BillVehicle/" + dataVihicle.Rows[i]["BillImage"].ToString(); /*row["BillImage"].ToString();*/

                
            //    try
            //    {
            //        var request = WebRequest.Create($"http://113.190.234.64:8083/api/Upload/BillVehicle/{dataVihicle.Rows[i]["BillImage"].ToString()}");
            //        var response = request.GetResponse();
            //        var stream = response.GetResponseStream();
            //        grvVeihcle.SetRowCellValue(i, "ImageBillVehicle", Image.FromStream(stream));

            //        // Tải xuống hình ảnh từ URL
            //        //byte[] imageData = DownloadImage(imageUrl);
            //        //if (imageData != null)
            //        //{
            //        //    grvVeihcle.SetRowCellValue(i, "ImageBillVehicle", imageData);

            //        //}

            //    }
            //    catch (Exception ex)
            //    {
            //        // Xử lý lỗi nếu có
            //        MessageBox.Show($"Failed to download image: {ex.Message}");
            //    }
            //}
        }


        private byte[] DownloadImage(string imageUrl)
        {
            byte[] imageData;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(imageUrl).Result;
            imageData = response.Content.ReadAsByteArrayAsync().Result;
            return imageData;
        }

        private void frmEmployeeBussinessVehicle_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cboVehicle_EditValueChanged(object sender, EventArgs e)
        {
            //LookUpEdit lookUpEdit = (LookUpEdit)sender;
            //int idVehicle = TextUtils.ToInt(lookUpEdit.EditValue);
            //if (idVehicle > 0)
            //{
            //    EmployeeVehicleBussinessModel vehicle = (EmployeeVehicleBussinessModel)cboVehicle.GetDataSourceRowByKeyValue(idVehicle);
            //    grvVeihcle.SetFocusedRowCellValue(colVehicleName, vehicle.VehicleName);
            //    grvVeihcle.SetFocusedRowCellValue(colCostVihicle, vehicle.Cost);
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        bool CheckValidate()
        {
            for (int i = 0; i < grvVeihcle.RowCount; i++)
            {
                int vehicleType = TextUtils.ToInt(grvVeihcle.GetRowCellValue(i, colEmployeeVehicleBussinessID));
                string vehicleName = TextUtils.ToString(grvVeihcle.GetRowCellValue(i, colVehicleName)).Trim();
                if (vehicleType < 0)
                {
                    MessageBox.Show(string.Format("Vui lòng nhập Loại phương tiện! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (vehicleType == 0 && string.IsNullOrEmpty(vehicleName))
                {
                    MessageBox.Show(string.Format("Vui lòng nhập Tên phương tiện! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        bool SaveData()
        {
            grvVeihcle.CloseEditor();
            dataVihicle.AcceptChanges();
            if (!CheckValidate())
            {
                return false;
            }
            foreach (DataRow row in dataVihicle.Rows)
            {
                EmployeeBussinessVehicleModel vehicle = new EmployeeBussinessVehicleModel();
                int id = TextUtils.ToInt(row["ID"]);
                if (id > 0)
                {
                    vehicle = SQLHelper<EmployeeBussinessVehicleModel>.FindByID(id);
                }

                vehicle.EmployeeBussinesID = TextUtils.ToInt(bussiness.GetType().GetProperty("ID").GetValue(bussiness));
                vehicle.EmployeeVehicleBussinessID = TextUtils.ToInt(row["EmployeeVehicleBussinessID"]);
                EmployeeVehicleBussinessModel vehicleType = (EmployeeVehicleBussinessModel)cboVehicle.GetDataSourceRowByKeyValue(vehicle.EmployeeVehicleBussinessID);
                vehicle.VehicleName = TextUtils.ToString(row["VehicleName"]);
                vehicle.Cost = 0;
                if (vehicleType != null)
                {
                    if (vehicle.EmployeeVehicleBussinessID == 1 || vehicle.EmployeeVehicleBussinessID == 2)
                    {
                        vehicle.Cost = vehicleType.Cost;
                    }
                    else
                    {
                        vehicle.Cost = TextUtils.ToDecimal(row["Cost"]);
                    }

                    vehicle.VehicleName = vehicle.EmployeeVehicleBussinessID != 0 ? vehicleType.VehicleName : TextUtils.ToString(row["VehicleName"]);
                }
                vehicle.Note = TextUtils.ToString(row["Note"]);
                if (vehicle.ID > 0)
                {
                    EmployeeBussinessVehicleBO.Instance.Update(vehicle);
                }
                else
                {
                    EmployeeBussinessVehicleBO.Instance.Insert(vehicle);
                }
            }

            var listVehicleName = dataVihicle.AsEnumerable().Select(x => x["VehicleName"]).ToList();
            vehicleInfo = new
            {
                TotalCost = dataVihicle.AsEnumerable().Sum(x => x.Field<decimal>("Cost")),
                VehicleName = string.Join(",", listVehicleName)
            };
            

            return true;
        }

        private void frmEmployeeBussinessVehicle_FormClosing(object sender, FormClosingEventArgs e)
        {
            grvVeihcle.FocusedRowHandle = -1;
            DataTable dataChange = dataVihicle.GetChanges();

            if (dataChange != null)
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (SaveData())
                    {
                        e.Cancel = false;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }
                else if (dialog == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void frmEmployeeBussinessVehicle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                grvVeihcle.FocusedRowHandle = -1;
                DataTable dataChange = dataVihicle.GetChanges();

                if (dataChange != null)
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        btnSave_Click(null, null);
                    }
                    else if (dialog == DialogResult.No)
                    {
                        this.DialogResult = DialogResult.OK;
                    }

                }
            }
        }

        private void grvVeihcle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colEmployeeVehicleBussinessID)
            {
                grvVeihcle.FocusedRowHandle = -1;
                int idVehicle = TextUtils.ToInt(grvVeihcle.GetFocusedRowCellValue(colEmployeeVehicleBussinessID));
                if (idVehicle > 0)
                {
                    EmployeeVehicleBussinessModel vehicle = (EmployeeVehicleBussinessModel)cboVehicle.GetDataSourceRowByKeyValue(idVehicle);
                    grvVeihcle.SetFocusedRowCellValue(colVehicleName, vehicle.VehicleName);
                    grvVeihcle.SetFocusedRowCellValue(colCostVihicle, vehicle.Cost);
                }
                else
                {
                    grvVeihcle.SetFocusedRowCellValue(colVehicleName, "");
                    grvVeihcle.SetFocusedRowCellValue(colCostVihicle, 0);
                }
            }
        }

        private void grvVeihcle_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GridView view = sender as GridView;
            string fileName = TextUtils.ToString(view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "BillImage"));
            if (e.Column == colBillImageDS && e.IsGetData)
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    var request = WebRequest.Create("http://113.190.234.64:8083/api/Upload/BillVehicle/" + fileName);
                    var response = request.GetResponse();
                    var stream = response.GetResponseStream();

                    e.Value = Image.FromStream(stream);
                }
            }
        }
    }
}
