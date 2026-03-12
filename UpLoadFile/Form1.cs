using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }
        string folderUpdate = Path.Combine(Application.StartupPath, "Update.txt");
        private void button1_Click(object sender, EventArgs e)
        {
           DocUtilss.InitFTPQLSX();
            string[] lst = Directory.GetFiles(folderUpdate);
            Array.Reverse(lst);
            try
            {
                DocUtilss.UploadFile(lst[0], @"UpdateVersion\RTC");
                MessageBox.Show("Update thành công");
            }
            catch
            {
                MessageBox.Show("Update thất bại");
            }
        }

		private void Form1_Load(object sender, EventArgs e)
		{
            if (File.Exists(folderUpdate))
            {
                string valueFit = File.ReadAllText(folderUpdate);
            }
        }
	}
}
