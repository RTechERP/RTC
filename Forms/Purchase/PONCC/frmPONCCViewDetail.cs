using BaseBusiness.DTO;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using Forms.Purchase.PONCC;
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
    public partial class frmPONCCViewDetail : _Forms
    {
        public int poId = 0;

        /// <summary>
        /// 1:tiếng việt
        /// 2.Tiếng anh
        /// </summary>
        public int type = 0;


        string imageURL = "http://113.190.234.64:8083/api/imagesign/SignNonBack";
        //string imageURL = @"C:\Users\Admin\Desktop\SignRTC\PNG";
        //List<object> listInfoBuyers = new List<object>()
        //{
        //    new {Buyer = "RTC Technology Viet Nam JSC.",
        //        //AddressBuyer = "A52 - TT10,Van Quan New Urban, Van Quan Ward, Ha Dong Dist., Hanoi, Vietnam, 10000.",
        //        AddressBuyer = "A52-TT10, Van Quan New Urban Area, Ha Dong Ward, Hanoi, Vietnam.",
        //        LegalRepresentative = "Mr. Nguyen Van Sao - Position: Director"},
        //    new {
        //        Buyer = "MVI VIET NAM., JSC.",
        //        //AddressBuyer = "No.35 - Lien Ke 4, Tan Tay Do urban area, Tan Lap commune, Dan Phuong district, Hanoi, Vietnam, 10000.",
        //        AddressBuyer = "No.35 - Lien Ke 4, Tan Tay Do urban area, O Dien commune, Hanoi, Vietnam, 10000.",
        //        LegalRepresentative = "Nguyen Van Thao (Mr.)- Manager"
        //    },
        //    new {Buyer = "APR VIET NAM JOINT STOCK COMPANY.",AddressBuyer = "No. 11, Lane 1074 Lang Street, Lang Thuong Ward, Dong Da District, Hanoi City, Vietnam.",LegalRepresentative = "Pham Van Quyen (Mr.)- Manager"},
        //    new {Buyer = "YONKO VIET NAM AUTOMATION JOINT STOCK COMPANY.",AddressBuyer = "No. 31, alley 1144/130 Quang Trung street, group 5, Yen Nghia Ward, Ha Dong District, Hanoi.",LegalRepresentative = "Nguyen Manh Tien"},
        //    new {Buyer = "",AddressBuyer = "",LegalRepresentative = ""},
        //};

        //List<object> listInfoBuyersTitle = new List<object>()
        //{
        //    new {
        //        Buyer = "CÔNG TY CỔ PHẦN  RTC TECHNOLOGY VIỆT NAM",
        //        //AddressBuyer = "Nhà A52, TT10, Khu đô thị mới Văn Quán, Phường Văn Quán, Quận Hà Đông, Thành phố Hà Nội, Việt Nam.",
        //        AddressBuyer = "Nhà A52, TT10, Khu đô thị mới Văn Quán, Phường Hà Đông, Thành phố Hà Nội, Việt Nam.",
        //        Tax = "Mã số thuế:0106845021"
        //    },
        //    new
        //    {
        //        Buyer = "CÔNG TY CỔ PHẦN MVI VIỆT NAM",
        //        //AddressBuyer = "Số 35-Liền kề 4, Khu đô thị Tân Tây Đô, Xã Tân Lập, Huyện Đan Phượng, Thành phố Hà Nội, Việt Nam.",
        //        AddressBuyer = "Số 35-Liền kề 4, Khu đô thị Tân Tây Đô, Xã Ô Diên, TP Hà Nội.",
        //        Tax = "Mã số thuế: 0108703953"
        //    },
        //     new
        //    {
        //        Buyer = "CÔNG TY CỔ PHẦN APR VIỆT NAM",
        //        AddressBuyer = "Số 11, ngõ 1074 Đường Láng, Phường Láng Thượng, Quận Đống đa, Thành phố Hà Nội, Việt Nam.",
        //        Tax = "Mã số thuế: 0107753736"
        //    },
        //      new
        //    {
        //        Buyer = "CÔNG TY CỔ PHẦN TỰ ĐỘNG HÓA YONKO VIỆT NAM",
        //        AddressBuyer = "Số nhà 31 ngách 1144/130 đường quang trung,tổ 5 , Phường Yên Nghĩa, Quận Hà Đông, Thành phố Hà Nội, Việt Nam.",
        //        Tax = "Mã số thuế: 0108115528"
        //    },
        //      new
        //    {
        //        Buyer = "CÔNG TY CÓ PHÂN PHÁT TRIỂN CÔNG NGHỆ R-TECH",
        //        AddressBuyer = "N6-SH12A khu nhà ở phường Kiến Hưng Mipec City View, Phường Kiến Hưng, Quận Hà Đông, Thành phố Hà Nội, Việt Nam.",
        //        Tax = "Mã số thuế: 0111002073"
        //    }

        //};


        public bool isShowSign = true;
        public bool isShowSeal = true;

        List<PONCCDetailDTO> listDetail = new List<PONCCDetailDTO>();

        public frmPONCCViewDetail()
        {
            InitializeComponent();
        }

        private void frmPONCCViewDetail_Load(object sender, EventArgs e)
        {
            //listDetail = SQLHelper<PONCCDetailDTO>.ProcedureToList("spGetPONCCDetail", new string[] { "@PONCCID" }, new object[] { poId })
            //    .Where(p => p.IsPurchase == false).ToList();//ndnhat update 15/10/2025
        }

        private void documentViewer1_Load(object sender, EventArgs e)
        {
            listDetail = SQLHelper<PONCCDetailDTO>.ProcedureToList("spGetPONCCDetail", new string[] { "@PONCCID" }, new object[] { poId })
                                                  .Where(p => p.IsPurchase == false).ToList();//ndnhat update 15/10/2025
            LoadData();
        }

        void LoadData()
        {
            if (type == 2)
            {
                LoadPOReportEnglish(isShowSign, isShowSeal, listDetail);
            }
            else
            {
                LoadPOReportVietNamese(isShowSign, isShowSeal, listDetail);
            }
        }

        private void LoadPOReportVietNamese(bool isShowSign, bool isShowSeal, object dataSource)
        {

            PONCCReportVietnamese report = new PONCCReportVietnamese();


            //List<PONCCDetailDTO> listDetail = SQLHelper<PONCCDetailDTO>.ProcedureToList("spGetPONCCDetail", new string[] { "@PONCCID" }, new object[] { poId });
            //listDetail = listDetail.GroupBy(item => new { item.ProductCode, item.UnitPrice })
            //                                                            .Select(cl => new PONCCDetailDTO
            //                                                            {
            //                                                                //STT = cl.First().STT,
            //                                                                ProductCodeOfSupplier = cl.First().ProductCodeOfSupplier,
            //                                                                Unit = cl.First().Unit,
            //                                                                QtyRequest = cl.Sum(q => q.QtyRequest),
            //                                                                UnitPrice = cl.First().UnitPrice,
            //                                                                ThanhTien = cl.Sum(q => q.ThanhTien),
            //                                                                VAT = cl.First().VAT,
            //                                                                VATMoney = cl.Sum(q => q.VATMoney),
            //                                                                Discount = cl.First().Discount,
            //                                                                TotalPrice = cl.Sum(q => q.TotalPrice),
            //                                                            }).ToList();

            //report.DataSource = listDetail;
            report.DataSource = dataSource;

            PONCCDTO po = SQLHelper<PONCCDTO>.ProcedureToList("spGetPONCCByID", new string[] { "@ID" }, new object[] { poId }).FirstOrDefault();
            if (po == null) return;

            report.Parameters["NameNCC"].Value = po.NameNCC;
            report.Parameters["RequestDate"].Value = po.RequestDate.Value.ToString("dd/MM/yyyy");
            report.Parameters["AddressNCC"].Value = po.AddressNCC;
            report.Parameters["BillCode"].Value = po.BillCode;
            report.Parameters["CurrencyText"].Value = po.CurrencyText;
            report.Parameters["MaSoThue"].Value = po.MaSoThue;
            report.Parameters["PhoneNCC"].Value = po.PhoneNCC;
            report.Parameters["Fax"].Value = po.Fax;
            report.Parameters["Note"].Value = po.Note;

            report.Parameters["DeliveryDate"].Value = po.DeliveryDate.Value.ToString("dd/MM/yyyy");
            report.Parameters["AddressDelivery"].Value = po.AddressDelivery;
            report.Parameters["RulePay"].Value = po.RulePayName;
            report.Parameters["SoTK"].Value = po.AccountNumberSupplier;

            TaxCompanyModel taxCompany = SQLHelper<TaxCompanyModel>.FindByAttribute("Code", po.TaxCompanyCode.ToUpper().Trim()).FirstOrDefault() ?? new TaxCompanyModel();
            //taxCompany = taxCompany ?? new TaxCompanyModel();
            var exp1 = new Expression("EmployeeID", po.EmployeeID);
            var exp2 = new Expression("TaxCompayID", taxCompany.ID);
            EmployeePurchaseModel employeePurchase = SQLHelper<EmployeePurchaseModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            employeePurchase = employeePurchase ?? new EmployeePurchaseModel();

            //report.Parameters["PhoneCreatedBy"].Value = po.SDTCaNhan;
            //report.Parameters["EmailCreatedBy"].Value = po.EmailCongTy;

            report.Parameters["PhoneCreatedBy"].Value = employeePurchase.Telephone;
            report.Parameters["EmailCreatedBy"].Value = employeePurchase.Email;

            report.Parameters["Code"].Value = po.Code;
            report.Parameters["SupplierContactPhone"].Value = po.SupplierContactPhone;

            //string imageURLSignBGD = "https://i.pinimg.com/236x/6a/1e/98/6a1e9846c09426a4b2404dd94686fe95.jpg";

            report.picPrepared.ImageUrl = $"{imageURL}/{po.Code.Trim()}.png";
            report.picDirector.ImageUrl = $"{imageURL}/seal{po.TaxCompanyCode.Trim().ToUpper()}.png";

            if (po.CompanyText.Trim().ToUpper() == "R-TECH")
            {
                report.picDirector.WidthF = 738.19f;
                report.picDirector.HeightF = 534.67f;

                report.picDirector.LocationF = new PointF(1220.81f, 106.15f);
            }


            report.picPrepared.Visible = isShowSign;
            report.picDirector.Visible = isShowSeal;


            report.Parameters["TotalMoneyText"].Value = NumberMoneyToText.ConvertNumberToTextVietNamese(TextUtils.ToDecimal(po.TotalMoneyPO), po.CurrencyText);

            if (po.Company > 0)
            {
                //var infoBuyer = listInfoBuyersTitle[TextUtils.ToInt(po.Company) - 1];
                //var infoBuyer = SQLHelper<TaxCompanyModel>.FindByAttribute(TaxCompanyModel_Enum.Code.ToString(), po.CompanyText).FirstOrDefault() ?? new TaxCompanyModel();

                //if (infoBuyer != null)
                if (taxCompany.ID > 0)
                {
                    //var type = infoBuyer.GetType();
                    //string companyName = TextUtils.ToString(type.GetProperty("Buyer").GetValue(infoBuyer));
                    //string companyAddress = TextUtils.ToString(type.GetProperty("AddressBuyer").GetValue(infoBuyer));
                    //string companyTax = TextUtils.ToString(type.GetProperty("Tax").GetValue(infoBuyer));

                    string companyName = taxCompany.BuyerVietnamese;
                    string companyAddress = taxCompany.AddressBuyerVienamese;
                    string companyTax = taxCompany.TaxVietnamese;

                    report.lblHeader.Text = $"{companyName}\n{companyAddress}\n{companyTax}";
                }
            }

            report.CreateDocument();
            documentViewer1.DocumentSource = report;
            documentViewer1.Refresh();
        }

        private void LoadPOReportEnglish(bool isShowSign, bool isShowSeal, object dataSource)
        {
            PONCCReportEnglish report = new PONCCReportEnglish();
            //documentViewer1.DocumentSource = report;

            //List<PONCCDetailDTO> listDetail = SQLHelper<PONCCDetailDTO>.ProcedureToList("spGetPONCCDetail", new string[] { "@PONCCID" }, new object[] { poId });
            //report.DataSource = listDetail;
            report.DataSource = dataSource;

            PONCCDTO po = SQLHelper<PONCCDTO>.ProcedureToList("spGetPONCCByID", new string[] { "@ID" }, new object[] { poId }).FirstOrDefault();
            if (po == null) return;

            //report.GroupHeader2.Visible = po.CompanyText.Trim().ToUpper() == "RTC";
            report.imgLogo.ImageUrl = $"{imageURL}/Logo{po.CompanyText.Trim().ToUpper()}.jpg";
            report.Parameters["POCode"].Value = po.POCode;
            report.Parameters["NameNCC"].Value = po.NameNCC;
            report.Parameters["AddressNCC"].Value = po.AddressNCC;
            report.Parameters["RequestDate"].Value = po.RequestDate.Value.ToString("dd/MM/yyyy");
            report.Parameters["BillCode"].Value = po.BillCode;
            report.Parameters["CurrencyText"].Value = po.CurrencyText.ToUpper().Trim();
            report.Parameters["MaSoThue"].Value = po.MaSoThue;
            report.Parameters["PhoneNCC"].Value = po.SupplierContactPhone;
            report.Parameters["SupplierContactPhone"].Value = po.SupplierContactPhone;
            report.Parameters["Fax"].Value = po.Fax;
            report.Parameters["SupplierContactName"].Value = po.SupplierContactName;
            report.Parameters["SupplierContactEmail"].Value = po.SupplierContactEmail;
            report.Parameters["Code"].Value = po.Code;


            TaxCompanyModel taxCompany = SQLHelper<TaxCompanyModel>.FindByAttribute("Code", po.CompanyText.ToUpper().Trim()).FirstOrDefault() ?? new TaxCompanyModel();

            if (po.Company > 0)
            {
                //var infoBuyer = listInfoBuyers[TextUtils.ToInt(po.Company) - 1];
                //if (infoBuyer != null)
                if (taxCompany.ID > 0)
                {
                    //var type = infoBuyer.GetType();
                    //report.Parameters["Buyer"].Value = TextUtils.ToString(type.GetProperty("Buyer").GetValue(infoBuyer));
                    //report.Parameters["AddressBuyer"].Value = TextUtils.ToString(type.GetProperty("AddressBuyer").GetValue(infoBuyer));
                    //report.Parameters["LegalRepresentative"].Value = TextUtils.ToString(type.GetProperty("LegalRepresentative").GetValue(infoBuyer));

                    report.Parameters["Buyer"].Value = taxCompany.BuyerEnglish;
                    report.Parameters["AddressBuyer"].Value = taxCompany.AddressBuyerEnglish;
                    report.Parameters["LegalRepresentative"].Value = taxCompany.LegalRepresentativeEnglish;
                }
            }

            string fullName = NumberMoneyToText.ConvertVietnameseToEnglish(po.FullName);


            //taxCompany = taxCompany ?? new TaxCompanyModel();
            var exp1 = new Expression("EmployeeID", po.EmployeeID);
            var exp2 = new Expression("TaxCompayID", taxCompany.ID);
            EmployeePurchaseModel employeePurchase = SQLHelper<EmployeePurchaseModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            employeePurchase = employeePurchase ?? new EmployeePurchaseModel();


            //report.Parameters["Purchaser"].Value = $"{fullName} - Phone: {ConvertPhoneNumberVietnamese(po.SDTCaNhan)} - Email: {po.EmailCongTy}";
            report.Parameters["Purchaser"].Value = $"{fullName} - Phone: {ConvertPhoneNumberVietnamese(employeePurchase.Telephone)} - Email: {employeePurchase.Email}";
            report.Parameters["OriginItem"].Value = po.OriginItem;
            report.Parameters["RuleIncoterm"].Value = po.RuleIncoterm;
            report.Parameters["TotalAmountText"].Value = NumberMoneyToText.ConvertNumberToTextEnglish(TextUtils.ToDecimal(po.TotalMoneyPO), po.CurrencyText);
            //report.Parameters["TotalAmountText"].Value = NumberMoneyToText.ConvertAmount((double)po.TotalMoneyPO);


            report.Parameters["DeliveryDate"].Value = po.DeliveryDate.Value.ToString("dd/MM/yyyy");
            report.Parameters["AddressDelivery"].Value = po.AddressDelivery;
            report.Parameters["RulePay"].Value = po.RulePayName;
            report.Parameters["BankCharge"].Value = po.BankCharge;
            report.Parameters["FedexAccount"].Value = po.FedexAccount;
            report.Parameters["BankAccount"].Value = po.AccountNumberSupplier;
            //report.Parameters["ShippingPoint"].Value = po.ShippingPoint;

            report.picPrepared.ImageUrl = $"{imageURL}/{po.Code.Trim()}.png";
            report.picDirector.ImageUrl = $"{imageURL}/seal{po.CompanyText.Trim().ToUpper()}.png";

            report.picPrepared.Visible = isShowSign;
            report.picDirector.Visible = isShowSeal;

            report.CreateDocument();
            documentViewer1.DocumentSource = report;
            documentViewer1.Refresh();

        }


        private string ConvertPhoneNumberVietnamese(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return "";
            List<string> listPhoneNumber = new List<string>();
            string[] phoneNumbers = phoneNumber.Split('\n');
            foreach (string number in phoneNumbers)
            {
                if (string.IsNullOrEmpty(number.Trim())) continue;
                string phone = "(+84) " + number.Substring(1);
                listPhoneNumber.Add(phone);
            }

            return string.Join("\n", listPhoneNumber);
        }

        private void frmPONCCViewDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void zoomTrackBarEditItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbiThumbnails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnShowSign_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////var report = documentViewer1.DocumentSource;

            //try
            //{
            //    //var report = documentViewer1.DocumentSource;

            //    //if (report == null) return;


            //    //var groupFooter = report.GetType().GetProperty("GroupFooter3");

            //    //PONCCReportVietnamese reportVN = (PONCCReportVietnamese)documentViewer1.DocumentSource;

            //    //var controls = reportVN();

            //    //string imgaeUrl = report.picPrepared.ImageUrl;
            //    //if (btnShowSign.Checked) imgaeUrl = "";
            //    //report.picPrepared.ImageUrl = imgaeUrl;


            //    if (type == 2)
            //    {
            //        LoadPOReportEnglish(btnShowSign.Checked, btnShowSeal.Checked);

            //        //PONCCReportEnglish report = (PONCCReportEnglish)documentViewer1.DocumentSource;
            //        //if (report == null) return;

            //        //report.picPrepared.Visible = !btnShowSign.Checked;
            //    }
            //    else
            //    {
            //        PONCCReportVietnamese report = (PONCCReportVietnamese)documentViewer1.DocumentSource;
            //        if (report == null) return;

            //        report.picPrepared.Visible = !btnShowSign.Checked;
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.ToString(), "Thông báo");
            //}
        }

        private void btnShowSeal_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //try
            //{

            //    if (type == 2)
            //    {
            //        LoadPOReportEnglish(btnShowSign.Checked, btnShowSeal.Checked);
            //        //PONCCReportEnglish report = (PONCCReportEnglish)documentViewer1.DocumentSource;
            //        //if (report == null) return;

            //        //report.picDirector.Visible = !btnShowSeal.Checked;

            //        //PONCCReportEnglish report = (PONCCReportEnglish)documentViewer1.DocumentSource;
            //        //if (report == null) return;

            //        //report.picDirector.Visible = !btnShowSeal.Checked;
            //    }
            //    else
            //    {
            //        PONCCReportVietnamese report = (PONCCReportVietnamese)documentViewer1.DocumentSource;
            //        if (report == null) return;
            //        report.picDirector.Visible = !btnShowSeal.Checked;

            //        documentViewer1.Refresh();
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.ToString(), "Thông báo");
            //}
        }

        private void btnMergeProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //LoadPOReportVietNamese(isShowSign, isShowSeal, listDetail);
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barCheckItem1.Checked)
            {
                //listDetail = SQLHelper<PONCCDetailDTO>.ProcedureToList("spGetPONCCDetail", new string[] { "@PONCCID" }, new object[] { poId });


                bool isHCNS = listDetail.Any(x => x.ProductGroupID == 77);
                if (isHCNS)
                {
                    listDetail = listDetail.GroupBy(item => new { item.ProductCodeOfSupplier, item.UnitPrice })
                                                                        .Select(cl => new PONCCDetailDTO
                                                                        {
                                                                            STT = cl.First().Status,
                                                                            ProductCodeOfSupplier = cl.First().ProductCodeOfSupplier,
                                                                            Unit = cl.First().Unit,
                                                                            UnitName = cl.First().UnitName,
                                                                            QtyRequest = cl.Sum(q => q.QtyRequest),
                                                                            UnitPrice = cl.First().UnitPrice,
                                                                            ThanhTien = cl.Sum(q => q.ThanhTien),
                                                                            VAT = cl.First().VAT,
                                                                            VATMoney = cl.Sum(q => q.VATMoney),
                                                                            Discount = cl.Sum(q => q.Discount),
                                                                            TotalPrice = cl.Sum(q => q.TotalPrice),
                                                                        })
                                                                        .Select((item, index) =>
                                                                        {
                                                                            item.STT = index + 1;
                                                                            return item;
                                                                        }).ToList();
                }
                else
                {
                    listDetail = listDetail.GroupBy(item => new { item.ProductCode, item.UnitPrice.Value, item.ProductCodeOfSupplier })
                                                                        .Select(cl => new PONCCDetailDTO
                                                                        {
                                                                            STT = cl.First().Status,
                                                                            ProductCodeOfSupplier = cl.First().ProductCodeOfSupplier,
                                                                            Unit = cl.First().Unit,
                                                                            UnitName = cl.First().UnitName,
                                                                            QtyRequest = cl.Sum(q => q.QtyRequest),
                                                                            UnitPrice = cl.First().UnitPrice,
                                                                            ThanhTien = cl.Sum(q => q.ThanhTien),
                                                                            VAT = cl.First().VAT,
                                                                            VATMoney = cl.Sum(q => q.VATMoney),
                                                                            Discount = cl.Sum(q => q.Discount),
                                                                            TotalPrice = cl.Sum(q => q.TotalPrice),
                                                                        })
                                                                        .Select((item, index) =>
                                                                        {
                                                                            item.STT = index + 1;
                                                                            return item;
                                                                        }).ToList();

                }
            }
            else
            {
                listDetail = SQLHelper<PONCCDetailDTO>.ProcedureToList("spGetPONCCDetail", new string[] { "@PONCCID" }, new object[] { poId });
            }

            LoadData();
        }
    }
}
