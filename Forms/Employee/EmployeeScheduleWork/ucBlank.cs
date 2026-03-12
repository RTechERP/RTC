using DevExpress.XtraEditors;
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
    public partial class ucBlank : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBlank()
        {
            InitializeComponent();
        }

        private void ucBlank_Load(object sender, EventArgs e)
        {

        }

        public void setText(string day)
        {
            lbDayDisabled.Text = day;
        }
    }
}
