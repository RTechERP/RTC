
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamListTestFacade : BaseFacade
	{
		protected static ExamListTestFacade instance = new ExamListTestFacade(new ExamListTestModel());
		protected ExamListTestFacade(ExamListTestModel model) : base(model)
		{
		}
		public static ExamListTestFacade Instance
		{
			get { return instance; }
		}
		protected ExamListTestFacade():base() 
		{ 
		} 
	
	}
}
	