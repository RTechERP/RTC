
using System;
namespace BMS.Model
{
	public partial class UserTeamLinkModel : BaseModel
	{
		public int ID {get; set;}
		
		public int UserID {get; set;}
		
		public int UserTeamID {get; set;}
		
	}
}
	