using BMS;
using BMS.Business;
using BMS.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Employee.UserDetail
{
    public partial class ucUserOther : UserControl
    {
        /// <summary>
        /// Variable
        /// </summary>

        public int ID;
        public bool BangCap;
        public bool Camketts;
        public bool ChuyenHD;
        public bool CMND;
        public bool CV;
        public bool DGTV;
        public bool DXV;
        public bool GiayKS;
        public bool GiayKSK;
        public bool HDLDKXDTH;
        public bool HDLDXDTH;
        public bool HDTV;
        public bool QDTD;
        public bool SoHK;
        public bool SYLL;
        public bool Thumoi;
        public bool TotrinhTD;
        public bool XNNS;
        public bool GiayXacNhanCuTru;

        public UsersModel Model;//= new UsersModel();
        public Dictionary<string, bool> _dic = new Dictionary<string, bool>();
        public ucUserOther()
        {
            InitializeComponent();
        }


        private void ucUserOther_Load(object sender, EventArgs e)
        {
            //loadData();
        }


        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            chkBangCap.Checked = chkAll.Checked;
            chkCamketts.Checked = chkAll.Checked;
            chkChuyenHD.Checked = chkAll.Checked;
            chkCMND.Checked = chkAll.Checked;
            chkCV.Checked = chkAll.Checked;
            chkDGTV.Checked = chkAll.Checked;
            chkDXV.Checked = chkAll.Checked;
            chkGiayKS.Checked = chkAll.Checked;
            chkGiayKSK.Checked = chkAll.Checked;
            chkHDLDKXDTH.Checked = chkAll.Checked;
            chkHDLDXDTH.Checked = chkAll.Checked;
            chkHDTV.Checked = chkAll.Checked;
            chkQDTD.Checked = chkAll.Checked;
            chkSoHK.Checked = chkAll.Checked;
            chkSYLL.Checked = chkAll.Checked;
            chkThumoi.Checked = chkAll.Checked;
            chkTotrinhTD.Checked = chkAll.Checked;
            chkXNNS.Checked = chkAll.Checked;
            chkGiayXacNhanCuTru.Checked = chkAll.Checked;

            //if (chkAll.Checked == true)
            //{
            //    chkBangCap.Checked = true;
            //    chkCamketts.Checked = true;
            //    chkChuyenHD.Checked = true;
            //    chkCMND.Checked = true;
            //    chkCV.Checked = true;
            //    chkDGTV.Checked = true;
            //    chkDXV.Checked = true;
            //    chkGiayKS.Checked = true;
            //    chkGiayKSK.Checked = true;
            //    chkHDLDKXDTH.Checked = true;
            //    chkHDLDXDTH.Checked = true;
            //    chkHDTV.Checked = true;
            //    chkQDTD.Checked = true;
            //    chkSoHK.Checked = true;
            //    chkSYLL.Checked = true;
            //    chkThumoi.Checked = true;
            //    chkTotrinhTD.Checked = true;
            //    chkXNNS.Checked = true;
            //}
            //else
            //{
            //    chkBangCap.Checked = false;
            //    chkCamketts.Checked = false;
            //    chkChuyenHD.Checked = false;
            //    chkCMND.Checked = false;
            //    chkCV.Checked = false;
            //    chkDGTV.Checked = false;
            //    chkDXV.Checked = false;
            //    chkGiayKS.Checked = false;
            //    chkGiayKSK.Checked = false;
            //    chkHDLDKXDTH.Checked = false;
            //    chkHDLDXDTH.Checked = false;
            //    chkHDTV.Checked = false;
            //    chkQDTD.Checked = false;
            //    chkSoHK.Checked = false;
            //    chkSYLL.Checked = false;
            //    chkThumoi.Checked = false;
            //    chkTotrinhTD.Checked = false;
            //    chkXNNS.Checked = false;
            //}

        }

        public void loadData(ref EmployeeModel Model)
        {

            chkBangCap.Checked = TextUtils.ToBoolean(Model.BangCap);
            chkCamketts.Checked = TextUtils.ToBoolean(Model.CamKetTs);
            chkChuyenHD.Checked = TextUtils.ToBoolean(Model.DGChuyenHD);
            chkCMND.Checked = TextUtils.ToBoolean(Model.CMNDorCCCD);
            chkCV.Checked = TextUtils.ToBoolean(Model.CV);
            chkDGTV.Checked = TextUtils.ToBoolean(Model.DGTV);
            chkDXV.Checked = TextUtils.ToBoolean(Model.DXV);
            chkGiayKS.Checked = TextUtils.ToBoolean(Model.GiayKS);
            chkGiayKSK.Checked = TextUtils.ToBoolean(Model.GiayKSK);
            chkHDLDKXDTH.Checked = TextUtils.ToBoolean(Model.HDLDKXDTH);
            chkHDLDXDTH.Checked = TextUtils.ToBoolean(Model.HDLDXDTH);
            chkHDTV.Checked = TextUtils.ToBoolean(Model.HDTV);
            chkQDTD.Checked = TextUtils.ToBoolean(Model.QDTD);
            chkSoHK.Checked = TextUtils.ToBoolean(Model.SoHK);
            chkThumoi.Checked = TextUtils.ToBoolean(Model.ThuMoiNhanViec);
            chkTotrinhTD.Checked = TextUtils.ToBoolean(Model.ToTrinhTD);
            chkXNNS.Checked = TextUtils.ToBoolean(Model.XNNS);
            chkSYLL.Checked = TextUtils.ToBoolean(Model.SYLL);
            chkGiayXacNhanCuTru.Checked = TextUtils.ToBoolean(Model.GiayXacNhanCuTru);



        }
        public void save(ref EmployeeModel usersModel)
        {

            usersModel.BangCap = chkBangCap.Checked;
            usersModel.CamKetTs = chkCamketts.Checked;
            usersModel.DGChuyenHD = chkChuyenHD.Checked;
            usersModel.CMNDorCCCD = chkCMND.Checked;
            usersModel.CV = chkCV.Checked;
            usersModel.DGTV = chkDGTV.Checked;
            usersModel.DXV = chkDXV.Checked;
            usersModel.GiayKS = chkGiayKS.Checked;
            usersModel.GiayKSK = chkGiayKSK.Checked;
            usersModel.HDLDKXDTH = chkHDLDKXDTH.Checked;
            usersModel.HDLDXDTH = chkHDLDXDTH.Checked;
            usersModel.HDTV = chkHDTV.Checked;
            usersModel.QDTD = chkQDTD.Checked;
            usersModel.SoHK = chkSoHK.Checked;
            usersModel.SYLL = chkSYLL.Checked;
            usersModel.ThuMoiNhanViec = chkThumoi.Checked;
            usersModel.ToTrinhTD = chkTotrinhTD.Checked;
            usersModel.XNNS = chkXNNS.Checked;
            usersModel.GiayXacNhanCuTru = chkGiayXacNhanCuTru.Checked;
            // UsersBO.Instance.Update(Model);
            //BangCap = chkBangCap.Checked;
            //Camketts= chkCamketts.Checked;
            //ChuyenHD= chkChuyenHD.Checked;
            //CMND= chkCMND.Checked;
            //CV=chkCV.Checked;
            //DGTV=chkDGTV.Checked;
            //DXV=chkDXV.Checked;
            //GiayKS=chkGiayKS.Checked;
            //GiayKSK=chkGiayKSK.Checked;
            //HDLDKXDTH= chkHDLDKXDTH.Checked;
            //HDLDXDTH=chkHDLDXDTH.Checked;
            //HDTV=chkHDTV.Checked;
            //QDTD=chkQDTD.Checked;
            //SoHK=chkSoHK.Checked;
            //SYLL=chkSYLL.Checked;
            //Thumoi=chkThumoi.Checked;
            //TotrinhTD=chkTotrinhTD.Checked;
            //XNNS=chkXNNS.Checked;



            //_dic.Add("BangCap", chkBangCap.Checked);
            //_dic.Add("Camketts", chkCamketts.Checked);
            //_dic.Add("ChuyenHD", chkChuyenHD.Checked);
            //_dic.Add("CMND", chkCMND.Checked);
            //_dic.Add("CV", chkCV.Checked);
            //_dic.Add("DGTV", chkDGTV.Checked);
            //_dic.Add("DXV", chkDXV.Checked);
            //_dic.Add("GiayKS", chkGiayKS.Checked);
            //_dic.Add("GiayKSK", chkGiayKSK.Checked);
            //_dic.Add("HDLDKXDTH", chkHDLDKXDTH.Checked);
            //_dic.Add("HDLDXDTH", chkHDLDXDTH.Checked);
            //_dic.Add("HDTV", chkHDTV.Checked);
            //_dic.Add("QDTD", chkQDTD.Checked);
            //_dic.Add("SoHK", chkSoHK.Checked);
            //_dic.Add("SYLL", chkSYLL.Checked);
            //_dic.Add("Thumoi", chkThumoi.Checked);
            //_dic.Add("TotrinhTD", chkTotrinhTD.Checked);
            //_dic.Add("XNNS", chkXNNS.Checked);
        }



    }
}
