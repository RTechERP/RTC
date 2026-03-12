using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

namespace BMS

{
    public partial class frmCurrency : _Forms
    {
        public frmCurrency()
        {
            InitializeComponent();
            
        }

        private void frmCurrency_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            //load data to gridview
            //DataTable dt = TextUtils.LoadDataFromSP(StoreProcedure.spGetAllCurrency, "Currency", new string[] { }, new object[] { });

            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            grdCurrency.DataSource = list;
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCurrencyDetails frm = new frmCurrencyDetails();
            
            //var dialogResult = frmDetail.ShowDialog();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
            //else if (dialogResult == DialogResult.Cancel)
            //{
            //    loadData();
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvCurrency.GetFocusedRowCellValue(colID));
            string currencyCode = TextUtils.ToString(grvCurrency.GetFocusedRowCellValue(colCode));

            if (MessageBox.Show($"Bạn có thực sự muốn xóa [{currencyCode}] không?", "Xóa bản ghi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                CurrencyBO.Instance.Delete(id);
                grvCurrency.DeleteSelectedRows();

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvCurrency.GetFocusedRowCellValue(colID));
            var focusedRowHandle = grvCurrency.FocusedRowHandle;
            CurrencyModel model = (CurrencyModel)CurrencyBO.Instance.FindByPK(id);
            frmCurrencyDetails frm = new frmCurrencyDetails();
            frm.currencyModel = model;
            //var dialogResult = frmCurrencyDetail.ShowDialog();

            if (frm.ShowDialog() == DialogResult.OK )
            {
                loadData();
                grvCurrency.FocusedRowHandle = focusedRowHandle;
            }
            //else if(dialogResult == DialogResult.Cancel)
            //{
            //    loadData();
            //    grvCurrency.FocusedRowHandle = focusedRowHandle;
            //}
        }

        
    }
}