
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ReaderBO : BaseBO
	{
		private ReaderFacade facade = ReaderFacade.Instance;
		protected static ReaderBO instance = new ReaderBO();

		protected ReaderBO()
		{
			this.baseFacade = facade;
		}

		public static ReaderBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	