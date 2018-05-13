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
using System.IO;
using System.Text;
using System.Media;
using System.Diagnostics.PerformanceData;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace readycre
{
    class readyinit
    {
        public void readyreportbug()
        {
            Form bugs = new Form();
            bugs.Text = "Readycare : Bug Reporter";
            bugs.StartPosition = FormStartPosition.CenterScreen;
            bugs.BackgroundImageLayout = ImageLayout.Stretch;
            bugs.Size = new Size(512, 250);
            bugs.ShowIcon = false;
            bugs.ShowInTaskbar = false;
            bugs.FormBorderStyle = FormBorderStyle.FixedSingle;
            bugs.Show();
        }
    }
}