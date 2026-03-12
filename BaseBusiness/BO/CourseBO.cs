
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseBO : BaseBO
	{
		private CourseFacade facade = CourseFacade.Instance;
		protected static CourseBO instance = new CourseBO();

		protected CourseBO()
		{
			this.baseFacade = facade;
		}

		public static CourseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	