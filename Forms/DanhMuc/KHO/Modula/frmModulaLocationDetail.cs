using BMS.Utils;
using DevExpress.XtraCharts;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BMS
{
    public partial class frmModulaLocationDetail : _Forms
    {
        public Action SaveEvent;
        public ModulaLocationModel modulaLocation = new ModulaLocationModel();

        List<ModulaLocationDetailModel> details = new List<ModulaLocationDetailModel>();

        List<int> deletedIDs = new List<int>();
        public frmModulaLocationDetail()
        {
            InitializeComponent();
        }

        private void frmModulaLocationDetail_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        void LoadData()
        {
            txtCode.Text = modulaLocation.Code;
            txtName.Text = modulaLocation.Name;
            txtStt.Value = TextUtils.ToInt(modulaLocation.STT);
            txtWidth.Value = TextUtils.ToInt(modulaLocation.Width);
            txtHeight.Value = TextUtils.ToInt(modulaLocation.Height);
            chkIsDeleted.Checked = TextUtils.ToBoolean(modulaLocation.IsDeleted);

            if (modulaLocation.ID <= 0)
            {
                var loactions = SQLHelper<ModulaLocationModel>.FindByAttribute(ModulaLocationModel_Enum.IsDeleted.ToString(), 0);
                txtStt.Value = loactions.Count <= 0 ? 1 : TextUtils.ToInt(loactions.Max(x => x.STT)) + 1;

                txtName.Text = $"Tray {txtStt.Value}";
            }
            LoadDetail();
        }

        void LoadDetail()
        {
            grdDetail.DataSource = null;
            var exp1 = new Expression(ModulaLocationDetailModel_Enum.ModulaLocationID, modulaLocation.ID);
            var exp2 = new Expression(ModulaLocationDetailModel_Enum.IsDeleted, 0);
            details = SQLHelper<ModulaLocationDetailModel>.FindByExpression(exp1.And(exp2));
            grdDetail.DataSource = details;
        }


        bool CheckValidate()
        {

            string code = txtCode.Text.Trim();

            int trayWidth = TextUtils.ToInt(txtWidth.Value);
            int trayHeigh = TextUtils.ToInt(txtHeight.Value);
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập Mã!", "Thông báo");
                return false;
            }
            else
            {
                var exp1 = new Expression(ModulaLocationModel_Enum.ID, modulaLocation.ID, "<>");
                var exp2 = new Expression(ModulaLocationModel_Enum.IsDeleted, 1, "<>");
                var exp3 = new Expression(ModulaLocationModel_Enum.Code, code);

                var locations = SQLHelper<ModulaLocationModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (locations.Count > 0)
                {
                    MessageBox.Show($"Mã tray [{code}] đã tồn tại!", "Thông báo");
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã!", "Thông báo");
                return false;
            }

            if (trayWidth <= 0 || trayWidth > 1400)
            {
                MessageBox.Show("Vui lòng nhập Chiều rộng (phải > 0 và  < 1.400 mm)!", "Thông báo");
                return false;
            }

            if (trayHeigh <= 0 || trayHeigh > 450)
            {
                MessageBox.Show("Vui lòng nhập Chiều cao (phải > 0 và  < 450 mm)!", "Thông báo");
                return false;
            }

            //int row = TextUtils.ToInt(txtRow.Value);
            //int column = TextUtils.ToInt(txtColumn.Value);

            //int totalLocations = row * column;
            //if (details.Count >= totalLocations)
            //{
            //    MessageBox.Show($"Số lượng vị trí không được lớn hơn Số cột x Số hàng ({totalLocations})!", "Thông báo");
            //    return false;
            //}


            List<RectangleModula> rectangles = new List<RectangleModula>();
            for (int i = 0; i < grvDetail.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.ID.ToString()));
                string codeDetail = TextUtils.ToString(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Code.ToString()));
                int stt = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.STT.ToString()));

                int width = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Width.ToString()));
                int height = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Height.ToString()));
                if (string.IsNullOrWhiteSpace(codeDetail))
                {
                    MessageBox.Show($"Vui lòng nhập Mã vị trí (Stt: {stt})!", "Thông báo");
                    return false;
                }
                else
                {
                    var exp1 = new Expression(ModulaLocationDetailModel_Enum.ID, id, "<>");
                    var exp2 = new Expression(ModulaLocationDetailModel_Enum.IsDeleted, 1, "<>");
                    var exp3 = new Expression(ModulaLocationDetailModel_Enum.Code, codeDetail);
                    var exp4 = new Expression(ModulaLocationDetailModel_Enum.ModulaLocationID, modulaLocation.ID);

                    var locationDetails = SQLHelper<ModulaLocationDetailModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
                    if (locationDetails.Count > 0)
                    {
                        MessageBox.Show($"Mã vị trí [{codeDetail}] trong tray đã tồn tại (Stt: {stt})!", "Thông báo");
                        return false;
                    }
                }

                if (TextUtils.ToInt(width) <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Chiều rộng vị trí (Stt: {stt})!", "Thông báo");
                    return false;
                }

                if (TextUtils.ToInt(height) <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Chiều cao vị trí (Stt: {stt})!", "Thông báo");
                    return false;
                }


                ModulaLocationDetailModel detail = new ModulaLocationDetailModel();

                detail.ModulaLocationID = modulaLocation.ID;
                detail.STT = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.STT.ToString()));
                detail.Code = TextUtils.ToString(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Code.ToString()));
                detail.Name = TextUtils.ToString(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Name.ToString()));
                detail.Width = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Width.ToString()));
                detail.Height = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Height.ToString()));
                rectangles.Add(new RectangleModula(detail.ID, TextUtils.ToInt(detail.Width), TextUtils.ToInt(detail.Height), detail.Code));
            }


            // Đóng gói
            MaxRectsBinPack packer = new MaxRectsBinPack(TextUtils.ToInt(txtWidth.Value), TextUtils.ToInt(txtHeight.Value));
            var (placed, notPlaced) = packer.Pack(rectangles);


            // Cảnh báo
            if (notPlaced.Count > 0)
            {
                string codes = string.Join(";", notPlaced.Select(x => x.Code).ToList());
                MessageBox.Show($"Các vị trí [{codes}] không thể đặt vào Tray!", "Thông báo");
                return false;
            }



            //int totalWidth = TextUtils.ToInt(grvDetail.Columns[ModulaLocationDetailModel_Enum.Width.ToString()].SummaryItem.SummaryValue);
            //int totalHeight = TextUtils.ToInt(grvDetail.Columns[ModulaLocationDetailModel_Enum.Height.ToString()].SummaryItem.SummaryValue);

            //if (totalWidth > trayWidth)
            //{
            //    MessageBox.Show($"Tổng chiều rộng các vị trí không được lớn hơn chiều rộng của Tray!", "Thông báo");
            //    return false;
            //}

            //if (totalHeight > trayHeigh)
            //{
            //    MessageBox.Show($"Tổng chiều cao các vị trí không được lớn hơn chiều cao của Tray!", "Thông báo");
            //    return false;
            //}
            return true;
        }

        bool SaveData()
        {
            try
            {
                //grvDetail.CloseEditor();
                grvDetail.FocusedRowHandle = -1;
                if (!CheckValidate()) return false;

                modulaLocation.Code = txtCode.Text.Trim();
                modulaLocation.STT = TextUtils.ToInt(txtStt.Value);
                modulaLocation.Width = TextUtils.ToInt(txtWidth.Value);
                modulaLocation.Height = TextUtils.ToInt(txtHeight.Value);
                modulaLocation.Name = txtName.Text.Trim();
                modulaLocation.IsDeleted = chkIsDeleted.Checked;

                if (modulaLocation.ID <= 0) modulaLocation.ID = SQLHelper<ModulaLocationModel>.Insert(modulaLocation).ID;
                else SQLHelper<ModulaLocationModel>.Update(modulaLocation);


                List<RectangleModula> rectangles = new List<RectangleModula>();


                for (int i = 0; i < grvDetail.RowCount; i++)
                {
                    int id = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.ID.ToString()));
                    ModulaLocationDetailModel detail = SQLHelper<ModulaLocationDetailModel>.FindByID(id);

                    detail.ModulaLocationID = modulaLocation.ID;
                    detail.STT = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.STT.ToString()));
                    detail.Code = TextUtils.ToString(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Code.ToString()));
                    detail.Name = TextUtils.ToString(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Name.ToString()));
                    detail.Width = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Width.ToString()));
                    detail.Height = TextUtils.ToInt(grvDetail.GetRowCellValue(i, ModulaLocationDetailModel_Enum.Height.ToString()));

                    //detail.AxisX = detail.Width / 2;
                    //detail.AxisY = detail.Height / 2;
                    if (detail.ID <= 0) detail.ID = SQLHelper<ModulaLocationDetailModel>.Insert(detail).ID;
                    else SQLHelper<ModulaLocationDetailModel>.Update(detail);

                    rectangles.Add(new RectangleModula(detail.ID, TextUtils.ToInt(detail.Width), TextUtils.ToInt(detail.Height), ""));

                }


                // Đóng gói
                MaxRectsBinPack packer = new MaxRectsBinPack(TextUtils.ToInt(modulaLocation.Width), TextUtils.ToInt(modulaLocation.Height));
                var (placed, notPlaced) = packer.Pack(rectangles);

                foreach (var rect in placed)
                {
                    var (X, Y) = rect.GetCenter();
                    //Console.WriteLine($"Vị trí {rect.Id}: X={X:F2}, Y={Y:F2}");

                    var myDict = new Dictionary<string, object>()
                    {
                        {ModulaLocationDetailModel_Enum.AxisX.ToString(),rect.GetCenter().X },
                        {ModulaLocationDetailModel_Enum.AxisY.ToString(),rect.GetCenter().Y},
                    };


                    SQLHelper<ModulaLocationDetailModel>.UpdateFieldsByID(myDict, rect.Id);
                }


                //// Cảnh báo
                //if (notPlaced.Count > 0)
                //{
                //    Console.WriteLine("\nCảnh báo: Không thể đặt các vị trí:");
                //    foreach (var rect in notPlaced)
                //    {
                //        MessageBox.Show($"Vị trí {rect.Code} không thể đặt vào Tray: width={rect.Width}, height={rect.Height}", "Thông báo");
                //    }
                //}

                //Update danh xóa
                if (deletedIDs.Count > 0)
                {
                    var myDict = new Dictionary<string, object>()
                    {
                        { ModulaLocationDetailModel_Enum.IsDeleted.ToString(),true},
                        { ModulaLocationDetailModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        { ModulaLocationDetailModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                    };

                    var exp = new Expression(ModulaLocationDetailModel_Enum.ID.ToString(), string.Join(",", deletedIDs), "IN");
                    SQLHelper<ModulaLocationDetailModel>.UpdateFields(myDict, exp);
                }

                SaveEvent();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }
        private void groupControl2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (TextUtils.ToString(e.Button.Properties.Tag) == "btnAdd")
            {
                //int row = TextUtils.ToInt(txtRow.Value);
                //int column = TextUtils.ToInt(txtColumn.Value);

                //int totalLocations = row * column;
                //if (details.Count >= totalLocations) return;
                details.Add(new ModulaLocationDetailModel()
                {
                    STT = details.Count <= 0 ? 1 : TextUtils.ToInt(details.Max(x => x.STT)) + 1
                });

                grdDetail.RefreshDataSource();
            }

        }

        private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                this.Close();
            }
        }

        private void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                modulaLocation = new ModulaLocationModel();
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(ModulaLocationDetailModel_Enum.ID.ToString()));
            //if (id <= 0) return;

            string name = TextUtils.ToString(grvDetail.GetFocusedRowCellValue(ModulaLocationDetailModel_Enum.Name.ToString()));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa Vị trí [{name}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (!deletedIDs.Contains(id)) deletedIDs.Add(id);
                grvDetail.DeleteSelectedRows();

            }
        }


        int axisXStart = 0;
        int axisYStart = 0;

        int maxWidth = 1400;
        int maxHeight = 450;
        private void grvDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column != colWidth && e.Column != colHeight) return;

            string code = TextUtils.ToString(grvDetail.GetRowCellValue(e.RowHandle, colCode));
            if (!code.Contains(".")) return;

            int row = TextUtils.ToInt(code.Split('.')[0]);
            int col = TextUtils.ToInt(code.Split('.')[1]);

            int width = TextUtils.ToInt(grvDetail.GetRowCellValue(e.RowHandle, colWidth));
            int height = TextUtils.ToInt(grvDetail.GetRowCellValue(e.RowHandle, colHeight));

            int axisX = (width / 2) + axisXStart;
            int axisY = (height / 2) + axisYStart;

            grvDetail.SetRowCellValue(e.RowHandle, colAxisX, axisX);
            grvDetail.SetRowCellValue(e.RowHandle, colAxisY, axisY);

            axisXStart = maxWidth - width;
            axisYStart = maxHeight - height;


        }
    }
}
