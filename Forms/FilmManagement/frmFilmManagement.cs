using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmFilmManagement : _Forms
    {
        public frmFilmManagement()
        {
            InitializeComponent();
        }

        private void frmFilmManagement_Load(object sender, EventArgs e)
        {
            loadMaster();
            loadData();
        }
        void loadMaster()
        {
            DataSet dt = TextUtils.LoadDataSetFromSP("spGetFilmManagement"
                , new string[] { "@FilterText", "@PageSize", "@PageNumber" }
                , new object[] { txtKeyword.Text.Trim(), TextUtils.ToInt(txtPageSize.Text.Trim()), TextUtils.ToInt(txtPageNumber.Text.Trim()) });
            grdMaster.DataSource = dt.Tables[0];

            txtTotalPage.Text = TextUtils.ToString(dt.Tables[1].Rows[0]["TotalPage"]);
        }
        void loadData()
        {
            int filmManagementID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetFilmManagementDetail", "A", new string[] { "@FilmManagementID", "@IsAll" }, new object[] { filmManagementID, 0 });
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue())
            frmFIlmManagementDetail frm = new frmFIlmManagementDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMaster();
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            int index = grvMaster.FocusedRowHandle;
            frmFIlmManagementDetail frm = new frmFIlmManagementDetail();
            frm.filmManagementID = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMaster();
                loadData();
                grvMaster.FocusedRowHandle = index;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            //string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            //DialogResult dialog = MessageBox.Show($"Bạn có thực sự muốn xoá phim [{code}] không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (dialog == DialogResult.OK)
            //{
            //    FilmManagementBO.Instance.Delete(id);
            //    FilmManagementDetailBO.Instance.DeleteByAttribute("FilmManagementID", id);
            //    grvMaster.DeleteSelectedRows();
            //}

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            DialogResult dialog = MessageBox.Show($"Bạn có thực sự muốn xoá phim [{code}] không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                string sql = $"UPDATE dbo.FilmManagement SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE ID = {id}" +
                            $"UPDATE dbo.FilmManagementDetail SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE FilmManagementID = {id}";

                //TextUtils.ExcuteSQL($"UPDATE dbo.FilmManagement SET IsDeleted = 1 WHERE ID = {id}");
                //TextUtils.ExcuteSQL($"UPDATE dbo.FilmManagementDetail SET IsDeleted = 1 WHERE FilmManagementID = {id}");
                TextUtils.ExcuteSQL(sql);
                grvMaster.DeleteSelectedRows();
            }
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadData();
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnNhapKhau_Click(object sender, EventArgs e)
        {
            frmInputExcelFilm frm = new frmInputExcelFilm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMaster();
                loadData();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadMaster();
            loadData();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());
            if (pageNumber == 1)
            {
                return;
            }

            txtPageNumber.Text = TextUtils.ToString(pageNumber - 1);
            loadMaster();
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());
            if (pageNumber >= TextUtils.ToInt(txtTotalPage.Text.Trim()))
            {
                return;
            }

            txtPageNumber.Text = TextUtils.ToString(pageNumber + 1);
            loadMaster();
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            txtPageNumber.Text = txtTotalPage.Text.Trim();
            loadMaster();
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadMaster();
            loadData();
        }
    }
}