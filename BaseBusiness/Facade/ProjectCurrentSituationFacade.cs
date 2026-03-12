
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectCurrentSituationFacade : BaseFacade
	{
		protected static ProjectCurrentSituationFacade instance = new ProjectCurrentSituationFacade(new ProjectCurrentSituationModel());
		protected ProjectCurrentSituationFacade(ProjectCurrentSituationModel model) : base(model)
		{
		}
		public static ProjectCurrentSituationFacade Instance
		{
			get { return instance; }
		}
		protected ProjectCurrentSituationFacade():base() 
		{ 
		} 
	
	}
}
	