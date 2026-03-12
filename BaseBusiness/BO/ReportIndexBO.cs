
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ReportIndexBO : BaseBO
	{
		private ReportIndexFacade facade = ReportIndexFacade.Instance;
		protected static ReportIndexBO instance = new ReportIndexBO();

		protected ReportIndexBO()
		{
			this.baseFacade = facade;
		}

		public static ReportIndexBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	