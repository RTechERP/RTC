using BaseBusiness.DTO;
using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmSpeDetailUserTeamSale : _Forms
    {
        DataTable dt = new DataTable();
        public List<UserTeamSaleModel> ListTeamSale = new List<UserTeamSaleModel>();
        List<int> ListDelete = new List<int>();
        List<int> ListChange = new List<int>();

        public frmSpeDetailUserTeamSale()
        {
            InitializeComponent();
        }

        private void frmSpeDetailUserTeamSale_Load(object sender, EventArgs e)
        {
            LoadUserTeamSale();
        }

        private void LoadUserTeamSale()
        {
            //Bên view có cột dùng checkbox nên cast 1 trường bit để tick
            //dt = SQLHelper<UserTeamSaleModel>.GetTableData("SELECT *,CAST(0 as BIT) as IsCheck FROM dbo.UserTeamSale WHERE IsDeleted = 0");
            dt =  TextUtils.LoadDataFromSP("spGetUserTeamSale","KhoiTable" ,new string[] { "@IsDeleted" }, new object[] { 0});
            grdData.DataSource = dt;
            if (ListTeamSale.Count > 0)
            {
                for (int i = 0; i < grvData.DataRowCount; i++)
                {
                    int UserteamId = TextUtils.ToInt(grvData.GetRowCellValue(i, colId));
                    grvData.SetRowCellValue(i, colIsCheck, ListTeamSale.Any(p => p.ID == UserteamId));
                }
            }
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);
                if (info.Column != null && info.Column == colAdd && info.HitTest == GridHitTest.Column)
                {
                    int curRowHandle = grvData.RowCount - 1;
                    int curSTT = TextUtils.ToInt(grvData.GetRowCellValue(curRowHandle, colSTT));
                    grvData.FocusedRowHandle = -1;
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();
                    dtrow["STT"] = curSTT + 1;
                    dt.Rows.Add(dtrow);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colId));
            int STT = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colSTT));
            if (MessageBox.Show(String.Format("Bạn có chắc chắn muốn xóa khách hàng thứ [{0}] không?", STT), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteRow(grvData.FocusedRowHandle);
                if (ID > 0)
                {
                    ListDelete.Add(ID);
                }
            }
        }

        private void grvData_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colId));
            if (ID > 0)
            {
                ListChange.Add(ID);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                Save();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Save()
        {
            DelUserTeamSale();
            ListTeamSale = new List<UserTeamSaleModel>();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colId));
                int STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheck));
                string Name = TextUtils.ToString(grvData.GetRowCellValue(i, colName));

                UserTeamSaleModel model = SQLHelper<UserTeamSaleModel>.FindByID(ID) ?? new UserTeamSaleModel();
                model.STT = STT;
                model.Name = Name;
                if (ID > 0)
                {
                    bool isChange = ListChange.Any(p => p == ID);
                    if (isChange)
                    {
                        SQLHelper<UserTeamSaleModel>.Update(model);
                    }
                }
                else
                {
                    model.IsDeleted = false;
                    model.ID = SQLHelper<UserTeamSaleModel>.Insert(model).ID;
                }
                if (isCheck) ListTeamSale.Add(model);
            }
        }
        private bool CheckValidate()
        {
            grvData.FocusedRowHandle = -1;
            int rowCount = grvData.RowCount;
            if (rowCount <= 0) return false;

            for (int i = 0; i < rowCount; i++)
            {
                int STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                if (STT <= 0)
                {
                    MessageBox.Show("[STT] Không được bỏ trống!", "Thông báo");
                    return false;
                }
                string UserTeamSale = TextUtils.ToString(grvData.GetRowCellValue(i, colName));
                if (String.IsNullOrWhiteSpace(UserTeamSale))
                {
                    MessageBox.Show($"Vui lòng nhập [TeamSale] thứ {STT}!", "Thông báo");
                    return false;
                }
            }
            return true;
        }
        private void DelUserTeamSale()
        {
            for (int i = 0; i < ListDelete.Count; i++)
            {
                UserTeamSaleModel model = SQLHelper<UserTeamSaleModel>.FindByID(ListDelete[i]);
                if (model.ID <= 0) continue;
                model.IsDeleted = true;
                SQLHelper<UserTeamSaleModel>.Update(model);
            }
        }
    }
}
