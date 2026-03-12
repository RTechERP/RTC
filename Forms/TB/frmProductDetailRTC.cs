using BMS.Business;
using BMS.Model;
using BMS.Utils;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;


namespace BMS
{
    public partial class frmProductDetailRTC : _Forms
    {
        public ProductRTCModel oProductRTCModel = new ProductRTCModel();
        string ImagePathSave, fileCopy;
        int warehouseID = 1;
        public frmProductDetailRTC(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        //public frmProductDetailRTC()
        //{
        //    InitializeComponent();
        //    //warehouseID = WarehouseID;
        //}

        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductRTC_Load(object sender, EventArgs e)
        {
            if (Global.IsAdmin || Global.UserID == 24)
            {
                txtNumber.ReadOnly = false;
            }
            if (frmProductRTC.editGrv == 1)
            {
                btnSaveNew.Visible = false;
                frmProductRTC.editGrv = 0;
            }
            loadProductLocation();
            //loadNewCode();
            loadUnit();
            loadGroup();

            LoadFirm();
            loadData();

            //txtCompany.AutoCompleteSource =  
        }
        void loadProductLocation()
        {
            DataTable dt = TextUtils.Select($"select * from ProductLocation where WarehouseID = 1");
            cbProductLocation.Properties.DataSource = dt;
            cbProductLocation.Properties.DisplayMember = "LocationName";
            cbProductLocation.Properties.ValueMember = "ID";
        }

        // load đơn vị
        private void loadUnit()
        {
            DataTable dt = TextUtils.Select("Select * from UnitCountKT");
            cbUnitCount.Properties.DisplayMember = "UnitCountName";
            cbUnitCount.Properties.ValueMember = "ID";
            cbUnitCount.Properties.DataSource = dt;
        }

        private void loadGroup()
        {
            cboGroup.Properties.DisplayMember = "ProductGroupName";
            cboGroup.Properties.ValueMember = "ID";
            //cboGroup.Properties.DataSource = ProductGroupRTCBO.Instance.FindByAttribute("WarehouseID", warehouseID);
            cboGroup.Properties.DataSource = ProductGroupRTCBO.Instance.FindByAttribute("WarehouseID", 1);
        }

        void LoadFirm()
        {
            //var listMakers = SQLHelper<MakerModel>.FindAll().OrderBy(x=>x.STT).ToList();
            var exp1 = new Expression(FirmModel_Enum.IsDelete, 0);
            var exp2 = new Expression(FirmModel_Enum.FirmType, 2);
            var listMakers = SQLHelper<FirmModel>.FindByExpression(exp1.And(exp2));
            //var listMakers = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 2).OrderByDescending(x => x.ID).ToList();
            cboFirm.Properties.ValueMember = "ID";
            cboFirm.Properties.DisplayMember = "FirmName";
            cboFirm.Properties.DataSource = listMakers;
        }

        private void loadData()
        {
            cboGroup.EditValue = oProductRTCModel.ProductGroupRTCID;
            txtName.Text = oProductRTCModel.ProductName;
            txtProductCode.Text = oProductRTCModel.ProductCode;
            txtCompany.Text = oProductRTCModel.Maker;
            //LoadAddressBox(oProductRTCModel.AddressBox);
            txtNumber.Text = oProductRTCModel.Number.ToString();
            cbUnitCount.EditValue = oProductRTCModel.UnitCountID;
            txtNote.Text = oProductRTCModel.Note;
            txtSerial.Text = oProductRTCModel.Serial;
            txtSerialNumber.Text = oProductRTCModel.SerialNumber;
            txtPartNumber.Text = oProductRTCModel.PartNumber;
            //pictureBox1.Image = Lib.ByteToImg(oProductRTCModel.ImagePath);
            //pictureBox1.ImageLocation = oProductRTCModel.LocationImg;
            cbTypeSP.Checked = TextUtils.ToBoolean(oProductRTCModel.BorrowCustomer);
            txtSL.Text = oProductRTCModel.SLKiemKe.ToString();
            cbProductLocation.EditValue = oProductRTCModel.ProductLocationID;
            //try
            //{
            //    dtpCreateDate.Value = TextUtils.ToDate5(oProductRTCModel.CreateDate);
            //}
            //catch
            //{
            //    dtpCreateDate.Value = DateTime.Now;
            //}

            dtpCreateDate.Value = oProductRTCModel.CreateDate.HasValue ? oProductRTCModel.CreateDate.Value : DateTime.Now;
            txtNumberInStore.Text = TextUtils.ToString(oProductRTCModel.NumberInStore);
            txtResolution.Text = oProductRTCModel.Resolution;
            txtMonoColor.Text = oProductRTCModel.MonoColor;
            txtDataInterface.Text = oProductRTCModel.DataInterface;
            txtSensorSize.Text = oProductRTCModel.SensorSize;
            txtLensMount.Text = oProductRTCModel.LensMount;
            txtShutterMode.Text = oProductRTCModel.ShutterMode;
            txtPixelSize.Text = oProductRTCModel.PixelSize;
            txtSensorSizeMax.Text = oProductRTCModel.SensorSizeMax;
            txtMOD.Text = oProductRTCModel.MOD;
            txtFNo.Text = oProductRTCModel.FNo;
            txtWD.Text = oProductRTCModel.WD;
            txtLampType.Text = oProductRTCModel.LampType;
            txtLampColor.Text = oProductRTCModel.LampColor;
            txtLampPower.Text = oProductRTCModel.LampPower;
            txtLampWattage.Text = oProductRTCModel.LampWattage;

            txtMagnification.Text = oProductRTCModel.Magnification;
            txtFocalLength.Text = oProductRTCModel.FocalLength;
            loadImage(oProductRTCModel.LocationImg);

            //TODO: HuyNT update 13/09/2024
            txtInputValue.Text = oProductRTCModel.InputValue;
            txtOuputValue.Text = oProductRTCModel.OutputValue;
            txtCurrentIntensityMax.Text = oProductRTCModel.CurrentIntensityMax;

            //Get id hãng
            if (!string.IsNullOrWhiteSpace(oProductRTCModel.Maker))
            {
                var exp1 = new Expression(FirmModel_Enum.FirmName.ToString(), oProductRTCModel.Maker);
                var exp2 = new Expression(FirmModel_Enum.FirmType.ToString(), 2);
                FirmModel firm = SQLHelper<FirmModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                firm = firm ?? new FirmModel();
                //cboFirm.EditValue = oProductRTCModel.FirmID;
                cboFirm.EditValue = firm.ID;
            }

            txtCodeHCM.Text = oProductRTCModel.CodeHCM;
            txtCodeHCM.Visible = warehouseID == 2;
            labelControl32.Visible = warehouseID == 2;

        }


        DataSet ds;
        private void LoadAddressBox(string selected)
        {
            var filePath = Path.Combine(Application.StartupPath, "AddressBox.xls");
            // DataTable tblAddressBox = Lib.ExcelToDatatableNoHeader(Directory.GetCurrentDirectory() + @"\AddressBox.xls", "AddressBox");
            //var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var stream = new FileStream(Directory.GetCurrentDirectory() + @"\AddressBox.xls", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
            ds = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                UseColumnDataType = false,
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = false
                }
            });
            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {
                cboAddressBox.Items.Add(ds.Tables[0].Rows[i]["F1"]);
            }
            cboAddressBox.SelectedText = selected;
        }
        //string _NewCodeRTC;
        //void loadNewCode()
        //{
        //    string codeRTC = TextUtils.ToString(TextUtils.ExcuteScalar("Select Top 1 ProductCodeRTC From ProductRTC order by ProductCodeRTC desc "));
        //    string code = codeRTC.Replace("Z", "");
        //    int stt = TextUtils.ToInt(code) + 1;
        //    string newcode = "Z";
        //    for (int i = 0; newcode.Length < (9 - stt.ToString().Length); i++)
        //    {
        //        newcode = newcode + "0";
        //    }
        //    _NewCodeRTC = newcode + stt.ToString();
        //}

        string GetProductCodeRTC()
        {
            string numberCodeDefault = "00000001";
            string productCodeRTC = "Z";
            var listProducts = SQLHelper<ProductRTCModel>.FindAll();
            var listproductCodeRTCs = listProducts.Select(x => new
            {
                ProductCodeRTC = x.ProductCodeRTC,
                STT = string.IsNullOrWhiteSpace(x.ProductCodeRTC) ? 0 : TextUtils.ToInt(x.ProductCodeRTC.Substring(1))
            }).ToList();

            int numberCode = listproductCodeRTCs.Count <= 0 ? 0 : listproductCodeRTCs.Max(x => x.STT);
            string numberCodeText = (++numberCode).ToString();

            while (numberCodeText.Length < numberCodeDefault.Length)
            {
                numberCodeText = "0" + numberCodeText;
            }
            productCodeRTC += numberCodeText;

            return productCodeRTC;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveData();
                this.DialogResult = DialogResult.OK;
                //if (!string.IsNullOrEmpty(fileCopy))
                //{
                //    string sourcePath = fileCopy;
                //    string destinationPath = @"\\192.168.1.2\ftp\Upload\Images\ProductDemo\" + ImagePathSave;
                //    File.Move(sourcePath, destinationPath);
                //}
                //this.Close();
            }
        }

        void SaveData()
        {
            //Task task = Task.Factory.StartNew(() =>
            //    {
            //        this.Invoke((MethodInvoker)delegate
            //        {
            //            DocUtils.InitFTPQLSX();

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo mã..."))
            {
                try
                {
                    //byte[] b = Lib.ImgtoByte(pictureBox1.Image);
                    //byte[] c = new byte[1];
                    oProductRTCModel.ProductGroupRTCID = TextUtils.ToInt(cboGroup.EditValue);
                    //if (oProductRTCModel.ID == 0)
                    //    oProductRTCModel.ProductCodeRTC = _NewCodeRTC;
                    oProductRTCModel.ProductCode = txtProductCode.Text.Trim();
                    oProductRTCModel.ProductName = txtName.Text.Trim();
                    //oProductRTCModel.Maker = txtCompany.Text.Trim();
                    oProductRTCModel.Maker = cboFirm.Text;
                    oProductRTCModel.Number = TextUtils.ToInt(txtNumber.Text.Trim());
                    oProductRTCModel.AddressBox = cboAddressBox.Text.Trim();
                    oProductRTCModel.UnitCountID = TextUtils.ToInt(cbUnitCount.EditValue);
                    oProductRTCModel.Note = txtNote.Text.Trim();
                    oProductRTCModel.CreateDate = dtpCreateDate.Value;
                    oProductRTCModel.NumberInStore = TextUtils.ToInt(txtNumberInStore.Text.Trim());
                    oProductRTCModel.Serial = txtSerial.Text.Trim();
                    oProductRTCModel.SerialNumber = txtSerialNumber.Text.Trim();
                    oProductRTCModel.PartNumber = txtPartNumber.Text.Trim();
                    oProductRTCModel.CreatedBy = Global.LoginName;
                    oProductRTCModel.BorrowCustomer = cbTypeSP.Checked;
                    //oProductRTCModel.ImagePath = c;
                    oProductRTCModel.LocationImg = ImagePathSave;
                    oProductRTCModel.SLKiemKe = TextUtils.ToInt(txtSL.Text);
                    oProductRTCModel.ProductLocationID = TextUtils.ToInt(cbProductLocation.EditValue);
                    //oProductRTCModel.WarehouseID = warehouseID;
                    string location = pictureBox1.ImageLocation;
                    //if (location != null)
                    //{
                    //    string[] s = location.Split('\\');
                    //    int sl = s.Length;
                    //    string name = s[sl - 1];
                    //    //giảm hình ảnh 
                    //    //Image ifirst = Image.FromFile(Lib.ImgtoByte(pictureBox1.Image));
                    //    if (pictureBox1.Image != null)
                    //    {
                    //        Image iresize = TextUtils.Resize(pictureBox1.Image, 0.5F);
                    //        iresize.Save($"{oProductRTCModel.ProductCode}.jpg");
                    //        oProductRTCModel.LocationImg = @"\\192.168.1.168\ftp\Anh\IMG\" + $"{oProductRTCModel.ProductCode}.jpg";
                    //        try
                    //        {
                    //DocUtils.UploadFile(Path.Combine(Application.StartupPath, $"{oProductRTCModel.ProductCode}.jpg"), @"Anh\IMG");
                    //        }
                    //        catch (Exception)
                    //        {
                    //            MessageBox.Show("Update ảnh thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        }

                    //    }
                    //}

                    //Khánh update 05/05/2023
                    oProductRTCModel.Resolution = txtResolution.Text.Trim();
                    oProductRTCModel.MonoColor = txtMonoColor.Text.Trim();
                    oProductRTCModel.SensorSize = txtSensorSize.Text.Trim();
                    oProductRTCModel.DataInterface = txtDataInterface.Text.Trim();
                    oProductRTCModel.LensMount = txtLensMount.Text.Trim();
                    oProductRTCModel.ShutterMode = txtShutterMode.Text.Trim();
                    oProductRTCModel.PixelSize = txtPixelSize.Text.Trim();
                    oProductRTCModel.SensorSizeMax = txtSensorSizeMax.Text.Trim();
                    oProductRTCModel.MOD = txtMOD.Text.Trim();
                    oProductRTCModel.FNo = txtFNo.Text.Trim();
                    oProductRTCModel.WD = txtWD.Text.Trim();

                    //Khánh update 27/07/2023
                    oProductRTCModel.LampType = txtLampType.Text.Trim();
                    oProductRTCModel.LampColor = txtLampColor.Text.Trim();
                    oProductRTCModel.LampPower = txtLampPower.Text.Trim();
                    oProductRTCModel.LampWattage = txtLampWattage.Text.Trim();


                    //LT.Anh update 18/07/2024
                    oProductRTCModel.Magnification = txtMagnification.Text.Trim();
                    oProductRTCModel.FocalLength = txtFocalLength.Text.Trim();

                    oProductRTCModel.FirmID = TextUtils.ToInt(cboFirm.EditValue);

                    //TODO: HuyNT update 13/09/2024
                    oProductRTCModel.InputValue = txtInputValue.Text.Trim();
                    oProductRTCModel.OutputValue = txtOuputValue.Text.Trim();
                    oProductRTCModel.CurrentIntensityMax = txtCurrentIntensityMax.Text.Trim();

                    oProductRTCModel.CodeHCM = txtCodeHCM.Text.Trim();

                    if (!string.IsNullOrEmpty(fileCopy))
                    {
                        UploadFile(fileCopy);
                    }
                    if (oProductRTCModel.ID > 0)
                    {
                        //ProductRTCBO.Instance.Update(oProductRTCModel);
                        SQLHelper<ProductRTCModel>.Update(oProductRTCModel);
                    }
                    else
                    {
                        oProductRTCModel.ProductCodeRTC = GetProductCodeRTC();
                        //ProductRTCBO.Instance.Insert(oProductRTCModel);
                        SQLHelper<ProductRTCModel>.Insert(oProductRTCModel);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Update sản phẩm " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //    });

            //});
            //await task;
            //this.DialogResult = DialogResult.OK;
        }

        //private async Task btnSaveNew_Click(object sender, EventArgs e)
        //{
        //    if (ValidateForm())
        //    {
        //        SaveData();

        //        txtName.Clear();
        //        txtProductCode.Clear();
        //        txtCompany.Clear();
        //        txtNote.Clear();
        //        txtNumber.Clear();
        //        txtSerialNumber.Clear();
        //        txtPartNumber.Clear();
        //        txtResolution.Clear();
        //        txtSensorSize.Clear();
        //        txtPixelSize.Clear();
        //        txtSensorSizeMax.Clear();
        //        txtShutterMode.Clear();
        //        txtWD.Clear();
        //        txtMOD.Clear();
        //        txtMonoColor.Clear();
        //        txtFNo.Clear();
        //        txtDataInterface.Clear();
        //        txtLensMount.Clear();
        //        txtLampColor.Clear();
        //        txtLampPower.Clear();
        //        txtLampType.Clear();
        //        txtLampWattage.Clear();
        //        //cbUnitCount.EditValue = -1;
        //        //this.Close();
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}


        private void frmProductRTC_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            //if (cboGroup.Text == "" || txtName.Text == "" || txtProductCode.Text == "" || txtCompany.Text == "" || txtNote.Text == "" || txtSerialNumber.Text == "" || txtPartNumber.Text == "")
            //{
            //    MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            //Khánh update 22/01/2024
            if (string.IsNullOrEmpty(cboGroup.Text.Trim()))
            {
                MessageBox.Show("Vui lòng chọn Mã nhóm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên thiết bị!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrEmpty(txtProductCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã thiết bị!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //if (string.IsNullOrEmpty(txtCompany.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập Hãng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtNote.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Ghi chú!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //if (string.IsNullOrEmpty(txtSerialNumber.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập Serial!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtPartNumber.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập PartNumber!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            DataTable dt = TextUtils.Select($"select top 1 ProductCode from ProductRTC where ProductCode = '{txtProductCode.Text.Trim()}' and ID <> {oProductRTCModel.ID} and ISNULL(IsDelete,0) <> 1");
            //if (oProductRTCModel.ID > 0)
            //{
            //    dt = TextUtils.Select("select top 1 ProductCode from ProductRTC where ProductCode = '" + txtProductCode.Text.Trim() + "' and ID <> " + oProductRTCModel.ID);
            //}
            //else
            //{
            //    dt = TextUtils.Select("select top 1 ProductCode from ProductRTC where ProductCode = '" + txtProductCode.Text.Trim() + "'");

            //}
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(string.Format("Mã sản phẩm này đã tồn tại trong danh sách!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return false;

                }
            }

            //if (string.IsNullOrWhiteSpace(cbProductLocation.Text))
            //{
            //    MessageBox.Show("Chọn vị trí trong list!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}

            if (TextUtils.ToInt(cboFirm.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Hãng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pictures files |*.png;*.jpge;*.jpeg;*.jpg";

            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = System.IO.Path.GetDirectoryName(openFile.FileName);
                    string name = System.IO.Path.GetFileNameWithoutExtension(openFile.FileName);
                    string extension = System.IO.Path.GetExtension(openFile.FileName);

                    string newfileImage = name + "_" + DateTime.Now.ToString("ddMMyyHHmmss") + extension;

                    System.IO.File.Copy(openFile.FileName, path + "\\" + newfileImage);

                    ImagePathSave = newfileImage;
                    fileCopy = path + "\\" + newfileImage;
                    pictureBox1.Image = Image.FromFile($"{openFile.FileName}");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNewUnit_Click(object sender, EventArgs e)
        {
            frmUnitCountKTDetail frm = new frmUnitCountKTDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadUnit();
            }

        }
        void loadImage(string imageName)
        {
            try
            {
                var request = WebRequest.Create("http://192.168.1.2:8083/api/Upload/Images/ProductDemo/" + imageName);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                pictureBox1.Image = Image.FromStream(stream);
                ImagePathSave = imageName;


            }
            catch (Exception)
            {
                return;
            }
        }
        void UploadFile(string fileName)
        {
            string API_UPLOAD = "http://192.168.1.2:8083/api/Home/uploadimagedemo";
            var client = new WebClient();
            client.Headers.Add("Content-Type", "binary/octet-stream");
            client.UploadFileAsync(new Uri(API_UPLOAD), fileName);
            client.UploadFileCompleted += Client_UploadFileCompleted;

        }
        private void Client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            try
            {
                System.IO.File.Delete(fileCopy);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAddMaker_Click(object sender, EventArgs e)
        {
            //frmMakerDetail frm = new frmMakerDetail();
            frmFirmDetail frm = new frmFirmDetail();
            frm.cboFirmType.SelectedIndex = 2;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadFirm();
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveData();

                txtName.Clear();
                txtProductCode.Clear();
                txtCompany.Clear();
                txtNote.Clear();
                txtNumber.Clear();
                txtSerialNumber.Clear();
                txtPartNumber.Clear();
                txtResolution.Clear();
                txtSensorSize.Clear();
                txtPixelSize.Clear();
                txtSensorSizeMax.Clear();
                txtShutterMode.Clear();
                txtWD.Clear();
                txtMOD.Clear();
                txtMonoColor.Clear();
                txtFNo.Clear();
                txtDataInterface.Clear();
                txtLensMount.Clear();
                txtLampColor.Clear();
                txtLampPower.Clear();
                txtLampType.Clear();
                txtLampWattage.Clear();
                //cbUnitCount.EditValue = -1;
                //this.Close();
            }
            else
            {
                return;
            }
        }

        private void frmProductDetailRTC_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(fileCopy))
            //{
            //    string sourcePath = fileCopy;
            //    File.Delete(sourcePath);
            //}
            //this.DialogResult = DialogResult.OK;
        }
        //private bool checkSameProduct()
        //{
        //    DataTable dt;
        //    if (oProductRTCModel.ID > 0)
        //    {
        //        dt = TextUtils.Select("select top 1 ProductCode from ProductRTC where ProductCode = '" + txtCode.Text.Trim() + "' and ID <> " + oProductRTCModel.ID);
        //    }
        //    else
        //    {
        //        dt = TextUtils.Select("select top 1 ProductCode from ProductRTC where ProductCode = '" + txtCode.Text.Trim() + "'");                
        //    }
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            if(MessageBox.Show(string.Format( "Mã sản phẩm này đã tồn tại trong danh sách! Bạn muốn thêm mới sản phẩm có cùng mã ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}
    }
}
