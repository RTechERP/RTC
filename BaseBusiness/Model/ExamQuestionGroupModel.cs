
using System;
namespace BMS.Model
{
	public partial class ExamQuestionGroupModel : BaseModel
	{
		public int ID {get; set;}
		public string GroupCode {get; set;}
		public string GroupName {get; set;}
        public int DepartmentID { get; set; }

    }
}
	