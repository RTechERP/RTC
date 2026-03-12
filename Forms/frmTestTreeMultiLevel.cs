using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmTestTreeMultiLevel : _Forms
    {

        TreeList treeListChild = new TreeList();
        TreeList treeListParent = new TreeList();
        public frmTestTreeMultiLevel()
        {
            InitializeComponent();
        }

        private void frmTestTreeMultiLevel_Load(object sender, EventArgs e)
        {
            // Tạo TreeList cha
            //TreeList treeListParent = new TreeList();
            treeListParent.Dock = DockStyle.Left;
            this.Controls.Add(treeListParent);

            // Thêm cột vào TreeList cha
            treeListParent.Columns.Add();
            treeListParent.Columns[0].Caption = "Parent Node";
            treeListParent.Columns[0].Visible = true;

            //treeListParent.AppendNode(new object[] { "Parent Node 1" }, null);
            //treeListParent.AppendNode(new object[] { "Parent Node 2" }, null);

            // Thêm một số node vào TreeList cha
            TreeListNode parentNode1 = treeListParent.AppendNode(new object[] { "Parent Node 1" }, null);
            TreeListNode parentNode2 = treeListParent.AppendNode(new object[] { "Parent Node 2" }, null);

            // Tạo TreeList con
            //TreeList treeListChild = new TreeList();
            //treeListChild.Dock = DockStyle.Fill;
            this.Controls.Add(treeListChild);

            // Thêm cột vào TreeList con
            treeListChild.Columns.Add();
            treeListChild.Columns[0].Caption = "Child Node";
            treeListChild.Columns[0].Visible = true;

            // Thêm một số node vào TreeList con
            treeListChild.AppendNode(new object[] { "Child Node 1" }, null);
            treeListChild.AppendNode(new object[] { "Child Node 2" }, null);

            // Thêm TreeList con vào node của TreeList cha
            parentNode1.Tag = treeListChild; // Gắn TreeList con vào một node trong TreeList cha

            // Tạo sự kiện để hiển thị TreeList con khi chọn node

            treeListParent.FocusedNodeChanged += TreeListParent_FocusedNodeChanged;
            //treeListParent.FocusedNodeChanged += (sender, e) =>
            //{
                
            //};

            // Ẩn TreeList con khi bắt đầu
            treeListChild.Visible = false;
        }

        private void TreeListParent_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) return;

            // Kiểm tra giá trị của node đã chọn
            string selectedNode = e.Node.GetValue(0).ToString();

            // Lấy vị trí của node đã chọn
            //int nodeHeight = treeListParent.GetRowHeight(e.Node); // Chiều cao của node
            //Point nodePosition = treeListParent.GetNodePosition(e.Node); // Vị trí của node trên màn hình

            // Đặt TreeList con ngay dưới node đã chọn
            treeListChild.Location = new Point(100, 100);
            treeListChild.Width = treeListParent.Width; // Đảm bảo TreeList con có chiều rộng bằng TreeList chính

            // Hiển thị TreeList con khi node "Parent Node 1" được chọn
            if (selectedNode == "Parent Node 1")
            {
                treeListChild.Visible = true;
            }
            else
            {
                treeListChild.Visible = false; // Ẩn TreeList con nếu không phải "Parent Node 1"
            }
        }
    }
}
