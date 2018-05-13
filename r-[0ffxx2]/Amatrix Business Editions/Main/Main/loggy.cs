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
        bool ok = false;
        public loggy()
        {
            this.Disposed += new EventHandler(loggy_Disposed);
            InitializeComponent();

            foreach (Form f in Application.OpenForms)
            {
                if (f != this)
                {
                    f.Enabled = false;
                }
            }
            load_instances();
            this.Show();
        }

        void loggy_Disposed(object sender, EventArgs e)
        {
            if (!ok)
            {
                Application.Exit();
            }
            else
            {
                foreach (Form f in Application.OpenForms)
                {
                    try
                    {
                        f.Enabled = true;
                    }
                    catch { }
                }
            }

            this.Disposed -= loggy_Disposed;
            this.button2.Click -= this.button2_Click;
            //this.chk_bx.CheckedChanged -= this.chk_bx_CheckedChanged;
            this.ofd.FileOk -= this.ofd_FileOk;
            this.strt.Tick -= this.strt_Tick;
            this.button4.Click -= this.button4_Click;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.Load -= this.loggy_Load;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void load_instances()
        {
            foreach (DataRow dr in SqlDataSourceEnumerator.Instance.GetDataSources().Rows)
            {
                try
                {
                    tv.Nodes.Add(dr.ItemArray[0].ToString() + "\\" + dr.ItemArray[1].ToString());
                    tv.Nodes[tv.Nodes.Count - 1].ImageIndex = 1;
                }
                catch { }
            }
            try
            {
                tv.SelectedNode = tv.Nodes[0];
            }
            catch { }
            return;
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
            try
            {
                DataTable dtp = new DataTable();
                dtp = Amatrix.basql.Execute("Data Source=" + tv.SelectedNode.Text + "; uid=" + textBox1.Text + "; pwd=" + textBox2.Text, "SELECT name FROM master..sysdatabases", "", dtp);
                ok = true;

                Amatrix.mgt = "Data Source=" + tv.SelectedNode.Text + "; initial catalog=" + cbx.SelectedItem.ToString() + "; uid=" + textBox1.Text + "; pwd=" + textBox2.Text;
                Amatrix.acc = "Data Source=" + tv.SelectedNode.Text + "; initial catalog=" + cbx.SelectedItem.ToString() + "; uid=" + textBox1.Text + "; pwd=" + textBox2.Text;
                Amatrix.doc = "Data Source=" + tv.SelectedNode.Text + "; initial catalog=" + cbx.SelectedItem.ToString() + "; uid=" + textBox1.Text + "; pwd=" + textBox2.Text;
                Amatrix.AMs = "Data Source=" + tv.SelectedNode.Text + "; initial catalog=" + cbx.SelectedItem.ToString() + "; uid=" + textBox1.Text + "; pwd=" + textBox2.Text;

                this.Close();
            }
            catch { }
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

        private void cbx_DropDown(object sender, EventArgs e)
        {
            cbx.Items.Clear();
            DataTable dtp = new DataTable();
            try
            {
                dtp = Amatrix.basql.Execute("Data Source=" + tv.SelectedNode.Text + "; uid=" + textBox1.Text + "; pwd=" + textBox2.Text, "SELECT name FROM master..sysdatabases", "", dtp);
                foreach (DataRow dr in dtp.Rows)
                {
                    cbx.Items.Add(dr[0].ToString());
                }
            }
            catch { }
                dtp.Clear();
                dtp.Dispose();
            
            return;
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            element_status();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            element_status();
        }

        //Check Status
        private void element_status()
        {
            if (tv.SelectedNode != null)
            {
                textBox1.Enabled = true;
            }
            else { textBox1.Enabled = false; }

            if (textBox1.Text != "" && textBox1.Enabled == true)
            {
                textBox2.Enabled = true;
            }
            else { textBox2.Enabled = false; }

            if (textBox2.Text != "" && textBox2.Enabled == true)
            {
                cbx.Enabled = true;
            }
            else { cbx.Enabled = false; }

            if (cbx.Enabled == true && cbx.SelectedIndex != null)
            {
                button4.Enabled = true;
            }
            else { button4.Enabled = false; }
            return;
        }
    }
}
