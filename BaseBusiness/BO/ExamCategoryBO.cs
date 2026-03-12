
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamCategoryBO : BaseBO
	{
		private ExamCategoryFacade facade = ExamCategoryFacade.Instance;
		protected static ExamCategoryBO instance = new ExamCategoryBO();

		protected ExamCategoryBO()
		{
			this.baseFacade = facade;
		}

		public static ExamCategoryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	