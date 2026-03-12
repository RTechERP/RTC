using BaseBusiness.DTO;
using BMS.Model;
using DevExpress.XtraPrinting;
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
    public partial class frmJobRequirementPrint : _Forms
    {
        //public int jobRequirementID = 0;
        public DataRow data;
        public frmJobRequirementPrint()
        {
            InitializeComponent();
        }

        private void frmJobRequirementPrint_Load(object sender, EventArgs e)
        {
            
        }

        private void documentViewer1_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        void LoadData()
        {
            rpJobRequirement report = new rpJobRequirement();
            documentViewer1.DocumentSource = report;

            int jobRequirementID = TextUtils.ToInt(data["ID"]);

            List<JobRequirementDetailModel> listDetail = SQLHelper<JobRequirementDetailModel>.FindByAttribute("JobRequirementID", jobRequirementID);
            report.DataSource = listDetail;

            foreach (var item in report.Parameters)
            {
                if (!data.Table.Columns.Contains(item.Name)) continue;
                item.Value = ": " + TextUtils.ToString(data[item.Name]);
            }


            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetJobRequirementDetail",
                                                   new string[] { "@JobRequirementID" },
                                                   new object[] { jobRequirementID });

            //DataTable dtDetails = dataSet.Tables[0];
            DataTable dtApproved = dataSet.Tables[1];
            DataTable dtFiles = dataSet.Tables[2];

            string attachFiles = string.Join("\n", dtFiles.AsEnumerable().Select(x => x.Field<string>("FilePath")));
            report.Parameters["AttachFile"].Value = attachFiles;

            DataRow dataTBPApproved = dtApproved.Select("Step = 2 AND IsApproved = 1").FirstOrDefault();
            DataRow dataHRApproved = dtApproved.Select("Step = 4 AND IsApproved = 1").FirstOrDefault();
            DataRow dataBGDApproved = dtApproved.Select("Step = 5 AND IsApproved = 1").FirstOrDefault();

            if (dataTBPApproved != null)
            {
                report.Parameters["DateApprovedTBP"].Value = TextUtils.ToDate5(dataTBPApproved["DateApproved"]).ToString("dd/MM/yyyy");
                report.Parameters["ApprovedTBP"].Value = TextUtils.ToString(dataTBPApproved["EmployeeActualName"]);
            }

            if (dataHRApproved != null)
            {
                report.Parameters["DateApprovedHR"].Value = TextUtils.ToDate5(dataHRApproved["DateApproved"]).ToString("dd/MM/yyyy");
                report.Parameters["ApprovedHR"].Value = TextUtils.ToString(dataHRApproved["EmployeeActualName"]);
            }

            if (dataBGDApproved != null)
            {
                report.Parameters["DateApprovedBGD"].Value = TextUtils.ToDate5(dataBGDApproved["DateApproved"]).ToString("dd/MM/yyyy");
                report.Parameters["ApprovedBGD"].Value = TextUtils.ToString(dataBGDApproved["EmployeeActualName"]);
            }

        }
    }
}
