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
    public partial class Calendar_alrm : Form
    {
        public Calendar_alrm()
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Disposed += new EventHandler(Calendar_alrm_Disposed);
            InitializeComponent();
            this.Opacity = Properties.Settings.Default.opacity;
            this.Text = "Calendar : Alarm";
            Init();
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

        void Calendar_alrm_Disposed(object sender, EventArgs e)
        {
            this.clse.MouseLeave -= this.clse_MouseLeave;
            this.clse.MouseEnter -= this.clse_MouseEnter;
            this.clse.Click -= this.clse_Click;
            this.prevals.Click -= this.prevals_Click;
            this.alrmclse.Tick -= this.alrmclse_Tick;
            this.stlrm.Click -= this.stlrm_Click;
            this.cncel.Click -= this.cncel_Click;
            this.reas.TextChanged -= this.reas_TextChanged;
            this.prevtme.Tick -= this.prevtme_Tick;
            this.dectmealrm.Tick -= this.dectmealrm_Tick;
            this.calalenbl.Tick -= this.calalenbltc;
            this.Deactivate -= this.Calendar_alrm_Deactivate;
            this.Load -= this.Calendar_alrm_Load;
            this.Activated -= this.Calendar_alrm_Activated;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void Init()
        {
            if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }
        }

        private void Calendar_alrm_Load(object sender, EventArgs e)
        {

        }

        private void clse_Click(object sender, EventArgs e)
        {
            alrmclse.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void alrmclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void cncel_Click(object sender, EventArgs e)
        {
            alrmclse.Start();
        }

        private void stlrm_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.alarmdate = dtp.Value.Day.ToString() + "-" +dtp.Value.Month.ToString() +"-"+ dtp.Value.Year.ToString();

            Properties.Settings.Default.alarmtime = tms.Value.ToShortTimeString();
            Properties.Settings.Default.alarmreason = reas.Text;
            Properties.Settings.Default.Save();
            alrmclse.Start();
        }

        private void reas_TextChanged(object sender, EventArgs e)
        {

        }

        private void prevtme_Tick(object sender, EventArgs e)
        {
            prevals.Text = "Alarm : " + Properties.Settings.Default.alarmreason + " on " + Properties.Settings.Default.alarmdate + " at "+ Properties.Settings.Default.alarmtime;
        }

        private void clse_MouseEnter(object sender, EventArgs e)
        {
            clse.Text = "Close";
            clse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clse_MouseLeave(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.Image;
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

        private void Calendar_alrm_Deactivate(object sender, EventArgs e)
        {
            dectmealrm.Start();
        }

        private void Calendar_alrm_Activated(object sender, EventArgs e)
        {
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void calalenbltc(object sender, EventArgs e)
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

        private void prevals_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.alarmdate = "";
            Properties.Settings.Default.alarmminute = "";
            Properties.Settings.Default.alarmreason = "";
            Properties.Settings.Default.Save();
            Main.Amatrix.alarmst = 1;
        }
    }
}
