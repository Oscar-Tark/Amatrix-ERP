/*Other Library*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintDataGrid
{
    public partial class PrintOptions : Form
    {
        public PrintOptions()
        {
            InitializeComponent();
            if (Main.Amatrix.mgt != "")
            {
                try
                { Main.Security_PWD pwd = new Main.Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
                catch (Exception erty) { }
            }
            else if (Main.Amatrix.acc != "")
            {
                try
                { Main.Security_PWD pwd = new Main.Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.acc); pwd.Owner = this; }
                catch (Exception erty) { }
            }
        }

        private const int CS_DROPSHADOW = 0x00020000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ClassStyle |= CS_DROPSHADOW;
                return p;
            }
        }

        public PrintOptions(List<string> availableFields)
        {
            InitializeComponent();
            int i = 0;
            foreach (string field in availableFields)
            {
                if (i < 6)
                {
                    chklst.Items.Add(field, true);
                }
                else
                {
                    chklst.Items.Add(field, false);
                }
                i++;
            }
        }

        private void PrintOtions_Load(object sender, EventArgs e)
        {
            // Initialize some controls
            rdoAllRows.Checked = true;
            chkFitToPageWidth.Checked = true; 
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public List<string> GetSelectedColumns()
        {
            List<string> lst = new List<string>();
            foreach (object item in chklst.CheckedItems)
                    lst.Add(item.ToString());
            return lst;
        }

        public string PrintTitle
        {
            get { return txtTitle.Text; }
        }

        public bool PrintAllRows
        {
            get { return rdoAllRows.Checked; }
        }

        public bool FitToPageWidth
        {
            get { return chkFitToPageWidth.Checked; }
        }

        private void tmeclsecal_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void dectmecal_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmecal.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void PrintOptions_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmecal.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Main.Properties.Settings.Default.opacity;
        }

        private void PrintOptions_Deactivate(object sender, EventArgs e)
        {
            dectmecal.Start();
        }
    }
}