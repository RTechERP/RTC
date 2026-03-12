
using System;
namespace BMS.Model
{
	public class CameraModel : BaseModel
	{
		private int iD;
		private string cameraCode;
		private string cameraName;
		private string cameraUrl;
		private int parkingLineID;
		private bool isLPR;
		private int cameraDirection;
		private bool active;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public string CameraCode
		{
			get { return cameraCode; }
			set { cameraCode = value; }
		}
	
		public string CameraName
		{
			get { return cameraName; }
			set { cameraName = value; }
		}
	
		public string CameraUrl
		{
			get { return cameraUrl; }
			set { cameraUrl = value; }
		}
	
		public int ParkingLineID
		{
			get { return parkingLineID; }
			set { parkingLineID = value; }
		}
	
		public bool IsLPR
		{
			get { return isLPR; }
			set { isLPR = value; }
		}
	
		public int CameraDirection
		{
			get { return cameraDirection; }
			set { cameraDirection = value; }
		}
	
		public bool Active
		{
			get { return active; }
			set { active = value; }
		}
	
	}
}
	