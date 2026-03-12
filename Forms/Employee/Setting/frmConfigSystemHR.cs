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
    public partial class frmConfigSystemHR : _Forms
    {
        string[] tableNames = new string[] { "EmployeeOvertime", "EmployeeBussiness" };
        public frmConfigSystemHR()
        {
            InitializeComponent();
        }

        private void frmConfigSystemHR_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        void LoadData()
        {
            grdData.DataSource = null;
            var data = SQLHelper<ConfigSystemModel>.FindAll().Where(x => tableNames.Contains(x.KeyName)).ToList();
            grdData.DataSource = data;
        }


        bool SaveData()
        {
            try
            {
                grvData.CloseEditor();
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                    if (id <= 0) continue;

                    var myDict = new Dictionary<string, object>()
                    {
                        {"KeyValue2",TextUtils.ToString(grvData.GetRowCellValue(i,colKeyValue2)) },
                        {"UpdatedBy",Global.AppUserName },
                        {"UpdatedDate",DateTime.Now },
                    };

                    SQLHelper<ConfigSystemModel>.UpdateFieldsByID(myDict, id);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                MessageBox.Show("Lưu thành công!","Thông báo");
                LoadData();
            }

        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}
