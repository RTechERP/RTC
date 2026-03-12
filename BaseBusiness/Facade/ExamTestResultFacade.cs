
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamTestResultFacade : BaseFacade
	{
		protected static ExamTestResultFacade instance = new ExamTestResultFacade(new ExamTestResultModel());
		protected ExamTestResultFacade(ExamTestResultModel model) : base(model)
		{
		}
		public static ExamTestResultFacade Instance
		{
			get { return instance; }
		}
		protected ExamTestResultFacade():base() 
		{ 
		} 
	
	}
}
	