using BMS.Model;
using BMS.Utils;
using Forms.TB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BMS
{
	public partial class frmListProductsHCMDetail : _Forms
	{
		int warehouseID = 0;

		public int productGroupId = 0;
		public ProductRTCModel product = new ProductRTCModel();
		public List<int> totalRecords = new List<int>();


		//string pathServer = "\\\\192.168.1.190\\Common\\11. HCNS";
		//string pathPattern = $@"DoPhongSach\Anh";
		//string urlAPI = $@"http://192.168.1.2:8083/api/hcns";

		public frmListProductsHCMDetail(int warehouseID)
		{
			InitializeComponent();
			this.warehouseID = warehouseID;
		}


		private void frmListProductsHCMDetail_Load(object sender, EventArgs e)
		{
			LoadProductGroup();
			LoadUnitCount();
			LoadFirm();
			LoadProductLocation();
			LoadData();
		}

		void LoadData()
		{
			txtProductCode.Text = product.ProductCode;
			txtProductName.Text = product.ProductName;
			cboUnitCount.EditValue = product.UnitCountID;
			cboFirm.EditValue = product.FirmID;
			cboProductLocation.EditValue = product.ProductLocationID;
			txtNote.Text = product.Note;
			txtSize.Text = product.Size;
			txtProductCode.Focus();
			//LoadImage();
		}


		void LoadProductGroup()
		{
			var list = SQLHelper<ProductGroupRTCModel>.FindAll();
			cboProductGroupRTC.Properties.ValueMember = "ID";
			cboProductGroupRTC.Properties.DisplayMember = "ProductGroupName";
			cboProductGroupRTC.Properties.DataSource = list;
			cboProductGroupRTC.EditValue = productGroupId;
		}


		private void LoadUnitCount()
		{
			var list = SQLHelper<UnitCountKTModel>.FindAll();
			cboUnitCount.Properties.DisplayMember = "UnitCountName";
			cboUnitCount.Properties.ValueMember = "ID";
			cboUnitCount.Properties.DataSource = list;
		}

		void LoadFirm()
		{
			var listMakers = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 2).OrderByDescending(x => x.ID).ToList();
			cboFirm.Properties.ValueMember = "ID";
			cboFirm.Properties.DisplayMember = "FirmName";
			cboFirm.Properties.DataSource = listMakers;
		}

		void LoadProductLocation()
		{
			var list = SQLHelper<ProductLocationModel>.FindByAttribute("WarehouseID", warehouseID);
			cboProductLocation.Properties.ValueMember = "ID";
			cboProductLocation.Properties.DisplayMember = "LocationName";
			cboProductLocation.Properties.DataSource = list;
		}


		//void LoadImage()
		//{
		//	try
		//	{
		//		if (string.IsNullOrWhiteSpace(product.LocationImg)) return;
		//		FileInfo file = new FileInfo(product.LocationImg);
		//		string url = $"{urlAPI}/{pathPattern}/{product.ProductCode}_{file.Name}";
		//		var request = WebRequest.Create(url);
		//		var response = request.GetResponse();
		//		var stream = response.GetResponseStream();

		//		pbImage.Image = Image.FromStream(stream);
		//	}
		//	catch (Exception ex)
		//	{
		//		return;
		//	}
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

		bool CheckValidate()
		{

			string productCode = txtProductCode.Text.Trim();
			if (TextUtils.ToInt(cboProductGroupRTC.EditValue) <= 0)
			{
				MessageBox.Show("Vui lòng nhập Mã nhóm!", "Thông báo");
				return false;
			}

			if (string.IsNullOrWhiteSpace(productCode))
			{
				MessageBox.Show("Vui lòng nhập Mã sản phẩm!", "Thông báo");
				return false;
			}
			else
			{
				var ex1 = new Expression("ProductCode", productCode);
				var ex2 = new Expression("ProductGroupRTCID", TextUtils.ToInt(cboProductGroupRTC.EditValue));
				var ex3 = new Expression("IsDelete", 1, "<>");
				var ex4 = new Expression("ID", product.ID, "<>");
				var listProducts = SQLHelper<ProductRTCModel>.FindByExpression(ex1.And(ex2).And(ex3).And(ex4));
				if (listProducts.Count > 0)
				{
					MessageBox.Show($"Mã sản phẩm [{productCode}] đã tồn tại!", "Thông báo");
					return false;
				}
			}

			if (string.IsNullOrWhiteSpace(txtProductName.Text.Trim()))
			{
				MessageBox.Show("Vui lòng nhập Tên sản phầm!", "Thông báo");
				return false;
			}

			if (TextUtils.ToInt(cboUnitCount.EditValue) <= 0)
			{
				MessageBox.Show("Vui lòng nhập Đơn vị tính!", "Thông báo");
				return false;
			}

			return true;
		}

		bool SaveData()
		{
			if (!CheckValidate()) return false;

			product.ProductGroupRTCID = TextUtils.ToInt(cboProductGroupRTC.EditValue);
			product.UnitCountID = TextUtils.ToInt(cboUnitCount.EditValue);
			product.FirmID = TextUtils.ToInt(cboFirm.EditValue);
			product.ProductLocationID = TextUtils.ToInt(cboProductLocation.EditValue);
			product.ProductCode = TextUtils.ToString(txtProductCode.Text);
			product.ProductName = TextUtils.ToString(txtProductName.Text);
			product.Note = TextUtils.ToString(txtNote.Text);
			product.Size = txtSize.Text.Trim();
			

			if (product.ID <= 0)
			{
				product.CreateDate = DateTime.Now;
				product.ProductCodeRTC = GetProductCodeRTC();
				var result = SQLHelper<ProductRTCModel>.Insert(product);
				totalRecords.Add(result.TotalRow);
			}
			else
			{
				var result = SQLHelper<ProductRTCModel>.Update(product);
				totalRecords.Add(result.TotalRow);
			}

			//UploadImage(product.ID);

			return true;
		}


		//async void UploadImage(int id)
		//{
		//	try
		//	{
		//		ProductRTCModel productRTC = SQLHelper<ProductRTCModel>.FindByID(id);
		//		if (productRTC.ID <= 0) return;


		//		string pathUpload = Path.Combine(pathServer, pathPattern);


		//		if (string.IsNullOrWhiteSpace(pbImage.ImageLocation)) return;
		//		FileInfo file = new FileInfo(pbImage.ImageLocation);
		//		if (file.Length < 0) return;

		//		var client = new HttpClient();
		//		var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
		//		byte[] bytes = new byte[file.Length];
		//		fileStream.Read(bytes, 0, (int)file.Length);
		//		var byteArrayContent = new ByteArrayContent(bytes);

		//		MultipartFormDataContent content = new MultipartFormDataContent();
		//		content.Add(byteArrayContent, "file", $"{productRTC.ProductCode}_{file.Name}");

		//		var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
		//		var result = await client.PostAsync(url, content);
		//		if (result.StatusCode == System.Net.HttpStatusCode.OK)
		//		{
		//			productRTC.LocationImg = file.FullName;
		//			SQLHelper<ProductRTCModel>.Update(productRTC);
		//		}
		//		else
		//		{
		//			MessageBox.Show(result.StatusCode.ToString() + "\r\n" + result.Content.ToString(), "Thông báo");
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.ToString(), "Thông báo");
		//	}

		//}

		private void btnAddAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			
			if (SaveData())
			{
				product = new ProductRTCModel();
				LoadData();
			}
		}

		private void btnAddAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (SaveData())
			{
				this.DialogResult = DialogResult.OK;
			}
		}

		private void frmListProductsHCMDetail_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btnAddLocation_Click(object sender, EventArgs e)
		{
			frmProductsHCMLocationDetail frm = new frmProductsHCMLocationDetail(warehouseID);
			if (frm.ShowDialog() == DialogResult.OK)
			{
				LoadProductLocation();
			}
		}

		//private void pbImage_Click(object sender, EventArgs e)
		//{
		//	OpenFileDialog openFile = new OpenFileDialog();
		//	openFile.Filter = "Pictures files |*.png;*.jpge;*.jpeg;*.jpg";

		//	openFile.RestoreDirectory = true;
		//	if (openFile.ShowDialog() == DialogResult.OK)
		//	{
		//		try
		//		{
		//			pbImage.Image = Image.FromFile($"{openFile.FileName}");
		//			pbImage.ImageLocation = openFile.FileName;
		//		}
		//		catch (Exception ex)
		//		{

		//			MessageBox.Show(ex.Message);
		//		}
		//	}

		//}
	}
}
