using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectField : _Forms
    {
        public frmProjectField()
        {
            InitializeComponent();
        }

        private void frmProjectField_Load(object sender, EventArgs e)
        {
            loadProjectField();
        }

        /// <summary>
        /// Load team
        /// </summary>
        void loadProjectField()
        {
            List<BusinessFieldModel> _lstField = SQLHelper<BusinessFieldModel>.FindAll();
            grdProjectField.DataSource = _lstField;
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadProjectField();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmBusinessField frm = new frmBusinessField();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectField();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvProjectField.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            frmBusinessField frm = new frmBusinessField();
            frm.Text = "LĨNH VỰC DỰ ÁN";
            frm.id = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProjectField();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<int> _lstID = new List<int>();
            foreach (int rowIndex in grvProjectField.GetSelectedRows())
            {
                int ID = TextUtils.ToInt(grvProjectField.GetRowCellValue(rowIndex, colID));
                _lstID.Add(ID);
            }
            if (_lstID.Count == 0) return;

            if (MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SQLHelper<BusinessFieldModel>.ExcuteNonQuerySQL($"DELETE FROM BusinessField WHERE ID IN ({string.Join(", ", _lstID)})");
                loadProjectField();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvProjectField.OptionsPrint.AutoWidth = false;
                grvProjectField.OptionsPrint.ExpandAllDetails = false;
                grvProjectField.OptionsPrint.PrintDetails = true;
                grvProjectField.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvProjectField.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void grvProjectField_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grdProjectField_Click(object sender, EventArgs e)
        {

        }
    }
}
