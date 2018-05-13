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
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Data.SqlServerCe;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Main
{
    public partial class Scrsve : Form
    {
        public Scrsve()
        {
            this.TopMost = true;
            this.FormClosing += new FormClosingEventHandler(Scrsve_FormClosing);
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ControlBox = false;
        }

        private void assco()
        {
            if (!FileAssociation.IsAssociated(".afd"))
            {
                try
                {
                    FileAssociation.Associate(".afd", "Main2", "afd File", Environment.CurrentDirectory + "\\beta tech.ico", Application.StartupPath + "\\Main2.exe");
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
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

        private bool iscl = false;
        void Scrsve_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iscl == false)
            {
                e.Cancel = true;
            }
            else { }
        }

        private void Scrsve_Load(object sender, EventArgs e)
        {

        }

        ArrayList al_P_ = new ArrayList();
        public void Manage(System.Diagnostics.Process Process_)
        {
            al_P_.Add(Process_);
        }

        private void captscrn_Tick(object sender, EventArgs e)
        {
            foreach (System.Diagnostics.Process p in al_P_)
            {
                try
                {
                    if (p.Responding == false)
                    {
                        Scrsve_termin tm = new Scrsve_termin();
                        tm.tx(p, this);
                        tm.Show();
                        tm.FormClosed += new FormClosedEventHandler(tm_FormClosed);
                        captscrn.Stop();
                    }
                }
                catch (Exception erty) { al_P_.Remove(p); }
            }
        }

        public void remove(System.Diagnostics.Process P_)
        {
            al_P_.Remove(P_);
        }

        void tm_FormClosed(object sender, FormClosedEventArgs e)
        {
            captscrn.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            File.Copy("e:\\O.A.C.png", "e:\\O.A.C2.png", true);
        }

        private void invis_Tick(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private bool fail_ = false;
        private void bkk_rc_DoWork(object sender, DoWorkEventArgs e)
        {
            /*try
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
                try
                {
                    conn.Open();
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Certificates", conn);
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    DataTable dtp = new DataTable();
                    dtp.Load(dr); int col = 0;
                    foreach (DataColumn dc in dtp.Columns)
                    {
                        if (dtp.Columns[col].Table.Rows[0].ItemArray[col].ToString() != "")
                        {
                        }
                        else { fail_ = true; }
                        col++;
                    }
                    conn.Close();
                }
                catch (Exception erty) { fail_ = true; }

                try
                {
                    conn.Open();
                    SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM WorkPath", conn);
                    SqlCeDataReader dr2 = cmd2.ExecuteReader();
                    DataTable dtp2 = new DataTable();
                    dtp2.Load(dr2);
                    if (dtp2.Rows[0].ItemArray[0].ToString() == Environment.CurrentDirectory)
                    {
                    }
                    else { fail_ = true; }
                    conn.Close();
                }
                catch (Exception erty) { fail_ = true; }
            }
            catch (Exception erty)
            {
            }*/
        }

        private void rdy_cre_Tick(object sender, EventArgs e)
        {
            //bkk_rc.RunWorkerAsync();
            //send mail
            /*if (Properties.Settings.Default.mail_sent == false)
            {
                try
                {
                    bkkmail.RunWorkerAsync();
                }
                catch (Exception erty) { }
            }*/
            portsniff.RunWorkerAsync();
        }

        private void bkk_rc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (fail_ == true)
            {
                Application.Exit();
                Application.ExitThread();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Scrsve sv = new Scrsve();
            sv.Show(); iscl = true;
            this.Close();
        }

        private void net_sex__Tick(object sender, EventArgs e)
        {
            /*try
            {
                bkk_net.RunWorkerAsync();
            }
            catch (Exception erty) { }*/
        }

        private void bkk_net_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bkk_net_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void tms_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.usable_month >= 10)
                {
                    foreach (Form ff in Application.OpenForms)
                    {
                        if (ff.Name != "Am_err" && ff.Name != "Amatrix")
                        {
                            ff.Close();
                        }
                        if (ff.Name == "Amatrix")
                        {
                            ff.Enabled = false;
                            ff.Visible = false;
                        }
                    }
                    Am_err ner = new Am_err();
                    ner.tx("Amatrix Has Terminated Its Demo Services.");
                    tms.Stop();
                    ner.Disposed += new EventHandler(ner_Disposed);
                }
            }
            catch (Exception erty) { }
        }

        void ner_Disposed(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.lnk != "")
            {
                Advort adv = new Advort();
                adv.open();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            /*WebBrowser wn = new WebBrowser();
            wn.Navigate("http://astreous.webs.com/adweb.html");
            if (wn.DocumentText == "yes")
            {
                Properties.Settings.Default.lnk = "http://astreous.webs.com/adverts.htm";
                Properties.Settings.Default.Save();
            }
            else if (wn.DocumentText == "no")
            {
                Properties.Settings.Default.lnk = "";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.lnk = wn.DocumentText;
                Properties.Settings.Default.Save();
            }*/
        }

        private void bkkmail_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");

                mail.From = new MailAddress("thetarkman@hotmail.it");
                mail.To.Add("astreous@hotmail.com");
                mail.Subject = "New Amatrix License Agreement";
                mail.Body = "+1";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("thetarkman@hotmail.it", "#include<oscar>");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                Properties.Settings.Default.mail_sent = true;
            }
            catch (Exception ex)
            {
                Properties.Settings.Default.mail_sent = false;
            }
            Properties.Settings.Default.Save();
        }

        private void portsniff_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }

    public class FileAssociation
    {
        // Associate file extension with progID, description, icon and application
        public static void Associate(string extension, string progID, string description, string icon, string application)
        {
            Registry.CurrentUser.CreateSubKey(extension).SetValue("", progID);
            if (progID != null && progID.Length > 0)
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(progID))
                {
                    if (description != null)
                        key.SetValue("", description);
                    if (icon != null)
                        key.CreateSubKey("DefaultIcon").SetValue("", ToShortPathName(icon));
                    if (application != null)
                        key.CreateSubKey(@"Shell\Open\Command").SetValue("",
                                    ToShortPathName(application) + " \"%1\"");
                }
        }

        // Return true if extension already associated in registry
        public static bool IsAssociated(string extension)
        {
            return (Registry.CurrentUser.OpenSubKey(extension, false) != null);
        }

        [DllImport("Kernel32.dll")]
        private static extern uint GetShortPathName(string lpszLongPath,
            [Out] StringBuilder lpszShortPath, uint cchBuffer);

        // Return short path format of a file name
        private static string ToShortPathName(string longName)
        {
            StringBuilder s = new StringBuilder(1000);
            uint iSize = (uint)s.Capacity;
            uint iRet = GetShortPathName(longName, s, iSize);
            return s.ToString();
        }
    }
}
