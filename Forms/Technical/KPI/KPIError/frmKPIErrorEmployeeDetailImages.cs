using BMS.Model;
using BMS.Utils;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmKPIErrorEmployeeDetailImages : _Forms
    {
        public List<KPIErrorEmployeeFileModel> files = new List<KPIErrorEmployeeFileModel>();
        public frmKPIErrorEmployeeDetailImages()
        {
            InitializeComponent();
        }

        private void frmKPIErrorEmployeeDetailImages_Load(object sender, EventArgs e)
        {
            LoadImage();
        }
        void LoadImage()
        {
            foreach (var item in files)
            {
                try
                {
                    KPIErrorEmployeeModel model = SQLHelper<KPIErrorEmployeeModel>.FindByID(item.KPIErrorEmployeeID);
                    if (!model.ErrorDate.HasValue) continue;
                    //string pathPattern = $@"{model.ErrorDate.Value.Year}\T{model.ErrorDate.Value.Month}\N{model.ErrorDate.Value.ToString("dd.MM.yyyy")}"; //LinhTN update 04/11/2024
                    //string url = $"http://113.190.234.64:8083/api/kpi/{pathPattern}";

                    string pathPattern = $@"{model.ErrorDate.Value.Year}\T{model.ErrorDate.Value.Month}\N{model.ErrorDate.Value.ToString("dd.MM.yyyy")}"; //LinhTN update 04/11/2024
                    string url = $"http://113.190.234.64:8083/api/kpi/{pathPattern}/{item.FileName}";

                    //string url = $"{item.OriginPath}\\{item.FileName}";
                    var request = WebRequest.Create(url);
                    var response = request.GetResponse();
                    var stream = response.GetResponseStream();

                    PictureBox picture = new PictureBox();
                    picture.BorderStyle = BorderStyle.FixedSingle;
                    picture.Image = Image.FromStream(stream);
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                    double widthScale = files.Count < 3 ? (double)1 / (double)files.Count - 0.01 : 0.33;
                    picture.Width = (int)(flowLayoutPanel.ClientSize.Width * widthScale);
                    picture.Height = (int)(flowLayoutPanel.ClientSize.Height * 0.99);

                    flowLayoutPanel.Controls.Add(picture);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "Thông báo");
                    continue;
                }
                
            }
            
        }
    }
}