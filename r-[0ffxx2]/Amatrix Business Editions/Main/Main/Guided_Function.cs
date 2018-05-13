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
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Main
{
    public partial class Guided_Function : Form
    {
        private string Path;
        private string Name;

        public Guided_Function()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Icon = Properties.Resources.installericon;
            this.Disposed += new EventHandler(Guided_Function_Disposed);
            InitializeComponent();
            this.Text = "Amatrix Guide : New Link";
            init();
            /*try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
        }

        void Guided_Function_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= Guided_Function_Disposed;
            this.Cancel.Click -= this.Cancel_Click;
            this.add_btn.Click -= this.add_btn_Click;
            this.am.Click -= this.fin_Click;
            this.fin.Click -= this.fin_Click;
            this.bt_nxt.Click -= this.bt_nxt_Click;
            this.cbh.CheckedChanged -= this.cbh_CheckedChanged;
            this.OFD.FileOk -= this.OFD_FileOk;
            this.bck.Click -= this.bt_nxt_Click;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.Deactivate -= this.Guided_Function_Deactivate;
            this.Load -= this.Guided_Function_Load;
            this.Activated -= this.Guided_Function_Activated;

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

        private void init()
        {
            if (Properties.Settings.Default.Show_Guides == true)
            {
                cbh.CheckState = CheckState.Checked;
            }
            else
            {
                cbh.CheckState = CheckState.Unchecked;
            }
        }

        private void Guided_Function_Load(object sender, EventArgs e)
        {

        }

        public void get_fle_strt()
        {
            this.Show();
        }

        private void cbh_CheckedChanged(object sender, EventArgs e)
        {
            if (cbh.CheckState == CheckState.Checked)
            {
                Properties.Settings.Default.Show_Guides = true;
            }
            else if (cbh.CheckState == CheckState.Unchecked)
            {
                Properties.Settings.Default.Show_Guides = false;
            }
            Properties.Settings.Default.Save();
        }

        private void bt_nxt_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(bt_nxt) == true)
                {
                    tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
                }
                else
                {
                    tabControl1.SelectTab(tabControl1.SelectedIndex - 1);
                }
            }
            catch (Exception erty) { }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            OFD.ShowDialog();
        }

        private void OFD_FileOk(object sender, CancelEventArgs e)
        {
            tbx1.Text = OFD.FileName;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            abtclse.Start();
        }

        private void fin_Click(object sender, EventArgs e)
        {
            string link, text;
            if (File.Exists(tbx1.Text))
            {
                //address
                if (checkBox1.Checked == false)
                {
                    if (Properties.Settings.Default.app2 == 0)
                    {
                        Properties.Settings.Default.app1 = Properties.Settings.Default.app1 + "|" + tbx1.Text + "|";
                    }
                    else { Properties.Settings.Default.app1 = Properties.Settings.Default.app1 + tbx1.Text + "|"; }
                    text = tbx1.Text;
                }
                else
                {
                    if (Properties.Settings.Default.app2 == 0)
                    {
                        Properties.Settings.Default.app1 = Properties.Settings.Default.app1 + "|" + OFD.FileName + "|";
                    }
                    else { Properties.Settings.Default.app1 = Properties.Settings.Default.app1 + OFD.FileName + "|"; }
                    text = OFD.FileName;
                }


                if (checkBox1.CheckState != CheckState.Checked)
                {
                    if (Properties.Settings.Default.app2ttp == 0)
                    {
                        Properties.Settings.Default.app1ttp = Properties.Settings.Default.app1ttp + "|" + textBox1.Text + "|";
                    }
                    else { Properties.Settings.Default.app1ttp = Properties.Settings.Default.app1ttp + textBox1.Text + "|"; } 
                    link = textBox1.Text;
                }
                else
                {
                    if (Properties.Settings.Default.app2ttp == 0)
                    {
                        Properties.Settings.Default.app1ttp = Properties.Settings.Default.app1ttp + '|' + OFD.SafeFileName + '|';
                    }
                    else { Properties.Settings.Default.app1ttp = Properties.Settings.Default.app1ttp + OFD.SafeFileName + '|'; }
                    link = OFD.SafeFileName;
                }

                Properties.Settings.Default.app2 = Properties.Settings.Default.app2 + 1;
                Properties.Settings.Default.app2ttp = Properties.Settings.Default.app2ttp + 1;

                Properties.Settings.Default.Save();
                Amatrix.al_lnk.Add(text);
                Amatrix.al_lnk.Add(link);

                if (sender.Equals(am) == true)
                { tabControl1.SelectTab(1); tbx1.Text = "Link Address"; textBox1.Text = "Link Name"; }
                else { this.Close(); }
                //this.Close();
            }
            else
            {
                tabControl1.SelectTab(1);
                Am_err mer = new Am_err();
                mer.tx("The Path Specified for your link is Wrong. Please Verify your Link's Path.");
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

        private void abtclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void Guided_Function_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Guided_Function_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }
    }
}
