
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseFileFacade : BaseFacade
	{
		protected static CourseFileFacade instance = new CourseFileFacade(new CourseFileModel());
		protected CourseFileFacade(CourseFileModel model) : base(model)
		{
		}
		public static CourseFileFacade Instance
		{
			get { return instance; }
		}
		protected CourseFileFacade():base() 
		{ 
		} 
	
	}
}
	