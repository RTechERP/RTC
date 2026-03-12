using BMS;
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
    public partial class frmAccoutingContractTypeMaster : _Forms
    {
        public frmAccoutingContractTypeMaster()
        {
            InitializeComponent();
            LoadData();
        }


        public void LoadData()
        {
            string keyWord = edtSearch.Text.Trim() ?? "";
            List<AccountingContractTypeModel> list = SQLHelper<AccountingContractTypeModel>.FindAll()
                                                                                            .Where(x=> string.IsNullOrEmpty(keyWord) 
                                                                                                        || x.TypeCode.Contains(keyWord) 
                                                                                                        || x.TypeName.Contains(keyWord))
                                                                                            .OrderBy(x => x.STT).ToList();
            grData.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAccountingContractTypeDetail form = new frmAccountingContractTypeDetail();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int contractTypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(ID));
            frmAccountingContractTypeDetail form = new frmAccountingContractTypeDetail();
            form.contractType = SQLHelper<AccountingContractTypeModel>.FindByID(contractTypeID);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }
    }
}
