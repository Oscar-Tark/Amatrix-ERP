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
    public partial class cnt_mgmt : UserControl
    {
        public cnt_mgmt()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void btnldg_Click(object sender, EventArgs e)
        {
            mgmt_stratgy prj = new mgmt_stratgy();
            prj.Show();
        }

        private void btninvc_Click(object sender, EventArgs e)
        {
            mgmt_hr hr = new mgmt_hr();
            hr.Show();
        }

        private void btnacc_Click(object sender, EventArgs e)
        {
            mgmt_opr opr = new mgmt_opr();
            opr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mgmt_pr pr = new mgmt_pr();
            pr.Show();
        }

        private void cnt_mgmt_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mgmt_supch shc_pc = new mgmt_supch();
            shc_pc.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reports prt = new Reports();
            prt.Show();
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

        private void btnacc_MouseEnter(object sender, EventArgs e)
        {
            btnacc.BackgroundImage = Properties.Resources.operon;
            vis_int = 0;
            vis_temp = (Control)btnacc;
            bigger.Start();
        }

        private void btnacc_MouseLeave(object sender, EventArgs e)
        {
            btnacc.BackgroundImage = Properties.Resources.oper;
            bigger.Stop();
            btnacc.Size = new Size(115, 114);
            btnacc.Location = new Point(145, 92);
        }

        private void btnldg_MouseEnter(object sender, EventArgs e)
        {
            btnldg.BackgroundImage = Properties.Resources.projcton;
            vis_int = 0;
            vis_temp = (Control)btnldg;
            bigger.Start();
        }

        private void btnldg_MouseLeave(object sender, EventArgs e)
        {
            btnldg.BackgroundImage = Properties.Resources.projct;
            bigger.Stop();
            btnldg.Size = new Size(115, 114);
            btnldg.Location = new Point(416, 92);
        }

        private void btninvc_MouseEnter(object sender, EventArgs e)
        {
            btninvc.BackgroundImage = Properties.Resources.humrecon;
            vis_int = 0;
            vis_temp = (Control)btninvc;
            bigger.Start();
        }

        private void btninvc_MouseLeave(object sender, EventArgs e)
        {
            btninvc.BackgroundImage = Properties.Resources.humrec;
            bigger.Stop();
            btninvc.Size = new Size(115, 114);
            btninvc.Location = new Point(686, 92);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.employon;
            vis_int = 0;
            vis_temp = (Control)button2;
            bigger.Start();
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.employ;
            bigger.Stop();
            button2.Size = new Size(115, 114);
            button2.Location = new Point(145, 267);
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackgroundImage = Properties.Resources.produson;
            vis_int = 0;
            vis_temp = (Control)button3;
            bigger.Start();
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackgroundImage = Properties.Resources.produs;
            bigger.Stop();
            button3.Size = new Size(115, 114);
            button3.Location = new Point(416, 267);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.crseson;
            vis_int = 0;
            vis_temp = (Control)button1;
            bigger.Start();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.crses;
            bigger.Stop();
            button1.Size = new Size(115, 114);
            button1.Location = new Point(686, 267);
        }

        private void btnacc_MouseDown(object sender, MouseEventArgs e)
        {
            btnacc.BackgroundImage = Properties.Resources.operdwn;
        }

        private void btnacc_MouseUp(object sender, MouseEventArgs e)
        {
            btnacc.BackgroundImage = Properties.Resources.operon;
        }

        private void btnldg_MouseDown(object sender, MouseEventArgs e)
        {
            btnldg.BackgroundImage = Properties.Resources.projctdwn;
        }

        private void btnldg_MouseUp(object sender, MouseEventArgs e)
        {
            btnldg.BackgroundImage = Properties.Resources.projcton;
        }

        private void btninvc_MouseDown(object sender, MouseEventArgs e)
        {
            btninvc.BackgroundImage = Properties.Resources.humrecdwn;
        }

        private void btninvc_MouseUp(object sender, MouseEventArgs e)
        {
            btninvc.BackgroundImage = Properties.Resources.humrecon;
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.employdwn;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.employon;
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            button3.BackgroundImage = Properties.Resources.produsdwn;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            button3.BackgroundImage = Properties.Resources.produson;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.crsesdwn;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.crseson;
        }
    }
}
