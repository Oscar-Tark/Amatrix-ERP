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
using System.Data.SqlServerCe;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Main
{
    partial class Amatrix_About : Form
    {
        public Amatrix_About()
        {
            this.Disposed += new EventHandler(Amatrix_About_Disposed);
            InitializeComponent();
            this.Text = String.Format("About {0} {0}", AssemblyTitle);

            if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }
            bkk_run.RunWorkerAsync();
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

        void Amatrix_About_Disposed(object sender, EventArgs e)
        {
            dt.Clear();
            dgv.DataSource = null;

            this.ukuboot.MouseLeave -= this.abcncloff;
            this.ukuboot.Click -= this.ukuboot_Click_1;
            this.ukuboot.MouseDown -= this.abcndwn;
            this.ukuboot.MouseUp -= this.abcnup;
            this.ukuboot.MouseEnter -= this.abcnclon;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.button1.MouseLeave -= this.abcncloff;
            this.button1.Click -= this.button1_Click;
            this.button1.MouseDown -= this.abcndwn;
            this.button1.MouseUp -= this.abcnup;
            this.button1.MouseEnter -= this.abcnclon;
            this.tme_sze.Tick -= this.tme_sze_Tick;
            this.Deactivate -= this.Amatrix_About_Deactivate;
            this.Load -= this.Amatrix_About_Load;
            this.Activated -= this.Amatrixabtact;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        DataTable dt = new DataTable();
        private void get_auth()
        {
            dt.Clear();
            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
            SqlCeCommand cmd = new SqlCeCommand("SELECT [Author Name], [Author CopyRight], [Author License] FROM Authors_of_Amatrix", conn);
            conn.Open();
            SqlCeDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            conn.Dispose();
            cmd.Dispose();
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void Amatrix_About_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.96;
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.Text = "Amatrix : About";
        }

        private void ukuboot_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ukuboot_Click_1(object sender, EventArgs e)
        {
            abtclse.Start();
        }

        Button b;
        private void abcnclon(object sender, EventArgs e)
        {
            b = (Button)sender;
            b.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void abcncloff(object sender, EventArgs e)
        {
            b = (Button)sender;
            b.BackgroundImage = Properties.Resources.btnsim1;
        }
        private void abcndwn(object sender, MouseEventArgs e)
        {
            b = (Button)sender;
            b.BackgroundImage = Properties.Resources.btsimp;
        }

        private void abcnup(object sender, MouseEventArgs e)
        {
            b = (Button)sender;
            b.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void Amatrixabtact(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception excabtdec)
            {
            }
            this.Opacity = 0.96;
        }

        private void Amatrix_About_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
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

        private void abtclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        int oooot = 0;
        private void tme_sze_Tick(object sender, EventArgs e)
        {
            if (oooot == 0)
            {
                if (this.Size.Height >= 573)
                {
                    tme_sze.Stop();
                    oooot = 1;
                }
                else
                { this.Size = new Size(this.Size.Width, this.Size.Height + 15); }
            }
            else if (oooot == 1)
            {
                if (this.Size.Height <= 388)
                {
                    tme_sze.Stop();
                    oooot = 0;
                }
                else { this.Size = new Size(this.Size.Width, this.Size.Height - 15); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tme_sze.Start();
        }
        //new
        private void bkk_run_DoWork(object sender, DoWorkEventArgs e)
        {
            get_auth();
        }

        private void bkk_run_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgv.DataSource = dt;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            App_Workbook wbk = new App_Workbook();
            wbk.openext(Application.StartupPath + "\\gpl-3.0.txt");
        }
    }
}
