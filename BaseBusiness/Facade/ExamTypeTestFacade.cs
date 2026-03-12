
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamTypeTestFacade : BaseFacade
	{
		protected static ExamTypeTestFacade instance = new ExamTypeTestFacade(new ExamTypeTestModel());
		protected ExamTypeTestFacade(ExamTypeTestModel model) : base(model)
		{
		}
		public static ExamTypeTestFacade Instance
		{
			get { return instance; }
		}
		protected ExamTypeTestFacade():base() 
		{ 
		} 
	
	}
}
	