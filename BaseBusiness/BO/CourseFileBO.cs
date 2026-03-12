
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseFileBO : BaseBO
	{
		private CourseFileFacade facade = CourseFileFacade.Instance;
		protected static CourseFileBO instance = new CourseFileBO();

		protected CourseFileBO()
		{
			this.baseFacade = facade;
		}

		public static CourseFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	