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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class gadg_pics : UserControl
    {
        public gadg_pics()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(gadg_pics_Disposed);
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

        void gadg_pics_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= gadg_pics_Disposed;
            this.Load -= gadg_pics_Load;
            this.BackgroundImageChanged -= gadg_pics_BackgroundImageChanged;
        }

        private void gadg_pics_Load(object sender, EventArgs e)
        {

        }

        public void fromfle(Image img)
        {
            try
            {
                this.BackgroundImage = img;
                //this.BackgroundImageLayout = ImageLayout.Zoom;
            }
            catch (Exception erty) { }
        }

        private void gadg_pics_BackgroundImageChanged(object sender, EventArgs e)
        {
            if (this.BackgroundImage.Size.Height < this.Size.Height || this.BackgroundImage.Size.Width < this.Size.Width)
            {
                this.BackgroundImageLayout = ImageLayout.Center;
            }
            else
            {
                this.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }
    }
}
