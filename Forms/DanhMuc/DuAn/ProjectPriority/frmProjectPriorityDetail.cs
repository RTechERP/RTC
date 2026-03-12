using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectPriorityDetail : _Forms
    {
        public ProjectPriorityModel model = new ProjectPriorityModel();
        public frmProjectPriorityDetail()
        {
            InitializeComponent();
        }

        private void frmProjectPriorityDetail_Load(object sender, EventArgs e)
        {
            LoadPriorityType();
            LoadData();

        }

        //private void LoadScore()
        //{
        //    List<int> listScore = new List<int> { 1, 2, 3, 4, 5 };
        //    txtScore.DataSource = listScore;
        //}

        private void LoadPriorityType()
        {
            List<ProjectPriorityModel> list = SQLHelper<ProjectPriorityModel>.FindByAttribute("ParentID", 0);
            cboParent.Properties.DataSource = list;
            cboParent.Properties.DisplayMember = "ProjectCheckpoint";
            cboParent.Properties.ValueMember = "ID";
        }

        private void LoadData()
        {

            if (model.ID != 0)
            {
                txtCode.Text = model.Code;
                txtCheckpoint.Text = model.ProjectCheckpoint;
                txtRate.Text = model.Rate.ToString();
                txtScore.SelectedIndex = model.Score;
                cboParent.EditValue = model.ParentID;

            }
            else
            {
                int STT = SQLHelper<ProjectPriorityModel>.FindAll().Count;
                txtCode.Text = "P" + STT.ToString();
            }
        }

        private void btnColseAndSave_Click(object sender, EventArgs e)
        {
            if (SaveData() == true)
            {
                this.DialogResult = DialogResult.OK;
            }
        }


        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData() == true)
            {
                model = new ProjectPriorityModel();
                txtCode.Text = "";
                txtCheckpoint.Text = "";
                txtRate.Text = "";
                txtScore.Text = "";
                cboParent.EditValue = null;
                LoadData();

            }
        }
        private bool SaveData()
        {
            if (ValidateData() == false) return false;
            model.Code = txtCode.Text.ToUpper().Trim();
            model.ParentID = TextUtils.ToInt(cboParent.EditValue);
            model.ProjectCheckpoint = txtCheckpoint.Text.Trim();
            model.Rate = TextUtils.ToDecimal(txtRate.Text) / 100;
            model.Score = (TextUtils.ToInt(txtScore.SelectedIndex));
            model.Priority = TextUtils.ToDecimal(txtRate.Text) * TextUtils.ToInt(txtScore.SelectedIndex) / 100 ;


            if (model.ID != 0)
            {
                var update = SQLHelper<ProjectPriorityModel>.Update(model);
                //  ProjectPriorityBO.Instance.Update(model);
            }
            else
            {
                var insert = SQLHelper<ProjectPriorityModel>.Insert(model);
                //  ProjectPriorityBO.Instance.Insert(model);
            }

            return true;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã ưu tiên!", "Thông báo");
                return false;
            }
            if (TextUtils.ToInt(cboParent.EditValue) == 0 && model.ParentID != 0)
            {
                MessageBox.Show("Vui lòng chọn Loại ưu tiên!", "Thông báo");
                return false;
            }
            if (string.IsNullOrEmpty(txtCheckpoint.Text))
            {
                MessageBox.Show("Vui lòng nhập Checkpoint!", "Thông báo");
                return false;
            }
            if (string.IsNullOrEmpty(txtRate.Text))
            {
                MessageBox.Show("Vui lòng nhập Trọng số!", "Thông báo");
                return false;
            }
            if (string.IsNullOrEmpty(txtScore.SelectedIndex.ToString()))
            {
                MessageBox.Show("Vui lòng nhập Điểm!", "Thông báo");
                return false;
            }
            string pattern = @"^[a-zA-Z0-9_-]+$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(txtCode.Text.Trim()))
            {
                MessageBox.Show("Mã ưu tiên không được chứa kí tự tiếng Việt và khoảng trắng!", "Thông báo");
                return false;
            }
            var exp1 = new Expression("Code", txtCode.Text.Trim());
            var exp2 = new Expression("ID", model.ID, "<>");
            var countCode = (SQLHelper<ProjectPriorityModel>.FindByExpression(exp1.And(exp2)));
            if (countCode.Count > 0)
            {
                MessageBox.Show($"Mã ưu tiên [{txtCode.Text.Trim()}] đã tồn tại. Vui lòng nhập mã ưu tiên khác!", "Thông báo");
                return false;
            }
            return true;
        }
        //private bool isContainVNChar(string code)
        //{
        //    int count = 0;
        //    foreach (char c in code)
        //    {
        //        if ((IsEnglishCharacter(c) && IsVietnameseCharacter(c)) == true && char.IsSymbol(c) == false
        //            && char.IsPunctuation(c) == false)
        //        {
        //            count++;
        //        }           
        //    }
        //    if (count > 0) return false;
        //    else return true;

        //}
        //static bool IsEnglishCharacter(char character)
        //{
        //    // Kiểm tra xem ký tự có phải là chữ cái không
        //    return char.IsLetter(character);
        //}

        //static bool IsVietnameseCharacter(char character)
        //{
        //    // Kiểm tra các ký tự tiếng Việt trong khoảng mã Unicode
        //    return character >= 0x00C0 && character <= 0x1EF9;
        //}

        private void frmProjectPriorityDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void txtCode_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtRate_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}