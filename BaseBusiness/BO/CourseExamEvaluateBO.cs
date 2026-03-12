
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseExamEvaluateBO : BaseBO
	{
		private CourseExamEvaluateFacade facade = CourseExamEvaluateFacade.Instance;
		protected static CourseExamEvaluateBO instance = new CourseExamEvaluateBO();

		protected CourseExamEvaluateBO()
		{
			this.baseFacade = facade;
		}

		public static CourseExamEvaluateBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	