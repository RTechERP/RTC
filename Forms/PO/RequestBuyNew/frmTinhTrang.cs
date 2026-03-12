using BMS.Business;
using BMS.Model;
using System;
using System.Collections;
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
    public partial class frmTinhTrang : _Forms
    {
        ArrayList isDelete1 = new ArrayList();
        ArrayList isDelete2 = new ArrayList();
        public frmTinhTrang()
        {
            InitializeComponent();
        }

        private void frmTinhTrang_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            DataTable dt1 = TextUtils.Select("Select * from RequestBuyRTCTTDH");

            grdTTDH.DataSource = dt1;
        }


        
        private void btnSave_Click(object sender, EventArgs e)
        {
            grvTTDH.FocusedRowHandle = -1;
            //Delete in Database
            if (isDelete1.Count > 0)
            {
                RequestBuyRTCTTDHBO.Instance.Delete(isDelete1);
            }
            
            //Save RequestBuyRTCTTDH 

            for (int i = 0; i < grvTTDH.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvTTDH.GetRowCellValue(i,colID));
                RequestBuyRTCTTDHModel model1 = new RequestBuyRTCTTDHModel();
                model1.Code = TextUtils.ToString(grvTTDH.GetRowCellValue(i,colCode));
                model1.Name = TextUtils.ToString(grvTTDH.GetRowCellValue(i,colName));

                if(id>0)
                {
                    TextUtils.ExcuteSQL($"Update RequestBuyRTCTTDH Set Code=N'{ model1.Code }',Name=N'{ model1.Name }'  where ID={id}");
                }
                else
                {
                    RequestBuyRTCTTDHBO.Instance.Insert(model1);
                }

            }
            this.DialogResult = DialogResult.OK;

        }


        private void grvTTDH_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colDelete)
            {
                int id = TextUtils.ToInt(grvTTDH.GetFocusedRowCellValue(colID));
                string Code = TextUtils.ToString(grvTTDH.GetFocusedRowCellValue(colCode));
                if (Code == "") return;
                if (MessageBox.Show($"Bạn có chắc muốn xoá tình trạng đơn hàng có mã {Code} không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (id > 0)
                    {
                        isDelete1.Add(id);//Xoá trên grid
                    }
                    grvTTDH.DeleteSelectedRows();
                }
            }
        }

        private void grvTTDH_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string code = TextUtils.ToString(grvTTDH.GetRowCellValue(e.RowHandle,colCode));
            for (int i = 0; i < grvTTDH.RowCount; i++)
            {
                if(i!= e.RowHandle)
                {
                    string coden = TextUtils.ToString(grvTTDH.GetRowCellValue(i,colCode));
                    if (code == coden)
                    {
                        grvTTDH.SetColumnError(colCode, "Mã này đã tồn tại");
                    } 
                        
                }    
            }
        }

       
    }
}
