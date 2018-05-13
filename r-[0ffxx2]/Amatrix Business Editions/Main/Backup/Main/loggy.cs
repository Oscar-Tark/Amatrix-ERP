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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class loggy : Form
    {
        public loggy()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Disposed += new EventHandler(loggy_Disposed);
            InitializeComponent();
            this.BringToFront();
            /*try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
        }

        void loggy_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= loggy_Disposed;
            this.lgu.Click -= this.lgu_Click;
            this.button2.Click -= this.button2_Click;
            //this.chk_bx.CheckedChanged -= this.chk_bx_CheckedChanged;
            this.ofd.FileOk -= this.ofd_FileOk;
            this.strt.Tick -= this.strt_Tick;
            this.button4.Click -= this.button4_Click;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.Deactivate -= this.loggy_Deactivate;
            this.Load -= this.loggy_Load;
            this.Activated -= this.loggy_Activated;

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

        private void loggy_Load(object sender, EventArgs e)
        {

        }

        private void lgu_Click(object sender, EventArgs e)
        {
            Main.Amatrix.doc = "";
            Main.Amatrix.mgt = "";
            Main.Amatrix.acc = "";
            Properties.Settings.Default.Save();
            abtclse.Start();
        }

        private void cbx_ch(object sender, EventArgs e)
        {
        }

        private void brw_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void strt_Tick(object sender, EventArgs e)
        {
            strt.Stop();
        }

        /*private void chk_bx_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_bx.Checked == true)
            {
                app_connection_settings.Default.local = true;
            }
            else if (chk_bx.Checked == false)
            {
                app_connection_settings.Default.local = false;
            }
            app_connection_settings.Default.Save();
        }*/

        private void button2_Click(object sender, EventArgs e)
        {
            abtclse.Start();
        }

        private void load_cbx(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loggy_adv dv = new loggy_adv();
            dv.Show();
            this.Close();
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

        private void loggy_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void loggy_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }

        private void chk_bx_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
