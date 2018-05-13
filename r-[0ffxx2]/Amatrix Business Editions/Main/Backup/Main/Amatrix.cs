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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;
using am_main.basics;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
//                                                                                                                                                                         0. END
//____________________________________________________________________________________________________________________________

namespace Main
{
    // Main class ~
    public partial class Amatrix : Form
    { 
        //Objects
        public static string path, hlpath;   //Memory logistics
        public static ArrayList al_lnk = new ArrayList();
        double opctydoub;
        public static int rgb = 0;

        //Open, Save Browse strings
        public static string sve;
        public static string opn;
        public static string brwse;

        public static Amatrix_Sever_Client_Lite ascl;


        //Controls for Amatrix
        public int voice;
        public static TextBox textb = new TextBox();
        public static string chstrng; 
        public static ArrayList al_prc = new ArrayList();

        //Flake

        Button flke = new Button();
        PictureBox pbxflke = new PictureBox();

        //Error reporter form {catch(Exception)}

        Form amhlp = new Form();

        //Open file dialog {}

        //First time error reporter
        Form errpwe = new Form();

        //First use form {fstmemeth()}
        Form oneuse = new Form();

        //First use forms
        Button dme = new Button();
        Button passbtn = new Button();

        //Beauty stuff forms
        PictureBox pbxoatt = new PictureBox();

        //Search
        ListBox lbsrc = new ListBox();

        //Secs
        public bool slug = new bool();
        public string reportthistoyou;

        //Cross Threading Operations
        private Thread thinit;
        private delegate void delinit();

        //private Thread thbk;
        //private delegate void delbk();

        private Thread thdb;
        private delegate void deldb();

        private Thread thfst;
        private delegate void delfst();

        //dll references
        am_main.basics.am_main amn = new am_main.basics.am_main();
        private cnt_mgmt mgmt = new cnt_mgmt();
        private cnt_bs bs = new cnt_bs();
        //links
        private ArrayList al = new ArrayList();

        //2. Application method {}                                                                                                            2.0 Main
        //____________________________________________________________________________________________________________________________________

        public Amatrix()
        {
            InitializeComponent();
            pbxoatt.VisibleChanged += new EventHandler(pbxoatt_VisibleChanged);
        }

        public static string acc, mgt, doc, AMs;
        private void load_connections()
        {
            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
            SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Connections", conn);
            conn.Open();
            SqlCeDataReader dr = cmd.ExecuteReader();
            DataTable dtp = new DataTable();
            dtp.Load(dr);
            conn.Close();

            Main.Amatrix.doc = dtp.Rows[3].ItemArray[1].ToString();
            Main.Amatrix.mgt = dtp.Rows[1].ItemArray[1].ToString();
            Main.Amatrix.acc = dtp.Rows[0].ItemArray[1].ToString();
            Main.Amatrix.AMs = dtp.Rows[4].ItemArray[1].ToString();
        }

        void pbxoatt_VisibleChanged(object sender, EventArgs e)
        {
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

        //non threaded initialization
        private void thnoinit()
        {
            bkc.Location = new Point(20, 35);
            bkc.Size = new Size(50, 50);
            bkc.BackgroundImage = Properties.Resources.bckacc;
            bkc.FlatStyle = FlatStyle.Flat;
            bkc.Visible = false;
            bkc.FlatAppearance.BorderSize = 0;
            bkc.FlatAppearance.MouseDownBackColor = Color.Transparent;
            bkc.FlatAppearance.MouseOverBackColor = Color.Transparent;
            bkc.BackgroundImageLayout = ImageLayout.Zoom;
            bkc.BackColor = Color.Transparent;
            bkc.Click += new EventHandler(booksclc);
            bkc.MouseEnter += new EventHandler(bookshov);
            bkc.MouseLeave += new EventHandler(booksovv);
            bkc.MouseDown += new MouseEventHandler(bkc_MouseDown);
            bkc.MouseUp += new MouseEventHandler(bkc_MouseUp);
            this.Controls.Add(bkc);
            this.Controls.Add(mgmt);
            this.Controls.Add(bs);
            bs.Visible = false;
            bs.Dock = DockStyle.Fill;
            mgmt.Visible = false;
        }

        void bkc_MouseUp(object sender, MouseEventArgs e)
        {
            bkc.BackgroundImage = Properties.Resources.bckacc;
        }

        void bkc_MouseDown(object sender, MouseEventArgs e)
        {
            bkc.BackgroundImage = Properties.Resources.bckaccdwn;
        }

        private void set_lnks()
        {
            try
            {
                int start = 0;
                int start2 = 0;
                int stop = start + 1;
                int stop2 = start2 + 1;
                string catch_ = "";
                string catch_2 = "";
                int ndx = 0;

                for(int k = 1; k <= Properties.Settings.Default.app2; k++)
                {
                    start = Properties.Settings.Default.app1.IndexOf('|', start);
                    stop = Properties.Settings.Default.app1.IndexOf('|', stop);
                    
                    if (ndx >= Properties.Settings.Default.app1.Length) { break; }

                    for (int i = start + 1; i < stop; i++)
                    {
                        catch_ = catch_ + Properties.Settings.Default.app1[i];
                    }
                    al.Add(catch_);
                    catch_ = "";
                    start = stop;
                    stop = start + 1;


                    start2 = Properties.Settings.Default.app1ttp.IndexOf('|', start2);
                    stop2 = Properties.Settings.Default.app1ttp.IndexOf('|', stop2);

                    for (int i2 = start2 + 1; i2 < stop2; i2++)
                    {
                        catch_2 = catch_2 + Properties.Settings.Default.app1ttp[i2];
                    }
                    Add_lnk_gui(catch_2, ndx, catch_);
                    catch_2 = "";
                    start2 = stop2;
                    stop2 = start2 + 1;
                    ndx++;
                }
            }
            catch (Exception erty) { }
        }

        private ToolStripMenuItem icn_setlink(ToolStripMenuItem tsi, int ndx)
        {
            string sicn = "";
            try
            {
                sicn = al[ndx].ToString();
            }
            catch (Exception erty2) { try { sicn = tsi.ToolTipText; } catch (Exception erty) { tsi.Image = Properties.Resources.file_link; } }
            try
            {
                if (sicn.ToLower().EndsWith(".doc") || sicn.ToLower().EndsWith(".xls") || sicn.ToLower().EndsWith(".ppt") || sicn.ToLower().EndsWith(".docx") || sicn.ToLower().EndsWith(".pptx") || sicn.ToLower().EndsWith(".xlsx") || sicn.ToLower().EndsWith(".odt") || sicn.ToLower().EndsWith(".txt") || sicn.ToLower().EndsWith(".rtf"))
                {
                    tsi.Image = Properties.Resources.filetype_doc;
                }
                else if (sicn.ToLower().EndsWith(".sdf") || sicn.ToLower().EndsWith(".mdf") || sicn.ToLower().EndsWith(".dbt"))
                {
                    tsi.Image = Properties.Resources.filetype_db;
                }
                else if (sicn.ToLower().EndsWith(".bmp") || sicn.ToLower().EndsWith(".jpg") || sicn.ToLower().EndsWith(".gif") || sicn.ToLower().EndsWith(".png") || sicn.ToLower().EndsWith(".tiff"))
                {
                    tsi.Image = Properties.Resources.filetype_image;
                }
                else if (sicn.ToLower().EndsWith(".mp3") || sicn.ToLower().EndsWith(".mp4") || sicn.ToLower().EndsWith(".aac") || sicn.ToLower().EndsWith(".wma") || sicn.ToLower().EndsWith(".wav"))
                {
                    tsi.Image = Properties.Resources.filetype_music;
                }
                else if (sicn.ToLower().EndsWith(".wmv") || sicn.ToLower().EndsWith(".avi") || sicn.ToLower().EndsWith(".mkv") || sicn.ToLower().EndsWith(".mov"))
                {
                    tsi.Image = Properties.Resources.filetype_mov;
                }
                else if (sicn.ToLower().EndsWith(".html") || sicn.ToLower().EndsWith(".htm") || sicn.ToLower().EndsWith(".mht") || sicn.ToLower().EndsWith(".php") || sicn.ToLower().EndsWith(".css") || sicn.ToLower().EndsWith(".asp"))
                {
                    tsi.Image = Properties.Resources.filetype_html;
                }
                else if (sicn.ToLower().EndsWith(".exe") || sicn.ToLower().EndsWith(".jar") || sicn.ToLower().EndsWith(".bin"))
                {
                    tsi.Image = Properties.Resources.filetype_exe;
                }
                else { tsi.Image = Properties.Resources.file_link; }
            }
            catch (Exception erty) { }
            return tsi;
        }

        private void Add_lnk_gui(string nme, int ndx, string location)
        {
            appmylnks.DropDown.ShowItemToolTips = true;
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi = icn_setlink(tsmi, ndx);
            tsmi.Text = nme;
            tsmi.ImageTransparentColor = Color.White;
            tsmi.ToolTipText = location;
            tsmi.Tag = (object)ndx;
            tsmi.Click += new EventHandler(tsmi_Click);

            ToolStripMenuItem tsmi_del = new ToolStripMenuItem("Delete Link");
            tsmi_del.Image = Properties.Resources.ex;
            tsmi_del.Click += new EventHandler(tsmi_del_Click);
            tsmi_del.ImageTransparentColor = Color.White;

            tsmi.DropDown.Items.Add(tsmi_del);

            appmylnks.DropDown.Items.Add(tsmi);
        }

        void tsmi_rep_Click(object sender, EventArgs e)
        {
            tsptmp = (ToolStripMenuItem)sender;
            delete_link(tsptmp);

            Guided_Function gf = new Guided_Function();
            gf.Show();
            gf.Disposed += new EventHandler(gf_Disposed);
        }

        void gf_Disposed(object sender, EventArgs e)
        {
            if (al_lnk.Count != 0)
            {
                al.Add(al_lnk[1]);
                Add_lnk_gui(al_lnk[1].ToString(), Properties.Settings.Default.app2-1, al_lnk[0].ToString());
                al_lnk.Clear();
            }
        }

        private ToolStripMenuItem tsptmp2; ToolStripMenuItem tsmitmo; private int ni2;
        void tsmi_del_Click(object sender, EventArgs e)
        {
            tsptmp2 = (ToolStripMenuItem)sender;
            tsmitmo = (ToolStripMenuItem)tsptmp2.OwnerItem;
            delete_link(tsmitmo);
            tsptmp2.OwnerItem.Dispose(); 
        }

        private void delete_link(ToolStripMenuItem tsptat)
        {
            int fux = (int)tsptat.Tag;
            string stng = al[fux].ToString();

            if (al[fux].ToString() == "" || Properties.Settings.Default.app1ttp.Contains(al[fux].ToString()) == true)
            {
                if (tsptat.ToolTipText != "" && tsptat.ToolTipText != null)
                {
                    stng = tsptat.ToolTipText;
                }
            }


            if (Properties.Settings.Default.app2 == 1)
            {
                Properties.Settings.Default.app1 = "";
                Properties.Settings.Default.app1ttp = "";
            }
            else
            {
                Properties.Settings.Default.app1 = Properties.Settings.Default.app1.Replace('|' + stng, "");
                Properties.Settings.Default.app1ttp = Properties.Settings.Default.app1ttp.Replace('|' + tsptat.Text, "");
            }

            Properties.Settings.Default.app2 = Properties.Settings.Default.app2 - 1;
            Properties.Settings.Default.app2ttp = Properties.Settings.Default.app2ttp - 1;
            Properties.Settings.Default.Save();
        }

        private ToolStripMenuItem tsptmp; private int ni;
        void tsmi_Click(object sender, EventArgs e)
        {
            tsptmp = (ToolStripMenuItem)sender;
            ni = (int)tsptmp.Tag;
            string s = al[ni].ToString();
            try
            {
                if (s.EndsWith(".txt"))
                {
                    App_Workbook aw = new App_Workbook();
                    aw.openext(s);
                }
                /*else if (s.EndsWith(".afd"))
                {
                    Doc_stdio std = new Doc_stdio();
                    std.open_out(s, false);
                }*/
                else
                {
                    System.Diagnostics.Process.Start(s);
                }
            }
            catch (Exception erty) { try { System.Diagnostics.Process.Start(tsptmp.ToolTipText); } catch (Exception erty2) { tsptmp.Image = Properties.Resources.Console; Am_err ner = new Am_err(); ner.tx("Could Not Find the Specified Link."); } tsptmp.Image = Properties.Resources.Console; }
        }


        //3.0 Initialization method {}                                                                                                       3.0 Initialization 
        //______________________________________________________________________________________________________________________________________                                

        private void bkk_init_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                th_init_strt_();
            }
            catch (Exception erty) { bkk_init.CancelAsync(); }
        }

