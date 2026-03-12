using DevExpress.Utils;
using BMS.Utils;
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
    public partial class frmCopyKPI : _Forms
    {
        public int year, quarter;
        public List<int> lsID = new List<int>();
        public frmCopyKPI()
        {
            InitializeComponent();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang sao chép dữ liệu..."))
            {

                CopyCriteria();
                MessageBox.Show("Dữ liệu đã được copy!", "Thông báo", MessageBoxButtons.OK);
                this.Close();
                return;


                if (txtQuarter.Value == quarter && txtYear.Value == year)
                {
                    MessageBox.Show($"Bạn không thể sao chép quý [{quarter}-{year}] vào chính nó!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                DataTable dt = TextUtils.LoadDataFromSP("spGetKPICriteria", "A",
                    new string[] { "@KPICriteriaQuater", "@KPICriteriaYear", "@Keyword" },
                    new object[] { txtQuarter.Value, txtYear.Value, "" });

                if (dt.Rows.Count == 0)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn sao chép quý [{quarter}-{year}] sang quý [{txtQuarter.Value}-{txtYear.Value}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CopyDataKPI();
                    }

                }
                else
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn ghi đè quý [{quarter}-{year}] vào quý [{txtQuarter.Value}-{txtYear.Value}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            int id = TextUtils.ToInt(item["ID"]);
                            KPICriteriaModel model = SQLHelper<KPICriteriaModel>.FindByID(id);
                            if (model.ID > 0)
                            {
                                model.IsDeleted = true;
                                SQLHelper<KPICriteriaModel>.Update(model);
                            }
                        }
                        CopyDataKPI();
                    }
                }
                MessageBox.Show("Dữ liệu đã được copy!", "Thông báo", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void frmCopyKPI_Load(object sender, EventArgs e)
        {

        }

        private void CopyDataKPI()
        {
            foreach (int id in lsID)
            {
                var md = SQLHelper<KPICriteriaModel>.FindByID(id);
                KPICriteriaModel model = new KPICriteriaModel();

                if (md != null)
                {
                    model.STT = md.STT;
                    model.CriteriaCode = md.CriteriaCode;
                    model.CriteriaName = md.CriteriaName;
                    model.KPICriteriaQuater = TextUtils.ToInt(txtQuarter.Value);
                    model.KPICriteriaYear = TextUtils.ToInt(txtYear.Value);
                    int idm = SQLHelper<KPICriteriaModel>.Insert(model).ID;

                    List<KPICriteriaDetailModel> mdd = SQLHelper<KPICriteriaDetailModel>.FindByAttribute("KPICriteriaID", md.ID);
                    if (mdd.Count > 0)
                    {
                        foreach (var mddc in mdd)
                        {
                            KPICriteriaDetailModel KPIDetail = new KPICriteriaDetailModel();
                            KPIDetail.KPICriteriaID = idm;
                            KPIDetail.STT = mddc.STT;
                            KPIDetail.Point = mddc.Point;
                            KPIDetail.PointPercent = mddc.PointPercent;
                            KPIDetail.CriteriaContent = mddc.CriteriaContent;
                            SQLHelper<KPICriteriaDetailModel>.Insert(KPIDetail);
                        }
                    }
                }
            }
        }

        private void CopyCriteria()
        {
            var epr1 = new Expression("YearEvaluation", year);
            var epr2 = new Expression("QuarterEvaluation", quarter);
            var epr3 = new Expression("IsDeleted", 0);
            var epr4 = new Expression(KPISessionModel_Enum.DepartmentID, 2);

            KPISessionModel session = SQLHelper<KPISessionModel>.FindByExpression(epr1.And(epr2).And(epr3).And(epr4)).FirstOrDefault();
            if (session == null) return;


            // Xoa du lieu cu
            Expression ex4 = new Expression(KPICriteriaModel_Enum.KPICriteriaQuater, txtQuarter.Value);
            Expression ex5 = new Expression(KPICriteriaModel_Enum.KPICriteriaYear, txtYear.Value);
            List<KPICriteriaModel> lstCriteriaOld = SQLHelper<KPICriteriaModel>.FindByExpression(ex4.And(ex5));

            foreach (KPICriteriaModel item in lstCriteriaOld)
            {
                SQLHelper<KPICriteriaDetailModel>.DeleteByAttribute("KPICriteriaID", item.ID);
                var mydict = new Dictionary<string, object>() {
                    { KPICriteriaModel_Enum.IsDeleted.ToString(), true },
                    {  KPICriteriaModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                    {  KPICriteriaModel_Enum.UpdatedBy.ToString(), Global.LoginName }
                };
                SQLHelper<KPICriteriaModel>.UpdateFieldsByID(mydict, item.ID);
            }


            // copy dữ liệu từ năm chọn sang quý và năm mới
            Expression ex1 = new Expression(KPICriteriaModel_Enum.IsDeleted, 0);
            Expression ex2 = new Expression(KPICriteriaModel_Enum.KPICriteriaQuater, session.QuarterEvaluation);
            Expression ex3 = new Expression(KPICriteriaModel_Enum.KPICriteriaYear, session.YearEvaluation);
            List<KPICriteriaModel> lstData = SQLHelper<KPICriteriaModel>.FindByExpression(ex1.And(ex2).And(ex3));
            foreach (KPICriteriaModel item in lstData)
            {
                List<KPICriteriaDetailModel> lstDetails = SQLHelper<KPICriteriaDetailModel>.FindByAttribute("KPICriteriaID", item.ID);
                item.ID = 0;
                item.KPICriteriaQuater = TextUtils.ToInt(txtQuarter.Value);
                item.KPICriteriaYear = TextUtils.ToInt(txtYear.Value);
                item.ID = SQLHelper<KPICriteriaModel>.Insert(item).ID;
                foreach (KPICriteriaDetailModel detail in lstDetails)
                {
                    detail.ID = 0;
                    detail.KPICriteriaID = item.ID;
                    SQLHelper<KPICriteriaDetailModel>.Insert(detail);
                }

            }
        }
    }
}
