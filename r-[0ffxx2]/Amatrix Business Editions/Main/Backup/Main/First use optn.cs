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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Main
{
    public partial class First_use_optn : Form
    {
        public First_use_optn()
        {
            this.TopMost = true;
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.BringToFront();
            this.Text = "Amatrix Password Settings";
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

        private void First_use_optn_Load(object sender, EventArgs e)
        {

        }

        private void wincol_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            cols cmn = new cols();
            cmn.Show();
            cmn.Disposed += new EventHandler(cmn_Disposed);
        }

        void cmn_Disposed(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Main.Amatrix.ActiveForm.BackgroundImage = null;
        }

        private void confnewpwus_Click(object sender, EventArgs e)
        {
            if (chbnewpw.Checked == true && poid.Text == poid2.Text && textBox1.Text == Properties.Settings.Default.lockpss)
            {
                if (poid.Text != "none")
                {
                    Properties.Settings.Default.lockpss = poid2.Text;
                    lblsvepriv.Visible = true;
                    Properties.Settings.Default.Save();
                    this.Close();
                }
                else { Am_err ner = new Am_err(); ner.tx("'none' is not a valid password."); }
            }
            else
            {
                if (poid.Text == poid2.Text)
                {
                    poid.ForeColor = Color.Gray;
                    poid2.ForeColor = Color.Gray;
                    poid.BackColor = Color.White;
                    poid2.BackColor = Color.White;
                }
                else
                {
                    poid.ForeColor = Color.White;
                    poid2.ForeColor = Color.White;
                    poid.BackColor = Color.DarkOrange;
                    poid2.BackColor = Color.DarkOrange;
                }
                if (textBox1.Text == Properties.Settings.Default.lockpss)
                {
                    textBox1.ForeColor = Color.Black;
                    textBox1.ForeColor = Color.Black;
                    textBox1.BackColor = Color.Gainsboro;
                    textBox1.BackColor = Color.Gainsboro;
                }
                else
                {
                    textBox1.ForeColor = Color.White;
                    textBox1.ForeColor = Color.White;
                    textBox1.BackColor = Color.DarkOrange;
                    textBox1.BackColor = Color.DarkOrange;
                }
            }
        }

        private void chbnewpw_CheckedChanged(object sender, EventArgs e)
        {
            if (chbnewpw.Checked == true)
            {
                poid.Enabled = true;
                poid2.Enabled = true;
                textBox1.Enabled = true;
            }
            else if (chbnewpw.Checked == false)
            {
                poid.Enabled = false;
                poid2.Enabled = false;
                textBox1.Enabled = false;
            }
        }

        private void usr_ch(object sender, EventArgs e)
        {
            /*if (checkBox2.Checked == true && textBox1.Text != "")
            {
                Properties.Settings.Default.usr = textBox1.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                ttp.Show("Check to change Your Username", this, checkBox2.Location.X - 8, checkBox2.Location.Y - 50);
            }*/
        }
    }
}
