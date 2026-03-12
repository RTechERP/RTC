using BMS;
using BMS.Model;
using BMS.Utils;
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
    public partial class frmKPIPositionDetails : _Forms
    {
        public int kpiSessionID;  //TN.Binh update 03/09/25
         int _depID; //TN.Binh update 03/09/25
        public KPIPositionModel kpiPosition = new KPIPositionModel();
        public frmKPIPositionDetails(int depID)
        {
            InitializeComponent();
            _depID = depID;
        }

        private void frmKPIPositionDetails_Load(object sender, EventArgs e)
        {
            LoadPositionType();
            LoadDetail();
           
        }
        //TN.Binh update 09/09/25
        void LoadTypePosition()
        {
            List<object> list = new List<object>()
            {
                new { ID = 1, Name = "Kỹ thuật, Pro" },
                new { ID = 3, Name = "Senior" },
                new { ID = 4, Name = "Phó phòng" },
                new { ID = 2, Name = "Admin" },
            };
            cboTypePosition.Properties.DataSource = list;
            cboTypePosition.Properties.ValueMember = "ID";
            cboTypePosition.Properties.DisplayMember = "Name";
            cboTypePosition.EditValue = kpiPosition.TypePosition;
        }


        void LoadPositionType()
        {
            //

            kpiSessionID = TextUtils.ToInt(cboKPISession.EditValue);
            List<KPIPositionTypeModel> lst = SQLHelper<KPIPositionTypeModel>.ProcedureToList("spGetPoistionTypeByKPISessionID", new string[] { "@KPISessionID" },
                                                                                                 new object[] { kpiSessionID });
            cboPositionType.Properties.DataSource = lst;
            cboPositionType.Properties.ValueMember = "ID";
            cboPositionType.Properties.DisplayMember = "TypeName";
            
        }

        //TN.Binh update 04/09/25
        private void LoadKPISession()
        {
            Expression ex1 = new Expression("IsDeleted", 0);
            Expression ex2 = new Expression("DepartmentID", _depID);
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByExpression(ex1.And(ex2)).OrderByDescending(p => p.ID).ToList();
            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.ValueMember = "ID";
            cboKPISession.Properties.DisplayMember = "Name";
            cboKPISession.EditValue = kpiSessionID;
        }
        private void LoadDetail()
        {
            txtCode.Text = kpiPosition.PositionCode;
            txtName.Text = kpiPosition.PositionName;
            txtSTT.Value = kpiPosition.STT ?? 0; //TN.Binh update 09/09/25
            cboPositionType.EditValue = kpiPosition.KPIPositionTypeID;
            //TN.Binh update 04/09/25
            LoadKPISession();
            LoadTypePosition();
        }
        private bool SaveData()
        {
            string code = txtCode.Text.Trim();
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập Mã chức vụ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập Tên chức vụ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            //TN.Binh update 04/09/25
            if (cboKPISession.EditValue == null || cboKPISession.EditValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn kỳ đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKPISession.Focus();
                return false;
            }
            if (cboTypePosition.EditValue == null || cboTypePosition.EditValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn loại đánh giá!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTypePosition.Focus();
                return false;
            }
            Expression ex1 = new Expression("PositionCode",code);
            Expression ex2 = new Expression("ID", kpiPosition.ID, "<>");
            Expression ex3 = new Expression("IsDeleted", 0);

            //TN.Binh update 10/09/25
            Expression ex4 = new Expression("TypePosition", TextUtils.ToInt(cboTypePosition.EditValue));
            Expression ex5 = new Expression("KPISessionID", TextUtils.ToInt(cboKPISession.EditValue));
            List<KPIPositionModel> lstDuplicate = SQLHelper<KPIPositionModel>.FindByExpression(ex1.And(ex2.And(ex3.And(ex4.And(ex5)))));
            //end update
            if (lstDuplicate.Count > 0)
            {
                MessageBox.Show($"Mã chức vụ [{code}] đã được sử dụng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            KPIPositionModel model = SQLHelper<KPIPositionModel>.FindByID(kpiPosition.ID);
            model.PositionCode = code;
            model.PositionName = name;
            model.IsDeleted = false;
            model.STT = TextUtils.ToInt(txtSTT.Value);
            model.KPISessionID = TextUtils.ToInt(cboKPISession.EditValue);  //TN.Binh update 04/09/25
            model.TypePosition = TextUtils.ToInt(cboTypePosition.EditValue);  //TN.Binh update 09/09/25
            model.KPIPositionTypeID = TextUtils.ToInt(cboPositionType.EditValue);  //TN.Binh update 09/09/25
            if (model.ID > 0) SQLHelper<KPIPositionModel>.Update(model);
            else SQLHelper<KPIPositionModel>.Insert(model);
            return true;
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData()) this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                kpiPosition = new KPIPositionModel();
                LoadDetail();
            }
        }

        private void frmKPIPositionDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboBillTypeNew_EditValueChanged(object sender, EventArgs e)
        {
            KPIPositionTypeModel data = (KPIPositionTypeModel)cboPositionType.GetSelectedDataRow();
            if (data != null)
            {
                txtCode.Text = data.TypeCode;
                txtName.Text = data.TypeName;
            }
        }

        private void cboKPISession_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmKPIPositionTypeDetail frm = new frmKPIPositionTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadPositionType();
            }
        }

        private void cboKPISession_EditValueChanged_1(object sender, EventArgs e)
        {
            LoadPositionType();

        }
    }
}
