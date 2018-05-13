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
using System.Threading;

namespace Main
{
    public partial class Scrn_snip : Form
    {
        public Scrn_snip()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Disposed += new EventHandler(Scrn_snip_Disposed);
            InitializeComponent();
            this.Text = "Amatrix Screen Snipper";
            this.Opacity = Properties.Settings.Default.opacity;
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

        void Scrn_snip_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= Scrn_snip_Disposed;
            this.sfd.FileOk -= this.sfd_FileOk;
            abtclse.Tick -= abtclse_Tick;
            rb2.CheckedChanged -= rb2_CheckedChanged;
            this.btok.Click -= this.btok_Click;

            this.btccl.Click -= this.button3_Click;

            this.button3.Click -= this.button3_Click;

            this.button2.Click -= this.button2_Click;

            this.button1.Click -= this.button1_Click;

            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            
            this.Deactivate -= this.Scrn_snip_Deactivate;
            this.Load -= this.Scrn_snip_Load;
            this.Activated -= this.Scrn_snip_Activated;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void sfd_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                nutim.Save(sfd.FileName);
                panel1.Visible = true;
                this.Visible = true;
                this.BringToFront();
                this.Activate();
            }
            catch (Exception excscrsveno)
            {
            }
        }

        private Image nutim;
        private void btok_Click(object sender, EventArgs e)
        {
            if (rb2.Checked == true)
            {
                this.Visible = false;
                foreach (Form fnt in Application.OpenForms)
                {
                    fnt.Visible = false;
                }
                nutim = (System.Drawing.Image)Utilities.CaptureScreen();
                sfd.ShowDialog();
                foreach (Form fnt2 in Application.OpenForms)
                {
                    if (fnt2.Name == "Scrn_snip" || fnt2.Name == "Scrsve")
                    { }
                    else
                    {
                        fnt2.Visible = true;
                    }
                }
                this.BringToFront();
            }
            else
            {
                this.Visible = false;
                nutim = (System.Drawing.Image)Utilities.CaptureScreen();
                sfd.ShowDialog();
                this.Visible = true;
                this.BringToFront();
            }
        }

        private Button bttn = new Button();

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(sfd.FileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void Scrn_snip_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            abtclse.Start();
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

        private void Scrn_snip_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Scrn_snip_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }

        private void abtclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2.Checked == true)
            {
                foreach (Form fnt in Application.OpenForms)
                {
                    if (fnt.Name == "Scrn_snip" || fnt.Name == "Scrsve")
                    { }
                    else
                    {
                        fnt.Visible = false;
                    }
                }
            }
            else
            {
                foreach (Form fnt2 in Application.OpenForms)
                {
                    if (fnt2.Name == "Scrn_snip" || fnt2.Name == "Scrsve" || fnt2.Name == "Amatrix_Server_Lite" || fnt2.Name == "Amatrix Sever Client Lite")
                    { }
                    else
                    {
                        fnt2.Visible = true;
                    }
                }
                this.Select();
            }
        }
    }
}
