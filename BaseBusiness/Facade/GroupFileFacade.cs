
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class GroupFileFacade : BaseFacade
	{
		protected static GroupFileFacade instance = new GroupFileFacade(new GroupFileModel());
		protected GroupFileFacade(GroupFileModel model) : base(model)
		{
		}
		public static GroupFileFacade Instance
		{
			get { return instance; }
		}
		protected GroupFileFacade():base() 
		{ 
		} 
	
	}
}
	