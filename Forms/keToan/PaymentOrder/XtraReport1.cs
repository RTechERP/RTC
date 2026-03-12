using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace BMS
{
    public partial class xtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public xtraReport1()
        {
            InitializeComponent();

        }

        private void XtraReport1_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            LoadData();
        }


        void LoadData()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetPaymentOrderByID", new string[] { "@ID" }, new object[] { 1 });
            DataTable dtOrder = dataSet.Tables[0];
            DataTable dtDetail = dataSet.Tables[1];
            DataTable dtSign = dataSet.Tables[2];

            if (dtOrder.Rows.Count <= 0) return;

            lblTitle.Text = $"GIẤY {TextUtils.ToString(dtOrder.Rows[0]["TypeOrderText"]).ToUpper()}";
            lblDate.Text = $"Ngày {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Day} tháng {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Month} năm {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Year}";
            lblFullName.Text = $": {TextUtils.ToString(dtOrder.Rows[0]["FullName"])}";
            lblDepartment.Text = $": {TextUtils.ToString(dtOrder.Rows[0]["DepartmentName"])}"; ;
            lblReasonOrder.Text = $": {TextUtils.ToString(dtOrder.Rows[0]["ReasonOrder"])}"; ;
            lblReceiverInfo.Text = $": {TextUtils.ToString(dtOrder.Rows[0]["ReceiverInfo"])}";
            chkTypePayment1.Checked = TextUtils.ToInt(dtOrder.Rows[0]["TypePayment"]) == 1;
            chkTypePayment2.Checked = TextUtils.ToInt(dtOrder.Rows[0]["TypePayment"]) == 2;
            lblAccountNumber.Text = $": {TextUtils.ToString(dtOrder.Rows[0]["AccountNumber"])}";
            lblBank.Text = $": {TextUtils.ToString(dtOrder.Rows[0]["Bank"])}";
            lblUnit.Text = $"ĐVT: {TextUtils.ToString(dtOrder.Rows[0]["Unit"])}";

            bool isIgnoreHR = TextUtils.ToBoolean(dtOrder.Rows[0]["IsIgnoreHR"]);
            var signEmployee = dtSign.Select("Step = 1 and IsApproved = 1");
            var signTBP = dtSign.Select("Step = 2 and IsApproved = 1");
            var signHR = isIgnoreHR ? null : dtSign.Select("Step = 3 and IsApproved = 1");
            var signKT = isIgnoreHR ? dtSign.Select("Step = 4 and IsApproved = 1") : dtSign.Select("Step = 5 and IsApproved = 1");
            var signBGĐ = isIgnoreHR ? dtSign.Select("Step = 5 and IsApproved = 1") : dtSign.Select("Step = 6 and IsApproved = 1");

            lblEmployeeSign.Text = signEmployee.Length <= 0 ? "" : $"{TextUtils.ToString(signEmployee[0]["FullName"])}\n{TextUtils.ToDate5(signEmployee[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            lblTBPSign.Text = signTBP.Length <= 0 ? "" : $"{TextUtils.ToString(signTBP[0]["FullName"])}\n{TextUtils.ToDate5(signTBP[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            lblHRSign.Text = signHR.Length <= 0 ? "" : $"{TextUtils.ToString(signHR[0]["FullName"])}\n{TextUtils.ToDate5(signHR[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            lblKTSign.Text = signKT.Length <= 0 ? "" : $"{TextUtils.ToString(signKT[0]["FullName"])}\n{TextUtils.ToDate5(signKT[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
            lblBGĐSign.Text = signBGĐ.Length <= 0 ? "" : $"{TextUtils.ToString(signBGĐ[0]["FullName"])}\n{TextUtils.ToDate5(signBGĐ[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";

            xrTable1.DataBindings.Add(new XRBinding());
        }
    }
}
