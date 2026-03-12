using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Columns;
using Forms.Classes;

namespace BMS
{
    public partial class frmQuestionTest : _Forms
    {
        public int ExamListTestID;
        
        public frmQuestionTest()
        {
            InitializeComponent();
        }

        void loadcboType()
        {
            DataTable data = TextUtils.Select("Select * from ExamQuestionGroup ");
            cboType.Properties.DataSource = data;
            cboType.Properties.DisplayMember = "GroupName";
            cboType.Properties.ValueMember = "ID";

            //DataTable data = TextUtils.Select("SELECT  et.*,eg.GroupName FROM ExamQuestionType et LEFT JOIN ExamQuestionGroup eg ON eg.ID = et.ExamQuestionGroupID ");
            

        }
        void LoadQuestion()
        {
            int GroupID = TextUtils.ToInt(cboType.EditValue);
            //DataTable dataTable = TextUtils.Select("EXEC spGetExamQuestionByType " + typeID);
            DataTable dataTable = TextUtils.LoadDataFromSP(StoreProcedures.spGetExamQuestionByType_v1, "A", new string[] { "@GroupID" }, new object[] { GroupID });
            grdQuestion.DataSource = dataTable;
            grvQuestion.ExpandAllGroups();
        }
        private void frmQuestionTest_Load(object sender, EventArgs e)
        {
            loadcboType();
            LoadQuestion();
            searchLookUpEdit1View.ExpandAllGroups();
            searchLookUpEdit1View.CollapseAllGroups();            
        }

        private void cboType_EditValueChanged(object sender, EventArgs e)
        {
            LoadQuestion();
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            int[] count = grvQuestion.GetSelectedRows();
            bool flag = false;
            if (count.Length > 0)
            {
                ArrayList ListQuestionID = new ArrayList();
                DataTable dataTable = TextUtils.Select($"SELECT * FROM ExamQuestionListTest WHERE  ExamListTestID = {ExamListTestID}");
                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        ListQuestionID.Add(TextUtils.ToInt(dataTable.Rows[i]["ExamQuestionID"]));
                    }
                }

                for (int i = 0; i < count.Length; i++)
                {
                    ExamQuestionListTestModel model = new ExamQuestionListTestModel();
                    model.ExamListTestID = ExamListTestID;
                    model.ExamQuestionID= TextUtils.ToInt(grvQuestion.GetRowCellValue(count[i], colQuestionID));
                    if (model.ExamQuestionID <= 0) continue;
                    model.STT = TextUtils.ToInt(grvQuestion.GetRowCellValue(count[i], colSTT));

                    //nếu câu hỏi đã có thì không thêm vào nữa.
                    if (!ListQuestionID.Contains(model.ExamQuestionID))
                    {
                        ExamQuestionListTestBO.Instance.Insert(model);
                        flag = true;
                    }
                }

                if(flag)
                {
                    //Update lại STT
                    ArrayList arrayList = ExamQuestionListTestBO.Instance.FindByAttribute("ExamListTestID", ExamListTestID.ToString());
                    if (arrayList.Count > 0)
                    {
                        int i = 1;
                        foreach (ExamQuestionListTestModel item in arrayList)
                        {
                            item.STT = i;
                            ExamQuestionListTestBO.Instance.Update(item);
                            i++;
                        }
                    }
                }    
            }
            this.DialogResult = DialogResult.OK;
        }

       
    }
}
