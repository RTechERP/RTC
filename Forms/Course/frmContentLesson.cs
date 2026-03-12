using BMS;
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
    public partial class frmContentLesson : _Forms
    {

        public string HtmlText
        {
            get { return this.redValue.HtmlText; }
            set { this.redValue.HtmlText = value; }
        }

        public frmContentLesson()
        {
            InitializeComponent();
        }
       
        private void btnCloseAndSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

       
    }
}
