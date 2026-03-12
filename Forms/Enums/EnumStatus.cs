using DevExpress.XtraCharts.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Enums
{
    public enum EStatus
    {
        Borrow = 0, // mượn
        Inventory = 1, // tồn kho
        ShippedOut = 2, // đã xuất kho
    }
    public enum EGroupSales
    {
        MRO = 0, 
        SamSung = 1, 
        Base = 2, 
    }

    public enum TSAssetStatus
    {
        CHUASUDUNG = 1,
        DANGSUDUNG = 2,
        SUACHUABAODUONG = 3,
        MAT = 4,
        HONG = 5,
        THANHLY = 6,
        DENGHITHANHLY = 7,
    }
}
