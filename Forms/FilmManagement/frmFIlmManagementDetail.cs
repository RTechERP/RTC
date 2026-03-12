using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
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
    public partial class frmFIlmManagementDetail : _Forms
    {
        public int filmManagementID = 0;
        private List<int> listIdDelete = new List<int>();

        public frmFIlmManagementDetail()
        {
            InitializeComponent();
        }

        private void frmFIlmManagementDetail_Load(object sender, EventArgs e)
        {
            loadUnit();
            loadData();
        }

        void loadUnit()
        {
            cboUnit.DataSource = SQLHelper<UnitCountModel>.SqlToList("SELECT * FROM dbo.UnitCount");
            cboUnit.ValueMember = "ID";
            cboUnit.DisplayMember = "UnitName";
        }

        void loadData()
        {
            if (filmManagementID > 0)
            {
                FilmManagementModel model = SQLHelper<FilmManagementModel>.SqlToModel($"SELECT * FROM dbo.FilmManagement WHERE ID = {filmManagementID}");
                txtCode.Text = model.Code;
                txtName.Text = model.Name;
                txtSTT.Value = model.STT;
                chkRequestResult.Checked = model.RequestResult;
            }
            else
            {
                FilmManagementModel model = SQLHelper<FilmManagementModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.FilmManagement ORDER BY ID DESC");
                txtSTT.Value = model.STT + 1;
                chkRequestResult.Checked = true;
            }
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetFilmManagementDetail", "A", new string[] { "@FilmManagementID", "@IsAll" }, new object[] { filmManagementID, 0 });
        }

        bool validate()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã phim!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                var exp1 = new Expression("Code", txtCode.Text.Trim());
                var exp2 = new Expression("ID", filmManagementID,"<>");
                var exp3 = new Expression("IsDeleted", 1, "<>");

                //FilmManagementModel model = SQLHelper<FilmManagementModel>.SqlToModel($"SELECT * FROM dbo.FilmManagement WHERE Code = '{txtCode.Text.Trim()}' AND ID <> {filmManagementID}");
                var matchValue = SQLHelper<FilmManagementModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (matchValue.Count > 0)
                {
                    MessageBox.Show($"Mã phim [{txtCode.Text}] đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên phim!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //if (grvData.RowCount <= 0)
            //{
            //    MessageBox.Show("Vui lòng nhập Công việc!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            return true;
        }

        bool save()
        {
            try
            {
                if (!validate()) return false;
                grvData.CloseEditor();
                FilmManagementModel model = new FilmManagementModel();
                if (filmManagementID > 0)
                {
                    model = SQLHelper<FilmManagementModel>.SqlToModel($"SELECT * FROM dbo.FilmManagement WHERE ID = {filmManagementID}");
                }
                model.Code = txtCode.Text.Trim();
                model.Name = txtName.Text.Trim();
                model.STT = TextUtils.ToInt(txtSTT.Value);
                model.RequestResult = chkRequestResult.Checked;
                if (model.ID > 0)
                {
                    FilmManagementBO.Instance.Update(model);

                }
                else
                {
                    model.ID = (int)FilmManagementBO.Instance.Insert(model);
                }

                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int detailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                    FilmManagementDetailModel detail = new FilmManagementDetailModel();
                    if (detailID > 0)
                    {
                        detail = SQLHelper<FilmManagementDetailModel>.SqlToModel($"SELECT * FROM dbo.FilmManagementDetail WHERE ID = {detailID}");
                    }
                    detail.FilmManagementID = model.ID;
                    detail.PerformanceAVG = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPerfomanceAVG));
                    detail.UnitID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnit));
                    detail.WorkContent = TextUtils.ToString(grvData.GetRowCellValue(i, colWorkContent));
                    detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                    if (detail.ID > 0)
                    {
                        FilmManagementDetailBO.Instance.Update(detail);
                    }
                    else
                    {
                        FilmManagementDetailBO.Instance.Insert(detail);
                    }
                }

                if (listIdDelete.Count > 0)
                {
                    string sql = $"UPDATE dbo.FilmManagementDetail SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE ID IN {string.Join(",", listIdDelete)}";
                    TextUtils.ExcuteSQL(sql);
                }
                

                //foreach (int id in listIdDelete)
                //{
                //    FilmManagementDetailBO.Instance.Delete(id);
                //}

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            listIdDelete.Clear();
            this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            txtCode.Text = "";
            txtName.Text = "";
            grdData.DataSource = null;
            filmManagementID = 0;
            loadData();
            listIdDelete.Clear();
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdData, grvData);
            }
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(null, null);

            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            string content = TextUtils.ToString(grvData.GetFocusedRowCellValue(colWorkContent));

            if (id > 0)
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn xóa công việc {content}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (id > 0)
                    {
                        listIdDelete.Add(id);
                    }

                    grvData.DeleteSelectedRows();
                }
            }
            else
            {
                grvData.DeleteSelectedRows();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!toolStripButton1.Enabled)
            {
                return;
            }
            MessageBox.Show("Đã click đc!");
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}