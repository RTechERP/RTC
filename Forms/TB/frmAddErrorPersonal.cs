using BMS.Business;
using BMS.Model;
using BMS.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmAddErrorPersonal : Form
    {
        public frmAddErrorPersonal()
        {
            InitializeComponent();
        }
        public int _ProductHistoryID;
        HistoryErrorModel errorModel;
        bool IsUpdate;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtText.Text=="")
            {
                MessageBox.Show("Note không được để trống (-_0)");
                return;
            }
            errorModel.DescriptionError = txtText.Text;
          
            try
            {
                if (IsUpdate)
                {
                    errorModel.UpdateDate = DateTime.Now;
                    HistoryErrorBO.Instance.Update(errorModel);
                    MessageBox.Show("Update thành công");
                }
                else
                {
                    errorModel.ProductHistoryID = _ProductHistoryID;
                    errorModel.CreatedDate = DateTime.Now;
                    errorModel.UpdateDate = errorModel.CreatedDate;

                    HistoryErrorBO.Instance.Insert(errorModel);
                    MessageBox.Show("Thêm thành công");
                }

                this.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi thêm");
            }
        }

        private void frmAddErrorPersonal_Load(object sender, EventArgs e)
        {
            errorModel = new HistoryErrorModel();
            Expression exp = new Expression("ProductHistoryID", _ProductHistoryID);
            ArrayList list =  HistoryErrorBO.Instance.FindByExpression(exp);
            if(list.Count>0)
            {
                errorModel = list[0] as HistoryErrorModel;
                txtText.Text = errorModel.DescriptionError;
                IsUpdate = true;

            }    

        }
    }
}
