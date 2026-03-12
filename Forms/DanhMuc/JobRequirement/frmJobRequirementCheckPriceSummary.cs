using BMS;
using BMS.Business;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.DanhMuc.JobRequirement
{
    public partial class frmJobRequirementCheckPriceSummary: _Forms
    {
        public int jobRequirementID = 0;
        public frmJobRequirementCheckPriceSummary()
        {
            InitializeComponent();
        }
        private void frmJobRequirementCheckPriceSummary_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            Dictionary<string, string> suffixCaptions = new Dictionary<string, string>
            {
                { "OfferPrice", "Giá chào/LS" },
                { "PurchasePrice", "Đơn giá mua" },
                { "ShippingFee", "Phí VC" },
                { "TotalAmount", "Tổng tiền" },
                { "LeadTime", "LeadTime" },
                { "VAT", "VAT" },
                { "Supplier", "Nhà cung cấp" },
                { "Status", "Trạng thái" },
            };
            List<string> hiddenSuffixes = new List<string>
            {
                "ID",
            };

            DataTable dt = TextUtils.LoadDataFromSP("spGetJobRequirementCheckPrice","A",
                                    new string[] { "@JobRequirementID" },
                                    new object[] { jobRequirementID });

            //for (int i = bandNCC.Children.Count - 1; i >= 0; i--)
            //{
            //    if (bandNCC.Children[i].Tag != null && bandNCC.Children[i].Tag.ToString() == "Dynamic")
            //    {
            //        bandNCC.Children.RemoveAt(i);
            //    }
            //}

            //for (int i = bandNCC.Children.Count - 1; i >= 0; i--)
            //{
            //    var col = bandNCC.Children[i];
            //    if (col.FieldName.StartsWith("Cps"))
            //    {
            //        bandNCC.Children.Remove(col);
            //    }
            //}

            for (int i = bandNCC.Children.Count - 1; i >= 0; i--)
            {
                var band = bandNCC.Children[i];

                for (int j = band.Columns.Count - 1; j >= 0; j--)
                {
                    var bandCol = band.Columns[j];
                    if (bandCol != null && !string.IsNullOrEmpty(bandCol.FieldName) && bandCol.FieldName.StartsWith("Cps"))
                    {
                        grvCheckPrice.Columns.Remove(bandCol);
                    }
                }

                if (band.Tag != null && band.Tag.ToString() == "Dynamic")
                {
                    bandNCC.Children.RemoveAt(i);
                }
            }


            grdCheckPrice.DataSource = dt;

            //// Tìm các cột động (Cps1_, Cps2_, ...)
            var cpsColumns = dt.Columns.Cast<DataColumn>()
                                .Where(c => c.ColumnName.StartsWith("Cps"))
                                .Select(c => c.ColumnName)
                                .ToList();

            var groups = cpsColumns.GroupBy(c => c.Split('_')[0]);

            foreach (var g in groups.OrderBy(x => x.Key))
            {
                //Tạo band
                string bandIndex = g.Key.Substring(3);
                string bandCaption = $"Nhà cung cấp {bandIndex}";

                //GridBand cpsBand = grvCheckPrice.Bands.AddBand(bandCaption);
                GridBand cpsBand = bandNCC.Children.AddBand(bandCaption);

                cpsBand.AppearanceHeader.Font = new Font("Tahoma", 9F, FontStyle.Bold);
                cpsBand.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
                cpsBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                cpsBand.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

                cpsBand.Tag = "Dynamic"; // đánh dấu band là dynamic

                foreach (var colName in g)
                {
                    if (!grvCheckPrice.Columns.Contains(grvCheckPrice.Columns[colName]))
                    {
                        var gridCol = grvCheckPrice.Columns.AddField(colName);
                        gridCol.Visible = true;

                        //Đổi caption từ fieldName sang tên 
                        var parts = colName.Split('_');
                        string suffix = parts.Length > 1 ? parts[1] : colName;

                        if (suffixCaptions.ContainsKey(suffix))
                            gridCol.Caption = suffixCaptions[suffix];
                        else
                            gridCol.Caption = suffix;

                        gridCol.Visible = !hiddenSuffixes.Contains(suffix);

                        gridCol.AppearanceHeader.Font = new Font("Tahoma", 9F, FontStyle.Bold);
                        gridCol.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
                        gridCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        gridCol.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

                        cpsBand.Columns.Add(gridCol);
                    }
                }
            }

            //grvCheckPrice.BestFitColumns();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"TongHopCheckGia_{DateTime.Now.ToString("ddMMyy")}.xlsx";

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    //string filepath = Path.Combine(f.SelectedPath, $"");
                    string filepath = f.FileName;

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdCheckPrice;

                    try
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
