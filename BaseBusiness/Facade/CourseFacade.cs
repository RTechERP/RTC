
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseFacade : BaseFacade
	{
		protected static CourseFacade instance = new CourseFacade(new CourseModel());
		protected CourseFacade(CourseModel model) : base(model)
		{
		}
		public static CourseFacade Instance
		{
			get { return instance; }
		}
		protected CourseFacade():base() 
		{ 
		} 
	
	}
}
	