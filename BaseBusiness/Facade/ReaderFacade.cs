
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ReaderFacade : BaseFacade
	{
		protected static ReaderFacade instance = new ReaderFacade(new ReaderModel());
		protected ReaderFacade(ReaderModel model) : base(model)
		{
		}
		public static ReaderFacade Instance
		{
			get { return instance; }
		}
		protected ReaderFacade():base() 
		{ 
		} 
	
	}
}
	