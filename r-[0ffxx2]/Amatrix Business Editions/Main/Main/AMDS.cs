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
using System.Data.Sql;
using System.Windows.Forms;
using System.IO;

namespace Main
{
    public partial class AMDS : Form
    {
        public string srev;
        public AMDS()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.Text = "Amatrix Database System";
            Init();
        }

        private void Init()
        {
            csamds.AllowTransparency = true;
            csamds.Opacity = 0.90;
            this.Opacity = Properties.Settings.Default.opacity;
            if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }
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

        private void AMDS_Load(object sender, EventArgs e)
        {

        }
        //Open Server
        private void opnamds_Click(object sender, EventArgs e)
        {
            opndbm.ShowDialog();
        }

        private void clseamds_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clsecamdstc(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void clseamdclc(object sender, EventArgs e)
        {
            clsecamds.Start();
        }

        private void decamdstc(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                decamds.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void AMDSdec(object sender, EventArgs e)
        {
            decamds.Start();
        }

        private void AMDSact(object sender, EventArgs e)
        {
            try
            {
                decamds.Stop();
            }
            catch (Exception excop)
            {
                this.Opacity = Properties.Settings.Default.opacity;
            }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void szekeep_Tick(object sender, EventArgs e)
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

        private void svasdbm_Click(object sender, EventArgs e)
        {
            sfdms.Title = "Select a Database File to Open";
            sfdms.ShowDialog();
        }

        private void sfdmsfok(object sender, CancelEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            opndbm.Title = "Open Database";
            opndbm.ShowDialog();
        }

        private void tosettdbm_Click(object sender, EventArgs e)
        {
            App_Shoppe apso = new App_Shoppe();
            apso.Show();
            this.Close();
        }

        private void toexret_Click(object sender, EventArgs e)
        {
        }

        private void svw_Click(object sender, EventArgs e)
        {
        }




        //Set strings & Open__________________________________________________________________________________________________
        private void winname()
        {
            if (srev == null || srev == "none")
            {
                this.Text = "Amatrix Database System";
                Properties.Settings.Default.Servernme = null;
                Properties.Settings.Default.Save();
            }
            else
            {
                string nmwe = "";
                this.Text = "Amatrix Database System : ";
                int refr = 0;
                foreach (char ini in srev)
                {
                    if (srev[refr] + srev[refr + 1] + srev[refr + 2] + srev[refr + 3] + srev[refr + 4] == '.' + 'a' + 'm' + 'd' + 's')
                    {
                        break;
                    }
                    else
                    {
                        nmwe = nmwe + srev[refr];
                        this.Text = this.Text + srev[refr];
                    }
                    refr++;
                }
                Properties.Settings.Default.Servernme = nmwe;
                Properties.Settings.Default.Save();
               //  + srev.ToString();
            }
        }

        private void opndbmfok(object sender, CancelEventArgs e)
        {
            if (opndbm.FileName.EndsWith(".amds"))
            {
                srev = opndbm.SafeFileName.ToString();
                Properties.Settings.Default.Serverloc = opndbm.FileName.ToString();
                Properties.Settings.Default.Save();
                winname();
            }
            else { }
        }

        private void AMDSdc(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reparttn rep = new reparttn();
            rep.Show();
            this.Close();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AMDS am = new AMDS();
            am.Show();
            this.Close();
        }

        //Beauty End___________________________________________________________________________________________________________________
    }
}
