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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class cnt_journ : UserControl
    {
        public cnt_journ()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            acc_journ jrn = new acc_journ();
            jrn.Show();
        }

        private void btnldg_Click(object sender, EventArgs e)
        {
            acc_ledg ldg = new acc_ledg();
            ldg.Show();
        }

        private void btninvc_Click(object sender, EventArgs e)
        {
            acc_invce vce = new acc_invce();
            vce.Show();
        }

        private void btnacc_Click(object sender, EventArgs e)
        {
            acc_rep_wiz wzz = new acc_rep_wiz();
            wzz.Show();
        }

        private void btncust_Click(object sender, EventArgs e)
        {
        }

        //effects
        private Control vis_temp; private int vis_int = 0;
        private void bigger_Tick(object sender, EventArgs e)
        {
            vis_int = vis_int + 1;
            vis_temp.Size = new Size(vis_temp.Size.Width + 2, vis_temp.Size.Width + 2);
            vis_temp.Location = new Point(vis_temp.Location.X - 1, vis_temp.Location.Y - 1);
            if (vis_int >= 2) { bigger.Stop(); }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.jrn1on;
            vis_int = 0;
            vis_temp = (Control)button1;
            bigger.Start();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.jrn1;
            bigger.Stop();
            button1.Size = new Size(115, 114);
            button1.Location = new Point(145, 92);
        }

        private void btnldg_MouseEnter(object sender, EventArgs e)
        {
            btnldg.BackgroundImage = Properties.Resources.ledg1on;
            vis_int = 0;
            vis_temp = (Control)btnldg;
            bigger.Start();
        }

        private void btnldg_MouseLeave(object sender, EventArgs e)
        {
            btnldg.BackgroundImage = Properties.Resources.ledg1;
            bigger.Stop();
            btnldg.Size = new Size(115, 114);
            btnldg.Location = new Point(416, 92);
        }

        private void btninvc_MouseEnter(object sender, EventArgs e)
        {
            btninvc.BackgroundImage = Properties.Resources.invc1on;
            vis_int = 0;
            vis_temp = (Control)btninvc;
            bigger.Start();
        }

        private void btninvc_MouseLeave(object sender, EventArgs e)
        {
            btninvc.BackgroundImage = Properties.Resources.invc1;
            bigger.Stop();
            btninvc.Size = new Size(115, 114);
            btninvc.Location = new Point(686, 92);
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.jrn1dwn;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.jrn1on;
        }

        private void btnldg_MouseDown(object sender, MouseEventArgs e)
        {
            btnldg.BackgroundImage = Properties.Resources.ledg1dwn;
        }

        private void btnldg_MouseUp(object sender, MouseEventArgs e)
        {
            btnldg.BackgroundImage = Properties.Resources.ledg1on;
        }

        private void btninvc_MouseDown(object sender, MouseEventArgs e)
        {
            btninvc.BackgroundImage = Properties.Resources.invc1dwn;
        }

        private void btninvc_MouseUp(object sender, MouseEventArgs e)
        {
            btninvc.BackgroundImage = Properties.Resources.invc1on;
        }
    }
}
