using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Classes
{
    public class StoreProcedures
    {
        //frmBillExportTechnical_New && frmBillExportTechnical_New
        public static string spGetBillExportTechDetail_New = "spGetBillExportTechDetail_New";

        public static string spGetProductRTCQRCode = "spGetProductRTCQRCode";

        public static string spUpdateStatusProductRTCQRCode = "spUpdateStatusProductRTCQRCode";
        public static string spGetProductQrCode = "spGetProductQrCode";
        public static string spGetProductRTCByQrCode = "spGetProductRTCByQrCode";



        //frmExamQuestion_New
        public static string spGetExamQuestionTypeByGroup = "spGetExamQuestionTypeByGroup";


        //frmQuestionTest
        public static string spGetExamQuestionByType = "spGetExamQuestionByType";
        public static string spGetExamQuestionByType_v1 = "spGetExamQuestionByType_v1";

        //frmListTest_New
        public static string spGetQuestionOfExam = "spGetQuestionOfExam";
        public static string spGetExamListTestByExamCategoryID = "spGetExamListTestByExamCategoryID";

        //frmRandomListTest
        public static string spInsertQuestionFromExamQuestionBankByRandom = "spInsertQuestionFromExamQuestionBankByRandom";
        public static string spRandomQuestionFromExamQuestionBank = "spRandomQuestionFromExamQuestionBank";
        public static string spGetExamQuestionTypeByListIDExamQuestionGroup = "spGetExamQuestionTypeByListIDExamQuestionGroup";


        //frmInventory  
        public static string spLoadBillImportInTwoMonth = "spLoadBillImportInTwoMonth";
    }
}
