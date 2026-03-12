using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace BMS
{
    public partial class frmShowExcel : _Forms
    {
        public frmShowExcel()
        {
            InitializeComponent();
        }

        void CreateUser()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {                
                string fullName = Lib.ToString(grvData.GetRowCellValue(i, "F2"));
                string code = Lib.ToString(grvData.GetRowCellValue(i, "F7"));
                int dID = Lib.ToInt(grvData.GetRowCellValue(i, "F8"));
                
                if (string.IsNullOrEmpty(fullName) || dID == 0)
                {
                    continue;
                }
                try
                {
                    UsersModel user = (UsersModel)UsersBO.Instance.FindByCode("Code", code);
                    if (user == null)
                    {
                        user = new UsersModel();
                    }

                    user.DepartmentID = dID;
                    user.FullName = fullName;
                    user.Code = code;
                    user.HandPhone = Lib.ToString(grvData.GetRowCellValue(i, "F5"));
                    user.LoginName = Lib.ToString(grvData.GetRowCellValue(i, "F6"));
                    user.Email = user.EmailCom = Lib.ToString(grvData.GetRowCellValue(i, "F4"));
                    user.PasswordHash = MD5.EncryptPassword("123456");

                    if (user.ID == 0)
                    {
                        user.ID = (int)UsersBO.Instance.Insert(user);
                    }
                    else
                    {
                        UsersBO.Instance.Update(user);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            MessageBox.Show("OK");
        }

        void createCustomer()
        {
            for (int i = 2; i < grvData.RowCount; i++)
            {
                string shortName = Lib.ToString(grvData.GetRowCellValue(i, "F2"));
                //string code = Lib.ToString(grvData.GetRowCellValue(i, "F7"));
                //int dID = Lib.ToInt(grvData.GetRowCellValue(i, "F8"));

                if (string.IsNullOrEmpty(shortName))
                {
                    continue;
                }
                try
                {
                    //CustomerModel user = (CustomerModel)CustomerBO.Instance.FindByCode("CustomerShortName", code);
                    bool isExist = CustomerBO.Instance.CheckExist("CustomerShortName", shortName);
                    if (isExist) continue;

                    CustomerModel c = new CustomerModel();
                    c.CustomerCode = TextUtils.CreateNewCode("Customer", "CustomerCode", "KH");
                    c.CustomerName = Lib.ToString(grvData.GetRowCellValue(i, "F4"));
                    c.CustomerShortName = shortName;
                    c.Address = Lib.ToString(grvData.GetRowCellValue(i, "F5"));
                    c.Email = Lib.ToString(grvData.GetRowCellValue(i, "F7"));
                    c.Phone = Lib.ToString(grvData.GetRowCellValue(i, "F6"));
                    c.ContactName = Lib.ToString(grvData.GetRowCellValue(i, "F3"));
                    c.ContactPhone = Lib.ToString(grvData.GetRowCellValue(i, "F6"));
                    c.ContactEmail = Lib.ToString(grvData.GetRowCellValue(i, "F7"));

                    if (c.ID == 0)
                    {
                        c.ID = (int)CustomerBO.Instance.Insert(c);
                    }
                    else
                    {
                        CustomerBO.Instance.Update(c);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            MessageBox.Show("OK");
        }

        void createSupplier()
        {
            DataTable dt = (DataTable)grdData.DataSource;

            for (int i = 2; i < grvData.RowCount; i++)
            {
                string shortName = Lib.ToString(grvData.GetRowCellValue(i, "F1"));
                if (string.IsNullOrEmpty(shortName))
                {
                    continue;
                }
                try
                {
                    bool isExist = SupplierBO.Instance.CheckExist("SupplierShortName", shortName);
                    if (isExist) continue;

                    SupplierModel c = new SupplierModel();
                    c.SupplierCode = TextUtils.CreateNewCode("Supplier", "SupplierCode", "S");
                    c.SupplierName = Lib.ToString(grvData.GetRowCellValue(i, "F2"));
                    c.SupplierShortName = shortName;
                    c.Address = Lib.ToString(grvData.GetRowCellValue(i, "F8"));
                    c.Email = Lib.ToString(grvData.GetRowCellValue(i, "F7"));
                    c.Phone = Lib.ToString(grvData.GetRowCellValue(i, "F6"));
                    c.ContactName = Lib.ToString(grvData.GetRowCellValue(i, "F5"));
                    c.ContactPhone = Lib.ToString(grvData.GetRowCellValue(i, "F6"));
                    c.ContactEmail = Lib.ToString(grvData.GetRowCellValue(i, "F7"));
                    c.Website = Lib.ToString(grvData.GetRowCellValue(i, "F10"));
                    c.SkypeID = Lib.ToString(grvData.GetRowCellValue(i, "F9"));
                    c.Note = Lib.ToString(grvData.GetRowCellValue(i, "F11"));
                    c.Defect = Lib.ToString(grvData.GetRowCellValue(i, "F13"));
                    c.Advantages = Lib.ToString(grvData.GetRowCellValue(i, "F12"));

                    c.ID = (int)SupplierBO.Instance.Insert(c);

                    // Thêm danh sách người liên hệ cho nhà cung cấp
                    List<string> lstHang = new List<string>();
                    List<string> lstSP = new List<string>();
                    List<string> lstTen = new List<string>();
                    DataRow[] arr = dt.Select(string.Format("F1 = '{0}'", shortName));
                    for (int j = 0; j < arr.Length; j++)
                    {                        
                        string nguoiLH = TextUtils.ToString(arr[j]["F5"]);
                        string sdt = TextUtils.ToString(arr[j]["F6"]);
                        string email = TextUtils.ToString(arr[j]["F7"]);
                        string hang = TextUtils.ToString(arr[j]["F4"]);
                        string sp = TextUtils.ToString(arr[j]["F3"]);

                        if (!lstSP.Contains(sp) && !string.IsNullOrEmpty(sp))
                        {
                            lstSP.Add(sp);
                        }
                        if (!lstHang.Contains(hang) && !string.IsNullOrEmpty(hang))
                        {
                            lstHang.Add(hang);
                        }

                        if (string.IsNullOrEmpty(sdt) && string.IsNullOrEmpty(nguoiLH)) continue;

                        SupplierContactModel s = new SupplierContactModel();
                        s.ContactEmail = email;
                        s.ContactName = nguoiLH;
                        s.ContactPhone = sdt;
                        s.Manufactures = hang;
                        s.ProductSale = sp;
                        s.SupplierID = c.ID;
                        SupplierContactBO.Instance.Insert(s);
                    }

                    //Cập nhật lại thông tin danh sách nhà cung cấp, sản phẩm chính cho nhà cung cấp
                    c.Manufactures = lstHang.Count > 0 ? string.Join("; ", lstHang) : "";
                    c.MainProduct = lstSP.Count > 0 ? string.Join("; ", lstSP) : "";
                    SupplierBO.Instance.Update(c);

                }
                catch (Exception ex)
                {
                }
            }
            MessageBox.Show("OK");
        }

        void createManufacturer()
        {
            for (int i = 2; i < grvData.RowCount; i++)
            {
                string name = Lib.ToString(grvData.GetRowCellValue(i, "F1"));

                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }
                try
                {
                    string[] arr = name.Split(';');
                    for (int j = 0; j < arr.Length; j++)
                    {
                        string mName = TextUtils.ToString(arr[j]);
                        if (string.IsNullOrEmpty(mName)) continue;
                        bool isExist = ManufacturerBO.Instance.CheckExist("ManufacturerCode", mName);
                        if (isExist) continue;

                        ManufacturerModel c = new ManufacturerModel();
                        c.ManufacturerCode = mName;
                        c.ManufacturerName = mName;

                        if (c.ID == 0)
                        {
                            c.ID = (int)ManufacturerBO.Instance.Insert(c);
                        }
                        else
                        {
                            ManufacturerBO.Instance.Update(c);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            MessageBox.Show("OK");
        }

        void creatProduct()
        {
            int ID = 10;
            for (int i = 1; i < grvData.RowCount; i++)
            {
                string shortName = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                if (string.IsNullOrEmpty(shortName))
                {
                    continue;
                }
                try
                {
                    ProductRTCModel model = new ProductRTCModel();
                    model.ProductGroupRTCID = ID;
                    model.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                    model.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                    model.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                    model.AddressBox = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
                    model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                    model.Number = 1;
                    //model.Number = TextUtils.ToDecimal(grvData.GetRowCellValue(i,"F7"));
                    ProductRTCBO.Instance.Insert(model);

                }
                catch
                {
                }
            }
            MessageBox.Show("Done");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            creatProduct();
            //createCustomer();
            //createManufacturer();
        }

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;
                cboSheet.DataSource = null;
                cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
            }
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                grdData.DataSource = dt;
                grvData.PopulateColumns();
                grvData.BestFitColumns();
            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
                grdData.DataSource = null;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TextUtils.ExportExcel(grvData);
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
