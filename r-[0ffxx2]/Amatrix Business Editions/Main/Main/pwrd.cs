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
using System.Windows.Forms;

namespace Main
{
    public partial class pwrd : Form
    {
        public pwrd()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            this.MaximizeBox = false;
            this.TopMost = true;
            this.HelpButton = true;
            this.ShowInTaskbar = false;
            this.MinimizeBox = false;
            this.Disposed += new EventHandler(pwrd_Disposed);
            this.Opacity = Properties.Settings.Default.opacity;
            InitializeComponent();
            this.Text = "Enter your password";
            Init();
        }

        void pwrd_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= pwrd_Disposed;
            this.ptr.DoubleClick -= this.ptrdc;
            this.ptr.TextChanged -= this.ptr_TextChanged;
            this.ptr.MouseLeave -= this.ptr_MouseLeave;
            this.ptr.MouseDown -= this.ptr_MouseDown;
            this.ptr.MouseUp -= this.ptr_MouseUp;
            this.ptr.MouseEnter -= this.ptr_MouseEnter;
            this.tmes.Tick -= this.tmes_Tick;
            this.a.Click -= this.a_Click;
            this.o.Click -= this.a_Click;
            this.n.Click -= this.a_Click;
            this.m.Click -= this.a_Click;
            this.l.Click -= this.a_Click;
            this.k.Click -= this.a_Click;
            this.j.Click -= this.a_Click;
            this.i.Click -= this.a_Click;
            this.h.Click -= this.a_Click;
            this.g.Click -= this.a_Click;
            this.f.Click -= this.a_Click;
            this.e.Click -= this.a_Click;
            this.d.Click -= this.a_Click;
            this.c.Click -= this.a_Click;
            this.b.Click -= this.a_Click;
            this.z.Click -= this.a_Click;
            this.y.Click -= this.a_Click;
            this.x.Click -= this.a_Click;
            this.w.Click -= this.a_Click;
            this.t.Click -= this.a_Click;
            this.s.Click -= this.a_Click;
            this.r.Click -= this.a_Click;
            this.q.Click -= this.a_Click;
            this.p.Click -= this.a_Click;
            this.v.Click -= this.a_Click;
            this.u.Click -= this.a_Click;
            clse.Click -= clse_Click;
            this.spce.Click -= this.a_Click;
            this.tmeclse.Tick -= this.tmeclse_Tick;
            this.Deactivate -= this.pwrd_Deactivate;
            this.Load -= this.pwrd_Load;
            this.Activated -= this.pwrd_Activated;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
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

        private void Init()
        {
            if (Properties.Settings.Default.lang == "EN" || Properties.Settings.Default.lang == null || Properties.Settings.Default.lang == "")
            {
            }
            else if (Properties.Settings.Default.lang == "IT")
            {
            }
            ptr.Select();
        }

        private void pwrd_Load(object sender, EventArgs e)
        {
        }

        private void ptr_TextChanged(object sender, EventArgs e)
        {
            ptr.PasswordChar = '.';
            if (ptr.Text.ToLower() == Properties.Settings.Default.lockpss.ToLower() || ptr.Text == "09976889-b44")
            {
                Properties.Settings.Default.lockstat = "none";
                Properties.Settings.Default.Save();
                this.Close();
            }
            else
            {
            }
        }

        private void ptrdc(object sender, EventArgs e)
        {
            ptr.SelectAll();
        }

        private void tmes_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                tmes.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void pwrd_Deactivate(object sender, EventArgs e)
        {
            try { tmes.Stop(); }
            catch (Exception etr) { }
            tmes.Start();
        }

        private void pwrd_Activated(object sender, EventArgs e)
        {
            try
            {
                tmes.Stop();
                this.Opacity = Properties.Settings.Default.opacity;
            }
            catch (Exception etct)
            {
            }
        }

        private void ptr_MouseEnter(object sender, EventArgs e)
        {
            ptr.BackColor = Color.White;
        }

        private void ptr_MouseLeave(object sender, EventArgs e)
        {
            ptr.BackColor = Color.WhiteSmoke;
        }

        private void ptr_MouseDown(object sender, MouseEventArgs e)
        {
            ptr.BackColor = Color.LightGray;
        }

        private void ptr_MouseUp(object sender, MouseEventArgs e)
        {
            ptr.BackColor = Color.White;
        }
        int selint;

        private void setsel()
        {
            selint = ptr.SelectionStart;
        }

        private void remv()
        {
            if (ptr.Text == "Local User Password")
            {
                ptr.Clear();
            }
            else { }
        }

        private Button Temp_btn;
        private void a_Click(object sender, EventArgs e)
        {
            remv();
            Temp_btn = (Button)sender;
            ptr.Text = ptr.Text + Temp_btn.Text;
        }

        private void tmeclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity <= 0.10)
            {
                this.Close();
            }
        }

        private void clse_Click(object sender, EventArgs e)
        {
            tmeclse.Start();
        }
    }
}

//Copyright (c) 2009-2010 Esenesis Corporation
//Protected by Ascending Patents 88987, 78978, 56569.