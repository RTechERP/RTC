using BMS;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Forms.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    class MyLib
    {
        private static Stopwatch watch = null;
        /// <summary>
        /// Thay đổi nội dung form wait
        /// </summary>
        /// <param name="description">Nội dung thông báo</param>
        public static void ChangeWaitFormDescription(string description = "")
        {
            if (cGlobVar.RTCSplashScreenManager.IsSplashFormVisible)
                if (description != "")
                    cGlobVar.RTCSplashScreenManager.SetWaitFormDescription(description);
        }
        /// <summary>
        /// Hiển thị wait form
        /// </summary>
        /// <param name="description">Nội dung thông báo</param>
        /// <param name="caption">Tiêu đề thông báo</param>
        public static void ShowWaitForm(string description = "", string caption = "")
        {
            if (cGlobVar.RTCSplashScreenManager == null || cGlobVar.RTCSplashScreenManager.IsSplashFormVisible)
                return;
            watch = new Stopwatch();
            watch.Start();
            cGlobVar.RTCSplashScreenManager.ShowWaitForm();

            if (caption != "")
                cGlobVar.RTCSplashScreenManager.SetWaitFormCaption(caption);
            if (description != "")
                cGlobVar.RTCSplashScreenManager.SetWaitFormDescription(description);
        }
        /// <summary>
        /// Đóng wait form
        /// </summary>
        public static void CloseWaitForm()
        {
            if (cGlobVar.RTCSplashScreenManager == null || !cGlobVar.RTCSplashScreenManager.IsSplashFormVisible)
                return;
            cGlobVar.RTCSplashScreenManager.CloseWaitForm();
            if (watch != null)
                watch.Stop();
        }
        public static void ViewWaitFormRunTime()
        {
            if (watch != null)
                MessageBox.Show(watch.ElapsedMilliseconds.ToString());
        }
        /// <summary>
        /// convert list string sang chuỗi string( dùng vào SQL)
        /// </summary>
        /// <param name="lstcode"></param>
        static public string ListToStringSQL(List<string> lstcode)
        {
            string code = string.Join(",", lstcode);
            string result = lstcode.Aggregate((total, part) => total + "'" + part + "'" + ",");
            result = result.TrimEnd(',');
            result = result.Replace(lstcode[0], $"'{lstcode[0]}',");
            return result;
        }
        static public void AddNewRow(GridControl grdData, GridView grvData)
        {

            int STT;
            DataTable dt = (DataTable)grdData.DataSource;
            if (dt.Rows.Count == 0)
            {
                STT = 1;
            }
            else
            {
                STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
            }
            DataRow dtrow = dt.NewRow();
            dtrow["STT"] = STT;
            dt.Rows.Add(dtrow);
            grdData.DataSource = dt;


        }
        static public void ExportExcelGrid(GridView grvData)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = true;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {

                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }


        }

    }
}
