using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.UIAutomation;
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
    public partial class frmKPIPositionTypeDetail : _Forms
    {
        public KPIPositionTypeModel kPIPositionTypeModel = new KPIPositionTypeModel();
        public frmKPIPositionTypeDetail()
        {
            InitializeComponent();
        }

        private void frmKPIPositionTypeDetail_Load(object sender, EventArgs e)
        {
            LoadProjectType();

            loadKPIPositionTypeDetail();
        }

        private void loadKPIPositionTypeDetail()
        {
            txtTypeCode.Text = kPIPositionTypeModel.TypeCode;
            txtTypeName.Text = kPIPositionTypeModel.TypeName;
            //numericQuaterValue.Value = kPIPositionTypeModel.QuaterValue == 0? (DateTime.Now.Month - 1) / 3 + 1 : kPIPositionTypeModel.QuaterValue;
            numericQuaterValue.Value = TextUtils.ToDecimal(kPIPositionTypeModel.QuaterValue);
            //txtSTT.Value = kPIPositionTypeModel.STT == 0 ? getMaxSTT() + 1 : kPIPositionTypeModel.STT;
            txtSTT.Value = TextUtils.ToDecimal(kPIPositionTypeModel.STT);
            //numericYearValue.Value = kPIPositionTypeModel.YearValue == 0 ? DateTime.Now.Year : kPIPositionTypeModel.YearValue;
            numericYearValue.Value = TextUtils.ToDecimal(kPIPositionTypeModel.YearValue);
            cbxProjectType.SelectedValue = kPIPositionTypeModel.ProjectTypeID == null ? 1 : kPIPositionTypeModel.ProjectTypeID;
        }
        private int getMaxSTT()
        {
           var list = SQLHelper<KPIPositionTypeModel>.FindAll().OrderByDescending(d => d.STT)
                .FirstOrDefault();
            if((list.STT <= 0))
            {
                return 1;
            }
            return TextUtils.ToInt(list.STT);
        }

        bool SaveData()
        {
            //if (!Validate()) return false;
            kPIPositionTypeModel.TypeCode = txtTypeCode.Text;
            kPIPositionTypeModel.TypeName = txtTypeName.Text;
            kPIPositionTypeModel.QuaterValue = TextUtils.ToInt(numericQuaterValue.Value);
            kPIPositionTypeModel.STT = TextUtils.ToInt(txtSTT.Value);
            kPIPositionTypeModel.YearValue = TextUtils.ToInt(numericYearValue.Value);
            kPIPositionTypeModel.IsDeleted = false;
            kPIPositionTypeModel.ProjectTypeID = TextUtils.ToInt(cbxProjectType.SelectedValue);
            if (kPIPositionTypeModel.ID > 0)
            {
                //KPIPositionTypeBO.Instance.Update(kPIPositionTypeModel);
                SQLHelper<KPIPositionTypeModel>.Update(kPIPositionTypeModel);
            }
            else
            {
                //KPIPositionTypeBO.Instance.Insert(kPIPositionTypeModel);
                SQLHelper<KPIPositionTypeModel>.Insert(kPIPositionTypeModel);
            }

            return true;
        }

        void LoadProjectType()
        {
            List<ProjectTypeModel> listProjectType = SQLHelper<ProjectTypeModel>.FindAll().OrderBy(x => x.ID).ToList();

            cbxProjectType.DataSource = listProjectType;
            cbxProjectType.DisplayMember = "ProjectTypeName";
            cbxProjectType.ValueMember = "ID";
            cbxProjectType.SelectedValue = kPIPositionTypeModel.ProjectTypeID == null ? 1 : kPIPositionTypeModel.ProjectTypeID;

        }
        private void btnSaveNew_Click_1(object sender, EventArgs e)
        {
            if (SaveData())
            {
                kPIPositionTypeModel = new KPIPositionTypeModel();
                loadKPIPositionTypeDetail();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
