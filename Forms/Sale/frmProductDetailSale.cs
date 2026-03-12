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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BMS
{
    public partial class frmProductDetailSale : _Forms
    {
        public ProductSaleModel oProductSaleModel = new ProductSaleModel();
        public string maker = "";

        public frmProductDetailSale()
        {
            InitializeComponent();
        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductRTC_Load(object sender, EventArgs e)
        {
            loadGroup();
            loadGroupLoaction();
            loadUnit();
            loadMaker();
            loadProductDetailSale();

        }
        private void loadGroup()
        {
            //DataTable dt = TextUtils.Select("select ProductGroupName,ID from ProductGroup ");
            DataTable dt = TextUtils.Select("select * from ProductGroup where IsVisible = 1");

            //List<ProductGroupModel> list = SQLHelper<ProductGroupModel>.FindByAttribute("IsVisible", 1);
            cboGroup.Properties.DisplayMember = "ProductGroupName";
            cboGroup.Properties.ValueMember = "ID";
            cboGroup.Properties.DataSource = dt;
        }

        // load vị trí
        private void loadGroupLoaction()
        {
            //DataTable dt = TextUtils.Select("select * from Location");
            //DataRow dr = dt.NewRow();
            //dr["ID"] = 0;
            //dr["LocationName"] = "";
            //dt.Rows.InsertAt(dr, 0);

            int productGroupID = TextUtils.ToInt(cboGroup.EditValue);
            productGroupID = productGroupID == 70 ? productGroupID : 0;
            List<LocationModel> list = SQLHelper<LocationModel>.FindAll().Where(x => x.ProductGroupID == productGroupID || productGroupID == 0).ToList();

            //list.Insert(0, new LocationModel()
            //{
            //    LocationName = ""
            //});

            //cboVitri.DisplayMember = "LocationName";
            //cboVitri.ValueMember = "ID";
            //cboVitri.DataSource = list;

            cboLocation.Properties.DisplayMember = "LocationName";
            cboLocation.Properties.ValueMember = "ID";
            cboLocation.Properties.DataSource = list;
        }

        // load đơn vị
        private void loadUnit()
        {
            DataTable dt = TextUtils.Select("select * from UnitCount ");
            DataRow dr = dt.NewRow();
            dr["ID"] = 0;
            dr["UnitName"] = "";
            dt.Rows.InsertAt(dr, 0); // cho cboUnit ban đầu trả về 0
            cboUnit.DisplayMember = "UnitName";
            cboUnit.ValueMember = "ID";
            cboUnit.DataSource = dt;
        }

        // load hãng
        private void loadMaker()
        {
            //DataTable dt = TextUtils.Select("select * from Firm ");
            //DataRow dr = dt.NewRow();
            //dr["ID"] = 0;
            //dr["FirmName"] = "";
            //dt.Rows.InsertAt(dr, 0);
            //cboMaker.DisplayMember = "FirmName";
            //cboMaker.ValueMember = "ID";
            //cboMaker.DataSource = dt;

            var exp1 = new Expression(FirmModel_Enum.IsDelete, 0);
            var exp2 = new Expression(FirmModel_Enum.FirmType, 1);
            var list = SQLHelper<FirmModel>.FindByExpression(exp1.And(exp2));

            cboMaker.Properties.DisplayMember = "FirmName";
            cboMaker.Properties.ValueMember = "FirmName";
            cboMaker.Properties.DataSource = list;
        }

        private void loadProductDetailSale()
        {
            if (oProductSaleModel.ID > 0)
            {
                cboGroup.EditValue = oProductSaleModel.ProductGroupID;
                cboLocation.EditValue = oProductSaleModel.LocationID;
                txtName.Text = oProductSaleModel.ProductName;
                txtNote.Text = oProductSaleModel.Note;
                //cboMaker.EditValue = oProductSaleModel.Maker;
                cboMaker.EditValue = maker;
                cboUnit.Text = oProductSaleModel.Unit;
                txtProductCode.Text = oProductSaleModel.ProductCode;
                numNumberInStoreDauKy.Value = oProductSaleModel.NumberInStoreDauky;
                numNumberInStoreCuoiKy.Value = oProductSaleModel.NumberInStoreCuoiKy;

                //PQ.Chien - UPDATE - 16/07/2025
                if (oProductSaleModel.IsFix && !Global.IsAdmin)
                {
                    chkIsFix.Enabled = false;
                    chkIsFix.Checked = true;
                    cboGroup.Enabled = false;
                    txtName.Enabled = false;
                    cboMaker.Enabled = false;
                    cboUnit.Enabled = false;
                    txtProductCode.Enabled = false;
                    numNumberInStoreCuoiKy.Enabled = false;
                    numNumberInStoreDauKy.Enabled = false;
                }
                else
                {
                    chkIsFix.Checked = false;
                }
                //END
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SaveData()) return;
            this.Close();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveData();
            txtName.Clear();
            txtProductCode.Clear();
            txtNote.Clear();
            cboUnit.Items.Clear();
            //cboMaker.Items.Clear();
            //cboVitri.Items.Clear();
            cboGroup.Text = "";
            numNumberInStoreCuoiKy.Value = 0;
            numNumberInStoreDauKy.Value = 0;

            //PQ.Chien - UPDATE - 16/07/2025
            chkIsFix.Checked = false;
            //END
        }

        private void btnLocationNew_Click(object sender, EventArgs e)
        {
            frmProductLocationDetail frm = new frmProductLocationDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGroupLoaction();
            }
        }

        private void btnNewUnit_Click(object sender, EventArgs e)
        {
            frmUnitCountDetail frm = new frmUnitCountDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadUnit();
            }
        }

        private void btnNewMaker_Click(object sender, EventArgs e)
        {
            frmFirmDetail frm = new frmFirmDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMaker();
            }
        }

        //string _NewCodeRTC;
        //void loadNewCode()
        //{
        //    DataSet ds = TextUtils.LoadDataSetFromSP("spLoadNewCodeRTC", new string[] { "@Group" }, new object[] { TextUtils.ToInt(cboGroup.EditValue) });
        //    string code = "";
        //    string codeRTC = TextUtils.ToString(ds.Tables[1].Rows[0][0]);

        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        _NewCodeRTC = codeRTC + "000000001";
        //    }
        //    else
        //    {
        //        if (!codeRTC.Contains("HCM"))
        //        {
        //            code = TextUtils.ToString(ds.Tables[0].Rows[0][0]).Replace(codeRTC, "");
        //            int stt = TextUtils.ToInt(code) + 1;
        //            for (int i = 0; codeRTC.Length < (9 - stt.ToString().Length); i++)
        //            {
        //                codeRTC = codeRTC + "0";
        //            }
        //            _NewCodeRTC = codeRTC + stt.ToString();
        //        }
        //        else
        //        {
        //            code = TextUtils.ToString(ds.Tables[0].Rows[0][0]).Replace(codeRTC, "");
        //            int stt = TextUtils.ToInt(code) + 1;
        //            string indexString = TextUtils.ToString(stt);
        //            for (int i = 0; indexString.Length < code.Length; i++)
        //            {
        //                indexString = "0" + indexString;
        //            }
        //            _NewCodeRTC = codeRTC + indexString.ToString();
        //        }
        //    }
        //}



        string GetProductNewCode()
        {
            string productNewCode = "";

            //GET DANH SÁCH SP THEO LOẠI KHO
            DataRowView productGroup = (DataRowView)cboGroup.GetSelectedDataRow();
            if (productGroup == null) return productNewCode;
            int productGroupID = TextUtils.ToInt(productGroup["ID"]);
            string productGroupCode = TextUtils.ToString(productGroup["ProductGroupID"]).Trim();

            List<ProductSaleModel> listProducts = SQLHelper<ProductSaleModel>.FindByAttribute("ProductGroupID", productGroupID);
            var listNewCodes = listProducts.Where(x => x.ProductNewCode.StartsWith(productGroupCode))
                                           .Select(x => new
                                           {
                                               ID = x.ID,
                                               ProductNewCode = x.ProductNewCode,
                                               STT = string.IsNullOrWhiteSpace(x.ProductNewCode) ? 0 : TextUtils.ToInt(x.ProductNewCode.Substring(productGroupCode.Length)),
                                           }).ToList();

            int numberCode = listNewCodes.Count <= 0 ? 0 : listNewCodes.Max(x => x.STT);
            string numberCodeText = (++numberCode).ToString();

            while ((numberCodeText.Length + productGroupCode.Length) < 9)
            {
                numberCodeText = "0" + numberCodeText;
            }
            productNewCode = $"{productGroupCode}{numberCodeText}";
            return productNewCode;
        }

        bool SaveData()
        {

            if (!ValidateForm()) return false;
            try
            {
                //loadNewCode();
                //oProductSaleModel.ProductGroupID = TextUtils.ToInt(cboGroup.EditValue);

                // ndnhat update 04/09/2025 nếu đổi nhóm kho thì sinh lại mã mới
                int newGroupId = TextUtils.ToInt(cboGroup.EditValue);

                if (oProductSaleModel.ID <= 0)
                {
                    oProductSaleModel.ProductNewCode = GetProductNewCode();
                }
                else if (oProductSaleModel.ID > 0 && oProductSaleModel.ProductGroupID != newGroupId)
                {
                    oProductSaleModel.ProductGroupID = newGroupId;
                    oProductSaleModel.ProductNewCode = GetProductNewCode();
                }
                oProductSaleModel.ProductGroupID = newGroupId;

                //oProductSaleModel.AddressBox = cboVitri.Text.Trim();
                oProductSaleModel.AddressBox = cboLocation.Text;
                oProductSaleModel.ProductCode = txtProductCode.Text.Trim();
                //if (oProductSaleModel.ID == 0)
                //{
                //    //oProductSaleModel.ProductNewCode = _NewCodeRTC;
                //    oProductSaleModel.ProductNewCode = GetProductNewCode();
                //}
                oProductSaleModel.ProductName = txtName.Text.Trim();
                oProductSaleModel.Maker = cboMaker.Text.Trim();
                oProductSaleModel.Note = txtNote.Text.Trim();
                oProductSaleModel.Unit = cboUnit.Text.Trim();
                oProductSaleModel.NumberInStoreCuoiKy = TextUtils.ToDecimal(numNumberInStoreCuoiKy.Text.Trim());
                oProductSaleModel.NumberInStoreDauky = TextUtils.ToDecimal(numNumberInStoreDauKy.Text.Trim());
                //oProductSaleModel.LocationID = TextUtils.ToInt(cboVitri.SelectedValue);
                oProductSaleModel.LocationID = TextUtils.ToInt(cboLocation.EditValue);

                var exp1 = new Expression(FirmModel_Enum.IsDelete, 0);
                var exp2 = new Expression(FirmModel_Enum.FirmType, 1);
                var exp3 = new Expression(FirmModel_Enum.FirmName, cboMaker.Text.Trim());
                var firm = SQLHelper<FirmModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault() ?? new FirmModel();
                oProductSaleModel.FirmID = firm.ID;//== 0 ? oProductSaleModel.FirmID : firm.ID;

                //PQ.Chien - UPDATE - 16/07/2025
                if (chkIsFix.Checked)
                {
                    oProductSaleModel.IsFix = true;
                }
                else
                {
                    oProductSaleModel.IsFix = false;
                }
                //END
                if (oProductSaleModel.ID > 0)
                {
                    ProductSaleBO.Instance.Update(oProductSaleModel);
                }
                else
                {
                    oProductSaleModel.ID = (int)ProductSaleBO.Instance.Insert(oProductSaleModel);

                    InventoryModel inventoryModel = new InventoryModel();
                    inventoryModel.WarehouseID = 1;
                    inventoryModel.ProductSaleID = oProductSaleModel.ID;
                    InventoryBO.Instance.Insert(inventoryModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi Update sản phẩm.\n{ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            //int idMax = 42614;
            int idMax = 42770;
            //======================== lee min khooi Update 24/06/2024 ============================================================
            //DateTime dayUpdate = new DateTime(2024,06,24);
            //string pattern = @"^[a-zA-Z0-9_!@#$%^()&*\-\s]+$";
            //Regex regex = new Regex(pattern);

            string pattern = @"^[^àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴ]+$";
            Regex regex = new Regex(pattern);
            if (string.IsNullOrEmpty(txtProductCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã sản phẩm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            bool isCheck = regex.IsMatch(txtProductCode.Text.Trim());
            if (oProductSaleModel.ID == 0 || oProductSaleModel.ID > idMax)
            {
                if (!isCheck)
                {
                    MessageBox.Show("[Mã sản phẩm] không được chứa ký tự tiếng Việt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            //======================== endddd  lee min khooi Update 24/06/2024 ============================================================
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên thiết bị!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cboGroup.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn Mã kho!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //DataTable dt = TextUtils.Select($"select top 1 ProductCode from ProductSale where ProductCode = '{txtProductCode.Text.Trim()}' and ID <> {oProductSaleModel.ID}");
            //if (oProductSaleModel.ID > 0)
            //{
            //    dt = TextUtils.Select("select top 1 ProductCode from ProductSale where ProductCode = '" + txtProductCode.Text.Trim() + "' and ID <> " + oProductSaleModel.ID);
            //}
            //else
            //{
            //    dt = TextUtils.Select("select top 1 ProductCode from ProductSale where ProductCode = '" + txtProductCode.Text.Trim() + "'");
            //}

            var exp1 = new Expression("ProductCode", txtProductCode.Text.Trim());
            var exp2 = new Expression("ProductGroupID", TextUtils.ToInt(cboGroup.EditValue));
            var exp3 = new Expression("ID", oProductSaleModel.ID, "<>");
            var listProduct = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2).And(exp3));

            if (listProduct.Count > 0)
            {
                MessageBox.Show(string.Format($"Mã sản phẩm [{txtProductCode.Text.Trim()}] này đã tồn tại trong kho [{cboGroup.Text}]!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboUnit.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Đơn vị!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboMaker.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Hãng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            //loadNewCode();
            loadGroupLoaction();
        }

        private void frmProductDetailSale_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            frmProductGroupDetail frm = new frmProductGroupDetail(0);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGroup();
            }
        }

        private void frmProductDetailSale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
