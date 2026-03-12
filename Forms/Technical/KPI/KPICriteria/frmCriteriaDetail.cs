using System;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using BMS.Business;
using BMS.Model;
using BMS.Utils;

namespace BMS
{
    public partial class frmCriteriaDetail : _Forms
    {
        public KPICriteriaModel _KPICriteria = new KPICriteriaModel();
        DataTable dtC = new DataTable();
        List<KPICriteriaDetailModel> isDeletedID = new List<KPICriteriaDetailModel>();
        public frmCriteriaDetail()
        {
            InitializeComponent();
        }

        private void frmCriteriaDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {

            if (_KPICriteria.ID > 0)
            {
                txtCriteriaCode.Text = _KPICriteria.CriteriaCode;
                txtCriteriaName.Text = _KPICriteria.CriteriaName;
                txtQuarter.Value = TextUtils.ToInt(_KPICriteria.KPICriteriaQuater);
                txtYear.Value = TextUtils.ToInt(_KPICriteria.KPICriteriaYear);
                txtSTT.Value = TextUtils.ToInt(_KPICriteria.STT);
            }
            else
            {
                txtSTT.Value = GetMaxSTT();
            }
            List<KPICriteriaDetailModel> list = SQLHelper<KPICriteriaDetailModel>.FindByAttribute("KPICriteriaID", _KPICriteria.ID);
            grdData.DataSource = list;
        }

