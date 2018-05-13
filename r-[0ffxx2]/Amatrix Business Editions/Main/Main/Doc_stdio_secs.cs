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
    public partial class Doc_stdio_secs : Form
    {
        public Doc_stdio_secs()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Disposed += new EventHandler(Doc_stdio_secs_Disposed);
            InitializeComponent();
            this.Text = "Amatrix Document Security";
        }

        void Doc_stdio_secs_Disposed(object sender, EventArgs e)
        {
            this.Disposed -= Doc_stdio_secs_Disposed;
            this.button4.Click -= this.button4_Click;
            this.button3.Click -= this.button3_Click;
            this.button2.Click -= this.button2_Click;
            this.textBox2.TextChanged -= this.textBox2_TextChanged;
            this.button5.Click -= this.button5_Click;
            this.textBox4.TextChanged -= this.textBox2_TextChanged;
            this.textBox3.TextChanged -= this.textBox2_TextChanged;
            this.button6.Click -= this.button6_Click;
            this.textBox1.Validated -= this.textBox2_TextChanged;
            this.button1.Click -= this.button1_Click;
            this.dectmeabt.Tick -= this.dectmeabt_Tick;
            this.abtclse.Tick -= this.abtclse_Tick;
            this.Deactivate -= this.Doc_stdio_secs_Deactivate;
            this.Load -= this.Doc_stdio_secs_Load;
            this.Activated -= this.Doc_stdio_secs_Activated;


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

        private void Doc_stdio_secs_Load(object sender, EventArgs e)
        {

        }

        string pass; Doc_stdio std;
        public void tx(string Password, Doc_stdio st)
        {
            std = st;
            pass = Password;
            this.Show();
            if (Password == "" || Password == null)
            {
                textBox1.Enabled = false;
                button1.Enabled = false;
                textBox4.Enabled = false;
                textBox3.Enabled = false;
                button6.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                button2.Enabled = false;
                textBox2.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == pass)
            {
                pass = "";
                std.pass("");
                abtclse.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            std.pass(textBox2.Text);
            abtclse.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == pass)
            {
                std.pass(textBox3.Text);
                abtclse.Start();
            }
            else
            {
                textBox4.BackColor = Color.DarkOrange;
            }
        }

        TextBox tbx;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            tbx = (TextBox)sender;
            tbx.PasswordChar = '.';
        }

        private void dectmeabt_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmeabt.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void abtclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.03;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void Doc_stdio_secs_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Doc_stdio_secs_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }
    }
}
