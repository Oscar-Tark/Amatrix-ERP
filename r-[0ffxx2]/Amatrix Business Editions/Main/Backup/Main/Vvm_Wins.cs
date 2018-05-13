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
    public partial class Vvm_Wins : Form
    {
        public Vvm_Wins()
        {
            InitializeComponent();
            //opcty_up.Start();
            this.Location = new Point(SystemInformation.WorkingArea.Width - this.Size.Width, SystemInformation.WorkingArea.Height - this.Size.Height);
        }

        private void Vvm_Wins_Load(object sender, EventArgs e)
        {
            
        }

        public void refresh()
        {
            flp.Controls.Clear();
            foreach (Control flpp in flp.Controls)
            {
                try
                {
                    flpp.MouseEnter -= pctre_MouseEnter;
                    flpp.MouseLeave -= Vvm_Wins_MouseLeave;
                    flpp.Click -= pctre_Click;
                }
                catch (Exception erty) { }
                flpp.Dispose();
            }
            foreach (Form f in Application.OpenForms)
            {
                PictureBox pctre = new PictureBox();
                pctre.BackgroundImageLayout = ImageLayout.Zoom;
                pctre.BackColor = Color.Transparent;
                pctre.Size = new Size(179, 92);
                pctre.ContextMenuStrip = cms;
                pctre.MouseEnter += new EventHandler(pctre_MouseEnter);
                pctre.MouseLeave += new EventHandler(Vvm_Wins_MouseLeave);
                pctre.Click += new EventHandler(pctre_Click);
                flp.Controls.Add(pctre);
                if (f.WindowState != FormWindowState.Minimized)
                {
                    Bitmap b = new Bitmap(f.DrawToImage());
                    pctre.BackgroundImage = b;
                }
                else
                {
                    pctre.BackgroundImage = Properties.Resources.finalize;
                }

                if (f.Name == this.Name || f.Name == "Virtual_window_manager" || f.Name == "Amatrix_Server_Lite" || f.Name == "Amatrix_Sever_Client_Lite")
                {
                    pctre.Visible = false;
                }
            }
        }

        void pctre_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            flp.Visible = false;
            this.Size = new Size(button1.Size.Width, button1.Size.Height);
            this.Location = new Point(SystemInformation.WorkingArea.Width - this.Size.Width, SystemInformation.WorkingArea.Height - this.Size.Height);
        }

        PictureBox pbx = new PictureBox();
        void pctre_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                timer2.Stop();
                pbx = (PictureBox)sender;
                Application.OpenForms[flp.Controls.IndexOf(pbx)].WindowState = FormWindowState.Normal;
                Application.OpenForms[flp.Controls.IndexOf(pbx)].Select();
            }
            catch (Exception erty) { }
        }

        private void Vvm_Wins_SizeChanged(object sender, EventArgs e)
        {
            //refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
            redraw();
            flp.Visible = true;
            button1.Visible = false;
            //this.Opacity = 0.20;
            //opcty_up.Start();
        }

        private void redraw()
        {
            this.Size = new Size(SystemInformation.VirtualScreen.Width, 100);
            this.Location = new Point(0, SystemInformation.VirtualScreen.Height - this.Size.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Size = new Size(button1.Size.Width, button1.Size.Height);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            opcty.Start();
        }

        private void Vvm_Wins_MouseLeave(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void flp_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void opcty_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity <= 0.20)
            {
                opcty.Stop(); 
                button1.Visible = true;
                flp.Visible = false;

                this.Size = new Size(button1.Size.Width, button1.Size.Height);
                this.Location = new Point(SystemInformation.WorkingArea.Width - this.Size.Width, SystemInformation.WorkingArea.Height - this.Size.Height);
                this.Opacity = 0.90;
            }
        }

        private void opcty_up_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 0.90) { opcty_up.Stop(); }
            this.Opacity = this.Opacity + 0.5;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.OpenForms[flp.Controls.IndexOf(pbx)].Close();
        }

        private void cms_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
        }
    }
}
