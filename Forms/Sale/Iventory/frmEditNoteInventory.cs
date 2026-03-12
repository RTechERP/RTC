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

namespace BMS
{
    public partial class frmEditNoteInventory : _Forms
    {
        public InventoryModel Inventory = new InventoryModel();
        public frmEditNoteInventory()
        {
            InitializeComponent();
        }

        private void frmEditNoteInventory_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            if(Inventory.ID > 0)
            {
                textBox1.Text = Inventory.Note;
            }    
        }
        bool ValidateForm()
        {    
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return true;

            Inventory.Note = textBox1.Text.Trim();

            InventoryBO.Instance.Update(Inventory);
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(saveData())
            {
                this.DialogResult = DialogResult.OK;
            }    
        }
    }
}
