
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PartSummaryDetailBO : BaseBO
	{
		private PartSummaryDetailFacade facade = PartSummaryDetailFacade.Instance;
		protected static PartSummaryDetailBO instance = new PartSummaryDetailBO();

		protected PartSummaryDetailBO()
		{
			this.baseFacade = facade;
		}

		public static PartSummaryDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	