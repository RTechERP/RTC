
using System;
namespace BMS.Model
{
	public class ReaderModel : BaseModel
	{
		private int iD;
		private string readerCode;
		private string readerName;
		private string cOMPort;
		private string portMode;
		private int parkingLineID;
		private bool adminReader;
		private bool active;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public string ReaderCode
		{
			get { return readerCode; }
			set { readerCode = value; }
		}
	
		public string ReaderName
		{
			get { return readerName; }
			set { readerName = value; }
		}
	
		public string COMPort
		{
			get { return cOMPort; }
			set { cOMPort = value; }
		}
	
		public string PortMode
		{
			get { return portMode; }
			set { portMode = value; }
		}
	
		public int ParkingLineID
		{
			get { return parkingLineID; }
			set { parkingLineID = value; }
		}
	
		public bool AdminReader
		{
			get { return adminReader; }
			set { adminReader = value; }
		}
	
		public bool Active
		{
			get { return active; }
			set { active = value; }
		}
	
	}
}
	