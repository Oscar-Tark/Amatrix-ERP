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
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Amatrix_Choose_Doc : Form
    {
        public Amatrix_Choose_Doc()
        {
            InitializeComponent();
            this.Show(); 
            /*try
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();
                ProcessStartInfo pi = new ProcessStartInfo(Environment.CurrentDirectory + "\\ShareP\\Amatrix Document Server.exe");
                p.StartInfo = pi;
                Amatrix.al_prc.Add(p);
                p.Start();
                this.Close();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to open Share Point, the .exe is missing, Call maintenance for Check-up."); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Doc_stdio ds = new Doc_stdio();
            ds.Show();
            this.Close();
        }
    }
}