        private int GetMaxSTT()
        {
            Expression ex1 = new Expression("KPICriteriaQuater", TextUtils.ToInt(txtQuarter.Value));
            Expression ex2 = new Expression("KPICriteriaYear", TextUtils.ToInt(txtYear.Value));
            Expression ex3 = new Expression("IsDeleted", 0);
            List<KPICriteriaModel> lst = SQLHelper<KPICriteriaModel>.FindByExpression(ex1.And(ex2.And(ex3)));
            //int STT = (lst.Count > 0 ? TextUtils.ToInt(lst.Max(x => x.STT)) : 0) + 1;
            int STT = lst.Count > 0 ? TextUtils.ToInt(lst.Max(x => x.STT)) + 1 : 1;
            return STT;
        }

        
        private bool SaveData()
        {
            if (!ValidateForm()) return false;
            grvData.FocusedRowHandle = -1;

            KPICriteriaModel model = SQLHelper<KPICriteriaModel>.FindByID(_KPICriteria.ID);
            model.CriteriaCode = txtCriteriaCode.Text.Trim();
            model.CriteriaName = txtCriteriaName.Text.Trim();
            model.KPICriteriaQuater = TextUtils.ToInt(txtQuarter.Value);
            model.KPICriteriaYear = TextUtils.ToInt(txtYear.Value);
            model.STT = TextUtils.ToInt(txtSTT.Value);

            if (model.ID > 0)
            {
                SQLHelper<KPICriteriaModel>.Update(model);
            }
            else
            {
                model.ID = SQLHelper<KPICriteriaModel>.Insert(model).ID;
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCriteriaDetailID));
                KPICriteriaDetailModel _KPICriteriaDetail = SQLHelper<KPICriteriaDetailModel>.FindByID(ID);
                /*string a = TextUtils.ToString(grvData.GetRowCellValue(i, colContent));
                if (a == "") continue;*/
                _KPICriteriaDetail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTTAdd));
                _KPICriteriaDetail.KPICriteriaID = model.ID;
                _KPICriteriaDetail.Point = TextUtils.ToInt(grvData.GetRowCellValue(i, colMucDiem));
                _KPICriteriaDetail.PointPercent = TextUtils.ToInt(grvData.GetRowCellValue(i, colMucDiemPhanTram));
                _KPICriteriaDetail.CriteriaContent = TextUtils.ToString(grvData.GetRowCellValue(i, colContent)).Trim();
                if (_KPICriteriaDetail.ID > 0)
                {
                    SQLHelper<KPICriteriaDetailModel>.Update(_KPICriteriaDetail);
                }
                else
                {
                    SQLHelper<KPICriteriaDetailModel>.Insert(_KPICriteriaDetail);
                }
            }

            DeleteAll();
            return true;
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int KPICriteriaID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTieuChiID));
            int Point = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colMucDiem));
            decimal PointPercent = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colMucDiemPhanTram));
            decimal CriteriaContent = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colContent));
        }


        private bool ValidateForm()
        {

            if (txtCriteriaCode.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Mã Tiêu Chí !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (txtCriteriaName.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập điền Tên Tiêu Chí !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(txtQuarter.Value) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Quý !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(txtYear.Value) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Năm !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            Expression ex1 = new Expression("ID", _KPICriteria.ID, "<>");
            Expression ex2 = new Expression("CriteriaCode", txtCriteriaCode.Text.Trim());
            Expression ex3 = new Expression("KPICriteriaQuater", TextUtils.ToInt(txtQuarter.Value));
            Expression ex4 = new Expression("KPICriteriaYear", TextUtils.ToInt(txtYear.Value));
            Expression ex5 = new Expression("IsDeleted", 0);
            KPICriteriaModel modelCheck = SQLHelper<KPICriteriaModel>.FindByExpression(ex1.And(ex2.And(ex3.And(ex4.And(ex5))))).FirstOrDefault();
            /*List<KPICriteriaModel> lst = SQLHelper<KPICriteriaModel>.FindByExpression(ex1.And(ex2.And(ex3.And(ex4.And(ex5)))));
            KPICriteriaModel model =  lst.Last<KPICriteriaModel>();*/
            if (modelCheck != null)
            {
                MessageBox.Show($"Mã tiêu chí [{txtCriteriaCode.Text.Trim()}] đã được sử dụng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void btnSaveDong_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                _KPICriteria = new KPICriteriaModel();
                txtCriteriaName.Clear();
                txtCriteriaCode.Clear();
                List<KPICriteriaDetailModel> list = SQLHelper<KPICriteriaDetailModel>.FindByAttribute("KPICriteriaID", _KPICriteria.ID);
                grdData.DataSource = list;
                //loadData();
            }

        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTTAdd && e.Y < 40)
            {
                List<KPICriteriaDetailModel> list = (List<KPICriteriaDetailModel>)grvData.DataSource;
                KPICriteriaDetailModel model = new KPICriteriaDetailModel();
                int stt = 0;
                int point = 0;
                int pointpercent = 0;
                if (list.Count == 0)
                {
                    stt = 1;
                }
                else
                {
                    stt = TextUtils.ToInt(grvData.GetRowCellValue(grvData.RowCount - 1, colSTTAdd)) + 1;
                    point = stt - 1;
                    pointpercent = point * 20;

                }
                model.STT = stt;
                model.Point = point;
                model.PointPercent = pointpercent;
                list.Add(model);
                grdData.DataSource = list;
                grdData.RefreshDataSource();
                grvData.FocusedRowHandle = grvData.RowCount - 1;
                grvData.FocusedColumn = colContent;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string Stt = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colSTTAdd));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colCriteriaDetailID));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa Tiêu chí [{0}] không?", Stt), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ID > 0)
                {
                    isDeletedID.Add(SQLHelper<KPICriteriaDetailModel>.FindByID(ID));
                }
                grvData.DeleteSelectedRows();
                List<KPICriteriaDetailModel> list = (List<KPICriteriaDetailModel>)grvData.DataSource;
                int stt = 1;
                foreach (KPICriteriaDetailModel item in list)
                {
                    item.STT = stt++;
                }
                grdData.DataSource = list;
                grdData.RefreshDataSource();
            }
        }
        private void DeleteAll()
        {
            if (isDeletedID.Count <= 0) return;
            SQLHelper<KPICriteriaDetailModel>.DeleteListModel(isDeletedID);
            isDeletedID = new List<KPICriteriaDetailModel>();
        }
        private void txtQuarter_ValueChanged(object sender, EventArgs e)
        {
            txtSTT.Value = GetMaxSTT();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            txtSTT.Value = GetMaxSTT();
        }
    }
}
