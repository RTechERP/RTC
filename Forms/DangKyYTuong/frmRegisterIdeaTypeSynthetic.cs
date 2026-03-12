using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRegisterIdeaTypeSynthetic : _Forms
    {
        public frmRegisterIdeaTypeSynthetic()
        {
            InitializeComponent();
        }

        private void frmRegisterIdeaTypeSynthetic_Load(object sender, EventArgs e)
        {
            LoadRegisterIdeaType();
        }

        private void LoadRegisterIdeaType()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseCatalog", "A", new string[] { "@CatalogType" }, new object[] { 2});
            
            grdData.DataSource = dt;
        }

        private void frmRegisterIdeaTypeSynthetic_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
