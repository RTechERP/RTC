
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseQuestionBO : BaseBO
	{
		private CourseQuestionFacade facade = CourseQuestionFacade.Instance;
		protected static CourseQuestionBO instance = new CourseQuestionBO();

		protected CourseQuestionBO()
		{
			this.baseFacade = facade;
		}

		public static CourseQuestionBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	