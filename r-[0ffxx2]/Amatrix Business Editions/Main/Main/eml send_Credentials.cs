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
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class eml_send_Credentials : Form
    {
        public eml_send_Credentials()
        {
            InitializeComponent();
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

        MailMessage mssge = new MailMessage();
        String from, to;
        public void tx(MailMessage Message, string To, string From)
        {
            mssge = Message;
            from = From;
            to = To;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bttn.Text = "Sending...";
            bkk_SEND.RunWorkerAsync();
        }

        private void bkk_SEND_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (to.ToLower().Contains("hotmail.") == true || to.ToLower().Contains("live.") == true)
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(from, txt.Text);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mssge);
                }
                else if (to.ToLower().Contains("yahoo.") == true)
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.mail.yahoo.com");
                    SmtpServer.Port = 995;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(from, txt.Text);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mssge);
                }
                else if (to.ToLower().Contains("gmail.") == true)
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(from, txt.Text);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mssge);
                }
                else if (to.ToLower().Contains("msn.") == true)
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.email.msn.com");
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(from, txt.Text);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mssge);
                }
                else if (to.ToLower().Contains("netscape.") == true)
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.isp.netscape.com");
                    SmtpServer.Port = 25;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(from, txt.Text);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mssge);
                }
                else { bkk_SEND.CancelAsync();/*Am_err ner = new Am_err(); ner.tx("Unfortunately the Address You are Sending Your Email To is Maintained by a Email Service Provider that is Not Supported by Amatrix.");*/ }
            }
            catch (Exception erty) { bkk_SEND.CancelAsync(); }
        }

        private void bkk_SEND_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Am_err ner = new Am_err();
                ner.tx("The Message Could not Be Sent, Check your Connection Settings.");
            }
            else if (e.Cancelled == false)
            {
                MessageBox.Show("The Message Was Sent.", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void eml_send_Credentials_Load(object sender, EventArgs e)
        {

        }
    }
}
