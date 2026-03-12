
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseLessonFacade : BaseFacade
	{
		protected static CourseLessonFacade instance = new CourseLessonFacade(new CourseLessonModel());
		protected CourseLessonFacade(CourseLessonModel model) : base(model)
		{
		}
		public static CourseLessonFacade Instance
		{
			get { return instance; }
		}
		protected CourseLessonFacade():base() 
		{ 
		} 
	
	}
}
	