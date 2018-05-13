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
    public partial class Doc_stdio_canopner : Form
    {
        public Doc_stdio_canopner()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.installericon;
            this.Disposed += new EventHandler(Doc_stdio_canopner_Disposed);
            this.TopMost = true;
            InitializeComponent();
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

        void Doc_stdio_canopner_Disposed(object sender, EventArgs e)
        {
            this.ofd.FileOk -= this.ofd_FileOk;
            this.button4.Click -= this.button4_Click;
            this.dectmealrm.Tick -= this.dectmealrm_Tick;
            this.alrmclse.Tick -= this.alrmclse_Tick;
            this.Deactivate -= this.Doc_stdio_canopner_Deactivate;
            this.Load -= this.Doc_stdio_canopner_Load;
            this.Activated -= this.Doc_stdio_canopner_Activated;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void Doc_stdio_canopner_Load(object sender, EventArgs e)
        {

        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            Doc_stdio std = new Doc_stdio();
            std.Show();
            std.open_out(ofd.FileName, false);
            alrmclse.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Doc_stdio stdio = new Doc_stdio();
            stdio.tx("general template");
            alrmclse.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == passs)
            {
                dt.pass(passs);
                this.Close();
            }
            else
            {
                textBox1.BackColor = Color.DarkOrange;
            }
        }

        string passs = ""; Doc_stdio dt;
        public void pass(string text, Doc_stdio dtt)
        {
            dt = dtt;
            this.Show();
            button4.Enabled = false;
            int ndx = text.IndexOf("~AM-!~!Password!~!(");
            ndx = ndx + 19;
            int ndx2 = text.IndexOf(")", ndx);

            for (int i = ndx; i < ndx2; i++)
            {
                passs = passs + text[i];
            }
            button4.Enabled = true;
        }

        private void dectmealrm_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmealrm.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void alrmclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void Doc_stdio_canopner_Activated(object sender, EventArgs e)
        {
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Doc_stdio_canopner_Deactivate(object sender, EventArgs e)
        {
            dectmealrm.Start();
        }
    }
}
