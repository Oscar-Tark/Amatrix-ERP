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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Splash_Screen : Form
    {
        public Splash_Screen()
        {
            this.TopMost = true;
            this.Disposed += new EventHandler(Splash_Screen_Disposed);

            InitializeComponent();

            this.Location = new Point(0, 0);
            this.Size = new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);

            Init();
            this.Show();
        }

        void Splash_Screen_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= Splash_Screen_Disposed;
            this.tme.Tick += new System.EventHandler(this.tme_Tick);
            this.Load += new System.EventHandler(this.Splash_Screen_Load);

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

        private void Init()
        {
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = null;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ClassStyle |= CS_DROPSHADOW;
                return p;
            }
        }

        private void Splash_Screen_Load(object sender, EventArgs e)
        {

        }

        private void tme_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if(this.Opacity <= 0.05)
            {
                this.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
