using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
	public partial class FrmAddDocumentFile : _Forms
	{
		public FrmAddDocumentFile()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
            //string filePath = "";
            //string connectionString = "";
            //FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //BinaryReader reader = new BinaryReader(stream);
            //byte[] file = reader.ReadBytes((int)stream.Length);
            //reader.Close();
            //stream.Close();

            //SqlCommand command;
            //SqlConnection connection = new SqlConnection(connectionString);
            //command = new SqlCommand("INSERT INTO FileTable (File) Values(@File)", connection);
            //command.Parameters.Add("@File", SqlDbType.Binary, file.Length).Value = file;
            //connection.Open();
            //command.ExecuteNonQuery();
        }

        private void FrmAddDocumentFile_Load(object sender, EventArgs e)
        {

        }
    }
}
