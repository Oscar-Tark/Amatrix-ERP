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
using System.Configuration;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class eml_send : Form
    {
        public eml_send()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.Text = "Amatrix Mailer";
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

        private void eml_send_Load(object sender, EventArgs e)
        {

        }

        public void Send_to(string who, string message, string me, string subject)
        {
            this.Show();
            textBox1.Text = who;
            richTextBox1.Text = message;
            textBox3.Text = subject;
        }

        public void Send_to(string who)
        {
            this.Show();
            textBox1.Text = who;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Contacts ctct = new Contacts();
            ctct.tx(true, this);
            ctct.Show();
        }

        MailMessage mail = new MailMessage();
        private void snd_Click(object sender, EventArgs e)
        {
            try
            {
                //SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                mail.From = new MailAddress(textBox2.Text);
                mail.To.Add(textBox1.Text);
                mail.Subject = textBox3.Text;
                mail.Body = richTextBox1.Text;//"RETURN TO - " + textBox1.Text + "\n" + "HEADING - " + textBox2.Text + "\n" + "MESSAGE - " + richTextBox1.Text;
                //SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("astreous@hotmail.com", "benjaminoscar");
                //SmtpServer.EnableSsl = true;
                //SmtpServer.Send(mail);
                eml_send_Credentials cred = new eml_send_Credentials();
                cred.tx(mail, textBox1.Text, textBox2.Text);
                cred.Show();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured While Amatrix Was Trying to Send Your Email, Please Verify That You Have Entered Email Addresses in Your To And From Fields."); }
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.FileInfo fnf = new System.IO.FileInfo(ofd.FileName);
            listView1.Items.Add(ofd.SafeFileName/* + " (To Remove, Double Click)"*/);
            listView1.Items[listView1.Items.Count - 1].SubItems.Add(fnf.Length.ToString() + "bytes");
            Attachment atc = new Attachment(ofd.FileName);
            mail.Attachments.Add(atc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        public void tx(ArrayList AL_ATTACHMENTS)
        {
            foreach (string s in AL_ATTACHMENTS)
            {
                listView1.Items.Add(s + " (To Remove, Double Click)");
                Attachment atc = new Attachment(s);
                mail.Attachments.Add(atc);
            }
            this.Show();
        }

        public void tx(String Contact)
        {
            textBox1.Text = Contact;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                mail.Attachments.RemoveAt(lvi.Index);
                lvi.Remove();
            }
        }
    }
}
