using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
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
    public partial class frmProjectWorkerSynthetic : _Forms
    {
        public int projectID = 0;
        public frmProjectWorkerSynthetic()
        {
            InitializeComponent();
        }

        private void frmProjectWorkerSynthetic_Load(object sender, EventArgs e)
        {
            loadProject();
            loadWorkerType();
            loadData();
        }
        void loadProject()
        {
            cboProject.Properties.DataSource = SQLHelper<ProjectModel>.FindAll();
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";
            cboProject.EditValue = projectID;
        }
        void loadWorkerType()
        {
            cboProjectWorkerType.Properties.DataSource = SQLHelper<ProjectWorkerTypeModel>.FindAll();
            cboProjectWorkerType.Properties.DisplayMember = "Name";
            cboProjectWorkerType.Properties.ValueMember = "ID";
        }
        void loadData()
        {
            DataTable dt = SQLHelper<ProjectWorkerModel>.LoadDataFromSP("spGetProjectWokerSynthetic", 
                new string[] { "@ProjectID", "@ProjectWorkerTypeID" }, 
                new object[] { TextUtils.ToInt(cboProject.EditValue), TextUtils.ToInt(cboProjectWorkerType.EditValue) });
            grdData.DataSource = dt;
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            grvData.OptionsPrint.AutoWidth = false;
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"TongHopNhanConguDuAn_{cboProject.Text}.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    

                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdData;

                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    compositeLink.ExportToXlsx(saveFileDialog.FileName);
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}