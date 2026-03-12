using BMS.Model;
using DevExpress.Utils;
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
    public partial class frmPaymentOrderViewDetail : _Forms
    {
        int id = 0;
        //bool isSpe = false;
        public frmPaymentOrderViewDetail(int id = 0)
        {
            InitializeComponent();
            this.id = id;
            //this.isSpe = isSpe;
        }

        private void frmPaymentOrderViewDetail_Load(object sender, EventArgs e)
        {
            using (WaitDialogForm waitDialog = new WaitDialogForm())
            {
                //PaymentOrderReport report = new PaymentOrderReport();
                //report.id = id;
                //reportDesigner1.OpenReport(report);
                //documentViewer1.DocumentSource = report;
            }
        }

        private void documentViewer1_Load(object sender, EventArgs e)
        {
            PaymentOrderModel payment = SQLHelper<PaymentOrderModel>.FindByID(id);
            if (TextUtils.ToBoolean(payment.IsSpecialOrder)) LoadPaymentOrderSpePrint();
            else LoadPaymentOrderPrint();

            //PaymentOrderReport report = new PaymentOrderReport();
            //documentViewer1.DocumentSource = report;
            //report.RequestParameters = false;
            ////report.Parameters["idOrder"].Value = id;
            //LoadData(report);
            //SetWidthPageFooterElement(report);
        }

        private void LoadPaymentOrderPrint()
        {
            PaymentOrderReport report = new PaymentOrderReport();
            documentViewer1.DocumentSource = report;
            report.RequestParameters = false;
            LoadData(report);
            SetWidthPageFooterElement(report);
        }
        private void LoadPaymentOrderSpePrint()
        {
            PaymentOrderSpeReport report = new PaymentOrderSpeReport();
            documentViewer1.DocumentSource = report;
            report.RequestParameters = false;
            LoadDataSpe(report);
            SetWidthPageFooterElementSpe(report);
        }

        void LoadData(PaymentOrderReport report)
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetPaymentOrderByID", new string[] { "@ID" }, new object[] { id });
            DataTable dtOrder = dataSet.Tables[0];
            //DataTable dtDetail = dataSet.Tables[1];
            DataTable dtSign = dataSet.Tables[2];

            if (dtOrder.Rows.Count <= 0) return;

            int typeOrder = TextUtils.ToInt(dtOrder.Rows[0]["TypeOrder"]);
            int typePaymentOrder = TextUtils.ToInt(dtOrder.Rows[0]["TypePayment"]);
            string nameNCC = TextUtils.ToString(dtOrder.Rows[0]["NameNCC"]);
            string POCode = TextUtils.ToString(dtOrder.Rows[0]["POCode"]);

            report.GroupFooter1.Visible = typeOrder == 1;
            //report.lblDatePaymentTitle.Visible = typeOrder == 1;
            //report.lblDatePayment.Visible = typeOrder == 1;
            report.GroupHeader3.Visible = typeOrder == 1;
            report.GroupHeader4.Visible = typePaymentOrder == 1;
            //if(string.IsNullOrEmpty(nameNCC) == true && string.IsNullOrEmpty(POCode) == true)
            //{
            //    report.GroupHeader6.Visible = false;
            //}
            //else
            //{
            //    report.GroupHeader6.Visible = true;
            //}

            report.GroupHeader6.Visible = !(string.IsNullOrEmpty(nameNCC) == true && string.IsNullOrEmpty(POCode) == true);

            report.LoadLable();

            string numberDocument = typeOrder == 1 ? "BM01-RTC.AC-QT03" : "BM02-RTC.AC-QT03";
            string totalMoneyText = TextUtils.ToString(dtOrder.Rows[0]["TotalMoneyText"]);
            totalMoneyText = totalMoneyText[0].ToString().ToUpper() + totalMoneyText.Substring(1);

            report.Parameters["NumberDocument"].Value = $"Mã số: {numberDocument}\nLần ban hành: 01\nTrang: 1";
            report.Parameters["PayementOrderTitle"].Value = $"GIẤY {TextUtils.ToString(dtOrder.Rows[0]["TypeOrderText"]).ToUpper()}";
            report.Parameters["DateOrder"].Value = $"Ngày {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Day} tháng {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Month} năm {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Year}";
            report.Parameters["FullName"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["FullName"])}";
            report.Parameters["DepartmentName"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["DepartmentName"])}";
            report.Parameters["ReasonOrder"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["ReasonOrder"])}";
            report.Parameters["ReceiverInfo"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["ReceiverInfo"])}";
            report.Parameters["DatePayment"].Value = $": Ngày {TextUtils.ToDate5(dtOrder.Rows[0]["DatePayment"]).Day} tháng {TextUtils.ToDate5(dtOrder.Rows[0]["DatePayment"]).Month} năm {TextUtils.ToDate5(dtOrder.Rows[0]["DatePayment"]).Year}";
            report.Parameters["TypePayment1"].Value = TextUtils.ToInt(dtOrder.Rows[0]["TypePayment"]) == 1;
            report.Parameters["TypePayment2"].Value = TextUtils.ToInt(dtOrder.Rows[0]["TypePayment"]) == 2;
            report.Parameters["AccountNumber"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["AccountNumber"])}";
            report.Parameters["Bank"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["Bank"])}";
            report.Parameters["Unit"].Value = $"{TextUtils.ToString(dtOrder.Rows[0]["Unit"]).ToUpper()}";
            report.Parameters["TotalMoneyText"].Value = $"Số tiền bằng chữ: {totalMoneyText}";
            report.Parameters["TypeBankTransferText"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["TypeBankTransferText"])}";
            report.Parameters["ContentBankTransfer"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["ContentBankTransfer"])}";
            report.Parameters["Code"].Value = $"Số {TextUtils.ToString(dtOrder.Rows[0]["Code"])}";
            report.Parameters["AccountingNote"].Value = $"{TextUtils.ToString(dtOrder.Rows[0]["AccountingNote"])}";
            report.Parameters["NameNCC"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["NameNCC"])}";
            report.Parameters["POCode"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["POCode"])}";
            report.Parameters["TypeOrder"].Value = dtOrder.Rows[0]["TypeOrder"];


            report.lblEmployeeSignTitle.Text = typeOrder == 1 ? "Người đề nghị tạm ứng" : "Người đề nghị thanh toán";
            bool isIgnoreHR = TextUtils.ToBoolean(dtOrder.Rows[0]["IsIgnoreHR"]);
            var signEmployee = dtSign.Select("Step = 1 and IsApproved = 1");

            DateTime date = new DateTime(2024, 03, 03).Date;
            DateTime dateOrder = TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Date;
            if (dateOrder <= date)
            {
                var signTBP = dtSign.Select("Step = 2 and IsApproved = 1");
                var signHR = isIgnoreHR ? null : dtSign.Select("Step = 3 and IsApproved = 1");
                var signKT = isIgnoreHR ? dtSign.Select("Step = 4 and IsApproved = 1") : dtSign.Select("Step = 5 and IsApproved = 1");
                var signBGĐ = isIgnoreHR ? dtSign.Select("Step = 5 and IsApproved = 1") : dtSign.Select("Step = 6 and IsApproved = 1");

                report.Parameters["EmployeeSign"].Value = signEmployee.Length <= 0 ? "" : $"{TextUtils.ToString(signEmployee[0]["FullName"])}\n{TextUtils.ToDate5(signEmployee[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                report.Parameters["TBPSign"].Value = signTBP.Length <= 0 ? "" : $"{TextUtils.ToString(signTBP[0]["FullName"])}\n{TextUtils.ToDate5(signTBP[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                report.Parameters["KTSign"].Value = signKT.Length <= 0 ? "" : $"{TextUtils.ToString(signKT[0]["FullName"])}\n{TextUtils.ToDate5(signKT[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                report.Parameters["BGDSign"].Value = signBGĐ.Length <= 0 ? "" : $"{TextUtils.ToString(signBGĐ[0]["FullName"])}\n{TextUtils.ToDate5(signBGĐ[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";

                report.lblHRSignTitle.Visible = !isIgnoreHR;
                report.lblHRSign.Visible = !isIgnoreHR;

                report.lblAccountingNoteTitle.HeightF = report.lblAccountingNote.HeightF;
                if (signHR == null || signHR.Length <= 0)
                {
                    report.Parameters["HRSign"].Value = "";
                }
                else
                {
                    report.Parameters["HRSign"].Value = $"{TextUtils.ToString(signHR[0]["FullName"])}\n{TextUtils.ToDate5(signHR[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                }
            }
            else
            {
                var signTBP = dtSign.Select("Step = 2 and IsApproved = 1");
                var signHR = isIgnoreHR ? null : dtSign.Select("Step = 4 and IsApproved = 1");
                var signKT = isIgnoreHR ? dtSign.Select("Step = 4 and IsApproved = 1") : dtSign.Select("Step = 6 and IsApproved = 1");
                var signBGĐ = isIgnoreHR ? dtSign.Select("Step = 5 and IsApproved = 1") : dtSign.Select("Step = 7 and IsApproved = 1");

                string dateEmployeeSign = "";
                string dateTBPSign = "";
                string dateKTSign = "";
                string dateBGDSign = "";

                if (signEmployee.Length > 0)
                {
                    dateEmployeeSign = TextUtils.ToDate4(signEmployee[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signEmployee[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
                }

                if (signTBP.Length > 0)
                {
                    dateTBPSign = TextUtils.ToDate4(signTBP[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signTBP[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
                }

                if (signKT.Length > 0)
                {
                    dateKTSign = TextUtils.ToDate4(signKT[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signKT[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
                }

                if (signBGĐ.Length > 0)
                {
                    dateBGDSign = TextUtils.ToDate4(signBGĐ[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signBGĐ[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
                }

                report.Parameters["EmployeeSign"].Value = signEmployee.Length <= 0 ? "" : $"{TextUtils.ToString(signEmployee[0]["FullName"])}\n{dateEmployeeSign}";
                report.Parameters["TBPSign"].Value = signTBP.Length <= 0 ? "" : $"{TextUtils.ToString(signTBP[0]["FullName"])}\n{dateTBPSign}";
                report.Parameters["KTSign"].Value = signKT.Length <= 0 ? "" : $"{TextUtils.ToString(signKT[0]["FullName"])}\n{dateKTSign}";
                report.Parameters["BGDSign"].Value = signBGĐ.Length <= 0 ? "" : $"{TextUtils.ToString(signBGĐ[0]["FullName"])}\n{dateBGDSign}";

                report.lblHRSignTitle.Visible = !isIgnoreHR;
                report.lblHRSign.Visible = !isIgnoreHR;

                report.lblAccountingNoteTitle.HeightF = report.lblAccountingNote.HeightF;
                if (signHR == null || signHR.Length <= 0)
                {
                    report.Parameters["HRSign"].Value = "";
                }
                else
                {
                    string dateHRSign = TextUtils.ToDate4(signHR[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signHR[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
                    report.Parameters["HRSign"].Value = $"{TextUtils.ToString(signHR[0]["FullName"])}\n{dateHRSign}";
                }
            }


            List<PaymentOrderDetailModel> details = SQLHelper<PaymentOrderDetailModel>.FindByAttribute("PaymentOrderID", id).OrderBy(x => x.ID).ToList();
            foreach (var item in details)
            {
                item.ContentPayment = item.ContentPayment.Replace("&nbsp;", "");
                item.Note = item.Note.Replace("&nbsp;", "");
            }
            report.DataSource = details;
        }

        private void LoadDataSpe(PaymentOrderSpeReport report)
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetPaymentOrderByID", new string[] { "@ID" }, new object[] { id });
            DataTable dtOrder = dataSet.Tables[0];
            DataTable dtDetail = dataSet.Tables[1];
            DataTable dtSign = dataSet.Tables[2];

            if (dtOrder.Rows.Count <= 0) return;

            int typeOrder = TextUtils.ToInt(dtOrder.Rows[0]["TypeOrder"]);
            int typePaymentOrder = TextUtils.ToInt(dtOrder.Rows[0]["TypePayment"]);
            string nameNCC = TextUtils.ToString(dtOrder.Rows[0]["NameNCC"]);
            string POCode = TextUtils.ToString(dtOrder.Rows[0]["POCode"]);

            string numberDocument = typeOrder == 1 ? "BM01-RTC.AC-QT03" : "BM02-RTC.AC-QT03";
            string totalMoneyText = TextUtils.ToString(dtOrder.Rows[0]["TotalMoneyText"]);
            totalMoneyText = totalMoneyText[0].ToString().ToUpper() + totalMoneyText.Substring(1);

            report.Parameters["NumberDocument"].Value = $"Mã số: {numberDocument}\nLần ban hành: 01\nTrang: 1";
            report.Parameters["PayementOrderTitle"].Value = $"GIẤY {TextUtils.ToString(dtOrder.Rows[0]["TypeOrderText"]).ToUpper()}";
            report.Parameters["DateOrder"].Value = $"Ngày {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Day} tháng {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Month} năm {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Year}";
            report.Parameters["FullName"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["FullName"])}";
            report.Parameters["ReasonOrder"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["ReasonOrder"])}"; ;
            //report.Parameters["ReceiverInfo"].Value = $": {TextUtils.ToString(dtOrder.Rows[0]["ReceiverInfo"])}";
            report.Parameters["Customers"].Value = dtOrder.Rows[0]["CustomerName"];
            //report.Parameters["TypePayment1"].Value = TextUtils.ToInt(dtOrder.Rows[0]["TypePayment"]) == 1;
            //report.Parameters["TypePayment2"].Value = TextUtils.ToInt(dtOrder.Rows[0]["TypePayment"]) == 2;

            report.Parameters["Unit"].Value = $"{TextUtils.ToString(dtOrder.Rows[0]["Unit"]).ToUpper()}";
            report.Parameters["TotalMoneyText"].Value = $"Số tiền bằng chữ: {totalMoneyText}";
            report.Parameters["Code"].Value = $"Số {TextUtils.ToString(dtOrder.Rows[0]["Code"])}"; 
            report.lblEmployeeSignTitle.Text = typeOrder == 1 ? "Người đề nghị tạm ứng" : "Người đề nghị thanh toán";




            //bool isIgnoreHR = TextUtils.ToBoolean(dtOrder.Rows[0]["IsIgnoreHR"]);
            //var signEmployee = dtSign.Select("Step = 1 and IsApproved = 1");
            //DateTime date = new DateTime(2024, 03, 03).Date;
            //DateTime dateOrder = TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Date;
            //if (dateOrder <= date)
            //{
            //    var signTBP = dtSign.Select("Step = 2 and IsApproved = 1");
            //    //var signHR = isIgnoreHR ? null : dtSign.Select("Step = 3 and IsApproved = 1");
            //    var signKT = isIgnoreHR ? dtSign.Select("Step = 3 and IsApproved = 1") : dtSign.Select("Step = 5 and IsApproved = 1");
            //    var signBGĐ = isIgnoreHR ? dtSign.Select("Step = 4 and IsApproved = 1") : dtSign.Select("Step = 6 and IsApproved = 1");

            //    report.Parameters["EmployeeSign"].Value = signEmployee.Length <= 0 ? "" : $"{TextUtils.ToString(signEmployee[0]["FullName"])}\n{TextUtils.ToDate5(signEmployee[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            //    report.Parameters["TBPSign"].Value = signTBP.Length <= 0 ? "" : $"{TextUtils.ToString(signTBP[0]["FullName"])}\n{TextUtils.ToDate5(signTBP[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            //    report.Parameters["KTSign"].Value = signKT.Length <= 0 ? "" : $"{TextUtils.ToString(signKT[0]["FullName"])}\n{TextUtils.ToDate5(signKT[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            //    report.Parameters["BGDSign"].Value = signBGĐ.Length <= 0 ? "" : $"{TextUtils.ToString(signBGĐ[0]["FullName"])}\n{TextUtils.ToDate5(signBGĐ[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";

            //    /*report.lblHRSignTitle.Visible = !isIgnoreHR;
            //    report.lblHRSign.Visible = !isIgnoreHR;*/

            //    report.lblAccountingNoteTitle.HeightF = report.lblAccountingNote.HeightF;

            //}
            //else
            //{
            //    var signTBP = dtSign.Select("Step = 2 and IsApproved = 1");
            //    var signHR = isIgnoreHR ? null : dtSign.Select("Step = 4 and IsApproved = 1");
            //    var signKT = isIgnoreHR ? dtSign.Select("Step = 4 and IsApproved = 1") : dtSign.Select("Step = 6 and IsApproved = 1");
            //    var signBGĐ = isIgnoreHR ? dtSign.Select("Step = 5 and IsApproved = 1") : dtSign.Select("Step = 7 and IsApproved = 1");

            //    string dateEmployeeSign = "";
            //    string dateTBPSign = "";
            //    string dateKTSign = "";
            //    string dateBGDSign = "";

            //    if (signEmployee.Length > 0)
            //    {
            //        dateEmployeeSign = TextUtils.ToDate4(signEmployee[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signEmployee[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
            //    }

            //    if (signTBP.Length > 0)
            //    {
            //        dateTBPSign = TextUtils.ToDate4(signTBP[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signTBP[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
            //    }

            //    if (signKT.Length > 0)
            //    {
            //        dateKTSign = TextUtils.ToDate4(signKT[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signKT[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
            //    }

            //    if (signBGĐ.Length > 0)
            //    {
            //        dateBGDSign = TextUtils.ToDate4(signBGĐ[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signBGĐ[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
            //    }

            //    /*report.lblHRSignTitle.Visible = !isIgnoreHR;
            //    report.lblHRSign.Visible = !isIgnoreHR;*/

            //    report.lblAccountingNoteTitle.HeightF = report.lblAccountingNote.HeightF;
            //    /* if (signHR == null || signHR.Length <= 0)
            //     {
            //         report.Parameters["HRSign"].Value = "";
            //     }
            //     else
            //     {
            //         string dateHRSign = TextUtils.ToDate4(signHR[0]["DateApproved"]).HasValue ? TextUtils.ToDate5(signHR[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm") : "";
            //         //report.Parameters["HRSign"].Value = $"{TextUtils.ToString(signHR[0]["FullName"])}\n{dateHRSign}";
            //     }*/
            //}
            //report.DataSource = dtDetail;



            //===================================================== lee min khooi update 28/08/2024 ======================================
            var signEmployee = dtSign.Select("Step = 1 and IsApproved = 1");
            var signTBP = dtSign.Select("Step = 2 and IsApproved = 1");
            var signKT = dtSign.Select("Step = 3 and IsApproved = 1");
            var signBGĐ = dtSign.Select("Step = 4 and IsApproved = 1");


            report.lblAccountingNoteTitle.HeightF = report.lblAccountingNote.HeightF;

            report.Parameters["EmployeeSign"].Value = signEmployee.Length <= 0 ? "" : $"{TextUtils.ToString(signEmployee[0]["FullName"])}\n{TextUtils.ToDate5(signEmployee[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            report.Parameters["TBPSign"].Value = signTBP.Length <= 0 ? "" : $"{TextUtils.ToString(signTBP[0]["FullName"])}\n{TextUtils.ToDate5(signTBP[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            report.Parameters["KTSign"].Value = signKT.Length <= 0 ? "" : $"{TextUtils.ToString(signKT[0]["FullName"])}\n{TextUtils.ToDate5(signKT[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            report.Parameters["BGDSign"].Value = signBGĐ.Length <= 0 ? "" : $"{TextUtils.ToString(signBGĐ[0]["FullName"])}\n{TextUtils.ToDate5(signBGĐ[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            report.DataSource = dtDetail;
            //===================================================== lee min khooi end update 28/08/2024 ======================================

        }

        void SetWidthPageFooterElement(PaymentOrderReport report)
        {
            int widthReport = report.PageWidth;
            float withPageFooter = report.PageWidth - report.Margins.Left - report.Margins.Right; 
            float withLable = 0;
            if (report.lblHRSign.Visible)
            {
                withLable = withPageFooter / 5;
                report.lblEmployeeSignTitle.WidthF = withLable;
                report.lblEmployeeSign.WidthF = withLable;

                report.lblTBPSignTitle.WidthF = withLable;
                report.lblTBPSign.WidthF = withLable;
                report.lblTBPSignTitle.LocationF = new PointF(withLable, 0);
                report.lblTBPSign.LocationF = new PointF(withLable, report.lblEmployeeSignTitle.HeightF);

                report.lblHRSignTitle.WidthF = withLable;
                report.lblHRSign.WidthF = withLable;
                report.lblHRSignTitle.LocationF = new PointF(withLable * 2, 0);
                report.lblHRSign.LocationF = new PointF(withLable * 2, report.lblEmployeeSignTitle.HeightF);

                report.lblKTSignTitle.WidthF = withLable;
                report.lblKTSign.WidthF = withLable;
                report.lblKTSignTitle.LocationF = new PointF(withLable * 3, 0);
                report.lblKTSign.LocationF = new PointF(withLable * 3, report.lblEmployeeSignTitle.HeightF);

                report.lblBGĐSignTitle.WidthF = withLable;
                report.lblBGĐSign.WidthF = withLable;
                report.lblBGĐSignTitle.LocationF = new PointF(withLable * 4, 0);
                report.lblBGĐSign.LocationF = new PointF(withLable * 4, report.lblEmployeeSignTitle.HeightF);
            }
            else
            {
                withLable = withPageFooter / 4;
                report.lblEmployeeSignTitle.WidthF = withLable;
                report.lblEmployeeSign.WidthF = withLable;

                report.lblTBPSignTitle.WidthF = withLable;
                report.lblTBPSign.WidthF = withLable;
                report.lblTBPSignTitle.LocationF = new PointF(withLable, 0);
                report.lblTBPSign.LocationF = new PointF(withLable, report.lblEmployeeSignTitle.HeightF);

                report.lblKTSignTitle.WidthF = withLable;
                report.lblKTSign.WidthF = withLable;
                report.lblKTSignTitle.LocationF = new PointF(withLable * 2, 0);
                report.lblKTSign.LocationF = new PointF(withLable * 2, report.lblEmployeeSignTitle.HeightF);

                report.lblBGĐSignTitle.WidthF = withLable;
                report.lblBGĐSign.WidthF = withLable;
                report.lblBGĐSignTitle.LocationF = new PointF(withLable * 3, 0);
                report.lblBGĐSign.LocationF = new PointF(withLable * 3, report.lblEmployeeSignTitle.HeightF);
            }


        }

        void SetWidthPageFooterElementSpe(PaymentOrderSpeReport report)
        {
            int widthReport = report.PageWidth;
            float withPageFooter = report.PageWidth - report.Margins.Left - report.Margins.Right;
            float withLable = 0;
            /*if (report.lblHRSign.Visible)
            {
                withLable = withPageFooter / 5;
                report.lblEmployeeSignTitle.WidthF = withLable;
                report.lblEmployeeSign.WidthF = withLable;

                report.lblTBPSignTitle.WidthF = withLable;
                report.lblTBPSign.WidthF = withLable;
                report.lblTBPSignTitle.LocationF = new PointF(withLable, 0);
                report.lblTBPSign.LocationF = new PointF(withLable, report.lblEmployeeSignTitle.HeightF);

                report.lblHRSignTitle.WidthF = withLable;
                report.lblHRSign.WidthF = withLable;
                report.lblHRSignTitle.LocationF = new PointF(withLable * 2, 0);
                report.lblHRSign.LocationF = new PointF(withLable * 2, report.lblEmployeeSignTitle.HeightF);

                report.lblKTSignTitle.WidthF = withLable;
                report.lblKTSign.WidthF = withLable;
                report.lblKTSignTitle.LocationF = new PointF(withLable * 3, 0);
                report.lblKTSign.LocationF = new PointF(withLable * 3, report.lblEmployeeSignTitle.HeightF);

                report.lblBGĐSignTitle.WidthF = withLable;
                report.lblBGĐSign.WidthF = withLable;
                report.lblBGĐSignTitle.LocationF = new PointF(withLable * 4, 0);
                report.lblBGĐSign.LocationF = new PointF(withLable * 4, report.lblEmployeeSignTitle.HeightF);
            }
            else
            {*/
            withLable = withPageFooter / 4;
            report.lblEmployeeSignTitle.WidthF = withLable;
            report.lblEmployeeSign.WidthF = withLable;

            report.lblTBPSignTitle.WidthF = withLable;
            report.lblTBPSign.WidthF = withLable;
            report.lblTBPSignTitle.LocationF = new PointF(withLable, 0);
            report.lblTBPSign.LocationF = new PointF(withLable, report.lblEmployeeSignTitle.HeightF);

            report.lblKTSignTitle.WidthF = withLable;
            report.lblKTSign.WidthF = withLable;
            report.lblKTSignTitle.LocationF = new PointF(withLable * 2, 0);
            report.lblKTSign.LocationF = new PointF(withLable * 2, report.lblEmployeeSignTitle.HeightF);

            report.lblBGĐSignTitle.WidthF = withLable;
            report.lblBGĐSign.WidthF = withLable;
            report.lblBGĐSignTitle.LocationF = new PointF(withLable * 3, 0);
            report.lblBGĐSign.LocationF = new PointF(withLable * 3, report.lblEmployeeSignTitle.HeightF);
            //}


        }

        private void frmPaymentOrderViewDetail_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void frmPaymentOrderViewDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
