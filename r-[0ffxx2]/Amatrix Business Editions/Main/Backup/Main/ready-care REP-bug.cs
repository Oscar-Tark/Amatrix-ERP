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
using System.Net.Mail;
using System.Windows.Forms;

namespace Main
{
    public partial class ready_care_REP_bug : Form
    {
        public ready_care_REP_bug()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.TopMost = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                sze.Start();
            }
            else
            {
                sze.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");

                mail.From = new MailAddress("thetarkman@hotmail.it");
                mail.To.Add("thetarkman@hotmail.it");
                mail.Subject = "Bug Report";
                if (radioButton1.Checked == true)
                {
                    mail.Body = radioButton1.Text;
                }
                if (radioButton2.Checked == true)
                {
                    mail.Body = radioButton2.Text;
                }
                if (radioButton3.Checked == true)
                {
                    mail.Body = radioButton3.Text;
                }
                if (radioButton4.Checked == true)
                {
                    mail.Body = richTextBox1.Text;
                }

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("thetarkman@hotmail.it", "#include<oscar>");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured Please check Your Internet Connection"); this.Close(); }
        }

        bool open = false;
        private void sze_Tick(object sender, EventArgs e)
        {
            if (open == false)
            {
                if (this.Size.Height <= 443)
                {
                    this.Size = new Size(484, this.Size.Height + 10);
                }
                else
                {
                    open = true;
                    sze.Stop();
                }
            }
            else
            {
                if (this.Size.Height >= 322)
                {
                    this.Size = new Size(484, this.Size.Height - 10);
                }
                else
                {
                    open = false;
                    sze.Stop();
                }
            }
        }
    }
}
