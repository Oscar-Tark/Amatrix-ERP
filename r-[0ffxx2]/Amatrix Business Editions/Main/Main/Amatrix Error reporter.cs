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
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Am_err : Form
    {
        public Am_err()
        {
            this.Icon = SystemIcons.Error;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Opacity = Properties.Settings.Default.opacity;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Disposed += new EventHandler(Am_err_Disposed);
            InitializeComponent();
            Initerr();
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

        void Am_err_Disposed(object sender, EventArgs e)
        {
            this.clseerr.MouseLeave -= this.uku_MouseLeave;
            this.clseerr.Click -= this.clseerr_Click;
            this.clseerr.MouseDown -= this.uku_MouseDown;
            this.clseerr.MouseUp -= this.uku_MouseUp;
            this.clseerr.MouseEnter -= this.uku_MouseEnter;
            this.dectmeerr.Tick -= this.dectmeerr_Tick;
            this.clseerrr.Tick -= this.clseerrr_Tick;
            this.enblerr.Tick -= this.enblerrtc;
            this.uku.MouseLeave -= this.uku_MouseLeave;
            this.uku.Click -= this.clseerr_Click;
            this.uku.MouseDown -= this.uku_MouseDown;
            this.uku.MouseUp -= this.uku_MouseUp;
            this.uku.MouseEnter -= this.uku_MouseEnter;
            this.bkk.WorkerSupportsCancellation = true;
            this.bkk.DoWork -= this.bkk_DoWork;
            this.bkk.RunWorkerCompleted -= this.bkk_RunWorkerCompleted;
            this.Deactivate -= this.Am_err_Deactivate;
            this.Load -= this.Am_err_Load;
            this.Activated -= this.Am_err_Activated;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void Initerr()
        {
            System.Media.SystemSounds.Asterisk.Play();
            this.Text = "ReadyCare Error Reporter";
        }

        public void tx(string stxt)
        {
            tbxmssge.Text = stxt.ToString();
            if (tbxmssge.Text.EndsWith(".") == false)
            {
                tbxmssge.Text = tbxmssge.Text + ".";
            }
            this.Show();
            bkk.RunWorkerAsync();
        }

        public void tx(string stxt, bool secure)
        {
            tbxmssge.Text = stxt;
            if (tbxmssge.Text.EndsWith(".") == false)
            {
                tbxmssge.Text = tbxmssge.Text + ".";
            }
            if (secure == true)
            {
                //stt.Enabled = false;
            }
            this.Show();
        }

        private void clseerr_Click(object sender, EventArgs e)
        {
            clseerrr.Start();
        }

        private void dectmeerr_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmeerr.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void Am_err_Deactivate(object sender, EventArgs e)
        {
            dectmeerr.Start();
        }

        private void Am_err_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeerr.Stop();
            }
            catch (Exception excprevdec)
            {
            }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Am_err_Load(object sender, EventArgs e)
        {

        }

        private void clseerrr_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void enblerrtc(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.lockstat == "Locked")
            {
                this.Enabled = false;
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
            else if (Properties.Settings.Default.lockstat == "none")
            {
                this.Enabled = true;
                this.Visible = true;
                this.ShowInTaskbar = true;
            }
            else
            { }
        }

        private Button btn_tmp;
        private void uku_MouseEnter(object sender, EventArgs e)
        {
            btn_tmp = (Button)sender;
            btn_tmp.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void uku_MouseLeave(object sender, EventArgs e)
        {
            btn_tmp = (Button)sender;
            btn_tmp.BackgroundImage = Properties.Resources.btnsim1;
        }

        private void uku_MouseDown(object sender, MouseEventArgs e)
        {
            btn_tmp = (Button)sender;
            btn_tmp.BackgroundImage = Properties.Resources.btsimp;
        }

        private void uku_MouseUp(object sender, MouseEventArgs e)
        {
            btn_tmp = (Button)sender;
            btn_tmp.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void stt_Click(object sender, EventArgs e)
        {
            First_use_optn sett = new First_use_optn();
            sett.Show();
        }

        private void bkk_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                string mex = tbxmssge.Text.Replace("'", "");
                SqlCeCommand cmd = new SqlCeCommand("INSERT INTO error_Log VALUES('" + mex + "', 'N.A.', 'N.A.', '" + DateTime.Now.ToString() + "')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception erty) { }
        }

        private void bkk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
    }
}
