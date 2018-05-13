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
    public partial class Scrsve_termin : Form
    {
        public Scrsve_termin()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.TopMost = true;
            this.Icon = SystemIcons.Exclamation;
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

        System.Diagnostics.Process P; Scrsve sv_;
        public void tx(System.Diagnostics.Process P_, Scrsve sv)
        {
            sv_ = sv;
            P = P_;
            this.Text = "Services";
            label1.Text = label1.Text + "\n\n" + "Service: " + P_.ProcessName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                P.Kill();
                sv_.remove(P);
                this.Close();
            }
            catch (Exception erty) { this.Close(); }
        }

        private void Scrsve_termin_Load(object sender, EventArgs e)
        {

        }
    }
}
