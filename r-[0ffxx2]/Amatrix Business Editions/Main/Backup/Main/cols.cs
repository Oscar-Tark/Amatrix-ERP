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
    public partial class cols : Form
    {
        public cols()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            this.Text = "Amatrix Color Picker";
            this.Disposed += new EventHandler(cols_Disposed);
            this.Opacity = 1.0;
            Init();
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
            try
            {
                r.Value = Properties.Settings.Default.r;
                g.Value = Properties.Settings.Default.g;
                bl.Value = Properties.Settings.Default.b;
            }
            catch (Exception retr)
            { }

            if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }
        }

        void cols_Disposed(object sender, EventArgs e)
        {
            Main.Amatrix.rgb = 0;
        }

        private void rgbup_Tick(object sender, EventArgs e)
        {
            if (deft.CheckState == CheckState.Unchecked)
            {
                Main.Amatrix.rgb = 1;
            }
            else if (deft.CheckState == CheckState.Checked)
            {
                Main.Amatrix.rgb = 2;
            }
        }

        private void r_ValueChanged(object sender, EventArgs e)
        {
            if (txti == 0)
            {
                Properties.Settings.Default.defrgb = 1;
                Properties.Settings.Default.Save();
                pbxprev.BackColor = Color.FromArgb(r.Value, g.Value, bl.Value);
                Properties.Settings.Default.r = r.Value;
                Properties.Settings.Default.Save();
            }
        }

        private void g_Scroll(object sender, EventArgs e)
        {
            if (txti == 0)
            {
                Properties.Settings.Default.defrgb = 1;
                Properties.Settings.Default.Save();
                pbxprev.BackColor = Color.FromArgb(r.Value, g.Value, bl.Value);
                Properties.Settings.Default.g = g.Value;
                Properties.Settings.Default.Save();
            }
        }

        private void bl_Scroll(object sender, EventArgs e)
        {
            if (txti == 0)
            {
                Properties.Settings.Default.defrgb = 1;
                Properties.Settings.Default.Save();
                pbxprev.BackColor = Color.FromArgb(r.Value, g.Value, bl.Value);
                Properties.Settings.Default.b = bl.Value;
                Properties.Settings.Default.Save();
            }
        }

        private void r_Scroll(object sender, EventArgs e)
        {
            if (gry.Checked == true)
            {
                Properties.Settings.Default.defrgb = 1;
                Properties.Settings.Default.Save();
                g.Value = r.Value;
                bl.Value = r.Value;
            }
            else if (txt.Checked == true)
            {
                Properties.Settings.Default.txtrgb = r.Value; Properties.Settings.Default.Save();
                g.Value = r.Value;
                bl.Value = r.Value;
            }
            else { }
            pbxprev.BackColor = Color.FromArgb(r.Value, g.Value, bl.Value);
        }

        private void g_Scroll_1(object sender, EventArgs e)
        {
            if (gry.Checked == true)
            {
                Properties.Settings.Default.defrgb = 1;
                Properties.Settings.Default.Save();
                r.Value = g.Value;
                bl.Value = g.Value;
            }
            else if (txt.Checked == true)
            {
                Properties.Settings.Default.txtrgb = r.Value; Properties.Settings.Default.Save();
                r.Value = g.Value;
                bl.Value = g.Value;
            }
            else { }
            pbxprev.BackColor = Color.FromArgb(r.Value, g.Value, bl.Value);
        }
        int txti = 0;
        private void bl_Scroll_1(object sender, EventArgs e)
        {
            if (gry.Checked == true)
            {
                Properties.Settings.Default.defrgb = 1;
                Properties.Settings.Default.Save();
                r.Value = bl.Value;
                g.Value = bl.Value;
            }
            else if (txt.Checked == true)
            {
                Properties.Settings.Default.txtrgb = r.Value; Properties.Settings.Default.Save();
                r.Value = bl.Value;
                g.Value = bl.Value;
            }
            else { }
            pbxprev.BackColor = Color.FromArgb(r.Value, g.Value, bl.Value);
        }

        private void clse_Click(object sender, EventArgs e)
        {
            tmeclse.Start();
        }

        private void cols_Load(object sender, EventArgs e)
        {

        }

        private void txt_CheckedChanged(object sender, EventArgs e)
        {
            if (txt.Checked == true)
            {
                txti = 1;
            }
            else
            {
                txti = 0;
            }
        }

        private void tmeclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void decjourn_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                decjourn.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void colact(object sender, EventArgs e)
        {
            try
            {
                decjourn.Stop();
            }
            catch (Exception tex)
            {
            }
            this.Opacity = 1.0;
        }

        private void coldect(object sender, EventArgs e)
        {
            decjourn.Start();
        }

        private void deft_CheckStateChanged(object sender, EventArgs e)
        {
            if (deft.CheckState == CheckState.Checked)
            {
                Properties.Settings.Default.defrgb = 0;
                Properties.Settings.Default.Save();
                r.Enabled = false;
                g.Enabled = false;
                bl.Enabled = false;
                gry.Enabled = false;
                txt.Enabled = false;
            }
            else if (deft.CheckState == CheckState.Unchecked)
            {
                Properties.Settings.Default.defrgb = 1;
                Properties.Settings.Default.Save();
                r.Enabled = true;
                g.Enabled = true;
                bl.Enabled = true;
                gry.Enabled = true;
                txt.Enabled = true;
            }
        }

        Button bt_temp;
        private void clls(object sender, EventArgs e)
        {
            bt_temp = (Button)sender;
            r.Value = bt_temp.BackColor.R;
            g.Value = bt_temp.BackColor.G;
            bl.Value = bt_temp.BackColor.B;
        }
    }
}
