
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class GroupFileBO : BaseBO
	{
		private GroupFileFacade facade = GroupFileFacade.Instance;
		protected static GroupFileBO instance = new GroupFileBO();

		protected GroupFileBO()
		{
			this.baseFacade = facade;
		}

		public static GroupFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	