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
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace Main
{
    public partial class Amatrix_Server_Lite : Form
    {
        public Amatrix_Server_Lite()
        {
            InitializeComponent();
            this.Show();
            //if (Properties.Settings.Default.IP != "none")
            //{
                this.Visible = false;
                BackgroundWorker bkk = new BackgroundWorker();
                bkk.DoWork += new DoWorkEventHandler(bkk_thrd_DoWork);
                bkk.RunWorkerAsync();
            //}
            //else { this.Close(); }
        }

        private Thread th_strt;
        private delegate void del_strt_();
        private void bkk_thrd_DoWork(object sender, DoWorkEventArgs e)
        {
            th_strt = new Thread(new ThreadStart(del_strt_init_));
            th_strt.IsBackground = true;
            th_strt.Start();
        }

        private void del_strt_init_()
        {
            this.Invoke(new del_strt_(Start));
        }

        private void Amatrix_Server_Lite_Load(object sender, EventArgs e)
        {

        }

        //server
        private Socket sock;
        // You'll probably want to initialize the port and address in the
        // constructor, or via accessors, but to start your server listening
        // on port 8080 and on any IP address available on the machine...
        private int port = 5632;
        private IPAddress addr = IPAddress.Any;
        private ArrayList al_IPs = new ArrayList();

        // This is the method that starts the server listening.
        public void Start()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            Properties.Settings.Default.IP = localIP;
            Properties.Settings.Default.Save();
            textBox1.Text = localIP;

            try
            {
                // Create the new socket on which we'll be listening.
                SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, Properties.Settings.Default.IP, SocketPermission.AllPorts);

                IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse(Properties.Settings.Default.IP));
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipep = new IPEndPoint(ipAddr, 5632);

                this.sock = new Socket(
                    ipAddr.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp);
                // Bind the socket to the address and port.
                sock.Bind(new IPEndPoint(ipAddr, this.port));
                // Start listening.
                this.sock.Listen(10);
                // Set up the callback to be notified when somebody requests
                // a new connection.
                this.sock.BeginAccept(this.OnConnectRequest, sock);
                //label1.Text = "Running...";
                textBox1.ForeColor = Color.Black;
                //this.Visible = false;
                Amatrix_Sever_Client_Lite ascl = new Amatrix_Sever_Client_Lite();
                Main.Amatrix.ascl = ascl;
            }
            catch (Exception erty) { textBox1.ForeColor = Color.Red; this.Visible = true; }
        }

        public void start_manual()
        {

            try
            {
                // Create the new socket on which we'll be listening.
                SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, textBox1.Text, SocketPermission.AllPorts);

                IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse(textBox1.Text));
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipep = new IPEndPoint(ipAddr, 5632);

                this.sock = new Socket(
                    ipAddr.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp);
                // Bind the socket to the address and port.
                sock.Bind(new IPEndPoint(ipAddr, this.port));
                // Start listening.
                this.sock.Listen(10);
                // Set up the callback to be notified when somebody requests
                // a new connection.
                this.sock.BeginAccept(this.OnConnectRequest, sock);
                //label1.Text = "Running...";
                textBox1.ForeColor = Color.Black;
                //this.Visible = false;
                Amatrix_Sever_Client_Lite ascl = new Amatrix_Sever_Client_Lite();
                Main.Amatrix.ascl = ascl;

                Properties.Settings.Default.IP = textBox1.Text;
                Properties.Settings.Default.Save();
                this.Visible = false;
            }
            catch (Exception erty) { textBox1.ForeColor = Color.Red; this.Visible = true; }
        }

        // This is the method that is called when the socket recives a request
        // for a new connection.
        private void OnConnectRequest(IAsyncResult result)
        {
            // Get the socket (which should be this listener's socket) from
            // the argument.
            Socket sock = (Socket)result.AsyncState;
            // Create a new client connection, using the primary socket to
            // spawn a new socket.
            Connection newConn = new Connection(sock.EndAccept(result), this);
            //sock.EnableBroadcast = true;
            // Tell the listener socket to start listening again.
            sock.BeginAccept(this.OnConnectRequest, sock);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sock.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Properties.Settings.Default.IP = textBox1.Text;
            //Properties.Settings.Default.Save();
            start_manual();
        }

        private Thread th_val_set;
        private delegate void del_val_set(object o);
        public void set_val(/*Control*/ToolStripItem c, string val)
        {
            ArrayList al_o = new ArrayList();
            al_o.Add(c); al_o.Add(val);
            th_val_set = new Thread(new ParameterizedThreadStart(del_strt));
            th_val_set.IsBackground = true;
            th_val_set.Start((object)al_o);
        }

        private void del_strt(object o)
        {
            this.Invoke(new del_val_set(set_var), o);
        }

        private void set_var(object o)
        {
            ArrayList al = (ArrayList)o;
            ToolStripButton c = (ToolStripButton)al[0];
            //Control c = (Control)al[0];
            try
            {
                c.ForeColor = Color.Orange;
                c.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            }
            catch (Exception erty) { MessageBox.Show(erty.Message); }
        }
    }

    public class Connection// : Form1
    {
        private Socket sock;
        // Pick whatever encoding works best for you.  Just make sure the remote 
        // host is using the same encoding.
        private Encoding encoding = Encoding.UTF8;
        byte[] dataRcvBuf = new byte[1024];

        Amatrix_Server_Lite amsl_;
        public Connection(Socket s, Amatrix_Server_Lite amsl)
        {
            amsl_ = amsl;
            this.sock = s;
            // Start listening for incoming data.  (If you want a multi-
            // threaded service, you can start this method up in a separate
            // thread.)
            this.BeginReceive();
        }

        // Call this method to set this connection's socket up to receive data.
        private void BeginReceive()
        {
            this.sock.BeginReceive(
                    this.dataRcvBuf, 0,
                    this.dataRcvBuf.Length,
                    SocketFlags.None,
                    new AsyncCallback(this.OnBytesReceived),
                    this);
        }

        // This is the method that is called whenever the socket receives
        // incoming bytes.
        string s;
        protected void OnBytesReceived(IAsyncResult result)
        {
            try
            {
                // End the data receiving that the socket has done and get
                // the number of bytes read.
                int nBytesRec = this.sock.EndReceive(result);
                // If no bytes were received, the connection is closed (at
                // least as far as we're concerned).
                if (nBytesRec <= 0)
                {
                    this.sock.Close();
                    return;
                }
                // Convert the data we have to a string.
                string strReceived = this.encoding.GetString(this.dataRcvBuf, 0, nBytesRec);
                s = strReceived;
                //richTextBox1.Text = "";



                // ...Now, do whatever works best with the string data.
                // You could, for example, look at each character in the string
                // one-at-a-time and check for characters like the "end of text"
                // character ('\u0003') from a client indicating that they've finished
                // sending the current message.  It's totally up to you how you want
                // the protocol to work.

                // Whenever you decide the connection should be closed, call 
                // sock.Close() and don't call sock.BeginReceive() again.  But as long 
                // as you want to keep processing incoming data...

                // Set up again to get the next chunk of data.
                this.sock.BeginReceive(
                    this.dataRcvBuf, 0,
                    this.dataRcvBuf.Length,
                    SocketFlags.None,
                    new AsyncCallback(this.OnBytesReceived),
                    this);

                string g = UnicodeEncoding.Unicode.GetString(this.dataRcvBuf);
                set__(g);
                //MessageBox.Show(g);
                //int o = sock.Send(dataRcvBuf);
            }
            catch (Exception erty) { /*MessageBox.Show(erty.Message + "\n\n" + " {" + erty.StackTrace + "}");*/ }
        }

        public void set__(string s)
        {
            int ndx = s.IndexOf("<app>", 0);
            int ndx2 = s.IndexOf("</app>", ndx);
            
            int ndx3 = s.IndexOf("<val>",0);
            int ndx4 = s.IndexOf("</val>", ndx3);

            int ndx5 = s.IndexOf("<con>", 0);
            int ndx6 = s.IndexOf("</con>", ndx5);

            int ndx7 = s.IndexOf("<typ>", 0);
            int ndx8 = s.IndexOf("</typ>", ndx7);

            int ndx9 = s.IndexOf("<par>", 0);
            int ndx10 = s.IndexOf("</par>", ndx9);

            int ndx11 = s.IndexOf("<ip>", 0);
            int ndx12 = s.IndexOf("</ip>", ndx11);

            string app = s.Remove(ndx2);
            app = app.Remove(0, ndx + 5);

            string val = s.Remove(ndx4);
            val = val.Remove(0, ndx3 + 5);

            string con = s.Remove(ndx6);
            con = con.Remove(0, ndx5 + 5);

            string typ = s.Remove(ndx8);
            typ = typ.Remove(0, ndx7 + 5);

            string par = s.Remove(ndx10);
            par = par.Remove(0, ndx9 + 5);

            string ip = s.Remove(ndx12);
            ip = ip.Remove(0, ndx11 + 4);

            //check if i am connected to this ip???

            int nf = Main.Amatrix.ascl.al_ips.IndexOf(ip);
            if (nf == -1) { Main.Amatrix.ascl.al_ips.Add(ip); Main.Amatrix.ascl.create_socket(ip); }

            //find parameters
            if (ip != Properties.Settings.Default.IP)
            {
                ArrayList al_pras = new ArrayList();
                ndx = 0; string pras;
                foreach (char c in par)
                {
                    try
                    {
                        ndx = par.IndexOf("[", ndx);
                        ndx2 = par.IndexOf("]", ndx);

                        pras = par.Remove(ndx2);
                        pras = pras.Remove(0, ndx + 1);

                        al_pras.Add(pras);

                        ndx++;
                    }
                    catch (Exception erty) { break; }
                }

                int ndx_par = 0;
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name.Contains("mgmt") == true && Main.Amatrix.mgt == "") { break; }
                    if (f.Name.Contains("acc") == true && Main.Amatrix.acc == "") { break; }

                    if (f.Name == app)
                    {
                        //now find the control to manipulate
                        if (al_pras.Count == 0)
                        {
                            /*foreach (Control c in f.Controls)
                            {
                                /if (c.Name == con)
                                {
                                    if (typ == "w")
                                    {
                                        c.Enabled = false;
                                    }
                                    else { c.Enabled = true; }
                                    amsl_.set_val(c, val);
                                    //c.Text = "hello world!";
                                    break;
                                }
                            }*/
                        }
                        else
                        {
                            //ToolStrip cc = new ToolStrip();
                            Control cc = new Control();
                            try
                            {
                                for (int i = ndx_par; ndx_par < al_pras.Count; i++)
                                {
                                    try
                                    {
                                        if (i == 0)
                                        {
                                            cc = re_loop_par(al_pras[i].ToString(), f);
                                        }
                                        else
                                        {
                                            cc = re_loop_par(al_pras[i].ToString(), cc);
                                        }
                                    }
                                    catch (Exception erty) { break; }
                                }
                                //foreach (Control c in cc.Controls)
                                ToolStrip ts = (ToolStrip)cc;
                                foreach(ToolStripItem c in ts.Items)
                                {
                                    if (c.Name == con)
                                    {
                                        amsl_.set_val(c, val);
                                        break;
                                    }
                                }
                            }
                            catch (Exception erty) { MessageBox.Show(erty.StackTrace); }
                        }
                        //break;
                    }
                }
            }
        }

        private Control re_loop_par(string for_, Control in_form)
        {
            
            foreach (Control c in in_form.Controls)
            {
                if (c.Name == for_)
                {
                    return c;
                    in_form = c;
                    break;
                }
            }
            return in_form;
        }

        private Control re_loop_par(string for_, Form ff)
        {
            Control cc = new Control();
            try
            {
                foreach (Control c in ff.Controls)
                {
                    if (c.Name == for_)
                    {
                        return c;
                        cc = c;
                        break;
                    }
                }
            }
            catch (Exception erty) { MessageBox.Show(erty.StackTrace); }
            return cc;
        }
    }
}
