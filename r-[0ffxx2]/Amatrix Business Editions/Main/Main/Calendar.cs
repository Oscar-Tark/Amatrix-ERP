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
    public partial class Calendar : Form
    {
        public Calendar()
        {
            this.MinimizeBox = false;
            this.Icon = Properties.Resources.amdsicnico;
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.Disposed += new EventHandler(Calendar_Disposed);
            InitializeComponent();
            this.Opacity = Properties.Settings.Default.opacity;
            Init();
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

        void Calendar_Disposed(object sender, EventArgs e)
        {
            tms.Stop();
            tms.Tick -= tms_Tick;
            this.tscal.MouseEnter -= this.tscal_MouseEnter;
            this.tscal.MouseLeave -= this.tscal_MouseLeave;
            this.clsecal.MouseLeave -= this.clsecalext;
            this.clsecal.MouseEnter -= this.clsecalhov;
            this.clsecal.Click -= this.clsecalclck;
            this.sndfday.Click -= this.sndfday_Click;
            this.mnfdy.Click -= this.mnfdy_Click;
            this.tsfdy.Click -= this.tsfdy_Click;
            this.wdfstdy.Click -= this.wdfstdy_Click;
            this.thfsdy.Click -= this.thfsdy_Click;
            this.fdyfsdy.Click -= this.fdyfsdy_Click;
            this.styfsdy.Click -= this.styfsdy_Click;
            this.fudter.Click -= this.fullDateToolStripMenuItem_Click;
            this.srdte.Click -= this.srdte_Click;
            this.ondte.Click -= this.ondte_Click;
            this.ontme.Click -= this.ontme_Click;
            this.uct.Click -= this.uct_Click;
            this.tmesetodef.Click -= this.tmesetodef_Click;
            this.salar.Click -= this.salar_Click;
            this.tmeclsecal.Tick -= this.tmeclsecal_Tick;
            this.presentationtmer.Tick -= this.presentationtmer_Tick;
            this.dectmecal.Tick -= this.dectmecal_Tick;
            this.calenenbl.Tick -= this.calenenbltc;
            this.Deactivate -= this.calenddec;
            this.Load -= this.Calendar_Load;
            this.Activated -= this.calendact;

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
            switch (Properties.Settings.Default.date)
            {
                case "Sunday":
                    mc.FirstDayOfWeek = Day.Sunday;
                    break;
                case "Monday":
                    mc.FirstDayOfWeek = Day.Monday;
                    break;
                case "Tuesday":
                    mc.FirstDayOfWeek = Day.Tuesday;
                    break;
                case "Wednesday":
                    mc.FirstDayOfWeek = Day.Wednesday;
                    break;
                case "Thursday":
                    mc.FirstDayOfWeek = Day.Thursday;
                    break;
                case "Friday":
                    mc.FirstDayOfWeek = Day.Friday;
                    break;
                case "Saturday":
                    mc.FirstDayOfWeek = Day.Saturday;
                    break;
                default:
                    mc.FirstDayOfWeek = Day.Default;
                    break;
            }
        }

        void Calendar_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Helper onshelp = new Helper();
            onshelp.tx(this.Name);
        }

        private void clsecalclck(object sender, EventArgs e)
        {
            tmeclsecal.Start();
        }

        private void clsecalhov(object sender, EventArgs e)
        {
            clsecal.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            clsecal.Text = "Close";
        }

        private void clsecalext(object sender, EventArgs e)
        {
            clsecal.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void calendact(object sender, EventArgs e)
        {
            try
            {
                dectmecal.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void calenddec(object sender, EventArgs e)
        {
            dectmecal.Start();
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void sndfday_Click(object sender, EventArgs e)
        {
            mc.FirstDayOfWeek = Day.Sunday;
            Properties.Settings.Default.date = "Sunday";
            Properties.Settings.Default.Save();
        }

        private void mnfdy_Click(object sender, EventArgs e)
        {
            mc.FirstDayOfWeek = Day.Monday;
            Properties.Settings.Default.date = "Monday";
            Properties.Settings.Default.Save();
        }

        private void tsfdy_Click(object sender, EventArgs e)
        {
            mc.FirstDayOfWeek = Day.Tuesday;
            Properties.Settings.Default.date = "Tuesday";
            Properties.Settings.Default.Save();
        }

        private void wdfstdy_Click(object sender, EventArgs e)
        {
            mc.FirstDayOfWeek = Day.Wednesday;
            Properties.Settings.Default.date = "Wednesday";
            Properties.Settings.Default.Save();
        }

        private void thfsdy_Click(object sender, EventArgs e)
        {
            mc.FirstDayOfWeek = Day.Thursday;
            Properties.Settings.Default.date = "Thursday";
            Properties.Settings.Default.Save();
        }

        private void fdyfsdy_Click(object sender, EventArgs e)
        {
            mc.FirstDayOfWeek = Day.Friday;
            Properties.Settings.Default.date = "Friday";
            Properties.Settings.Default.Save();
        }

        private void styfsdy_Click(object sender, EventArgs e)
        {
            mc.FirstDayOfWeek = Day.Saturday;
            Properties.Settings.Default.date = "Saturday";
            Properties.Settings.Default.Save();
        }

        private void salar_Click(object sender, EventArgs e)
        {
            Calendar_alrm calalrm = new Calendar_alrm();
            calalrm.Show();
        }

        private void tmeclsecal_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        bool daysaving;
        private void presentationtmer_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 16)
            {
                label3.Text = "In the Afternoon";
            }
            else if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 19)
            {
                label3.Text = "In the Evening";
            }
            else if (DateTime.Now.Hour >= 19 && DateTime.Now.Hour <= 23)
            {
                label3.Text = "In the Night";
            }
            else
            {
                label3.Text = "In the Morning";
            }
            label10.Text = DateTime.Now.Day.ToString();
            label11.Text = "Of " + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            daysaving = DateTime.Now.IsDaylightSavingTime();
            if (daysaving == true)
            {
                label8.Text = "Daylight Saving";
            }
            else { label8.Text = "Not Daylight Saving"; }
            label5.Text = DateTime.UtcNow.Hour.ToString() + ":" + DateTime.UtcNow.Minute.ToString();
            tme.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
        }

        private void dectmecal_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmecal.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void calenenbltc(object sender, EventArgs e)
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

        private void uct_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.hrfrmt = 2;
            Properties.Settings.Default.Save();
        }

        private void tmesetodef_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.hrfrmt = 0;
            Properties.Settings.Default.Save();
        }

        private void fullDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.hrfrmt = 3;
            Properties.Settings.Default.Save();
        }

        private void srdte_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.hrfrmt = 4;
            Properties.Settings.Default.Save();
        }

        private void ondte_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.hrfrmt = 5;
            Properties.Settings.Default.Save();
        }

        private void ontme_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.hrfrmt = 6;
            Properties.Settings.Default.Save();
        }

        private void tscal_MouseEnter(object sender, EventArgs e)
        {
            tscal.BackColor = Color.WhiteSmoke;
        }

        private void tscal_MouseLeave(object sender, EventArgs e)
        {
            tscal.BackColor = Color.White;
        }

        private void tms_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour == 1 || DateTime.Now.Hour == 13)
            {
                pictureBox13.BackgroundImage = Properties.Resources._1_1;
            }
            else if (DateTime.Now.Hour == 2 || DateTime.Now.Hour == 14)
            {
                pictureBox13.BackgroundImage = Properties.Resources._2;
            }
            else if (DateTime.Now.Hour == 3 || DateTime.Now.Hour == 15)
            {
                pictureBox13.BackgroundImage = Properties.Resources._3;
            }
            else if (DateTime.Now.Hour == 4 || DateTime.Now.Hour == 16)
            {
                pictureBox13.BackgroundImage = Properties.Resources._4;
            }
            else if (DateTime.Now.Hour == 5 || DateTime.Now.Hour == 17)
            {
                pictureBox13.BackgroundImage = Properties.Resources._5;
            }
            else if (DateTime.Now.Hour == 6 || DateTime.Now.Hour == 18)
            {
                pictureBox13.BackgroundImage = Properties.Resources._6;
            }
            else if (DateTime.Now.Hour == 7 || DateTime.Now.Hour == 19)
            {
                pictureBox13.BackgroundImage = Properties.Resources._7;
            }
            else if (DateTime.Now.Hour == 8 || DateTime.Now.Hour == 20)
            {
                pictureBox13.BackgroundImage = Properties.Resources._8;
            }
            else if (DateTime.Now.Hour == 9 || DateTime.Now.Hour == 21)
            {
                pictureBox13.BackgroundImage = Properties.Resources._9;
            }
            else if (DateTime.Now.Hour == 10 || DateTime.Now.Hour == 22)
            {
                pictureBox13.BackgroundImage = Properties.Resources._10;
            }
            else if (DateTime.Now.Hour == 11 || DateTime.Now.Hour == 23)
            {
                pictureBox13.BackgroundImage = Properties.Resources._11;
            }
            else if (DateTime.Now.Hour == 12 || DateTime.Now.Hour == 90 || DateTime.Now.Hour == 0)
            { pictureBox13.BackgroundImage = Properties.Resources._12; }
            label13.Text = DateTime.Now.Minute.ToString(); label12.Text = DateTime.Now.Hour.ToString();
        }
    }
}
