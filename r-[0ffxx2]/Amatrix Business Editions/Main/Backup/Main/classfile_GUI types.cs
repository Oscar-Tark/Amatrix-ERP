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
using System.Collections;
using System.Windows.Forms;

namespace GUI_TYPES
{
    class TEMPLATE_GUI_TYPES
    {
        public TEMPLATE_GUI_TYPES()
        {
            Header_Font = new Font("Segoe UI", 16, FontStyle.Regular);
            Default_Font = new Font("Seguoe UI", 8, FontStyle.Regular);
        }
        public Font Header_Font;
        public Font Default_Font;

        public Label Header_Label()
        {
            Label lbl = new Label();
            lbl.Font = Header_Font;
            lbl.Text = "Enter Header Text Here";
            lbl.Location = new Point(10, 45);
            lbl.ForeColor = Color.DimGray;
            lbl.AutoSize = true;
            lbl.MouseEnter += new EventHandler(_MouseEnter);
            lbl.MouseLeave += new EventHandler(_MouseLeave);
            return lbl;
        }

        public PictureBox Picture_Box()
        {
            PictureBox pbx = new PictureBox();
            pbx.Size = new Size(200, 150);
            pbx.Location = new Point();
            pbx.Location = new Point(479, 23);
            pbx.BackgroundImage = Main.Properties.Resources.pict;
            pbx.BackgroundImageLayout = ImageLayout.Center;
            pbx.BorderStyle = BorderStyle.FixedSingle;
            return pbx;
        }

        Control cntrl_temp; Label lbl_temp; PictureBox pbx_temp;
        void _MouseLeave(object sender, EventArgs e)
        {
            try
            {
                lbl_temp = (Label)sender;
                lbl_temp.BorderStyle = BorderStyle.None;
            }
            catch (Exception erty) { }
            try
            {
                pbx_temp = (PictureBox)sender;
                pbx_temp.BorderStyle = BorderStyle.None;
            }
            catch (Exception erty) { }
        }

        void _MouseEnter(object sender, EventArgs e)
        {
            try
            {
                lbl_temp = (Label)sender;
                lbl_temp.BorderStyle = BorderStyle.FixedSingle;
            }
            catch (Exception erty) { }
            try
            {
                pbx_temp = (PictureBox)sender;
                pbx_temp.BorderStyle = BorderStyle.FixedSingle;
            }
            catch (Exception erty) { }
        }
    }
}