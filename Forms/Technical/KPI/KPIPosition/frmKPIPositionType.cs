using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace Forms.Technical.KPI
{
    public partial class frmKPIPositionType : _Forms
    {
        public frmKPIPositionType()
        {
            InitializeComponent();
        }


        private void KPIPositionType_Load(object sender, EventArgs e)
        {
            loadKPIPositionType();
        }
        void loadKPIPositionType()
        {
            //List<KPIPositionTypeModel> list = SQLHelper<KPIPositionTypeModel>.FindByAttribute("IsDelete", 0).OrderBy(p => p.STT).ToList();
            //List<KPIPositionTypeModel> list = SQLHelper<KPIPositionTypeModel>.FindAll().Where(x => x.IsDeleted != true).OrderBy(p => p.STT).ToList();
            DataTable list = TextUtils.LoadDataFromSP("spGetKPIPositionTypeAndProjectType", "A",
            new string[] {} , new object[] {});
            gridControl1.DataSource = list; // Use gridControl1, not gridView1


        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmKPIPositionTypeDetail frm = new frmKPIPositionTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadKPIPositionType();
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            // Lấy GridView hiện tại
            GridView view = gridView1;

            // Lấy hàng đang được focus
            int rowHandle = view.FocusedRowHandle;

            // Kiểm tra hợp lệ
            if (rowHandle >= 0)
            {
                // 🔹 Cách 1: Lấy trực tiếp giá trị từ cột ID (ví dụ "TypeCode" là ID)
                object idValue = view.GetRowCellValue(rowHandle, "ID");
                if (idValue != null)
                {
                    int id = int.Parse(idValue.ToString());
                    var model = SQLHelper<KPIPositionTypeModel>.FindAll().FirstOrDefault(x => x.ID == id);
                    frmKPIPositionTypeDetail frm = new frmKPIPositionTypeDetail();
                    frm.kPIPositionTypeModel = model;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        loadKPIPositionType();
                    }
                }

                // 🔹 Cách 2: Nếu bạn có cột ID thật sự (ví dụ "ID") thì đổi tên cột:
                // object idValue = view.GetRowCellValue(rowHandle, "ID");
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Lấy GridView hiện tại
            GridView view = gridView1;

            // Lấy hàng đang được focus
            int rowHandle = view.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                // 🔹 Cách 1: Lấy trực tiếp giá trị từ cột ID (ví dụ "TypeCode" là ID)
                object idValue = view.GetRowCellValue(rowHandle, "ID");
                if (idValue != null)
                {
                    int id = int.Parse(idValue.ToString());
                    var model = SQLHelper<KPIPositionTypeModel>.FindAll().FirstOrDefault(x => x.ID == id);
                    if (MessageBox.Show(string.Format("Bạn có thực sự muốn  không?"), "Xóa loại", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //VehicleManagementBO.Instance.Delete(id);

                        var myDict = new Dictionary<string, object>()
                            {
                                {"IsDeleted",1 }
                            };

                        SQLHelper<KPIPositionTypeModel>.UpdateFieldsByID(myDict, id);


                        loadKPIPositionType();
                    }
                }
            }
        }
    }
}