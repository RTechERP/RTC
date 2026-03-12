
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BusinessFieldBO : BaseBO
	{
		private BusinessFieldFacade facade = BusinessFieldFacade.Instance;
		protected static BusinessFieldBO instance = new BusinessFieldBO();

		protected BusinessFieldBO()
		{
			this.baseFacade = facade;
		}

		public static BusinessFieldBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	