        private void th_init_strt_()
        {
            try
            {
                thinit = new Thread(new ThreadStart(delinitstrt));
                thinit.IsBackground = true;
                thinit.Start();
            }
            catch (Exception erty) { if (bkk_init.IsBusy == true) { bkk_init.CancelAsync(); } else { Inint(); } }
        }

        private void bkk_init_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true) { th_init_strt_(); }
        }

        private void delinitstrt()
        {
            try
            {
                this.Invoke(new delinit(Inint));
            }
            catch (Exception erty) { if (bkk_init.IsBusy == true) { bkk_init.CancelAsync(); } else { Inint(); } }
        }

        protected void Inint()
        {
            load_connections();
            Virtual_window_manager vm = new Virtual_window_manager();
            vm.Show();
            set_lnks();

            if (Properties.Settings.Default.lockstat == "Locked")
            {
                lck();
                this.Text = "Amatrix : Started in Locked mode";
            }
            else if (Properties.Settings.Default.lockstat == "none")
            {
            }
            else { Properties.Settings.Default.lockstat = "none"; Properties.Settings.Default.Save(); }

            Properties.Settings.Default.usable_month = Properties.Settings.Default.usable_month + 1;
            Properties.Settings.Default.Save();

            /*try
            {
                Bitmap bmp = new Bitmap(Properties.Resources.fle_App_thumb);
                //bmp.MakeTransparent(Color.White);
                Icon i = Icon.FromHandle(bmp.GetHicon());
                Bitmap bmp2 = new Bitmap(Properties.Resources.mgmt_App_thumb);
                Icon i2 = Icon.FromHandle(bmp2.GetHicon());
                Bitmap bmp3 = new Bitmap(Properties.Resources.Acc_App_thmb);
                Icon i3 = Icon.FromHandle(bmp3.GetHicon());
                Bitmap bmp4 = new Bitmap(Properties.Resources.bus_App_thmb);
                Icon i4 = Icon.FromHandle(bmp4.GetHicon());
                Bitmap bmp5 = new Bitmap(Properties.Resources.dbs_App_THUMB);
                Icon i5 = Icon.FromHandle(bmp5.GetHicon());
                Bitmap bmp6 = new Bitmap(Properties.Resources.shp_App_thumb);
                Icon i6 = Icon.FromHandle(bmp6.GetHicon());

                Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton[] tmarr = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton[6];
                Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton ttbb = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton(i, "Document Studio");
                Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton ttbb2 = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton(i2, "Managment Studio");
                Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton ttbb3 = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton(i3, "Accounting Studio");
                Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton ttbb4 = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton(i4, "Business Studio");
                Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton ttbb5 = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton(i5, "Database Studio");
                Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton ttbb6 = new Microsoft.WindowsAPICodePack.Taskbar.ThumbnailToolBarButton(i6, "Shoppe");

                ttbb.Click += new EventHandler<Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs>(oabtn_Click);
                ttbb2.Click += new EventHandler<Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs>(devbtn_Click);
                ttbb3.Click += new EventHandler<Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs>(sysbttn_Click);
                ttbb4.Click += new EventHandler<Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs>(cs_Click);
                ttbb5.Click += new EventHandler<Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs>(mpbttn_Click);
                ttbb6.Click += new EventHandler<Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs>(shpbtn_Click);

                tmarr[0] = ttbb;
                tmarr[1] = ttbb2;
                tmarr[2] = ttbb3;
                tmarr[3] = ttbb4;
                tmarr[4] = ttbb5;
                tmarr[5] = ttbb6;

                Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.ThumbnailToolBars.AddButtons(this.Handle, tmarr);
            }
            catch (Exception erty) { }*/

            try
            {
                Bitmap bmp = new Bitmap(Properties.Resources.main_Mnu_Thumb);
                bmp.MakeTransparent(Color.White);
                Icon i = Icon.FromHandle(bmp.GetHicon());
                amnti.Icon = i;
            }
            catch (Exception erty) { }
            thnoinit();
            try
            {
                ProcessStartInfo stfo = new ProcessStartInfo();
                stfo.UseShellExecute = true;
                stfo.FileName = Application.StartupPath + "\\RD_DIR.exe";
                Process.Start(stfo);
            }
            catch (Exception erty) { }

            cmsclck.AllowTransparency = true;
            cmsclck.Opacity = 0.90;
            appmylnks.DropDown.AllowTransparency = true;
            appmylnks.DropDown.Opacity = 0.90;
            oabttntls.DropDown.AllowTransparency = true;
            oabttntls.DropDown.Opacity = 0.90;

            //objects
            slug = true;
            //beauty
            opctydoub = this.Opacity;
            Amcs.AllowTransparency = true;
            Amcs.Opacity = 0.90;
            pbxoatt.Location = new Point(718, 386);
            pbxoatt.BackgroundImageLayout = ImageLayout.Zoom;
            pbxoatt.BackColor = Color.Transparent;
            pbxoatt.BorderStyle = BorderStyle.None;
            pbxoatt.BackgroundImageLayout = ImageLayout.Zoom;
            pbxoatt.Size = new Size(225, 100);
            bttncret();

            try
            {
                Amatrix_Server_Lite asl = new Amatrix_Server_Lite();
            }
            catch (Exception erty) { }
            //Scorpion.Scorp_Ce ce = new Scorpion.Scorp_Ce();
            //ce.start();
        }

        void lg_Disposed(object sender, EventArgs e)
        {
            mnuamtx.Enabled = true;
            appsmnustr.Enabled = true;
            Virtual_window_manager vvm = new Virtual_window_manager();
            vvm.Show();
        }

        //Event handling for init ->

        void AMact(object sender, EventArgs e)
        {
            try
            {
                decttime.Stop();
            }
            catch (Exception excprevdec)
            {
            }
            try
            {
                this.Opacity = Properties.Settings.Default.opacity;
            }
            catch (Exception erty) { }
        }

        void AMdec(object sender, EventArgs e)
        {
            decttime.Start();
        }    
        //                                                                                                                                                             3.0 END
        //______________________________________________________________________________________________________________________________________


        //Background Image Starter

        /*private void thbkstrt()
        {
            backimgtme.Stop();
            try
            {
                thbk = new Thread(new ThreadStart(delbkstrt));
                thbk.Start();
            }
            catch (Exception erty) { backimage(); }
        }

        private void delbkstrt()
        {
            try
            {
                this.Invoke(new delbk(backimage));
            }
            catch (Exception erty) { backimage(); }
        }

        protected void backimage()
        {
            try
            {
                if (Properties.Settings.Default.backimage == "Default")
                {
                    this.BackgroundImage = Properties.Resources.backrnddim;
                }
                else if (Properties.Settings.Default.backimage == "none")
                {
                    this.BackgroundImage = null;
                }
                else
                {
                    this.BackgroundImage = System.Drawing.Bitmap.FromFile(Properties.Settings.Default.backimage.ToString());
                }
            }
            catch (Exception eback)
            {
                this.BackgroundImage = Properties.Resources.backrnddim;
            }
        }*/

        //External controls
        TextBox txbpass = new TextBox();
        //Event handling for oneuse
        protected void onedveves(object dveves, EventArgs dvevevan)
        {
            dme.BackgroundImage = Properties.Resources.btnsimp2;
        }
        protected void ondvevesext(object dvefes, EventArgs dvevefan)
        {
            dme.BackgroundImage = Properties.Resources.btnsim1;
        }
        protected void dmecl(object dmon, EventArgs dmonev)
        {
            oneuse.Close();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Enabled = true;
            amnti.BalloonTipIcon = ToolTipIcon.None;
            amnti.BalloonTipTitle = "Welcome";
            amnti.BalloonTipText = "Amatrix has been successfully configured, you may now use Amatrix";
            amnti.ShowBalloonTip(200);
        }
        //                                                                                                                                                    8.0 END
        //________________________________________________________________________________________________________________

        //10.0 event handling for errors ->                                                                             10.0 Error handler events
        //_________________________________________________________________________________________________________________

        protected void rstrtclick(object restrtxrerr, EventArgs erreventxar)
        {
            Application.Restart();
        }
        protected void clsefaterr(object restrtxrerrfat, EventArgs erreventxarfat)
        {
            Close();
        }

        //15.0 Three unknowns                                                                                                  15.0 Three unknowns
        //___________________________________________________________________________________________________________________
  
        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        //                                                                                                                                       15.0 END
        //___________________________________________________________________________________________________________________







        //16.0 Main load parent method {}                                                                              16.0 Main Loader
        //__________________________________________________________________________________________________________________

        private void Amatrix_Load(object sender, EventArgs e)
        {
            this.Disposed += new EventHandler(Amatdp);
            try
            {
                string[] s = Environment.GetCommandLineArgs();
                if (s[1] != "" || File.Exists(s[1]) == true)
                {
                    Doc_stdio d = new Doc_stdio();
                    d.Show();
                    a.Read(s[1]);
                    d.open_out(s[1], false);
                }
            }
            catch (Exception erty) { }
        }

        void Amatdp(object sender, EventArgs e)
        {
            amnti.Dispose();
        }

        //                                                                                                                                     16.0 END
        //_________________________________________________________________________________________________________________





        //20.0 Shoppe button in applications menu []                                                            20.0 Shoppe in app menu
        //__________________________________________________________________________________________________________________

        private void shpbtn_Click(object sender, EventArgs e)
        {
            //shoppe button dtop
            try
            {
                Process p = new Process();
                ProcessStartInfo pi = new ProcessStartInfo(Environment.CurrentDirectory + "\\Connect\\Amatrix Connect Business Client.exe");
                p.StartInfo = pi;
                al_prc.Add(p);
                p.Start();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to Start Amatrix Connect"); }
        }

        //                                                                                                                                    20.0 END
        //__________________________________________________________________________________________________________________






        //21.0 Shoppe method {}                                                                                                21.0 Shoppe method
        //__________________________________________________________________________________________________________________

        protected void spe()
        {
            try
            {
            }
            catch (Exception expo)
            {
                reportthistoyou = "An error occured in Shoppe, the application has successfully recovered";
                rcas();
            }
        }

        //Event handling for spe controls

        //                                                                                                                                    21.0 END
        //__________________________________________________________________________________________________________________






        //22.0 Main menu media player button []                                                                    22.0 Main mp button
        //__________________________________________________________________________________________________________________

        private void mpbttn_Click(object sender, EventArgs e)
        {
            //AMDS
            AMDS amdb = new AMDS();
            amdb.Show();
        }

        //                                                                                                                                       22.0 END
        //__________________________________________________________________________________________________________________





        //24.0 Developer button in main menu []                                                                         24.0 Developer main button
        //__________________________________________________________________________________________________________________

        private void devbtn_Click(object sender, EventArgs e)
        {
            tick = 0;
            bkc.Visible = true;
            tmsin.Start();
            expl = 2;
        }
        //                                                                                                                                        24.0 END
        //__________________________________________________________________________________________________________________


        //26.0 Office button in main menu []                                                                                26.0 Main OA button
        //__________________________________________________________________________________________________________________

        private void oabtn_Click(object sender, EventArgs e)
        {
            Amatrix_Choose_Doc ams = new Amatrix_Choose_Doc();
        }

        //                                                                                                                                          26.0 END
        //__________________________________________________________________________________________________________________






        //27. Office Method {}                                                                                                         27.0 OA method
        //_________________________________________________________________________________________________________________                                                                                                             
        
        protected void oa()
        {
            //office application
            /*try
            {
                Doc_stdio_canopner can = new Doc_stdio_canopner();
                can.Show();
            }
            catch (Exception expo)
            {
                reportthistoyou = "An error occured in Office, the application has successfully recovered";
                rcas();
            }*/
        }

        //Event handling for OA controls

  

        //                                                                                                                                           27.0 END
        //_________________________________________________________________________________________________________________



        //36.0 Tray icon event handling ->                                                                                     36.0 Tray icon event handling
        //_____________________________________________________________________________________________________________

        private void amnti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //notification icon
            Close();
        }

        //                                                                                                                                           36.0 END
        //_____________________________________________________________________________________________________________







        //37.0 Main settings button []                                                                                               37.0 Main sett button
        //_____________________________________________________________________________________________________________

        private void sysbttn_Click(object sender, EventArgs e)
        {
            tick = 0;
            bkc.Visible = true;
            tmsin.Start();
            expl = 3;
        }

        //                                                                                                                                           37.0 END
        //_____________________________________________________________________________________________________________






        //40.0 Error method {}                                                                                                             40.0 Error method
        //____________________________________________________________________________________________________________

        protected void error()
        {
            Form err = new Form();
            err.Size = new Size(1006, 594);
            err.MaximizeBox = false;
            err.Opacity = 0.80;
            err.ShowIcon = false;
            err.TopMost = true;
            err.BackColor = Color.Black;
            err.Text = "Error";
            err.FormBorderStyle = FormBorderStyle.FixedSingle;
            err.MinimizeBox = false;
            err.MaximizeBox = false;
            err.StartPosition = FormStartPosition.CenterScreen;
            err.Show();
        }

        //                                                                                                                                                40.0 END
        //____________________________________________________________________________________________________________






        //41.0 Clipboard method {}                                                                                                       41.0 Clip board method
        //___________________________________________________________________________________________________________

        public void Clipboard()
        {
            try
            {

            }
            catch (Exception ecb)
            {
                reportthistoyou = "Clipboard has faced a problem although the application has successfully recovered";
                rcas();
            }
        }

     
        //42.0 Crap ->                                                                                                                             42.0 CRAP ->
        //___________________________________________________________________________________________________________

        public void clsebttn(object sender, EventArgs e)
        {
            //method used to close forms
            Close();
        }

        //43.0 School Online {}                                                                                                                43.0 School Online
        //___________________________________________________________________________________________________________

        
        protected void so()
        {
            try
            {
            }
            catch (Exception expo)
            {
                reportthistoyou = "School Online has faced a problem although the application has successfully recovered";
                rcas();
            }
        }

        //45.0 Securer event handing ->                                                                                              45.0 Securer Events
        //___________________________________________________________________________________________________________

        TextBox tbsecsnw = new TextBox();
        protected void nwhvsecs(object secsb, EventArgs secsevan)
        {
       
        }
        protected void addclsecs(object secsba, EventArgs secsfevan)
        {
            FileStream fsdis0 = new FileStream("mod.btcore", FileMode.Truncate);
            fsdis0.Flush();
            fsdis0.Close();
            FileStream fsdis = new FileStream("mod.btcore", FileMode.Open, FileAccess.Write);
            StreamWriter swdis = new StreamWriter(fsdis);
            swdis.Write("no");
            swdis.Flush();
            fsdis.Flush();
            swdis.Close();
            fsdis.Close();
        }

        //46.0 Restart application method {}                                                                                      46.0 App restart method
        //___________________________________________________________________________________________________________

        protected void main_restart(object sender, EventArgs eres)
        {
            Application.Restart();
        }


        //47.0 Close main application event ->                                                                                    47.0 Close main ->
        //___________________________________________________________________________________________________________

        protected void clse_main(object sender, EventArgs ecls)
        {
            try
            {
                amcletmer.Interval = Properties.Settings.Default.Frmrtem;
                amcletmer.Start();
            }
            catch (Exception entmncl)
            {
                this.Close();
            }
        }

        // 48.0 Menu items                                                                                                                   48.0 Menu items
        //__________________________________________________________________________________________________________


        private void oaappmen_Click(object sender, EventArgs e)
        {
            //application menu office
            oa();
        }

        private void shpeappmnu_Click(object sender, EventArgs e)
        {

        }

        private void help()
        {
            Helper hlp = new Helper();
            hlp.Show();
        }

        private void hlpmnmnubtn_Click(object sender, EventArgs e)
        {
            help();
        }

        private void hlpappppmnu_Click(object sender, EventArgs e)
        {
            help();
        }

        private void cbviewbttnmnu_Click(object sender, EventArgs e)
        {
            Clipboard();
        }


        private void dhlpbttn_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.Show();
        }



        private void mphlpbttn_Click(object sender, EventArgs e)
        {
            //mp application button help
            help();
        }



        private void shppemnbttnapp_Click(object sender, EventArgs e)
        {
            //shoppe application button help
            help();
        }



        private void devappbttn_Click(object sender, EventArgs e)
        {
            //devloper application button help
            help();
        }



        private void busbttnapphlp_Click(object sender, EventArgs e)
        {
            //business application button help
            help();
        }

        int menu = 1;
        private void flemnu_Click(object sender, EventArgs e)
        {
            if (menu == 1)
            {
                flemnu.Text = "File (Open)";
                filemnutab.Visible = true;
                menu = 2;
            }
            else if (menu == 2)
            {
                flemnu.Text = "File";
                filemnutab.Visible = false;
                menu = 1;
            }
            else
            {
            }
            //file menu main
        }


        //                                                                                                                                 48.0 END
        //__________________________________________________________________________________________________________




        //??. DESTROYER                                                                                                                 DESTROYER
        //__________________________________________________________________________________________________________

        protected void destroyer()
        {
            //Main

            this.Controls.Remove(oabtn);
            this.Controls.Remove(devbtn);
            this.Controls.Remove(sep);
            this.Controls.Remove(cs);
            this.Controls.Remove(mpbttn);
            this.Controls.Remove(sysbttn);
            this.Controls.Remove(mnuamtx);
        }


        //READYCARE________________________________________________________________________________________________________
        
        //Readycare application security________________________________________________________________________________

        //objects for rcas
        Button bxrcas = new Button();
        Label lblrcas = new Label();
        Label lblcontorstp = new Label();
        Button btnrcascont = new Button();
        Button btnrcasres = new Button();

        public void rcas()
        {
            try
            {
                //Public functions
                this.Text = "Readycare : Reporter";
                this.MinimizeBox = true;
                this.ControlBox = true;
                this.MaximizeBox = true;
                //Visible
                this.BackgroundImage = Properties.Resources.backagegreyconv;
                this.BackgroundImageLayout = ImageLayout.Stretch;

                //General
                appsmnustr.Visible = false;
                appsmnustr.Enabled = false;
                pbxoatt.Visible = false;
                pbxoatt.Enabled = false;
                flke.Visible = false;
                flke.Enabled = false;

                //Non visible

                //In context menu's
                //Main
                mnuamtx.Visible = false;
                oabtn.Visible = false;
                devbtn.Visible = false;
                sep.Visible = false;
                cs.Visible = false;
                mpbttn.Visible = false;
                sysbttn.Visible = false;
                //Main
                oabtn.Enabled = false;
                devbtn.Enabled = false;
                sep.Enabled = false;
                cs.Enabled = false;
                mpbttn.Enabled = false;
                sysbttn.Enabled = false;


                //For this objects

                lblcontorstp.Text = "Would you like to Continue or Restart Amatrix?";
                lblcontorstp.ForeColor = Color.WhiteSmoke;
                lblcontorstp.Location = new Point(380, 320);
                lblcontorstp.Size = new Size(340, 40);
                lblcontorstp.BackColor = Color.Transparent;
                lblrcas.Text = reportthistoyou;
                lblrcas.ForeColor = Color.WhiteSmoke;
                lblrcas.Location = new Point(330, 280);
                lblrcas.Size = new Size(340, 40);
                lblrcas.BackColor = Color.Transparent;
                bxrcas.Image = Properties.Resources.rdycreconv;
                bxrcas.Size = new Size(128, 128);
                bxrcas.FlatStyle = FlatStyle.Flat;
                bxrcas.BackColor = Color.Transparent;
                bxrcas.FlatAppearance.BorderColor = Color.DarkGray;
                bxrcas.FlatAppearance.BorderSize = 0;
                bxrcas.FlatAppearance.MouseDownBackColor = Color.Transparent;
                bxrcas.FlatAppearance.MouseOverBackColor = Color.Transparent;
                bxrcas.Location = new Point(20, 20);
                btnrcascont.FlatStyle = FlatStyle.Flat;
                btnrcascont.FlatAppearance.BorderColor = Color.DarkGray;
                btnrcascont.FlatAppearance.BorderSize = 0;
                btnrcascont.ForeColor = Color.WhiteSmoke;
                btnrcascont.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btnrcascont.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btnrcascont.BackColor = Color.Transparent;
                btnrcascont.Text = "Continue";
                btnrcascont.Location = new Point(383, 390);
                btnrcascont.BackgroundImage = Properties.Resources.btnsim1;
                btnrcascont.BackgroundImageLayout = ImageLayout.Stretch;
                btnrcascont.Size = new Size(100, 35);
                btnrcasres.FlatStyle = FlatStyle.Flat;
                btnrcasres.FlatAppearance.BorderColor = Color.DarkGray;
                btnrcasres.FlatAppearance.BorderSize = 0;
                btnrcasres.ForeColor = Color.WhiteSmoke;
                btnrcasres.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btnrcasres.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btnrcasres.BackColor = Color.Transparent;
                btnrcasres.Text = "Restart";
                btnrcasres.Location = new Point(503, 390);
                btnrcasres.BackgroundImage = Properties.Resources.btnsim1;
                btnrcasres.BackgroundImageLayout = ImageLayout.Stretch;
                btnrcasres.Size = new Size(100, 35);
                this.Controls.Add(lblcontorstp);
                this.Controls.Add(lblrcas);
                this.Controls.Add(bxrcas);
                this.Controls.Add(btnrcascont);
                this.Controls.Add(btnrcasres);

                //Event assosiation
                btnrcascont.MouseEnter += new EventHandler(btrasent);
                btnrcascont.MouseLeave += new EventHandler(btrasext);
                btnrcasres.MouseEnter += new EventHandler(btresent);
                btnrcasres.MouseLeave += new EventHandler(btreext);
                btnrcascont.Click += new EventHandler(conterrok);
                btnrcasres.Click += new EventHandler(resterrnow);
            }
            catch (Exception ebrwexr)
            {
            }
        }

        //Eventhandling readycareappreporter ->

        protected void conterrok(object ercok, EventArgs rerrok)
        {
            appsmnustr.Visible = true;
            appsmnustr.Enabled = true;
            flke.Visible = true;
            flke.Enabled = true;
            this.Controls.Remove(btnrcasres);
            this.Controls.Remove(btnrcascont);
            this.Controls.Remove(lblrcas);
            this.Controls.Remove(lblcontorstp);
            this.Controls.Remove(bxrcas);
        }
        protected void resterrnow(object resbgg, EventArgs seraes)
        {
            Application.Restart();
        }
        protected void btrasent(object entdex, EventArgs enterea)
        {
           btnrcascont.BackgroundImage = Properties.Resources.btnsimp2;
        }
        protected void btrasext(object objextbtn, EventArgs eaefangh)
        {
            btnrcascont.BackgroundImage = Properties.Resources.btnsim1;
        }
        protected void btresent(object enrescre, EventArgs btrentea)
        {
            btnrcasres.BackgroundImage = Properties.Resources.btnsimp2;
        }
        protected void btreext(object teraentresobj, EventArgs eaterafan)
        {
            btnrcasres.BackgroundImage = Properties.Resources.btnsim1;
        }
        //Readycare application reporter -END-
        //Closure methods______________________________________________________________________________________

        private void clsemainmt()
        {
            try
            {
                amcletmer.Interval = Properties.Settings.Default.Frmrtem;
                amcletmer.Start();
            }
            catch (Exception entmncl)
            {
                this.Close();
            }
        }

        private void mnclse_Click(object sender, EventArgs e)
        {
            clsemainmt();
        }

        private void addappsmylnq_Click(object sender, EventArgs e)
        {
            try
            {
                Form appsadd = new Form();
                appsadd.Size = new Size(512, 256);
                appsadd.StartPosition = FormStartPosition.CenterScreen;
                appsadd.BackColor = Color.Black;
                appsadd.ShowInTaskbar = false;
                appsadd.ShowIcon = false;
                appsadd.TopMost = true;
                appsadd.FormBorderStyle = FormBorderStyle.FixedSingle;
                appsadd.Text = "Add Link";
                appsadd.Opacity = 0.96;
                appsadd.MinimizeBox = true;
                appsadd.MaximizeBox = false;
                appsadd.Show();
            }
            catch (Exception exapadd)
            {
            }
        }

        private void appsflemnu_Click(object sender, EventArgs e)
        {
            //applications in main file menu
            try
            {
                //apps();
            }
            catch (Exception appex)
            {
                Form appsflemnuexception = new Form();
                appsflemnuexception.FormBorderStyle = FormBorderStyle.FixedSingle;
                appsflemnuexception.BackColor = Color.Black;
                appsflemnuexception.StartPosition = FormStartPosition.CenterScreen;
                appsflemnuexception.ShowIcon = false;
                appsflemnuexception.Opacity = 0.96;
                appsflemnuexception.ShowInTaskbar = false;
                appsflemnuexception.Size = new Size(512, 256);
                appsflemnuexception.Show();
            }
        }

        private void cs_Click(object sender, EventArgs e)
        {
            tick = 0;
            bkc.Visible = true;
            tmsin.Start();
            expl = 4;
        }

        //??. Notify icon event handling ->

        private void rstrtntfy_Click(object sender, EventArgs e)
        {
            //restart in notify icon mnu
            Application.Restart();
        }

        private void clsntfy_Click(object sender, EventArgs e)
        {
            //close in notify icon menu
            amcletmer.Start();
        }

        private void Sobtn_ButtonClick(object sender, EventArgs e)
        {
            //SO btn in apps ts
            so();
        }

        private void somnu_Click(object sender, EventArgs e)
        {
            //SO in apps mnu
            so();
        }

        private void dsbl_Click(object sender, EventArgs e)
        {
            //disable in secutrity apps bttn

        }

        //Notify events -END-


        //Effects for pbx____________________________________________________________________________

        public void pbx_effects()
        {
            //698, 412
            for(int loc = 800; loc == 698; loc--)
            {
                pbxoatt.Location = new Point(loc, 412);
            }
        }

        //Effects for pbx -END-_______________________________________________________________________


        //Beauty stuff


        PictureBox pbxpre = new PictureBox();
        Button pbxvol = new Button();

        //Save password button
        //Application tool bar buttons
        //Open
        protected void opnhov(object ophovbttn, EventArgs opphovbttn)
        {
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.OPNbox;
            this.Controls.Add(pbxoatt);
        }
        protected void opnmnbttnext(object opnmnbttnext, EventArgs opnbbtnevan)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        //Readycare
        protected void rdehov(object rdhovbttn, EventArgs rddhovbttn)
        {
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.RDYbox;
            this.Controls.Add(pbxoatt);
        }
        protected void rdemnbttnext(object rdemnbttnext, EventArgs rdebbtnevan)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        //New
        protected void nwhov(object nwhovbttn, EventArgs nwwhovbttn)
        {
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.NWbox;
            this.Controls.Add(pbxoatt);
        }
        protected void nwemnbttnext(object newmnbttnext, EventArgs newbbtnevan)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        //Save
        protected void svehov(object svhovbttn, EventArgs svvhovbttn)
        {
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.SVEbox;
            this.Controls.Add(pbxoatt);
        }
        protected void svemnbttnext(object svemnbttnext, EventArgs svebbtnevan)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        protected void appstlshov(object apptulex, EventArgs lulapps)
        {
            //tester comment
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.MLbox;
            this.Controls.Add(pbxoatt);
        }
        protected void appsextext(object atulsf, EventArgs lulfapps)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        protected void exttlshov(object extuls, EventArgs extlsevan)
        {
            mnclse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.EXTbox;
            this.Controls.Add(pbxoatt);
        }
        protected void exttlsext(object extulsted, EventArgs exbeavan)
        {
            mnclse.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        protected void hmetlshov(object susu, EventArgs susevan)
        {

        }
        protected void hmetlsext(object susuf, EventArgs susefan)
        {
            oabttntls.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        protected void brwsetlshov(object subrssu, EventArgs subrsesevan)
        {
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.BRWSEbox;
            this.Controls.Add(pbxoatt);
        }
        protected void brwsetlsext(object subrsuf, EventArgs susbrrseefan)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        protected void dctlshov(object susudc, EventArgs susevdcan)
        {
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.DCbox;
            this.Controls.Add(pbxoatt);
        }
        protected void dctlsext(object sudcsuf, EventArgs sdcusefan)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        protected void Sotlshov(object soussu, EventArgs soousevan)
        {
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.SObox;
            this.Controls.Add(pbxoatt);
        }
        protected void Sotlsext(object susufso, EventArgs susefason)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        protected void busstlshov(object sbussusu, EventArgs susevanbus)
        {
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.BCbox;
            this.Controls.Add(pbxoatt);
        }
        protected void busstlsext(object susbusuf, EventArgs susbusefan)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }

        //private int oath = 0;
        private Control vis_temp; private int vis_int = 0;
        protected void oatlshov(object susuoa, EventArgs susevaoan)
        {
            oabtn.BackgroundImage = Properties.Resources.OAmnuon;
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.docentnew;

            this.Controls.Add(pbxoatt);
            vis_int = 0;
            vis_temp = (Control)oabtn;
            bigger.Start();
        }

        private void bigger_Tick(object sender, EventArgs e)
        {
            vis_int = vis_int + 1;
            vis_temp.Size = new Size(vis_temp.Size.Width + 2, vis_temp.Size.Width + 2);
            vis_temp.Location = new Point(vis_temp.Location.X - 1, vis_temp.Location.Y - 1);
            if (vis_int >= 2) { bigger.Stop(); }
        }

        protected void oatlsext(object suoasuf, EventArgs susoaefan)
        {
            bigger.Stop();
            oabtn.BackgroundImage = Properties.Resources.OAmnu;
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            oabtn.Size = new Size(115, 114);
            oabtn.Location = new Point(145, 92);
            this.Controls.Remove(pbxoatt);
        }

        protected void devtlshov(object susudev, EventArgs susevandev)
        {
            devbtn.BackgroundImage = Properties.Resources.DEVmnuon;
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.mgmtnew;
            this.Controls.Add(pbxoatt);
            vis_int = 0;
            vis_temp = (Control)devbtn;
            bigger.Start();
        }
        protected void devtlsext(object susufdev, EventArgs susefandev)
        {
            bigger.Stop();
            devbtn.BackgroundImage = Properties.Resources.DEVmnu;
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            devbtn.Size = new Size(115, 114);
            devbtn.Location = new Point(416, 92);
            this.Controls.Remove(pbxoatt);
        }
        protected void spetlshov(object sususpe, EventArgs susevanspe)
        {
            sep.BackgroundImage = Properties.Resources.SHPmnuon;
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.SHPbox;
            this.Controls.Add(pbxoatt);
            vis_int = 0;
            vis_temp = (Control)sep;
            bigger.Start();
        }
        protected void spetlsext(object susufspe, EventArgs susefanspe)
        {
            bigger.Stop();
            sep.BackgroundImage = Properties.Resources.SHPmnu;
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false; 
            sep.Size = new Size(115, 114);
            sep.Location = new Point(686, 267);
            this.Controls.Remove(pbxoatt);
        }
        protected void medtlshov(object susumed, EventArgs susevanmed)
        {
            mpbttn.BackgroundImage = Properties.Resources.SOdn;
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.amdb;
            this.Controls.Add(pbxoatt);
            vis_int = 0;
            vis_temp = (Control)mpbttn;
            bigger.Start();
        }
        protected void medtlsext(object susufmed, EventArgs susefanmed)
        {
            bigger.Stop();
            mpbttn.BackgroundImage = Properties.Resources.SO;
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            mpbttn.Size = new Size(115, 114);
            mpbttn.Location = new Point(416, 267);
            this.Controls.Remove(pbxoatt);
        }
        protected void libtlshov(object susulib, EventArgs susevanlib)
        {
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.LIBbox;
            this.Controls.Add(pbxoatt);
        }
        protected void libtlsext(object susuflib, EventArgs susefanlib)
        {
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            this.Controls.Remove(pbxoatt);
        }
        protected void cstlshov(object susucs, EventArgs susevancs)
        {
            cs.BackgroundImage = Properties.Resources.CSmnuon;
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.busnewbx;
            this.Controls.Add(pbxoatt);
            vis_int = 0;
            vis_temp = (Control)cs;
            bigger.Start();
        }
        protected void cstlsext(object susufcs, EventArgs susefancs)
        {
            bigger.Stop();
            cs.BackgroundImage = Properties.Resources.CSmnu;
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            cs.Size = new Size(115, 114);
            cs.Location = new Point(145, 267);
            this.Controls.Remove(pbxoatt);
        }
        protected void exptlshov(object snuzz, EventArgs porcelaiop)
        {
            sysbttn.BackgroundImage = Properties.Resources.SETTmnuon;
            pbxoatt.Enabled = true;
            pbxoatt.Visible = true;
            pbxoatt.BackgroundImage = Properties.Resources.accstd;
            this.Controls.Add(pbxoatt);
            vis_int = 0;
            vis_temp = (Control)sysbttn;
            bigger.Start();
        }
        protected void eptlsext(object snux, EventArgs traxsuzzfan)
        {
            bigger.Stop();
            sysbttn.BackgroundImage = Properties.Resources.SETTmnu;
            pbxoatt.Enabled = false;
            pbxoatt.Visible = false;
            sysbttn.Size = new Size(115, 114);
            sysbttn.Location = new Point(686, 92);
            this.Controls.Remove(pbxoatt);
        }



        protected void sveme(object svebm, EventArgs svevanp)
        {
            passbtn.BackgroundImage = Properties.Resources.btnsimp2;
        }
        protected void svemext(object svebmf, EventArgs svevfan)
        {
            passbtn.BackgroundImage = Properties.Resources.btnsim1;
        }

        private void devtlsdwn(object sender, MouseEventArgs e)
        {
            devbtn.BackgroundImage = Properties.Resources.DEVmnudwn;
        }

        private void devtlsup(object sender, MouseEventArgs e)
        {
            devbtn.BackgroundImage = Properties.Resources.DEVmnuon;
        }

        private void oatlsdwn(object sender, MouseEventArgs e)
        {
            oabtn.BackgroundImage = Properties.Resources.OAmnudwn;
        }

        private void oatlsup(object sender, MouseEventArgs e)
        {
            oabtn.BackgroundImage = Properties.Resources.OAmnuon;
        }

        private void shptlsdwn(object sender, MouseEventArgs e)
        {
            sep.BackgroundImage = Properties.Resources.SHPmnudwn;
        }

        private void shptlsup(object sender, MouseEventArgs e)
        {
            sep.BackgroundImage = Properties.Resources.SHPmnuon;
        }

        private void exptlsdwn(object sender, MouseEventArgs e)
        {
            sysbttn.BackgroundImage = Properties.Resources.SETTmnudwn;
        }

        private void exptlsup(object sender, MouseEventArgs e)
        {
            sysbttn.BackgroundImage = Properties.Resources.SETTmnuon;
        }

        private void medtlsdwn(object sender, MouseEventArgs e)
        {
            mpbttn.BackgroundImage = Properties.Resources.SOon;
        }

        private void medtlsup(object sender, MouseEventArgs e)
        {
            mpbttn.BackgroundImage = Properties.Resources.SOdn;
        }

        private void cstlsdwn(object sender, MouseEventArgs e)
        {
            cs.BackgroundImage = Properties.Resources.CSmnudwn;
        }

        private void cstlsup(object sender, MouseEventArgs e)
        {
            cs.BackgroundImage = Properties.Resources.CSmnuon;
        }

        private void shppebttntls_ButtonClick(object sender, EventArgs e)
        {
            spe();
        }

        //Opacity events
        //high

        private Thread thopcty;
        private delegate void delopcty();

        //all opacity setter
        private void thopctystrt()
        {
            thopcty = new Thread(new ThreadStart(delopctystrt));
            thopcty.Start();
        }

        private void delopctystrt()
        {
            this.Invoke(new delopcty(opctyset));
        }

        private void opctyset()
        {
            foreach (Form fs in Application.OpenForms)
            {
                if (fs.Name == "Virtual_window_manager") { }
                else
                {
                    fs.Opacity = Properties.Settings.Default.opacity;
                }
            }
            thopcty.Abort();
        }

        private void hvyopcty_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.opacity = 1.0;
            Properties.Settings.Default.Save();
            opctydoub = 1.0;
            thopctystrt();
        }
        //medium
        private void ninprcopcty_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.opacity = 0.96;
            Properties.Settings.Default.Save();
            opctydoub = 0.96;
            thopctystrt();
        }
        //light
        private void egtmnopcty_Click(object ope, EventArgs e)
        {
            Properties.Settings.Default.opacity = 0.80;
            Properties.Settings.Default.Save();
            opctydoub = 0.80;
            thopctystrt();
        }

        private void extvw_Click_1(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void mnvw_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.opacity = 1.0;
            this.Opacity = 1.0;
            Properties.Settings.Default.Save();
        }
        //Border menu no
        private void nowinbord_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.border = "no";
                Properties.Settings.Default.Save();
            }
            catch (Exception eborderx)
            {
            }
            this.FormBorderStyle = FormBorderStyle.None;
        }
        //Border menu yes
        private void normwinbord_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.border = "yes";
                Properties.Settings.Default.Save();
            }
            catch (Exception ebrdrxu)
            {
            }
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        //Topmost_____________________________________________________________________________________________________________________

        //Cross Thread Objects

        private Thread thtpy;
        private delegate void deltpy();

        private Thread thtpn;
        private delegate void deltpn();

        private void truetopmost_Click(object tm, EventArgs tmevan)
        {
            try
            {
                thtpystrt();
            }
            catch (Exception erty) { tpy(); }
        }

        private void thtpystrt()
        {
            try
            {
                thtpy = new Thread(new ThreadStart(deltpystrt));
                thtpy.Start();
            }
            catch (Exception erty) { tpy(); }
        }

        private void deltpystrt()
        {
            try
            {
                this.Invoke(new deltpy(tpy));
            }
            catch (Exception erty) { tpy(); }
        }

        private void tpy()
        {
            foreach (Form fs in Application.OpenForms)
            {
                fs.TopMost = true;
            }
            this.TopMost = true;
            choicesett.Default.tpmst = true;
            choicesett.Default.Save();
            thtpy.Abort();
        }

        private void falsetopmost_Click(object sender, EventArgs e)
        {
            try
            {
                thdtpnstrt();
            }
            catch (Exception erty) { tpn(); }
        }

        private void thdtpnstrt()
        {
            try
            {
                thtpn = new Thread(new ThreadStart(deltpnstrt));
                thtpn.Start();
            }
            catch (Exception erty) { tpn(); }
        }

        private void deltpnstrt()
        {
            try
            {
                this.Invoke(new deltpn(tpn));
            }
            catch (Exception erty) { tpn(); }
        }

        private void tpn()
        {
            foreach (Form fs in Application.OpenForms)
            {
                if (fs.Text == "Amatrix Settings" || fs.Text == "Amatrix Configuration" || fs.Text == "ReadyCare Error Reporter" || fs.Text == "Enter your password")
                {
                }
                else
                {
                    fs.TopMost = false;
                }
            }
            this.TopMost = false;
            choicesett.Default.tpmst = false;
            choicesett.Default.Save();
            thtpn.Abort();
        }

        //____________________________________________________________________________________________________________________________

        private void nrmlwsze_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1006, 600);
        }

        private void smlwinsze_Click(object sender, EventArgs e)
        {
            this.Size = new Size(600, 350);
        }

        private void rc_Click(object sender, EventArgs e)
        {
            //readycare taskbar bttn
        }

        private void errrep_Click(object sender, EventArgs e)
        {
            Form errorreport = new Form();
            errorreport.TopMost = true;
            errorreport.Text = "Readycare : Reporter";
            errorreport.BackgroundImage = Properties.Resources.backagegreyconv;
            errorreport.BackgroundImageLayout = ImageLayout.Stretch;
            errorreport.Size = new Size(800, 500);
            errorreport.StartPosition = FormStartPosition.CenterScreen;
            errorreport.ShowIcon = false;
            errorreport.ShowInTaskbar = false;
            errorreport.Show();
            this.Enabled = false;
            errorreport.Disposed += new EventHandler(errredycrepot);
        }

        void errredycrepot(object grrrrecr, EventArgs errevan)
        {
            this.Enabled = true;
        }

        private void amthm_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.backrnddim;
        }

        //About method {}
        private void abt_Click(object sender, EventArgs e)
        {
            Amatrix_About ablotshw = new Amatrix_About();
            ablotshw.Show();
        }

        private void secs1_Load(object sender, EventArgs e)
        {

        }

        private void sotlsbn_Click(object sender, EventArgs e)
        {
            so();
        }

        private void vwincnssm_Click(object sender, EventArgs e)
        {
            oabtn.Size = new Size(72, 72);
            mpbttn.Size = new Size(72, 72);
            sysbttn.Size = new Size(72, 72);
            devbtn.Size = new Size(72, 72);
            sep.Size = new Size(72, 72);
            cs.Size = new Size(72, 72);
        }

        private void vwicnslrge_Click(object sender, EventArgs e)
        {
            oabtn.Size = new Size(115, 123);
            mpbttn.Size = new Size(115, 123);
            sysbttn.Size = new Size(115, 123);
            devbtn.Size = new Size(115, 123);
            sep.Size = new Size(115, 123);
            cs.Size = new Size(115, 123);
        }
        protected void amsthtnow(object sretrertm, EventArgs yyuou)
        {
        this.WindowState = FormWindowState.Minimized;
        }

        private void hlpamcs_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
            }
        }

        private void clc_Click(object sender, EventArgs e)
        {
            Calculator_1 njm = new Calculator_1();
            try
            {
                njm.Show();
            }
            catch (Exception erty) { }
        }

        private void indxhlp_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.Show();
        }

        private void clsesrcitmbtn_Click(object sender, EventArgs e)
        {
            lbsrc.Visible = false; 
            lbsrc.Enabled = false;
        }

        private void bannon(object sender, EventArgs e)
        {
            appsmnustr.BackgroundImage = Properties.Resources.banner3blue;
        }
        private void bannoff(object snbnoff, EventArgs esnbnoff)
        {
            appsmnustr.BackgroundImage = Properties.Resources.banner2blue;
        }

        private void rdycrelnkflemnu_Click(object sender, EventArgs e)
        {
            readycre.readyinit rdystrt = new readycre.readyinit();
            rdystrt.readyreportbug();
        }
        private void wbbtn_Click(object sender, EventArgs e)
        {
            App_Workbook apwb = new App_Workbook();
            apwb.Show();
        }

        //Toolbars___________________________________________________________________________________________
        int vwmnuint = 1;
        private void viewmenu_Click(object sender, EventArgs e)
        {
            if (vwmnuint == 1)
            {
                viewmenu.Text = "View (Open)";
                bnnvwtab.Visible = true;
                vwmnuint = 2;
            }
            else if (vwmnuint == 2)
            {
                viewmenu.Text = "View";
                bnnvwtab.Visible = false;
                vwmnuint = 1;
            }
            else
            { }
        }
        int appsmnuint = 1;
        private void appsmenu_Click(object sender, EventArgs e)
        {
            if (appsmnuint == 1)
            {
                appsmenu.Text = "Applications (Open)";
                Appstabbnn.Visible = true;
                appsmnuint = 2;
            }
            else if (appsmnuint == 2)
            {
                appsmenu.Text = "Applications";
                Appstabbnn.Visible = false;
                appsmnuint = 1;
            }
            else 
            { }
        }

        private void tbrn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbrn.Text.ToLower() == "sec")
                {
                    Security_PWD scpwd = new Security_PWD();
                    scpwd.Show();
                }
                else if (tbrn.Text.ToLower() == "advertisments.no")
                {
                    Properties.Settings.Default.lnk = "";
                    Properties.Settings.Default.Save();
                }
                else if (tbrn.Text.ToLower() == "advertisments.yes")
                {
                    Properties.Settings.Default.lnk = "www.Addtk.tk";
                    Properties.Settings.Default.Save();
                }
                else if (tbrn.Text.ToLower() == "pos.setup")
                {
                    System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\Setup\\Setup.exe");
                }
                else
                {
                    Progstat.Text = "Started : " + tbrn.Text;
                    System.Diagnostics.Process.Start(tbrn.Text);
                }
            }
            catch (Exception ern)
            {
                Progstat.Text = "Program not found";
            }
        }
        int utilbanint = 1;
        private void utilsbnshw_Click(object sender, EventArgs e)
        {
            if (utilbanint == 1)
            {
                utilsbnshw.Text = "Utilities (Open)";
                utilsbann.Visible = true;
                utilbanint = 2;
            }
            else if (utilbanint == 2)
            {
                utilsbnshw.Text = "Utilities";
                utilsbann.Visible = false;
                utilbanint = 1;
            }
            else
            { }
        }

        private void utilsclsehov(object sender, EventArgs e)
        {
            utilclse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            utilclse.Text = "Close Banner";
        }

        private void utilsclseext(object sender, EventArgs e)
        {
            utilclse.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        int hlpbnint = 1;
        private void helpmenu_Click(object sender, EventArgs e)
        {
            if (hlpbnint == 1)
            {
                helpmenu.Text = "Help (Open)";
                hlpbann.Visible = true;
                hlpbnint = 2;
            }
            else if (hlpbnint == 2)
            {
                helpmenu.Text = "Help";
                hlpbann.Visible = false;
                hlpbnint = 1;
            }
            else { }
        }

        ToolStripButton tsb_temp;
        private void clsebtnhov(object sender, EventArgs e)
        {
            tsb_temp = (ToolStripButton)sender;
            tsb_temp.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            //clsehlp.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsb_temp.Text = "Close Banner";
        }

        private void clsebtnext(object sender, EventArgs e)
        {
            tsb_temp = (ToolStripButton)sender;
            tsb_temp.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void Appstabbnn_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void calenutil_Click(object sender, EventArgs e)
        {
            Calendar cal = new Calendar();
            cal.Show();
        }

        private void filemnutab_DoubleClick(object sender, EventArgs e)
        {
           
        }

        public static int alarmst =0;
        private void alarm()
        {
            try
            {
                if (Properties.Settings.Default.alarmdate.ToString() == DateTime.Now.Day.ToString()+"-"+DateTime.Now.Month.ToString()+"-"+DateTime.Now.Year.ToString() && alarmst != 1)
                {
                    if (Properties.Settings.Default.alarmtime == DateTime.Now.ToShortTimeString())
                    {
                        alarmst = 1;
                        MessageBox.Show("ALARM! FOR : " + Properties.Settings.Default.alarmreason, "ALARM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else {}
            }
            catch (Exception eal)
            {
            }
        }

        //Clock and closure
        public static int clsemnprocc;
        private void clcktmer_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                amnti.Visible = false;
            }
            else { amnti.Visible = true; }

            if (Properties.Settings.Default.hrfrmt == 0 || Properties.Settings.Default.hrfrmt == null || Properties.Settings.Default.hrfrmt == 1)
            {
                clock.Text = DateTime.Now.ToString();
            }
            else if (Properties.Settings.Default.hrfrmt == 2)
            {
                clock.Text = DateTime.Now.ToUniversalTime().ToString();
            }
            else if (Properties.Settings.Default.hrfrmt == 3)
            {
                clock.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            }
            else if (Properties.Settings.Default.hrfrmt == 4)
            {
                clock.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " - " + DateTime.Now.ToShortTimeString();
            }
            else if (Properties.Settings.Default.hrfrmt == 5)
            {
                clock.Text = DateTime.Now.ToLongDateString();
            }
            else if (Properties.Settings.Default.hrfrmt == 6)
            {
                clock.Text = DateTime.Now.ToLongTimeString();
            }
            utc_tme.Text = "UTC : " + DateTime.UtcNow.ToString();
            dayOfWeekToolStripMenuItem.Text = "Day of Week : " + DateTime.Now.DayOfWeek.ToString();
        }

        //int clockint = 0;
        private void clock_Click(object sender, EventArgs e)
        {
            cmsclck.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void mincount_Tick(object sender, EventArgs e)
        {
            if (alarmst == 0)
            {
                alarm();
            }
        }

        private void safwp_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ofd.FileName.ToString());
            Properties.Settings.Default.backimage = ofd.FileName.ToString();
            Properties.Settings.Default.Save();
        }

        private void resdef_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.backrnddim;
            Properties.Settings.Default.backimage = "Default";
            Properties.Settings.Default.Save();
        }

        private void stwlp_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            Properties.Settings.Default.backimglayout = "Stretch";
            Properties.Settings.Default.Save();
        }

        private void tlewp_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Tile;
            Properties.Settings.Default.backimglayout = "Tile";
            Properties.Settings.Default.Save();
        }

        private void cenwp_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Center;
            Properties.Settings.Default.backimglayout = "Center";
            Properties.Settings.Default.Save();
        }

        private void zoowp_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Zoom;
            Properties.Settings.Default.backimglayout = "Zoom";
            Properties.Settings.Default.Save();
        }

        private void noly_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.None;
            Properties.Settings.Default.backimglayout = "None";
            Properties.Settings.Default.Save();
        }
   
        private void opnam_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void oanw_Click(object sender, EventArgs e)
        {
            sveoa.ShowDialog();
        }

        private void sveoa_FileOk(object sender, CancelEventArgs e)
        {
            FileStream fsoa = new FileStream(sveoa.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (sveoa.FileName.EndsWith(".txt"))
            {
                App_Workbook apwb = new App_Workbook();
                string transport = sveoa.FileName.ToString();
                sveoa.Dispose();
                fsoa.Flush();
                fsoa.Close();
                apwb.openext(transport);
                this.WindowState = FormWindowState.Minimized;
            }
            else {}
        }

        private void clock_MouseHover(object sender, EventArgs e)
        {
            clock.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void clock_MouseLeave(object sender, EventArgs e)
        {
            clock.BackgroundImage = null;
        }

        private void clock_MouseDown(object sender, MouseEventArgs e)
        {
            clock.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void clock_MouseUp(object sender, MouseEventArgs e)
        {
            clock.BackgroundImageLayout = ImageLayout.Tile;
        }

        private void rmwwp_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = null;
        }

        private void trnsprt_Click(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.DarkOrange;
            this.BackColor = Color.DarkOrange;
        }

        private void amcletmer_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        //process killing
        private void Amatrix_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*Main.Amatrix.doc = "";
            Main.Amatrix.mgt = "";
            Main.Amatrix.acc = "";
            Main.Amatrix.AMs = "";
            Properties.Settings.Default.Save();*/

            try
            {
                DataTable dtp = new DataTable();
                basql.Execute(Main.Amatrix.AMs, "DELETE FROM AMS WHERE [IP] LIKE '%" + Properties.Settings.Default.IP + "%'", "AMS", dtp);
                dtp.Dispose();
            }
            catch (Exception erty) { }

            try
            {
                System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("RD_DIR");
                foreach (Process pr in p)
                {
                    try
                    {
                        pr.Kill();
                    }
                    catch (Exception erty) { }
                }
            }
            catch (Exception erty) { }

            try
            {
                foreach (Process pp in al_prc)
                {
                    try
                    {
                        pp.Kill();
                    }
                    catch (Exception erty) { }
                }
            }
            catch (Exception erty) { }
        }

        private void decttime_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                decttime.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        ToolStrip ts_temp;
        private void flemnent(object sender, EventArgs e)
        {
            ts_temp = (ToolStrip)sender;
            ts_temp.BackColor = Color.WhiteSmoke;
        }

        private void filemnuext(object sender, EventArgs e)
        {
            ts_temp = (ToolStrip)sender;
            ts_temp.BackColor = Color.White;
        }

        internal static void externfuck()
        {
            throw new NotImplementedException();
        }

        private void tbrnclc(object sender, EventArgs e)
        {
            tbrn.SelectAll();
        }

        private void lckamhve(object sender, EventArgs e)
        {
            lckam.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void lckamext(object sender, EventArgs e)
        {
            lckam.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void lckmn(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.lockpss.ToLower() == "none")
            {
                MessageBox.Show("No Password Was Created, Please Create A Password First.", "Amatrix Ready Care", MessageBoxButtons.OK, MessageBoxIcon.Information);
                First_use_optn nmi = new First_use_optn();
                nmi.Show();
            }
            else
            {
                Properties.Settings.Default.lockstat = "Locked";
                Properties.Settings.Default.Save();
                lck();
            }
        }
        private void lckmn2(object sender, EventArgs e)
        {
            pwrd wrd = new pwrd();
            wrd.Show();
            tmepw.Start();
        }

        private void lck()
        {
            object f = this.BackgroundImage;
            if (Properties.Settings.Default.lockstat == "Locked")
            {
                try
                {
                    foreach (Form sf in Application.OpenForms)
                    {
                        if (sf.Name == "Amatrix")
                        { }
                        else if (sf.Name == "Splash_Screen")
                        {
                        }
                        else
                        {
                            sf.Visible = false;
                        }
                    }
                }
                catch (Exception nope) { Am_err mer = new Am_err(); mer.tx(nope.Message); }
                lckglzz.Visible = true;
                appsmnustr.Visible = false;
                this.Text = "Amatrix : Locked";
                Properties.Settings.Default.lockstat = "Locked";
                Properties.Settings.Default.Save();

                //Disabled items 
                /*try
                {
                    DwmApi.MARGINS m = new DwmApi.MARGINS(1500, 1500, 1500, 1500);
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, m);
                }
                catch (Exception excaerno) { }*/
                this.BackgroundImage = null;

                foreach(Control cn in this.Controls)
                {
                    cn.Visible = false;
                }

                button1.Visible = true;
                lckglzz.Visible = true;
                this.Activate();
            }
            else if (Properties.Settings.Default.lockstat == "none")
            {
                try
                {
                    foreach (Form sf in Application.OpenForms)
                    {
                        if (sf.Name == "Amatrix" || sf.Name == "Amatrix_Server_Lite")
                        { }
                        else if (sf.Name == "Virtual_window_manager") { sf.Visible = true; }
                        else
                        {
                            sf.Visible = true;
                        }
                    }
                }
                catch (Exception nope) { Am_err mer = new Am_err(); mer.tx(nope.Message); }
                appsmnustr.Visible = true;
                lckglzz.Visible = false;
                Properties.Settings.Default.lockstat = "none";
                Properties.Settings.Default.Save();
                this.Text = "Amatrix";
                //backimage();

                //Enabled itms
                /*try
                {
                    DwmApi.MARGINS m = new DwmApi.MARGINS(0, 0, 0, 0);
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, m);
                }
                catch (Exception aernoo)
                {
                }
                this.TransparencyKey = Color.Empty;*/
                //this.BackColor = Color.Black;
                this.BackgroundImage = Properties.Resources.backrnddim;

                oabtn.Visible = true;
                devbtn.Visible = true;
                sep.Visible = true;
                cs.Visible = true;
                sysbttn.Visible = true;
                mpbttn.Visible = true;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;

                mnuamtx.Visible = true;
                mnclse.Enabled = true;
                oabtn.Enabled = true;
                devbtn.Enabled = true;
                button1.Visible = false;
                sep.Enabled = true;
                mpbttn.Enabled = true;
                cs.Enabled = true;
                sysbttn.Enabled = true;
                clock.Enabled = true;
                flemnu.Enabled = true;
                filemnutab.Enabled = true;
                oabttntls.Enabled = true;
                viewmenu.Enabled = true;
                appsmenu.Enabled = true;
                utilsbnshw.Enabled = true;
                helpmenu.Enabled = true;
                appmylnks.Enabled = true;
                bnnvwtab.Enabled = true;
                Appstabbnn.Enabled = true;
                utilsbann.Enabled = true;
                hlpbann.Enabled = true;
                this.Activate();
            }
            else { }
        }

        private void lckamclc(object sender, EventArgs e)
        {
            //lckmn();
        }
        
        private void thumpthump_Click(object sender, EventArgs e)
        {
            Thumbnail n = new Thumbnail();
            if (DwmApi.DwmIsCompositionEnabled())
            {
                n.CreateAndShow((IntPtr)(Amatrix.ActiveForm.Handle));
               // new Thumbnail().CreateAndShow((IntPtr)(Amatrix.ActiveForm.Handle));
            }
        }

        private void enblaer_Click(object sender, EventArgs e)
        {
                    try
                    {
                        bool dwm = true;
                        DwmApi.DwmEnableComposition(dwm);
                    }
                    catch (Exception excaerono)
                    {
                        Am_err amero = new Am_err();
                        amero.tx("Aero could not be Enabled on your Machine. It is already running or is not supported/enabled by your machine");
                    }
        }

        private void dsblaero_Click(object sender, EventArgs e)
        {
                    try
                    {
                        bool dwm = false;
                        DwmApi.DwmEnableComposition(dwm);
                    }
                    catch (Exception excaerono)
                    {
                        Am_err amero = new Am_err();
                        amero.tx("Aero could not be Disabled on your Machine.");
                    }
        }

        public void lckmnoff()
        {
            
        }

        private void tmepw_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.lockstat == "none")
            {
                tmepw.Stop();
                lck();
            }
            else { }
        }


        //MyLinks_________________________________________________________________________________________________________________________

        private void ntpappclc(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Notepad.exe");
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured While Opening Notepad."); ntpapp.Image = Properties.Resources.Console; }
        }

        private void expappsclc(object sender, EventArgs e)
        {
            try
            {
                string[] gtdrv = Environment.GetLogicalDrives();
                System.Diagnostics.Process.Start(gtdrv[0]);
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured While Opening Windows Explorer."); expapps.Image = Properties.Resources.Console; }
        }

        private void clcappclc(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("winword");
            }
            catch (Exception eryy) { Am_err ner = new Am_err(); ner.tx("An Error Occured While Opening Word."); clcapp.Image = Properties.Resources.Console; }
        }

        private void pptappclc(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("powerpnt");
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured While Opening PowerPoint."); pptapp.Image = Properties.Resources.Console; }
        }

        //Custom links


        private void mylnqexe_FileOk(object sender, CancelEventArgs e)
        {

        }

        //END -

        //MyLinks___________________________________________________________________________________________________________________________




        //Screen Snipper_________________________________________________________________________________________________________________-
        private System.Drawing.Image nutim;
        private void scrsnipclc(object sender, EventArgs e)
        {
            Scrn_snip scr = new Scrn_snip();
            scr.Show();
            //sfdsnip.ShowDialog();
        }

        void sfdsnip_Disposed(object sender, EventArgs e)
        {
        }

        private void sfdsnip_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                nutim.Save(sfdsnip.FileName);
            }
            catch (Exception excscrsveno)
            {
            }
        }

        private void apps(object sender, EventArgs e)
        {
            appmylnks.ShowDropDown();
        }

        private void amkeys(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '1')
            {
            try
            {
                Process.Start(Environment.CurrentDirectory + "\\ShareP\\Amatrix Document Server.exe");
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to open Share Point, the .exe is missing, Call maintenance for Check-up."); }
            }
            else if (e.KeyChar == '2')
            {
                tick = 0;
                bkc.Visible = true;
                tmsin.Start();
                expl = 2;
            }
            else if (e.KeyChar == '3')
            {
                tick = 0;
                bkc.Visible = true;
                tmsin.Start();
                expl = 3;
            }
            else if (e.KeyChar == '4')
            {
                tick = 0;
                bkc.Visible = true;
                tmsin.Start();
                expl = 4;
            }
            else if (e.KeyChar == '5')
            {
                AMDS ds = new AMDS();
                ds.Show();
            }
            else if (e.KeyChar == '6')
            {
                App_Shoppe shp = new App_Shoppe();
                shp.Show();
            }
        }

        //Splash_Screen spt = new Splash_Screen();

        public static int langcontrll = 0;
        private int cond = 0;
        private void bullshit_Tick(object sender, EventArgs e)
        {
            try
            {
                if (clsemnprocc == 1)
                {
                    this.Close();
                }
                else { }
            }
            catch (Exception closureerror)
            {
            }
        }

        private void calndr_Click(object sender, EventArgs e)
        {
            Calendar calcal = new Calendar();
            calcal.Show();
        }

        private void fudtetme_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.hrfrmt = 3;
            Properties.Settings.Default.Save();
        }

        private void shrttme_Click(object sender, EventArgs e)
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

        private void tmedef_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.hrfrmt = 0;
            Properties.Settings.Default.Save();
        }

        private delegate void itdel();
        private Thread thit;
        //default languages

        //IT
        private void setoit_Click(object sender, EventArgs e)
        {
        }

        private void thrdit()
        {
            thit = new Thread(new ThreadStart(initIT));
            thit.Start();
        }

        private void initIT()
        {
            try
            {
                this.Invoke(new itdel(itset));
            }
            catch (Exception etc) { }
        }

        private void itset()
        {
        }


        //EN
        private delegate void endelgt();
        private Thread then;

        private void settoen_Click(object sender, EventArgs e)
        {
        }

        private void enthd()
        {
            then = new Thread(new ThreadStart(endel));
            then.Start();
        }

        private void endel()
        {
            try
            {
                this.Invoke(new endelgt(eng));
            }
            catch (Exception eyt) { }
        }

        private void eng()
        {
        }

        private void lang_Tick(object sender, EventArgs e)
        {
            if (langcontrll == 1)
            {
                if (Properties.Settings.Default.lang == "IT")
                {
                    thrdit();
                    langcontrll = 0;
                }
                else if (Properties.Settings.Default.lang == "EN" || Properties.Settings.Default.lang == null || Properties.Settings.Default.lang == "")
                {
                    enthd();
                    langcontrll = 0;
                }
            }
        }

        private ContextMenuStrip cms_temp;
        private void Amcs_MouseEnter(object sender, EventArgs e)
        {
            cms_temp = (ContextMenuStrip)sender;
            cms_temp.Opacity = 0.95;
        }

        private void Amcs_MouseLeave(object sender, EventArgs e)
        {
            cms_temp = (ContextMenuStrip)sender;
            cms_temp.Opacity = 0.85;
        }

        private void sttg_Click(object sender, EventArgs e)
        {
            First_use_optn nmi = new First_use_optn();
            nmi.Show();
        }

        //_____________________________________________________________________________________________________________________________

        ToolStripItem tsi_tmp;
        private void dbste_MouseEnter(object sender, EventArgs e)
        {
            tsi_tmp = (ToolStripItem)sender;
            tsi_tmp.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void dbste_MouseLeave(object sender, EventArgs e)
        {
            tsi_tmp = (ToolStripItem)sender;
            tsi_tmp.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void tmeinit_Tick(object sender, EventArgs e)
        {
            tmeinit.Stop();
            try
            {
                bkk_init.RunWorkerAsync();
            }
            catch (Exception erty) { Inint(); }
        }

        int expl = 0;//0 = out; 123... = in
        int tick = 0;
        cnt_journ jrng = new cnt_journ();
        Button bkc = new Button();
        Button btninvc = new Button();
        private void bttncret()
        {
            jrng.Location = new Point(84, 57);
            jrng.Visible = false;
            jrng.Dock = DockStyle.Fill;
            this.Controls.Add(jrng);
        }

        //events
        private void booksclc(object sn, EventArgs e)
        {
            tick = 0;
            expl = 0;
            tmsin.Start();
            bkc.Visible = false;
        }

        private void bookshov(object sn, EventArgs e)
        {
            bkc.BackgroundImage = Properties.Resources.bckonacc;
        }

        private void booksovv(object sn, EventArgs e)
        {
            bkc.BackgroundImage = Properties.Resources.bckacc;
        }

        private void ledgclc(object sn, EventArgs e)
        {
            acc_ledg ledg = new acc_ledg();
            ledg.Show();
        }

        int notwice = 0;
        private void tmsin_Tick(object sender, EventArgs e)
        {
            if (tick == 0)
            {
                this.Opacity = this.Opacity - 0.05;
                if(this.Opacity <= 0.40)
                {
                    tick = 1;
                    oabtn.Visible = false;
                    sep.Visible = false;
                    devbtn.Visible = false;
                    cs.Visible = false;
                    mpbttn.Visible = false;
                    sysbttn.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    notwice = 0;
                }
            }
            if (tick == 1)
            {

                if (expl == 0 && notwice == 0)
                {
                    jrng.Visible = false;
                    bs.Visible = false;
                    mgmt.Visible = false;
                    oabtn.Visible = true;
                    sep.Visible = true;
                    devbtn.Visible = true;
                    cs.Visible = true;
                    mpbttn.Visible = true;
                    sysbttn.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    notwice = 1;
                }
                else if(expl == 3 && notwice == 0)
                {
                    jrng.Visible = true;
                    notwice = 1;
                }

                if (expl == 2 && notwice == 0)
                {
                    mgmt.Visible = true;
                    notwice = 1;
                }

                if (expl == 4 && notwice == 0)
                {
                    bs.Visible = true;
                    notwice = 1;
                }

                this.Opacity = this.Opacity + 0.05;
                if (this.Opacity == Properties.Settings.Default.opacity)
                {
                    tick = 0;
                    tmsin.Stop();
                }
            }
        }

        private void Add_lnk_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Show_Guides == true)
            {
                Guided_Function gf = new Guided_Function();
                gf.get_fle_strt();
                gf.Disposed += new EventHandler(gf_Disposed);
            }
            else { mylnqexe.ShowDialog(); }
        }

        public void setnew_lnk(string text, string link)
        {
            Add_lnk_gui(link, Properties.Settings.Default.app2, text);
            al.Add(text);
        }

        private void customBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gadg_custombook cst = new gadg_custombook();
            this.Controls.Add(cst);
            cst.BringToFront();
        }

        private void GC_work_Tick(object sender, EventArgs e)
        {
            try
            {
                GC.Collect();
                GC_work.Interval = Properties.Settings.Default.GACTIME;
            }
            catch (Exception erty) { rcas(); GC_work.Stop(); Am_err ner = new Am_err(); ner.tx("Amatrix Encountered a Fatal Problem you May Still use Amatrix although Amatrix may Remain Unstable. If you Wish to Continue Amatrix will Run its Fail-safe Default Memory Cleaner."); }
        }

        private void changeConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy_adv adv = new loggy_adv();
            adv.Show();
        }

        private void bkk2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (app_connection_settings.Default.Server_addr.ToLower() == "internal server")
            {
                SqlCeConnection cetest = new SqlCeConnection("Data Source = Amdtbse.sdf;");
                try
                {
                    cetest.Open();
                }
                catch (Exception ert)
                {
                    try
                    {
                        cetest.Close();
                    }
                    catch (Exception ent) { Am_err mer = new Am_err(); mer.tx("Amatrix was unable to close a test connection this may create problems during database access."); }
                }
            }
            else
            {
                SqlConnection cetest = new SqlConnection();
                SqlCeConnection ceetest = new SqlCeConnection();
                try
                {
                    if (File.Exists(app_connection_settings.Default.Db_name) == false)
                    {
                        cetest = new SqlConnection("Data Source = " + app_connection_settings.Default.Server_addr + "; Database=" + app_connection_settings.Default.Db_name + "; uid=" + app_connection_settings.Default.user_name + "; password=" + app_connection_settings.Default.password);
                        cetest.Open();
                    }
                    else
                    {
                        try
                        {
                            ceetest = new SqlCeConnection("Data Source = " + app_connection_settings.Default.Db_name);
                            ceetest.Open();
                        }
                        catch (Exception ert)
                        {
                            try
                            {
                                ceetest = new SqlCeConnection("Data Source = " + app_connection_settings.Default.Db_name + "; uid=" + app_connection_settings.Default.user_name + "; password=" + app_connection_settings.Default.password);
                               
                                ceetest.Open();
                            }
                            catch (Exception erty) { }
                        }
                    }
                }
                catch (Exception ery) { Am_err ner = new Am_err(); ner.tx("Amatrix was Unable to Connect to the Server '" + app_connection_settings.Default.Server_addr + "'"); }

            }
        }

        DataTable dt_temp;
        private void bkk_sec_DoWork(object sender, DoWorkEventArgs e)
        {
            /*try
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Sec_AMConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT [Is Registered] FROM Settings", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dt_temp = new DataTable();
                dt_temp.Load(dr);
                conn.Close();
            }
            catch (Exception ERTY) { Application.Exit(); }*/
        }

        private void bkk_sec_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           /* if (dt_temp.Columns[0].Table.Rows[0].ItemArray[0].ToString() == "false")
            {
                license l = new license();
                l.Show();
            }
            else
            {
                Scrsve sv = new Scrsve(); sv.Show();
            }
            try
            {
                dt_temp.Dispose();
            }
            catch (Exception erty) { }*/
        }

        private void rdy_c_opn(object sender, EventArgs e)
        {
            Ready_Care_Control cnt = new Ready_Care_Control();
            cnt.Show();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.astreous.tk");
        }

        private void employeeAndCompanyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            First_use fu = new First_use();
            fu.Show();
        }

        void dslk_FormClosed(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void tpmstmnuitm_Click(object sender, EventArgs e)
        {
            cols col = new cols();
            col.Show();
        }

        Ky_A.ASE_Key_Wr a = new Ky_A.ASE_Key_Wr();
        private void OpenFle_Tick(object sender, EventArgs e)
        {
            //DEPRECIATED
            /*OpenFle.Stop();
            string[] s = Environment.GetCommandLineArgs();
            if (s.Length > 1)
            {
                try
                {
                    Doc_stdio std = new Doc_stdio();
                    a.Read(s[1]);
                    std.open_out(s[1], false);
                    std.Show();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An error Occured While Opening the Document"); }
            }*/
        }

        private void maximizedViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.backimage = "Default";
            Properties.Settings.Default.opacity = 0.96;
            this.BackgroundImage = Properties.Resources.backrnddim;
            this.Opacity = 0.96;
            Properties.Settings.Default.Save();
        }

        private void amatrixMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eml_send snd = new eml_send();
            snd.Show();
        }

        private void microsoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("EXCEL.exe");
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured While Opening Excel"); microsoftExcelToolStripMenuItem.Image = Properties.Resources.Console; }
        }

        private void clf_Tick(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Reports prts = new Reports();
            prts.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Inventory_Searcher is_ = new Inventory_Searcher();
            is_.Show();
        }

        private void grph_Click(object sender, EventArgs e)
        {
            gadg_grph grph = new gadg_grph();
            grph.in_form();
        }

        private void sIPCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\Apps\\2bit.exe");
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Amatrix Could Not Find Your IM Messenger's Executable '2bit.exe', Please Contact Astreous to Fix this Problem."); sIPCallToolStripMenuItem.Image = Properties.Resources.Console; }
        }

        private void contactsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contacts cnt = new Contacts(); 
            cnt.tx(false);
            cnt.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reparttn tnn = new reparttn();
            tnn.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tbrn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EventArgs ev = new EventArgs();
                tbrn_TextChanged(tbrn, ev);
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Calendar_alrm alm = new Calendar_alrm();
            alm.Show();
        }

        private void openDocumentCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Doc_stdio ds = new Doc_stdio();
            ds.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }
    }
    //  -CLASS END-
}

//Copyright (c) 2009-2012 Astreous Technologies
//[ASL] Astreous Software License 2012 V1.0