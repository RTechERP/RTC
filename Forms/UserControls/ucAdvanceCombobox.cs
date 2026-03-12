using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS.UserControls
{
    public delegate void AddButtonClickEvents();
    public delegate void ComboSelectedIndexChanged(int selectedIndex = -1);
    public partial class ucAdvanceCombobox : UserControl
    {
        #region VARIABLES
        private const string ROWVALUE_ADDNEW = "<Add New>";
        private const string COLNAME_DISPLAY = "Display";
        private const string COLNAME_VALUE = "Value";
        private bool lockEvents = false;//Cờ để lock sự kiện
        private DataTable dataTableSource;//Data để chứa dữ liệu
        #endregion

        public ucAdvanceCombobox()
        {
            InitializeComponent();
            cbValue.Items.Clear();
        }

        #region EVENTS
        /// <summary>
        /// Sự kiện button thêm
        /// </summary>
        public event AddButtonClickEvents OnAddButtonClickEvents;
        /// <summary>
        /// Sự kiện khi chọn item của combo
        /// </summary>
        public event ComboSelectedIndexChanged OnComboSelectedIndexChanged;
        #endregion

        #region PROPERTY
        private int _SelectedIndex=-1;
        /// <summary>
        /// Index phần tử đang chọn của combo
        /// </summary>
        public int RTCSelectedIndex
        {
            get 
            { 
                return cbValue.SelectedIndex; 
            }
            set 
            { 
                _SelectedIndex = value;
                cbValue.SelectedIndex = _SelectedIndex;
            }
        }
        /// <summary>
        /// Giá trị kiểu string của combo
        /// </summary>
        public string RTCText
        {
            get { return cbValue.Text; }
            set { cbValue.Text = value; }
        }
        private bool _IsUseAddButton=true;
        /// <summary>
        /// Cờ báo có sử dụng Add button hay không.
        /// </summary>
        public bool RTCIsUseAddButton
        {
            get { return _IsUseAddButton; }
            set 
            { 
                _IsUseAddButton = value;
                btnAdd.Visible = _IsUseAddButton;
                if (!btnAdd.Visible)
                    cbValue.Width = Width - 5;
                else
                    cbValue.Width = Width - btnAdd.Width-5;

            }
        }
        /// <summary>
        /// Ẩn hiện nút thêm phần tử
        /// </summary>
        public bool RTCAddButtonVisible
        {
            get 
            { 
                return btnAdd.Visible; 
            }
            set 
            {
                btnAdd.Visible = value;
                if (!btnAdd.Visible)
                    cbValue.Width = Width - 5;
                else
                    cbValue.Width = Width - btnAdd.Width - 5;
            }
        }
        private object _DataSource;
        /// <summary>
        /// Dữ liệu của combobox
        /// </summary>
        public object RTCDataSource
        {
            get { return _DataSource; }
            set {_DataSource = value; }
        }
        /// <summary>
        /// Tên cột dữ liệu hiển thị (khi datasource là datatable)
        /// </summary>
        public string RTCDisplayMember { get; set; }
        /// <summary>
        /// Tên cột dữ liệu lấy giá trị (khi datasource là datatable)
        /// </summary>
        public string RTCValueMember { get; set; }
        #endregion

        #region FUNCTIONS
        /// <summary>
        /// Nạp dữ liệu
        /// </summary>
        public void LoadData()
        {
            lockEvents = true;
            try
            {
                if (cbValue.DataSource!=null)
                    cbValue.DataSource = null;
                else
                    cbValue.Items.Clear();
                btnAdd.Enabled = false;
                if (_DataSource == null) return;
                
                dataTableSource = new DataTable();
                dataTableSource.Columns.Add(COLNAME_DISPLAY, typeof(string));
                dataTableSource.Columns.Add(COLNAME_VALUE, typeof(string));

                if (!_IsUseAddButton)
                {
                    DataRow dataRow = dataTableSource.NewRow();
                    dataRow[COLNAME_DISPLAY] = ROWVALUE_ADDNEW;
                    dataRow[COLNAME_VALUE] = ROWVALUE_ADDNEW;
                    dataTableSource.Rows.Add(dataRow);
                }

                if (_DataSource is DataTable)
                {
                    foreach (DataRow row in ((DataTable)_DataSource).Rows)
                    {
                        DataRow dataRow = dataTableSource.NewRow();
                        dataRow[COLNAME_DISPLAY] = row[RTCDisplayMember];
                        dataRow[COLNAME_VALUE] = row[RTCValueMember];
                        dataTableSource.Rows.Add(dataRow);
                    }
                }
                else if (_DataSource is List<string>)
                {
                    foreach (string item in (List<string>)_DataSource)
                    {
                        DataRow dataRow = dataTableSource.NewRow();
                        dataRow[COLNAME_DISPLAY] = item;
                        dataRow[COLNAME_VALUE] = item;
                        dataTableSource.Rows.Add(dataRow);
                    }
                }
                cbValue.DataSource = dataTableSource;
                cbValue.DisplayMember = COLNAME_DISPLAY;
                cbValue.ValueMember = COLNAME_VALUE;

                cbValue.SelectedIndex = -1;
                btnAdd.Enabled = true;
            }
            catch
            {
            }
            finally
            {
                lockEvents = false;
            }
        }
        /// <summary>
        /// Nạp dữ liệu
        /// </summary>
        /// <param name="dataSource">Dữ liệu nguồn</param>
        public void LoadData(object dataSource)
        {
            _DataSource = dataSource;
            LoadData();
        }
        #endregion

        #region EVENTS
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddButtonClickEvents?.Invoke();
        }
        private void cbValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lockEvents) return;
            if (_DataSource == null) return;
            if (!_IsUseAddButton)
            {
                if (cbValue.SelectedIndex == 0)
                {
                    try
                    {
                        lockEvents = true;
                        if (_IsUseAddButton)
                            cbValue.SelectedIndex = _SelectedIndex;
                        else
                            cbValue.SelectedIndex = _SelectedIndex+1;
                    }
                    catch
                    {
                    }
                    finally
                    {
                        lockEvents = false;
                    }
                    OnAddButtonClickEvents?.Invoke();
                    return;
                }
            }
            if (_IsUseAddButton)
            {
                _SelectedIndex = cbValue.SelectedIndex;
            }
            else 
                _SelectedIndex = cbValue.SelectedIndex-1;

            OnComboSelectedIndexChanged?.Invoke(_SelectedIndex);
        }
        #endregion
    }
}
