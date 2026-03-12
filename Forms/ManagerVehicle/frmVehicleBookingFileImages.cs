using BMS;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmVehicleBookingFileImages : _Forms
    {
        public List<VehicleBookingManagementModel> lisVehicleBookingManagementModel = new List<VehicleBookingManagementModel>();
        public frmVehicleBookingFileImages()
        {
            InitializeComponent();
        }
        private void frmVehicleBookingFileImages_Load(object sender, EventArgs e)
        {
            loadImage();
        }
        void loadImage()
        {
            try
            {
                foreach (var item1 in lisVehicleBookingManagementModel)
                {
                    if (item1.Category == 2)
                    {
                        //TabPage tabPage = new TabPage($"Ảnh các kiện hàng - Người nhận:[{item1.ReceiverName}] - Thời gian nhận: [{item1.TimeNeedPresent.Value:dd/MM/yyyy HH:mm}]");
                        TabNavigationPage tab = new TabNavigationPage();
                        tab.Caption = $"Ảnh các kiện hàng - Người nhận:[{item1.ReceiverName}] - Thời gian nhận: [{item1.TimeNeedPresent.Value:dd/MM/yyyy HH:mm}]";

                        var exp = new Expression("VehicleBookingID", item1.ID);
                        List<VehicleBookingFileModel> files = SQLHelper<VehicleBookingFileModel>.FindByExpression(exp);

                        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                        flowLayoutPanel.Dock = DockStyle.Fill;
                        flowLayoutPanel.AutoScroll = true;

                        foreach (var item2 in files)
                        {
                            var createDate = item1.CreatedDate.Value.ToString("dd.MM.yyyy");

                            string url = $"http://113.190.234.64:8083/api/datxe/DANGKYDATXENGAY{createDate}/{item2.FileName}";
                            //var request = WebRequest.Create($"http://192.168.1.2:8083/api/demo/image/DANGKYDATXENGAY{createDate}/{item2.FileName}");
                            var request = WebRequest.Create(url);
                            var response = request.GetResponse();
                            var stream = response.GetResponseStream();

                            PictureBox picture = new PictureBox();
                            picture.Image = Image.FromStream(stream);
                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                            picture.Width = 450;
                            picture.Height = 450;

                            flowLayoutPanel.Controls.Add(picture);
                        }

                        tab.Controls.Add(flowLayoutPanel);

                        tabPane1.Pages.Add(tab);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
    }
}