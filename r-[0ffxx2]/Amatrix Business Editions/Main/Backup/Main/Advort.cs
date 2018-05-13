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
    public partial class Advort : Form
    {
        public Advort()
        {
            InitializeComponent();
            this.Controls.Remove(webBrowser1);
            webBrowser1.Navigate("http://astreous.webs.com/adds.htm");
            this.SendToBack();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void open()
        {
            this.Show();
            TopMost = true;
            this.Location = new Point(SystemInformation.WorkingArea.Width - this.Size.Width, SystemInformation.WorkingArea.Height - this.Size.Height);
        }

        private void toolStripButton1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox1.Select();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "09976889-b44")
            {
                Advort_settings stt = new Advort_settings();
                stt.Show();
            }
        }

        private void bkk_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void bkk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Controls.Add(webBrowser1);
        }
    }
}
