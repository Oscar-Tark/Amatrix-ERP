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
    public partial class acc_rep_wiz : Form
    {
        public acc_rep_wiz()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Icon = Properties.Resources.installericon;
            InitializeComponent();
            this.Text = "Amatrix Guide : New Report";
            if (Main.Amatrix.acc != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.acc); pwd.Owner = this; }
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

        private void acc_rep_wiz_Load(object sender, EventArgs e)
        {

        }

        private Button bttn_tmp;
        private void bt_nxt_MouseEnter(object sender, EventArgs e)
        {
            bttn_tmp = (Button)sender;
            bttn_tmp.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void bt_nxt_MouseLeave(object sender, EventArgs e)
        {
            bttn_tmp = (Button)sender;
            bttn_tmp.BackgroundImage = Properties.Resources.btnsim1;
        }

        private void bt_nxt_MouseDown(object sender, MouseEventArgs e)
        {
            bttn_tmp = (Button)sender;
            bttn_tmp.BackgroundImage = Properties.Resources.btsimp;
        }

        private void bt_nxt_MouseUp(object sender, MouseEventArgs e)
        {
            bttn_tmp = (Button)sender;
            bttn_tmp.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void bt_nxt_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(bt_nxt) == true)
                {
                    tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
                }
                else
                {
                    tabControl1.SelectTab(tabControl1.SelectedIndex - 1);
                }
            }
            catch (Exception erty) { }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool p_a;
        private void button1_Click(object sender, EventArgs e)
        {
            acc_trns tranny = new acc_trns();

            try
            {
                tranny.tx(lv.FocusedItem.Text, lv.FocusedItem.Group.Header, p_a);
                this.Close();
            }
            catch (Exception erty) { }
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lbl_fin1.Text = lv.FocusedItem.Text;
            }
            catch (Exception erty) { }
        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            if (cb.Checked == true)
            {
                p_a = true;
            }
            else
            {
                p_a = false;
            }
        }
    }
}
