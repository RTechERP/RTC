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

namespace Forms.keToan.Tax.TaxUserDetail
{
    public partial class ucTaxUserOther : UserControl
    {
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

        public UsersModel Model;//= new UsersModel();
        public Dictionary<string, bool> _dic = new Dictionary<string, bool>();
        public ucTaxUserOther()
        {
            InitializeComponent();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                chkBangCap.Checked = true;
                chkCamketts.Checked = true;
                chkChuyenHD.Checked = true;
                chkCMND.Checked = true;
                chkCV.Checked = true;
                chkDGTV.Checked = true;
                chkDXV.Checked = true;
                chkGiayKS.Checked = true;
                chkGiayKSK.Checked = true;
                chkHDLDKXDTH.Checked = true;
                chkHDLDXDTH.Checked = true;
                chkHDTV.Checked = true;
                chkQDTD.Checked = true;
                chkSoHK.Checked = true;
                chkSYLL.Checked = true;
                chkThumoi.Checked = true;
                chkTotrinhTD.Checked = true;
                chkXNNS.Checked = true;
            }
            else
            {
                chkBangCap.Checked = false;
                chkCamketts.Checked = false;
                chkChuyenHD.Checked = false;
                chkCMND.Checked = false;
                chkCV.Checked = false;
                chkDGTV.Checked = false;
                chkDXV.Checked = false;
                chkGiayKS.Checked = false;
                chkGiayKSK.Checked = false;
                chkHDLDKXDTH.Checked = false;
                chkHDLDXDTH.Checked = false;
                chkHDTV.Checked = false;
                chkQDTD.Checked = false;
                chkSoHK.Checked = false;
                chkSYLL.Checked = false;
                chkThumoi.Checked = false;
                chkTotrinhTD.Checked = false;
                chkXNNS.Checked = false;
            }
        }

        public void loadData(ref TaxEmployeeModel Model)
        {

            chkBangCap.Checked = Model.BangCap;
            chkCamketts.Checked = Model.CamKetTs;
            chkChuyenHD.Checked = Model.DGChuyenHD;
            chkCMND.Checked = Model.CMNDorCCCD;
            chkCV.Checked = Model.CV;
            chkDGTV.Checked = Model.DGTV;
            chkDXV.Checked = Model.DXV;
            chkGiayKS.Checked = Model.GiayKS;
            chkGiayKSK.Checked = Model.GiayKSK;
            chkHDLDKXDTH.Checked = Model.HDLDKXDTH;
            chkHDLDXDTH.Checked = Model.HDLDXDTH;
            chkHDTV.Checked = Model.HDTV;
            chkQDTD.Checked = Model.QDTD;
            chkSoHK.Checked = Model.SoHK;
            chkThumoi.Checked = Model.ThuMoiNhanViec;
            chkTotrinhTD.Checked = Model.ToTrinhTD;
            chkXNNS.Checked = Model.XNNS;
            chkSYLL.Checked = Model.SYLL;

        }

        public void save(ref TaxEmployeeModel usersModel)
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
        }
    }
}
