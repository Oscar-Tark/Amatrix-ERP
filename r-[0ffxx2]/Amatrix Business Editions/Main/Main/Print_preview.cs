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
    public partial class Print_preview : Form
    {
        public Print_preview()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            this.Disposed += new EventHandler(Print_preview_Disposed);
            InitializeComponent();
            this.TopMost = true;
            this.Opacity = Properties.Settings.Default.opacity;
            this.Text = "Amatrix Previewer";
        }

        void Print_preview_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= Print_preview_Disposed;
            this.toolStripButton3.Click -= this.toolStripButton3_Click;
            this.toolStripButton1.Click -= this.toolStripButton1_Click;
            this.toolStripButton2.Click -= this.toolStripButton2_Click;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.Deactivate -= this.Print_preview_Deactivate;
            this.Load -= this.Print_preview_Load;
            this.Activated -= this.Print_preview_Activated;


            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
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

        private void Print_preview_Load(object sender, EventArgs e)
        {
            
        }

        public void tx(System.Drawing.Printing.PrintDocument pr, int pages)
        {
            printPreviewControl1.Rows = pages;
            printPreviewControl1.Document = pr;
            pd.Document = pr;
            this.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            printPreviewControl1.Zoom = printPreviewControl1.Zoom + 0.01;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            printPreviewControl1.Zoom = printPreviewControl1.Zoom - 0.01;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            pd.ShowDialog();
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

        private void Print_preview_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Print_preview_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }
    }
}
