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
    public partial class doc_stdio_Page_stle : Form
    {
        public doc_stdio_Page_stle()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Doc_stdio std = new Doc_stdio();
            std.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Doc_stdio std = new Doc_stdio();
            std.open_out(Application.StartupPath + "\\Templates\\Survey.afd", false);
            std.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Doc_stdio std = new Doc_stdio();
            std.open_out(Application.StartupPath + "\\Templates\\Data Form.afd", false);
            std.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Doc_stdio std = new Doc_stdio();
            std.open_out(Application.StartupPath + "\\Templates\\General Letter.afd", false);
            std.Show();
        }

        private void doc_stdio_Page_stle_Load(object sender, EventArgs e)
        {

        }
    }
}
