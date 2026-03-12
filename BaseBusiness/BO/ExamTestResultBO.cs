
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamTestResultBO : BaseBO
	{
		private ExamTestResultFacade facade = ExamTestResultFacade.Instance;
		protected static ExamTestResultBO instance = new ExamTestResultBO();

		protected ExamTestResultBO()
		{
			this.baseFacade = facade;
		}

		public static ExamTestResultBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	