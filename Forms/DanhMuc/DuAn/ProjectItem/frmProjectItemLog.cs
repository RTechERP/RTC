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
    public partial class frmProjectItemLog : _Forms
    {
        public int projectItemID = 0;
        public frmProjectItemLog()
        {
            InitializeComponent();
        }

        private void frmProjectItemLog_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectItemLog", "A", new string[] { "@ProjectItemID" }, new object[] { projectItemID });
            grdData.DataSource = dt;
        }

        private void frmProjectItemLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
