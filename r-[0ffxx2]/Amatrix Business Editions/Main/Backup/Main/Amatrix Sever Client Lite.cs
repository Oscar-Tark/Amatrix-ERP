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
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Main
{
    public partial class Amatrix_Sever_Client_Lite : Form
    {
        public ArrayList al_ips = new ArrayList();
        public Amatrix_Sever_Client_Lite()
        {
            InitializeComponent();
            this.Show();
            this.Visible = false;
            if (Main.Amatrix.AMs != "")
            {
                bkk_ip.RunWorkerAsync();
            }
            else { this.Close(); }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        private void get_IP_table()
        {
            try
            {
                DataTable dtp = new DataTable();
                dtp = basql.Execute(Main.Amatrix.AMs, "SELECT * FROM AMS", "AMS", dtp);

                foreach (DataRow dr in dtp.Rows)
                {
                    try
                    {
                        al_ips.Add(dr.ItemArray[0].ToString());
                    }
                    catch (Exception errty) { }
                }
                //set my IP
                basql.Execute(Main.Amatrix.AMs, "INSERT INTO AMS values('" + Properties.Settings.Default.IP + "','')", "AMS", dtp);
            }
            catch (Exception erty) { MessageBox.Show(erty.StackTrace); }
        }

        //server code
        byte[] b; ArrayList al_socks = new ArrayList();
        private void initialize()
        {
            foreach (string s in al_ips)
            {
                try
                {
                    SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, s, SocketPermission.AllPorts);

                    IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse(s));
                    IPAddress ipAddr = ipHost.AddressList[0];
                    IPEndPoint ipep = new IPEndPoint(ipAddr, 5632);

                    Socket senderSock = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    senderSock.Connect(ipep);

                    al_socks.Add(senderSock);
                    //richTextBox1.Text = richTextBox1.Text + "Connected to: " + s + "\n";
                }
                catch (Exception erty) { MessageBox.Show(erty.Message); }
            }
            broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><val>0</val><app>0</app><typ>h</typ><con>0</con><par>0</par>");
        }

        public void create_socket(string ip_address)
        {
            try
            {
                SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, ip_address, SocketPermission.AllPorts);

                IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse(ip_address));
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipep = new IPEndPoint(ipAddr, 5632);

                //ipa = ipAddr; ipe = ipep;

                Socket senderSock = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                senderSock.Connect(ipep);

                al_socks.Add(senderSock);

                richTextBox1.Text = richTextBox1.Text + "Connected to: " + ip_address + "\n";
            }
            catch (Exception erty) { MessageBox.Show(erty.Message); }
        }

        private void recieve_(IAsyncResult iar)
        {
            string s = Encoding.Unicode.GetString(b);
            MessageBox.Show(s);
        }

        public void broadcast(string Message)
        {
            foreach (Socket senderSock in al_socks)
            {
                try
                {
                    byte[] msg = Encoding.Unicode.GetBytes(Message);
                    int byteSend = senderSock.Send(msg);
                }
                catch (Exception ertyt) { }
            }
        }

        private void get_string(Socket s)
        {
            byte[] b = new byte[1024];
            s.BeginReceive(b, 0, 0, SocketFlags.None, out_, this);
        }

        private void out_(IAsyncResult iar)
        {
            MessageBox.Show("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            initialize();
        }

        private void Amatrix_Sever_Client_Lite_Load(object sender, EventArgs e)
        {

        }

        private void bkk_ip_DoWork(object sender, DoWorkEventArgs e)
        {
            get_IP_table();
        }

        private void bkk_ip_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == false)
            {
                initialize();
            }
        }

        public void send(string message)
        {
        }
    }
}
