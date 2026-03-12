using BaseBusiness.DTO;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Export;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Forms.DanhMuc.DuAn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
	public partial class frmSyntheticWorkerPartlist : _Forms
	{
        public ProjectModel project = new ProjectModel();
        public bool isTBP = false;
        public frmSyntheticWorkerPartlist()
		{
			InitializeComponent();
		}

		private void frmSyntheticWorkerPartlist_Load(object sender, EventArgs e)
		{
            this.Text += $" DỰ ÁN {project.ProjectCode} - {project.ProjectName}";
            while (xtraTabControl1.TabPages.Count > 0)
            {
                xtraTabControl1.TabPages.RemoveAt(0);
            }
            frmProjectPartList_New frm = new frmProjectPartList_New(isTBP);
			frm.project = project;
			OpenFormInTab(frm, $"VẬT TƯ DỰ ÁN ");

            frmProjectWorker_New frm1 = new frmProjectWorker_New();
            frm1.project = project;
            OpenFormInTab(frm1, $"NHÂN CÔNG DỰ ÁN");

            xtraTabControl1.SelectedTabPageIndex = 0;
        }

        private void OpenFormInTab(Form form, string tabTitle)
        {
			try
			{
                foreach (XtraTabPage tab in xtraTabControl1.TabPages)
                {
                    if (tab.Text == tabTitle)
                    {
                        xtraTabControl1.SelectedTabPage = tab;
                        return;
                    }
                }

                XtraTabPage tabPage = new XtraTabPage();
                tabPage.Text = tabTitle;

                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                form.Anchor = AnchorStyles.Top;
                form.Anchor = AnchorStyles.Bottom;
                form.Anchor = AnchorStyles.Left;
                form.Anchor = AnchorStyles.Right;

                tabPage.Controls.Add(form);

                xtraTabControl1.TabPages.Add(tabPage);

                xtraTabControl1.SelectedTabPage = tabPage;

                form.Show();
            }
			catch(Exception ex)
			{
                MessageBox.Show(ex.Message);
			}
        }
    }
}
