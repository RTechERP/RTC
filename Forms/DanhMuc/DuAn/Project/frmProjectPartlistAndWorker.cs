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
using DevExpress.Utils;
namespace Forms.DanhMuc.DuAn.Project
{
    public partial class frmProjectPartlistAndWorker : _Forms
    {
        public ProjectModel project;
        public frmProjectPartlistAndWorker()
        {
            InitializeComponent();
        }

        private void frmProjectPartlistAndWorker_Load(object sender, EventArgs e)
        {
            var partlist = new frmProjectPartList_New(false);
            var worker = new frmProjectWorker_New2();
            partlist.project = worker.project = project;
            TextUtils.OpenChildForm(partlist, this);
            TextUtils.OpenChildForm(worker, this);
        }
    }
}
