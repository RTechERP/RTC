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
    public partial class frmNote : _Forms
    {
        public delegate void note(string note);
        public note dataNote;
        public frmNote()
        {
            InitializeComponent();
        }

        private void frmNote_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.dataNote?.Invoke(txtNode.Text.Trim());
            this.Close();
        }
    }
}
