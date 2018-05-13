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
    public partial class gadg_resz : UserControl
    {
        public gadg_resz()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(gadg_resz_Disposed);
        }

        void gadg_resz_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= gadg_resz_Disposed;
            this.Load -= gadg_resz_Load;
            this.Resize -= gadg_resz_Resize;

            try
            {
                foreach (Control cnt in this.Controls)
                {
                    cnt.Dispose();
                }

                this.components.Dispose();
                this.Dispose(true);
                GC.Collect();
            }
            catch (Exception erty) { }
        }

        private void gadg_resz_Resize(object sender, EventArgs e)
        {
            label1.Text = "X : " + this.Size.Width.ToString() + "     Y : " + this.Size.Height.ToString();
        }

        private void gadg_resz_Load(object sender, EventArgs e)
        {

        }
    }
}
