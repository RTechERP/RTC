using BMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Classes
{
    class cPOStatus
    {
        public static void AutoUpdateStatus(int id)
        {
            DataTable dt = TextUtils.Select($"Select * From POKH where ID={id}");
            if (dt.Rows.Count == 0) return;
            bool IsExport = TextUtils.ToBoolean(dt.Rows[0]["IsExport"]);
            bool IsPay = TextUtils.ToBoolean(dt.Rows[0]["IsPay"]);
            bool IsBill = TextUtils.ToBoolean(dt.Rows[0]["IsBill"]);
            bool IsShip = TextUtils.ToBoolean(dt.Rows[0]["IsShip"]);
            if (IsPay && IsExport && IsBill)
                TextUtils.ExcuteSQL($"Update POKH set Status=1 where ID={id}");//Đã giao đã thanh toán
            if (!IsPay && IsExport)
                TextUtils.ExcuteSQL($"Update POKH set Status=3 where ID={id}");//Đã giao nhưng chưa thanh toán
            if (!IsPay && !IsExport)
                TextUtils.ExcuteSQL($"Update POKH set Status=0 where ID={id}");//Chưa giao chưa thanh toán
            if (IsPay && !IsExport)
                TextUtils.ExcuteSQL($"Update POKH set Status=2 where ID={id}");//Chưa giao đã thanh toán
            if (IsPay && IsExport && !IsBill)
                TextUtils.ExcuteSQL($"Update POKH set Status=4 where ID={id}");//Đã thanh toán nhưng chưa xuất hóa đơn
            if (IsShip && !IsExport)
                TextUtils.ExcuteSQL($"Update POKH set Status=5 where ID={id}");//Đã giao 1 phần 
        }
    }
}
