using BMS.Business;
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
    public partial class frmPosition1 : _Forms
    {
        public frmPosition1()
        {
            InitializeComponent();
        }

        private void frmPosition1_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            DataTable dt = TextUtils.Select("Select * From SaleUserType");
            grdData.DataSource = dt;
            DataTable dtg = TextUtils.Select("Select * From GroupSales");
            grdGroup.DataSource = dtg;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i,colID));
                SaleUserTypeModel model = new SaleUserTypeModel();
                if(ID>0)
                model = (SaleUserTypeModel)SaleUserTypeBO.Instance.FindByPK(ID);
                model.SaleUserTypeName = TextUtils.ToString(grvData.GetRowCellValue(i,colSaleUserTypeName));
                model.SaleUserTypeCode = TextUtils.ToString(grvData.GetRowCellValue(i,colSaleUserTypeCode));
                if(ID>0)
                    SaleUserTypeBO.Instance.Update(model);
                else
                    SaleUserTypeBO.Instance.Insert(model);
            }
            for (int i = 0; i < lstDelete.Count; i++)
            {
                SaleUserTypeBO.Instance.Delete(lstDelete[i]);
            }


            for (int i = 0; i < grvGroup.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvGroup.GetRowCellValue(i, colIDgroup));
                GroupSalesModel model = new GroupSalesModel();
                if (ID > 0)
                    model = (GroupSalesModel)GroupSalesBO.Instance.FindByPK(ID);
                model.GroupSalesName = TextUtils.ToString(grvGroup.GetRowCellValue(i, colGroupSalesName));
                model.GroupSalesCode = TextUtils.ToString(grvGroup.GetRowCellValue(i, colGroupSalesCode));
                if (ID > 0)
                    GroupSalesBO.Instance.Update(model);
                else
                    GroupSalesBO.Instance.Insert(model);
            }
            for (int i = 0; i < lstDeleteG.Count; i++)
            {
                GroupSalesBO.Instance.Delete(lstDeleteG[i]);
            }
            MessageBox.Show("Đã lưu thành công! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        List<int> lstDelete = new List<int>();
        List<int> lstDeleteG = new List<int>();
        private void btnDelete_Click(object sender, EventArgs e)
        {
            
          
         
        }
        /// <summary>
        /// Xóa Group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int IDG = TextUtils.ToInt(grvGroup.GetFocusedRowCellValue(colIDgroup));
            lstDeleteG.Add(IDG);
            grvGroup.DeleteSelectedRows();
        }
        /// <summary>
        /// Xóa Chức vụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xóaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            lstDelete.Add(ID);
            grvData.DeleteSelectedRows();
        }
    }
}
