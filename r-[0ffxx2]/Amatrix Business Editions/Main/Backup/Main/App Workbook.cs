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
using System.IO;
using Microsoft;
using System.DirectoryServices;
using System.Linq;
using System.Text;
//using clsam.dll;
using System.Windows.Forms;

namespace Main
{
    public partial class App_Workbook : Form
    {
        string addressemptyornot;
        int entriesn;
        string address;
        public App_Workbook()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.Opacity = 0.96;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += new FormClosingEventHandler(App_Workbook_FormClosing);
            this.Disposed += new EventHandler(App_Workbook_Disposed);
            InitializeComponent();
            cntxtwb.AllowTransparency = true;
            cntxtwb.Opacity = 0.90;
            INIt();
            svewbfle.AddExtension = true;
        }

        void App_Workbook_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changed == true)
            {
                e.Cancel = true; clesefct.Stop();
                if (DialogResult.Yes == MessageBox.Show("Save Changes to the Current Document?", "Work Book", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    EventArgs ew = new EventArgs();
                    svewb_Click(this, ew);
                }
                else
                {
                    changed = false;
                    this.Close();
                }
            }
            else
            {
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

        void App_Workbook_Disposed(object sender, EventArgs e)
        {
            redoToolStripMenuItem.Click -= redoToolStripMenuItem_Click;
            undoToolStripMenuItem.Click -= undoToolStripMenuItem_Click;
            toolStripButton3.Click -= printToolStripMenuItem_Click;
            printPreviewToolStripMenuItem.Click -= printPreviewToolStripMenuItem_Click;
            pageSetupToolStripMenuItem.Click -= pageSetupToolStripMenuItem_Click;
            printToolStripMenuItem.Click -= printToolStripMenuItem_Click;
            printDocument1.BeginPrint -= printDocument1_BeginPrint;
            printDocument1.PrintPage -= printDocument1_PrintPage;
            replaceNowToolStripMenuItem.Click -= replaceAllToolStripMenuItem_Click;
            toolStripButton2.ButtonClick -= toolStripButton2_Click;
            this.FormClosing -= App_Workbook_FormClosing;
            this.newToolStripMenuItem.Click -= this.nwe_Click;
            this.openToolStripMenuItem.Click -= this.opnbttn_Click;
            this.saveToolStripMenuItem.Click -= this.svewb_Click;
            this.svas.Click -= this.svas_Click;
            this.restartToolStripMenuItem.Click -= this.rts_Click;
            this.clsewb.Click -= this.clsewb_Click;
            this.nrmfnt.Click -= this.normtext_Click;
            this.bldtxt.Click -= this.bldtxt_Click;
            this.italicsToolStripMenuItem.Click -= this.italics_Click;
            this.underlinedToolStripMenuItem.Click -= this.undrlne_Click;
            this.selcapll.Click -= this.selcapllclc;
            this.capall.Click -= this.capall_Click;
            this.capno.Click -= this.capno_Click;
            this.ysrgt.Click -= this.ysrgtclc;
            this.ninetrte.Click -= this.ninetrteclc;
            this.slecttxtall.Click -= this.slecttxtall_Click;
            this.cpy.Click -= this.cpy_Click;
            this.pstewb.Click -= this.pstewb_Click;
            this.zmmnu.Click -= this.zmwbrt_Click;
            this.zmootmnu.Click -= this.zmootmnu_Click;
            this.topicsToolStripMenuItem.Click -= this.topicsToolStripMenuItem_Click;
            this.aboutToolStripMenuItem.Click -= this.aboutToolStripMenuItem_Click;
            this.toolStripMenuItem1.Click -= this.blkfnt_Click;
            this.toolStripMenuItem2.Click -= this.gryfnt_Click;
            this.toolStripMenuItem3.Click -= this.wht_Click;
            this.toolStripMenuItem4.Click -= this.rdfnt_Click;
            this.toolStripMenuItem5.Click -= this.blefnt_Click;
            this.toolStripMenuItem6.Click -= this.grnfnt_Click;
            this.toolStripMenuItem7.Click -= this.yellarfnt_Click;
            this.toolStripMenuItem8.Click -= this.pcfn_Click;
            this.curspoc.Click -= this.curspoc_Click;
            this.cntxtwb.Opening -= this.cntxtwb_Opening;
            this.copyToolStripMenuItem1.Click -= this.cpy_Click;
            this.pstecntxmnu.Click -= this.pstewb_Click;
            this.selectAllToolStripMenuItem.Click -= this.slecttxtall_Click;
            this.svewbfle.FileOk -= this.svewbfle_FileOk;
            this.wpdtme.Tick -= this.wpdtme_Tick;
            this.opn.FileOk -= this.opn_FileOk;
            this.clesefct.Tick -= this.clesefct_Tick;
            this.fdwb.Apply -= this.italics_Click;
            this.pbxttpleave.Tick -= this.pbxttpleave_Tick;
            this.dectmewb.Tick -= this.dectmewbtck;
            this.enbl.Tick -= this.enbltc;
            this.nwe.Click -= this.nwe_Click;
            this.opnbttn.Click -= this.opnbttn_Click;
            this.svewb.Click -= this.svewb_Click;
            this.clsemn.MouseLeave -= this.clsemn_MouseLeave;
            this.clsemn.ButtonClick -= this.clsewb_Click;
            this.clsemn.MouseEnter -= this.clsemn_MouseEnter;
            this.rts.Click -= this.rts_Click;
            this.zmmre.Click -= this.zmwbrt_Click;
            this.toolStripButton5.Click -= this.zmootmnu_Click;
            this.rtlft.Click -= this.rtlft_Click;
            this.toolStripButton1.Click -= this.slecttxtall_Click;
            this.toolStripButton7.Click -= this.capall_Click;
            this.toolStripButton8.Click -= this.capno_Click;
            this.tscbfnt.TextChanged -= this.tscbfnt_TextChanged;
            this.tscbfnt.Click -= this.tscbfnt_Click;
            this.szetxt.TextChanged -= this.szetxt_TextChanged;
            this.blkfnt.Click -= this.blkfnt_Click;
            this.gryfnt.Click -= this.gryfnt_Click;
            this.wht.Click -= this.wht_Click;
            this.rdfnt.Click -= this.rdfnt_Click;
            this.blefnt.Click -= this.blefnt_Click;
            this.grnfnt.Click -= this.grnfnt_Click;
            this.yellarfnt.Click -= this.yellarfnt_Click;
            this.tbfnt.Scroll -= this.tbfnt_Scroll;
            this.rss.ItemClicked -= this.rss_ItemClicked;
            this.tbxwbx.KeyDown -= this.tbxwbx_KeyDown;
            this.tbxwbx.TextChanged -= this.tbxwbx_TextChanged;
            this.Deactivate -= this.Appwbdec;
            this.Load -= this.App_Workbook_Load;
            this.Activated -= this.Appwbact;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void INIt()
        {
            tbxwbx.Select();
            rss.Visible = false;
            cntxtwb.AllowTransparency = true;
            cntxtwb.Opacity = 0.90;

            if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }
        }

        bool changed = false;
        private void tbxwbx_TextChanged(object sender, EventArgs e)
        {
            changed = true;
            lns.Text = tbxwbx.Lines.LongLength.ToString();
            //_______________________________________________________
            entriesn++;
            nentries.Text = entriesn.ToString();
            txtlngth.Text = tbxwbx.Text.Length.ToString();
        }

        public void openext(string fleget)
        {
            openext__(fleget);
        }

        private void openext__(string readfle)
        {
            try
            {
                address = readfle;
                addressemptyornot = readfle;
                FileStream fswbextopn = new FileStream(readfle, FileMode.Open, FileAccess.Read);
                StreamReader srextopn = new StreamReader(fswbextopn);
                string sendtortf = srextopn.ReadToEnd();
                fswbextopn.Flush();
                srextopn.Close();
                fswbextopn.Close();
                this.Text = "Amatrix Work Book : " + readfle;

                this.Show();
                tbxwbx.Text = sendtortf;
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
        }

        private void clsewb_Click(object sender, EventArgs e)
        {
            clesefct.Interval = Properties.Settings.Default.Frmrtem;
            clesefct.Start();
        }

        private void capall_Click(object sender, EventArgs e)
        {
            tbxwbx.Text = tbxwbx.Text.ToUpper();
        }

        private void capno_Click(object sender, EventArgs e)
        {
            tbxwbx.Text = tbxwbx.Text.ToLower();
        }

        private void slecttxtall_Click(object sender, EventArgs e)
        {
            tbxwbx.SelectAll();
        }

        private void bldtxt_Click(object sender, EventArgs e)
        {
            int szetit = Convert.ToInt32(szetxt.Text);
            float fteit = (float)szetit;
            using (Font fnt = new Font(tscbfnt.Text, fteit, FontStyle.Bold))
            {
                tbxwbx.SelectionFont = fnt;
            }
        }

        private void pstewb_Click(object sender, EventArgs e)
        {
            tbxwbx.Paste();
        }

        private void App_Workbook_Load(object sender, EventArgs e)
        {
            this.Text = "Amatrix : Work Book (New Page)";
        }
        int zoom = 2;
        private void zmwbrt_Click(object sender, EventArgs e)
        {
            if (zoom == 20)
            {
                zmmnu.Text = "Zoom In (Unavailable)";

            }
            else
            {
                zmmnu.Text = "Zoom In";
                tbxwbx.ZoomFactor = zoom++;
                tbfnt.Value = zoom;
            }
            //zmtell.Text = zoom.ToString();
        }

        private void zmootmnu_Click(object sender, EventArgs e)
        {
            if (zoom == 1)
            {
                zmootmnu.Text = "Zoom Out (Unavailable)";
            }
            else
            {
                zmootmnu.Text = "Zoom Out";
                zoom--;
                tbxwbx.ZoomFactor = zoom;
                tbfnt.Value = zoom;
            }
            //zmtell.Text = zoom.ToString();
        }
        int rtol;
        private void rtlft_Click(object sender, EventArgs e)
        {
            if (rtol == 1)
            {
                tbxwbx.RightToLeft = RightToLeft.Yes;
                rtol = 2;
            }
            else if (rtol == 2)
            {
                tbxwbx.RightToLeft = RightToLeft.No;
                rtol = 1;
            }
            else
            {
                rtol = 1;
            }
            zoom = 2;
        }


        // beauty____________________________________________________________________________________
        private void mnlblhov(object sender, EventArgs e)
        {
            mnmnutls.Text = "Main : Modify this menu";
        }

        private void mnlblext(object sender, EventArgs e)
        {
            mnmnutls.Text = "Main";
        }

        private void vwhov(object sender, EventArgs e)
        {
            vwslbl.Text = "View : Modify this menu";
        }

        private void vwext(object sender, EventArgs e)
        {
            vwslbl.Text = "View";
        }

        private void edtlhov(object sender, EventArgs e)
        {
            edtlbl.Text = "Edit : Modify this menu";
        }

        private void edtext(object sender, EventArgs e)
        {
            edtlbl.Text = "Edit";
        }

        private void clsemntl(object sender, EventArgs e)
        {
            clsemn.Text = "Close";
            clsemn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clsemntlext(object sender, EventArgs e)
        {
            clsemn.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void txtszedrdwn_Click(object sender, EventArgs e)
        {

        }

        private void txtszedrdwn_TextChanged(object sender, EventArgs e)
        {
        }

        private void zmtell_Click(object sender, EventArgs e)
        {
            //zmtell.Text = zoom.ToString();
        }

        private void txthov(object sender, EventArgs e)
        {
            txtlbl.Text = "Text : Modify this menu";
        }

        private void txtext(object sender, EventArgs e)
        {
            txtlbl.Text = "Text";
        }

        private void opnbttn_Click(object sender, EventArgs e)
        {
            opn.Title = "Open a File";
            opn.ShowDialog();
        }

        private void cpy_Click(object sender, EventArgs e)
        {
            tbxwbx.Copy();
        }

        private void wpdtme_Tick(object sender, EventArgs e)
        {
            curspoc.Text = Cursor.Position.X.ToString() + "," + Cursor.Position.Y.ToString();
        }

        int shwdia = 0;
        private void svewb_Click(object sender, EventArgs e)
        {
            savenow();
            changed = false;
        }

        private void savenow()
        {
            try
            {
                FileStream fswwb = new FileStream(address, FileMode.Open, FileAccess.Write);
                StreamWriter swwb = new StreamWriter(fswwb);
                swwb.Write(tbxwbx.Text.ToString());
                swwb.Flush();
                swwb.Close();
            }
            catch (Exception ntsved)
            {
                svewbfle.ShowDialog();
                shwdia = 1;
            }
        }

        private void svewbfle_FileOk(object sender, CancelEventArgs e)
        {
            this.Text = "Amatrix Work Book [" + svewbfle.FileName + "]";
            addressemptyornot = svewbfle.FileName.ToString();
            address = svewbfle.FileName.ToString();
            FileStream fssvewb = new FileStream(svewbfle.FileName, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter swwbas = new StreamWriter(fssvewb);
            swwbas.Write(tbxwbx.Text.ToString());
            swwbas.Flush();
            fssvewb.Flush();
            swwbas.Close();
            fssvewb.Close();
        }

        private void opn_FileOk(object sender, CancelEventArgs e)
        {
            this.Text = "Amatrix Work Book [" + opn.FileName + "]";
            addressemptyornot = null;
            address = opn.FileName.ToString();
            FileStream fsopn = new FileStream(address, FileMode.Open, FileAccess.Read);
            StreamReader swb = new StreamReader(fsopn);
            string sread = swb.ReadToEnd();
            tbxwbx.Text = sread;

            fsopn.Flush();
            swb.Close();
            fsopn.Close();
        }

        private void svas_Click(object sender, EventArgs e)
        {
            svewbfle.ShowDialog();
        }

        private void ttp(int togo)
        {
        }

        private void zse(int toget)
        {
        }

        private void nwe_Click(object sender, EventArgs e)
        {
            App_Workbook apwbr = new App_Workbook();
            apwbr.StartPosition = FormStartPosition.Manual;
            apwbr.Location = new Point(this.Location.X + 20, this.Location.Y + 20);
            apwbr.Show();
        }

        private void clesefct_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }
        private void italics_Click(object sender, EventArgs e)
        {
            int szetit = Convert.ToInt32(szetxt.Text);
            float fteit = (float)szetit;
            using (Font fnt = new Font(tscbfnt.Text, fteit, FontStyle.Italic))
            {
                tbxwbx.SelectionFont = fnt;
            }
        }

        private void normtext_Click(object sender, EventArgs e)
        {
            int szet = Convert.ToInt32(szetxt.Text);
            float fte = (float)szet;
            using (Font fnt = new Font(tscbfnt.Text, fte, FontStyle.Regular))
            {
                tbxwbx.SelectionFont = fnt;
            }
        }

        private ToolStripComboBox cbx_temp;
        private void szetxt_TextChanged(object sender, EventArgs e)
        {
            cbx_temp = (ToolStripComboBox)sender;
            try
            {
                int szet = Convert.ToInt32(cbx_temp.Text);
                float fte = (float)szet;
                using (Font fnt = new Font(tscbfnt.Text, fte))
                {
                    tbxwbx.SelectionFont = fnt;
                }
            }
            catch (Exception expszeint)
            {

            }
        }

        private void brwn_Click(object sender, EventArgs e)
        {
            int szet = Convert.ToInt32(szetxt.Text);
            float fte = (float)szet;
            using (Font fnt = new Font(tscbfnt.Text, fte))
            {
                tbxwbx.Font = fnt;
            }
        }

        private void nwe_MouseLeave(object sender, EventArgs e)
        {
            pbxttpleave.Start();
        }

        private void pbxttpleave_Tick(object sender, EventArgs e)
        {
            pbxttpleave.Stop();
        }

        private void pbxttp_MouseEnter(object sender, EventArgs e)
        {
            pbxttpleave.Stop();
        }

        private void tscbfnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int szet = Convert.ToInt32(szetxt.Text);
                float fte = (float)szet;
                using (Font fnt = new Font(tscbfnt.Text, fte))
                {
                    tbxwbx.SelectionFont = fnt;
                }
            }
            catch (Exception fntszech)
            {
                Am_err amrfnt = new Am_err();
                amrfnt.tx("Work Book was unable to find the required font.");
            }
        }

        private void pcfn_Click(object sender, EventArgs e)
        {
            clfnt.ShowDialog();
        }

        private void yellarfnt_Click(object sender, EventArgs e)
        {
            tbxwbx.SelectionColor = Color.Yellow;
            tbxwbx.BackColor = Color.Gainsboro;
        }

        private void grnfnt_Click(object sender, EventArgs e)
        {
            tbxwbx.SelectionColor = Color.Green;
            tbxwbx.BackColor = Color.Gainsboro;
        }

        private void blefnt_Click(object sender, EventArgs e)
        {
            tbxwbx.SelectionColor = Color.Blue;
            tbxwbx.BackColor = Color.Gainsboro;
        }

        private void rdfnt_Click(object sender, EventArgs e)
        {
            tbxwbx.SelectionColor = Color.Red;
            tbxwbx.BackColor = Color.Gainsboro;
        }

        private void wht_Click(object sender, EventArgs e)
        {
            tbxwbx.SelectionColor = Color.White;
            tbxwbx.BackColor = Color.DimGray;
        }

        private void gryfnt_Click(object sender, EventArgs e)
        {
            tbxwbx.SelectionColor = Color.Gray;
            tbxwbx.BackColor = Color.Gainsboro;
        }

        private void blkfnt_Click(object sender, EventArgs e)
        {
            tbxwbx.SelectionColor = Color.Black;
            tbxwbx.BackColor = Color.Gainsboro;
        }

        private void bld_Click(object sender, EventArgs e)
        {
            int szetit = Convert.ToInt32(szetxt.Text);
            float fteit = (float)szetit;
            using (Font fnt = new Font(tscbfnt.Text, fteit, FontStyle.Bold))
            {
                tbxwbx.SelectionFont = fnt;
            }
        }

        private void undrlne_Click(object sender, EventArgs e)
        {
            int szetit = Convert.ToInt32(szetxt.Text);
            float fteit = (float)szetit;
            using (Font fnt = new Font(tscbfnt.Text, fteit, FontStyle.Underline))
            {
                tbxwbx.SelectionFont = fnt;
            }
        }

        private void strkoot_Click(object sender, EventArgs e)
        {
            int szetit = Convert.ToInt32(szetxt.Text);
            float fteit = (float)szetit;
            using (Font fnt = new Font(tscbfnt.Text, fteit, FontStyle.Strikeout))
            {
                tbxwbx.SelectionFont = fnt;
            }
        }

        private void dectmewbtck(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmewb.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void Appwbact(object sender, EventArgs e)
        {
            try
            {
                dectmewb.Stop();
            }
            catch (Exception excprevdec)
            {
            }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Appwbdec(object sender, EventArgs e)
        {
            dectmewb.Start();
        }

        int getr = 0;
        private void fstot_Click(object sender, EventArgs e)
        {
            string srt = tbxwbx.Text;
            foreach (char ser in srt)
            {
                getr++;
                if (ser == ' ')
                {
                    try
                    {
                        char ty = srt[getr];
                    }
                    catch (Exception erty) { }
                }
                else { }
            }
        }

        private void vwdets_Click(object sender, EventArgs e)
        {
            wbninfort();
        }

        private void wbninfort()
        {
            if (Properties.Settings.Default.intwbinfo == 0)
            {
                stwb.Visible = true;
                Properties.Settings.Default.intwbinfo = 1;
            }
            else if (Properties.Settings.Default.intwbinfo == 1)
            {
                stwb.Visible = false;
                Properties.Settings.Default.intwbinfo = 0;
            }
            else
            {
                Properties.Settings.Default.intwbinfo = 0;
            }
        }

        private void selcapllclc(object sender, EventArgs e)
        {
            tbxwbx.SelectedText = tbxwbx.SelectedText.ToUpper();
        }

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ysrgtclc(object sender, EventArgs e)
        {
            Form warnrght = new Form();
            warnrght.StartPosition = FormStartPosition.CenterScreen;
            warnrght.FormBorderStyle = FormBorderStyle.FixedSingle;
            warnrght.Opacity = 0.96;
            warnrght.Text = "Warning";
            warnrght.MinimizeBox = false;
            warnrght.MaximizeBox = false;
            warnrght.Size = new Size(250, 100);

            Button bttok = new Button();
            bttok.Text = "Ok";
            bttok.Location = new Point(100, 50);
            bttok.FlatStyle = FlatStyle.System;
            bttok.Click += new EventHandler(bttokclc);

            warnrght.Controls.Add(bttok);
            warnrght.Show();
        }

        void bttokclc(object sender, EventArgs e)
        {
            try
            {
                tbxwbx.RightToLeft = RightToLeft.Yes;
            }
            catch (Exception excrtlft)
            {
                Am_err aerrtlft = new Am_err();
                aerrtlft.tx(excrtlft.ToString() + "An error has occured.");
            }
        }

        private void ninetrteclc(object sender, EventArgs e)
        {
            tbxwbx.RightToLeft = RightToLeft.No;
        }

        private void cntxtwb_Opening(object sender, CancelEventArgs e)
        {
        }

        private void pnlettedc(object sender, EventArgs e)
        {
        }

        private void enbltc(object sender, EventArgs e)
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


        private void fullscreenclc(object sender, EventArgs e)
        {
        }

        private void curspoc_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tscbfnt_Click(object sender, EventArgs e)
        {

        }

        private float fn;
        private void tbfnt_Scroll(object sender, EventArgs e)
        {
            tbxwbx.ZoomFactor = tbfnt.Value;
            /*Color mn = new Color();
            mn = tbxwbx.ForeColor;
            fn = (float)tbfnt.Value;
            using(Font fni = new Font(tbxwbx.Font.FontFamily.GetName(1), fn, FontStyle.Regular))
            {
                tbxwbx.ForeColor = mn;
                tbxwbx.Font = fni;
            }*/
        }

        private void tbxwbx_DoubleClick(object sender, EventArgs e)
        {

        }

        private void tbxwbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.PageUp && e.KeyCode != Keys.PageDown)
            {
                rss.Items.Clear();
                rss.Items.Add("Press PgUp or PgDn To Activate");
                rss.Items[0].Enabled = false;
            }
            else
            { /*rss.BackColor = Color.Black;*/ }

            if (e.KeyCode == Keys.A)
            {
                rss.Visible = true;
                rss.Items.Add("nd");
                rss.Items.Add("re");
                rss.Items.Add("t");
                rss.Items.Add("m");
                rss.Items.Add("fter");
                rss.Items.Add("ble");
            }
            else if (e.KeyCode == Keys.B)
            {
                rss.Visible = true;
                rss.Items.Add("ut");
                rss.Items.Add("etween");
                rss.Items.Add("y");
            }
            else if (e.KeyCode == Keys.C)
            {
                rss.Visible = true;
                rss.Items.Add("an");
                rss.Items.Add("an't");
            }
            else if (e.KeyCode == Keys.D)
            {
                rss.Visible = true;
                rss.Items.Add("o");
                rss.Items.Add("on't");
            }
            else if (e.KeyCode == Keys.O)
            {
                rss.Visible = true;
                rss.Items.Add("r");
                rss.Items.Add("ur");
                rss.Items.Add("n");
                rss.Items.Add("kay");
            }
            else if (e.KeyCode == Keys.I)
            {
                rss.Visible = true;
                rss.Items.Add("n");
            }
            else if (e.KeyCode == Keys.H)
            {
                rss.Visible = true;
                rss.Items.Add("im");
                rss.Items.Add("ello");
            }
            else if (e.KeyCode == Keys.E)
            {
                rss.Visible = true;
                rss.Items.Add("No Suggestions Available");
            }
            else if (e.KeyCode == Keys.J)
            {
                rss.Visible = true;
                rss.Items.Add("avascript");
            }
            else if (e.KeyCode == Keys.T)
            {
                rss.Visible = true;
                rss.Items.Add("he");
                rss.Items.Add("hen");
                rss.Items.Add("han");
                rss.Items.Add("here");
                rss.Items.Add("o");
                rss.Items.Add("hough");
            }
            else if (e.KeyCode == Keys.Alt)
            {
                rss.Visible = true;
                rss.Items.Add("No Suggestions Available");
            }
            else if (e.KeyCode == Keys.ControlKey)
            {
                rss.Visible = true;
                rss.Items.Add(".");
                rss.Items.Add(",");
                rss.Items.Add(";");
                rss.Items.Add(":");
                rss.Items.Add("`");
                rss.Items.Add("?");
                rss.Items.Add("!");
                rss.Items.Add("\"\"");
                rss.Items.Add("''");
                rss.Items.Add("()");
                rss.Items.Add("\\");
                rss.Items.Add("@");
                rss.Items.Add("<>");
                rss.Items.Add("</>");
                rss.Items.Add("<></>");
            }
            else if (e.KeyValue == 188)
            {
                rss.Visible = true;
                rss.Items.Add("HTML>");
                rss.Items.Add("/HTML>");
                rss.Items.Add("HTML></HTML>");
                rss.Items.Add("TITLE>");
                rss.Items.Add("/TITLE>");
                rss.Items.Add("TITLE></TITLE");
                rss.Items.Add("HEAD>");
                rss.Items.Add("/HEAD>");
                rss.Items.Add("HEAD></HEAD>");
                rss.Items.Add("BODY>");
                rss.Items.Add("/BODY>");
                rss.Items.Add("BODY></BODY>");
                rss.Items.Add("br>");
                rss.Items.Add("hr>");
                rss.Items.Add("/a>");
                rss.Items.Add("a href = \"\"></a>");
            }
            else if (e.KeyValue == 190)
            {
                rss.Visible = true;
                rss.Items.Add("</HTML>");
                rss.Items.Add("</HEAD>");
                rss.Items.Add("</BODY>");
                rss.Items.Add("</a>");
            }
            else if (e.KeyCode == Keys.PageUp ||e.KeyCode == Keys.PageDown)
            {
                try
                {
                    rss.Select();
                    rss.Items[1].Select();
                }
                catch (Exception ertt) { }
            }
            else
            {
                rss.Visible = false;
                rss.Items.Add("No Suggestions Available");
            }
        }

        private int selint;
        private void rss_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            selint = tbxwbx.SelectionStart;
            tbxwbx.Text = tbxwbx.Text.Insert(selint, e.ClickedItem.Text);
            tbxwbx.SelectionStart = selint + e.ClickedItem.Text.Length;
        }

        private void clsemn_MouseEnter(object sender, EventArgs e)
        {
            clsemn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clsemn_MouseLeave(object sender, EventArgs e)
        {
            clsemn.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void rts_Click(object sender, EventArgs e)
        {
            App_Workbook wb = new App_Workbook();
            wb.Show();
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app_abt abt = new app_abt();
            abt.descr("Amatrix Work-Book");
        }

        private void topicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Name);
        }

        int ndx_src = 0;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                ndx_src = tbxwbx.Text.IndexOf(toolStripTextBox1.Text, ndx_src);
                tbxwbx.Select(ndx_src, toolStripTextBox1.Text.Length);
                ndx_src++;
            }
            catch (Exception erty) { ndx_src = 0; }
        }

        private void replaceAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbxwbx.Text = tbxwbx.Text.Replace(toolStripTextBox1.Text, toolStripComboBox1.Text);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            checkPrint = richTextBoxPrintCtrl1.Print(checkPrint, richTextBoxPrintCtrl1.TextLength, e);

            // Check for more pages
            if (checkPrint < richTextBoxPrintCtrl1.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        //printing
        private int checkPrint;
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            checkPrint = 0;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Text = tbxwbx.Text;
            richTextBoxPrintCtrl1.Font = tbxwbx.Font;
            richTextBoxPrintCtrl1.RightToLeft = tbxwbx.RightToLeft;
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbxwbx.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbxwbx.Redo();
        }

    }
}
    

