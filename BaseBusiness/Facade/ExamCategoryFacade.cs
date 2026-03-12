
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamCategoryFacade : BaseFacade
	{
		protected static ExamCategoryFacade instance = new ExamCategoryFacade(new ExamCategoryModel());
		protected ExamCategoryFacade(ExamCategoryModel model) : base(model)
		{
		}
		public static ExamCategoryFacade Instance
		{
			get { return instance; }
		}
		protected ExamCategoryFacade():base() 
		{ 
		} 
	
	}
}
	