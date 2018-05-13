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
    public partial class app_abt : Form
    {
        public app_abt()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.Text = "Application About";
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

        private void app_abt_Load(object sender, EventArgs e)
        {

        }

        public void descr(string prod)
        {
            descset(prod);
            this.Show();
        }

        private void descset(string prod2)
        {
            tsl1.Text = prod2;
            this.Text = "About : " + prod2;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            abtclse.Start();
        }

        private void toolStripButton1_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void toolStripButton1_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void abtclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void dectmeabt_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmeabt.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void app_abt_Activated(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = Properties.Settings.Default.opacity;
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void app_abt_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }
    }
}
