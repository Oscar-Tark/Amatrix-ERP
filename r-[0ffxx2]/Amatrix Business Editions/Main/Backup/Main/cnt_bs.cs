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
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Main
{
    public partial class cnt_bs : UserControl
    {
        public cnt_bs()
        {
            InitializeComponent();
            this.AutoScroll = true;
            load_();
        }

        private int series_ = 1;
        private ArrayList al_button = new ArrayList();
        private ArrayList al_label = new ArrayList();
        private void load_()
        {
            try
            {
                string docs_ = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (Directory.Exists(docs_ + "\\Amatrix User Folder") == true)
                {
                    DirectoryInfo nfo = new DirectoryInfo(docs_ + "\\Amatrix User Folder");
                    foreach (DirectoryInfo d in nfo.GetDirectories())
                    {
                        Button b = new Button();

                        b.FlatStyle = FlatStyle.Flat;
                        b.Anchor |= AnchorStyles.None;
                        b.FlatAppearance.BorderSize = 0;
                        b.BackColor = Color.Transparent;
                        b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        b.Size = new Size(115, 120);//114
                        b.Location = new Point(35, 29);


                        Panel p = new Panel();
                        p.Size = new Size(189, 167);
                        p.BackColor = Color.Transparent;
                        p.BorderStyle = BorderStyle.None;
                        p.Click += new EventHandler(b_Click);
                        b.Click += new EventHandler(b_Click);
                        p.Controls.Add(b);

                        Label lbl = new Label();
                        lbl.ForeColor = Color.FromArgb(192, 192, 255);
                        lbl.Text = d.Name;
                        lbl.Location = new Point(0,0);
                        p.Controls.Add(lbl);

                        FileStream fs = new FileStream(d.FullName + "\\info_image.txt", FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);
                        b.BackgroundImage = Image.FromFile(d.FullName + "\\" + sr.ReadToEnd());
                        b.BackgroundImageLayout = ImageLayout.Zoom;
                        fs.Flush();
                        sr.Close();
                        fs.Close();

                        FileStream fs2 = new FileStream(d.FullName + "\\info.txt", FileMode.Open, FileAccess.Read);
                        StreamReader sr2 = new StreamReader(fs2);
                        b.Tag = (object)sr2.ReadToEnd();
                        fs2.Flush();
                        sr2.Close();
                        fs2.Close();

                        flowLayoutPanel1.Controls.Add(p);
                        al_button.Add(p);
                        series_++;
                    }
                }
                else
                {
                    if (Directory.Exists(docs_ + "\\Amatrix User Folder") == false)
                    {
                        Directory.CreateDirectory(docs_ + "\\Amatrix User Folder");
                    }
                }
            }
            catch (Exception erty) { }

            if (series_ <= 1)
            {
                label1.Visible = true;
            }
        }

        Button bn = new Button(); Panel pn = new Panel();
        void b_Click(object sender, EventArgs e)
        {
            try
            {
                bn = (Button)sender;
                string Loc = bn.Tag.ToString();
                try
                {
                    System.Diagnostics.Process.Start(Loc);
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(bn.Tag.ToString()); MessageBox.Show(erty.Message); }
            }
            catch (Exception erty)
            {
                pn = (Panel)sender;
                string Loc = pn.Tag.ToString();
                try
                {
                    System.Diagnostics.Process.Start(Loc);
                }
                catch (Exception erty1) { Am_err ner = new Am_err(); ner.tx(pn.Tag.ToString()); MessageBox.Show(erty1.Message); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cnt_bs_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Form1 fmm1 = new Form1();
            fmm1.Show();
        }

        private void btnldg_Click(object sender, EventArgs e)
        {
            Reports prt = new Reports();
            prt.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            gadg_grph grph = new gadg_grph();
            grph.in_form();
            grph.Show();
        }

        //effects
        private Control vis_temp; private int vis_int = 0;
        private void bigger_Tick(object sender, EventArgs e)
        {
            vis_int = vis_int + 1;
            vis_temp.Size = new Size(vis_temp.Size.Width + 2, vis_temp.Size.Width + 2);
            vis_temp.Location = new Point(vis_temp.Location.X - 1, vis_temp.Location.Y - 1);
            if (vis_int >= 2) { bigger.Stop(); }
        }

        private void Eino_MouseEnter(object sender, EventArgs e)
        {

        }

        private void Eino_MouseLeave(object sender, EventArgs e)
        {

        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void Eino_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Eino_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.graphsdwn;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
