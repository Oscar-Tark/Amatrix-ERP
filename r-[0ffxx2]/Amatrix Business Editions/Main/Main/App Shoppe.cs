/*Amatrix Data Center
    Copyright (C) 2013  Oscar Arjun Singh Tark, Benjamin Jack Johnson

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class App_Shoppe : Form
    {
        public App_Shoppe()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Disposed += new EventHandler(shpdisp);
            InitializeComponent();
            this.Opacity = Properties.Settings.Default.opacity;
            Init();
            /*try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
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

        private void Init()
        {
            shpclse.Text = "Close";
            if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }
            wb.Navigate("http://astreous.tk/");
        }

        void shpdisp(object sender, EventArgs e)
        {
            toolStripButton4.Click -= this.toolStripButton4_Click;
            toolStripButton5.Click -= toolStripButton5_Click;
            this.toolStripButton1.Click -= this.toolStripButton1_Click_1;
            this.shproccess.Tick -= this.shproccesstc;
            this.toolStripButton3.Click -= this.toolStripButton1_Click;
            this.toolStripButton2.Click -= this.toolStripButton2_Click;
            this.shpclse.MouseLeave -= this.shpclseext;
            this.shpclse.ButtonClick -= this.shpclse_Click;
            this.shpclse.MouseEnter -= this.shpclsehov;
            this.rstrtshp.Click -= this.rstrtshp_Click;
            this.clseshp.Tick -= this.clseshptc;
            this.dectmeshp.Tick -= this.dectmeshp_Tick;
            this.wb.ProgressChanged -= this.wb_ProgressChanged;
            this.wb.Navigating -= this.wb_Navigating;
            this.wb.Navigated -= this.wb_Navigated;
            this.dots.Tick -= this.dots_Tick;
            this.Deactivate -= this.Appshpdev;
            this.Load -= this.App_Shoppe_Load;
            this.DoubleClick -= this.App_Shoppe_dc;
            this.Activated -= this.App_Shoppe_Activated;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void App_Shoppe_Load(object sender, EventArgs e)
        {
            this.Text = "Amatrix Shoppe";
        }

        private void shproccesstc(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.lockstat == "Locked")
            {
                this.Enabled = false;
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
            else if (Properties.Settings.Default.lockstat == "none")
            {
                this.Enabled = true;
                this.Visible = true;
                this.ShowInTaskbar = true;
            }
            else
            { }
        }

        private void shpclse_Click(object sender, EventArgs e)
        {
            clseshp.Interval = Properties.Settings.Default.Frmrtem;
            clseshp.Start();
        }

        private void dectmeshp_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmeshp.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void Appshpdev(object sender, EventArgs e)
        {
            dectmeshp.Start();
        }

        private void App_Shoppe_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeshp.Stop();
            }
            catch (Exception excprevdec)
            {
            }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void clseshptc(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void todev_Click(object sender, EventArgs e)
        {
            AMDS amd = new AMDS();
            amd.Show();
            this.Close();
        }
        private void shpclsehov(object sender, EventArgs e)
        {
            shpclse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void shpclseext(object sender, EventArgs e)
        {
            shpclse.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void App_Shoppe_dc(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void rstrtshp_Click(object sender, EventArgs e)
        {
            App_Shoppe shp = new App_Shoppe();
            shp.Show();
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            wb.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            wb.GoForward();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            wb.Navigate("http://amatrixshoppe.tk/");
        }

        private void dots_Tick(object sender, EventArgs e)
        {
            if (toolStripLabel1.Text == ".")
            {
                toolStripLabel1.Text = " .";
            }
            else if (toolStripLabel1.Text == " .")
            {
                toolStripLabel1.Text = "  .";
            }
            else if (toolStripLabel1.Text == "  .")
            {
                toolStripLabel1.Text = ".";
            }
        }

        private void wb_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            toolStripLabel4.Visible = true;
            toolStripLabel1.Visible = true;
            addr.Text = "Navigating";
        }

        private void wb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            toolStripLabel4.Visible = false;
            toolStripLabel1.Visible = false;
            addr.Text = wb.DocumentTitle;
        }

        private void wb_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            toolStripLabel4.Text = "Loaded " + e.CurrentProgress.ToString() + " of " + e.MaximumProgress.ToString();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Advert_Submit sbt = new Advert_Submit();
            sbt.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            wb.Stop();
        }
    }
}
