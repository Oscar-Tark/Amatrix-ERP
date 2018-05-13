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
using System.Net.Mail;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Advert_Submit : Form
    {
        public Advert_Submit()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Disposed += new EventHandler(Advert_Submit_Disposed);
            InitializeComponent();
        }

        void Advert_Submit_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= Advert_Submit_Disposed;
            this.button1.Click -= this.button1_Click;
            this.button2.Click -= this.button2_Click;
            this.ofd.FileOk -= this.ofd_FileOk;
            this.Load -= this.Advert_Submit_Load;
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

        private void Advert_Submit_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                pictureBox1.BackgroundImage = Image.FromFile(ofd.FileName);
            }
            catch (Exception ertyt) { Am_err ner = new Am_err(); ner.tx("Invalid Image."); }
        }

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            Application.DoEvents();
            try
            {
                Attachment atc = new Attachment(ofd.FileName);
                Image i = Image.FromFile(ofd.FileName);
                mail.Attachments.Add(atc);
                mail.From = new MailAddress("thetarkman@hotmail.it");
                mail.To.Add("astreous@hotmail.com");
                mail.Subject = "ADVERT - " + DateTime.Now.ToString();
                mail.Body = "RETURN TO - " + textBox1.Text + "\n" + "HEADING - " + textBox2.Text + "\n" + "MESSAGE - " + richTextBox1.Text;
                bkk.RunWorkerAsync();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Could Not Send the Email, Check Your Image, if The Image File is Correct, Contact Astreous at info@astreous.tk for Help."); }
            panel2.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("www.astreous.tk");
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured While Opening your Browser."); }
        }

        private void bkk_DoWork(object sender, DoWorkEventArgs e)
        {
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("thetarkman@hotmail.it", "#include<oscar>");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

        private void bkk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Am_err ner = new Am_err();
                ner.tx("The Message Could not Be Sent, Check your Connection Settings.");
            }
            else
            {
                MessageBox.Show("Advertisment Submitted For Approval, You will recieve an Email at " + textBox1.Text + " within a week", "Advertisment Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
