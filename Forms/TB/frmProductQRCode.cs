using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QRCoder;
using System.Drawing.Printing;
using Forms;

namespace BMS
{
    public partial class frmProductQRCode : _Forms
    {
        public ProductRTCModel oProductRTCModel = new ProductRTCModel();
        public List<string> LST = new List<string>();
        public frmProductQRCode()
        {
            InitializeComponent();
            
        }
        private void frmProductQRCode_Load(object sender, EventArgs e)
        {
            //txtCode.Text = oProductRTCModel.ProductCode + ";" + oProductRTCModel.AddressBox + oProductRTCModel.Note;
            QRCodeGenerator qrCode = new QRCodeGenerator();
            for (int i = 0; i < LST.Count; i++)
            {
                QRCodeData data = qrCode.CreateQrCode(LST[i], QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(data);
                Bitmap img = code.GetGraphic(8);
                img.SetResolution(50,50);
                PictureBox pic2 = new PictureBox();
                pic2.Width = picQR.Width;
                pic2.Height = picQR.Height;
                pic2.Location = new Point(picQR.Location.X + picQR.Width*i + 10, picQR.Location.Y);
                pic2.SizeMode = PictureBoxSizeMode.StretchImage;
                flowLayoutPanel1.Controls.Add(pic2);
                pic2.Image = img;


                //using (Graphics graphics = Graphics.FromImage(img))
                //{
                //    using (Font arialFont = new Font("Arial", 80))
                //    {
                //        //graphics.DrawString("a", arialFont, Brushes.Black,new Point(pic2.Location.X , pic2.Location.Y + pic2.Height + 5));
                //        graphics.DrawString("a", arialFont, Brushes.Black,new Point(pic2.Location.X , pic2.Location.Y + 200 + 5));
                //        //graphics.DrawString("a", arialFont, Brushes.Black, new Point(picQR.Location.X, picQR.Location.Y + picQR.Height + 5));
                //    }
                //}
                //pic2.Image = img;
                //picQR.Image = img;
                //Bitmap bmp = new Bitmap(img);
                //bmp.Save("D:\\QRCODE\\qr.png");

            }
        }
        /// <summary>
        /// print QR-Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIMG_Click(object sender, EventArgs e)
        {
            printCode();
        }

        private void printCode()
        {
            PrintDialog pr = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;
            pr.Document = doc;
            if (pr.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }
        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(flowLayoutPanel1.Width,flowLayoutPanel1.Height);
            flowLayoutPanel1.DrawToBitmap(bmp,new Rectangle(0,0, flowLayoutPanel1.Width, flowLayoutPanel1.Height));
            e.Graphics.DrawImage(bmp,0,0);
            bmp.Dispose();
        }
    }
}
