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
using System.IO;
using System.Windows.Forms;

namespace Main
{
    public partial class Helper : Form
    {
        public Helper()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            contntse();
            tv1.ExpandAll();
           /* try
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

        private void Helper_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public void tx(string Who_Opened_Me)
        {
            this.Show();
        }

        private void clsehlp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cntntsmn_Click(object sender, EventArgs e)
        {
            contntse();
        }
        protected void contntse()
        {
           // webBrowser1.Navigate("www.astreous.webs.com/helpindx.html");
        }

        private void clseclseclck(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clsehovhlp(object sender, EventArgs e)
        {
            clsehlper.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            clsehlper.Text = "Close";
        }

        private void clseexthlp(object sender, EventArgs e)
        {
            clsehlper.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void hlpact(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
        }

        private void hldec(object sender, EventArgs e)
        {
            //this.Opacity = 0.80;
        }

        private void cntents_Click(object sender, EventArgs e)
        {
            contntse();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //webBrowser1.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //webBrowser1.GoForward();
        }

        private void dectmeabt_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmeabt.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void abtclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void tv1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(tv1.SelectedNode.Text.ToLower() == "using amatrix")
            {
                webBrowser1.Navigate(Environment.CurrentDirectory + "\\Hlp\\Help welcome Page.htm");
            }
            else if (tv1.SelectedNode.Text.ToLower() == "using the applications")
            {
                webBrowser1.Navigate(Environment.CurrentDirectory + "\\Hlp\\Help welcome Page.htm");
            }
            else if (tv1.SelectedNode.Text.ToLower() == "using the network facilities")
            {
                webBrowser1.Navigate(Environment.CurrentDirectory + "\\Hlp\\Help welcome Page.htm");
            }
            else if (tv1.SelectedNode.Text.ToLower() == "using the utilities")
            {
                webBrowser1.Navigate(Environment.CurrentDirectory + "\\Hlp\\Help welcome Page.htm");
            }
            else
            {
                webBrowser1.Navigate(Environment.CurrentDirectory + "\\Hlp\\" + tv1.SelectedNode.Text + ".htm");
            }
        }
    }
}
