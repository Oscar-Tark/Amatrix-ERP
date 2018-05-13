using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.IO;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Astreous_Device_Module
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void find()
        {
            richTextBox1.Text = "";
            System.Net.NetworkInformation.IPGlobalProperties network = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            System.Net.NetworkInformation.TcpConnectionInformation[] connections = network.GetActiveTcpConnections();
            foreach (System.Net.NetworkInformation.TcpConnectionInformation tcp in connections)
            {
                richTextBox1.Text = richTextBox1.Text + "IP >>> " + "Remote Network Address : " + tcp.RemoteEndPoint.Address + " Local Network Address : " + tcp.LocalEndPoint.Address + " State : " + tcp.State.ToString() + "\n";
            }
        }

        public void GetComputersOnNetwork()
        {
            richTextBox1.Text = "";
            List<string> list = new List<string>();
            using (DirectoryEntry root = new DirectoryEntry("WinNT:"))
            {
                foreach (DirectoryEntry computers in root.Children)
                {
                    foreach (DirectoryEntry computer in computers.Children)
                    {
                        if ((computer.Name != "Schema"))
                        {
                            richTextBox1.Text = richTextBox1.Text + "Device >>> " + computer.Name + "\n";
                            list.Add(computer.Name);
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Size.Height >= 335)
            {
                timer1.Stop();
            }
            else
            {
                this.Size = new Size(this.Size.Width, this.Size.Height + 20);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            find();
            timer1.Start();
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void toolStripMenuItem82_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetComputersOnNetwork();
            timer1.Start();
        }

        private void mov_Tick(object sender, EventArgs e)
        {
            this.Location = new Point(Cursor.Position.X - 5, Cursor.Position.Y - 5);
        }

        private void tsmn_MouseDown(object sender, MouseEventArgs e)
        {
            mov.Start();
        }

        private void tsmn_MouseUp(object sender, MouseEventArgs e)
        {
            mov.Stop();
        }

        private void mov_clc(object sender, EventArgs e)
        {
            mov.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            using (var ex = new DirectoryEntry("LDAP://DC=example,DC=com"))
            using (var s = new DirectorySearcher(ex))
            {
                s.Filter = "(objectClass=printQueue)";
                try
                {
                    using (var c = s.FindAll())
                    {
                        foreach (SearchResult r in c)
                        {
                            richTextBox1.Text = richTextBox1.Text + r.ToString();
                        }
                    }
                }
                catch (Exception erty) { richTextBox1.Text = "Unfortunately No Printers Were Found, You may re-connect all Your Printers and Click on Find Printers Again."; }
            }
            timer1.Start();
        }
    }
}
