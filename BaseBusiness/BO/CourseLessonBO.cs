
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseLessonBO : BaseBO
	{
		private CourseLessonFacade facade = CourseLessonFacade.Instance;
		protected static CourseLessonBO instance = new CourseLessonBO();

		protected CourseLessonBO()
		{
			this.baseFacade = facade;
		}

		public static CourseLessonBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	