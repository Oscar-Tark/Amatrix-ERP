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
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Calculator_1 : Form
    {
        public Calculator_1()
        {
            InitializeComponent();
            System.Diagnostics.Process.Start("calc.exe");
            this.Close();
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

        private void Init()
        {
            if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Calculator_1_Load(object sender, EventArgs e)
        {
            this.Text = "Amatrix : Calculator";
        }



        //Calculations____________________________________________________________________________________________

        double recogno, num1, num2, tot;
        double accum;

        int getme = 0;
        protected void toto()
        {
            num1 = 0;
            num2 = 0;
            tot = 0;
            rtfclc.Clear();
            Resulttf.Clear();
            getme = 0;
            /*if (recogno == 1)
            {
                num2 = Convert.ToInt32(rtfclc.Text);
                tot = num1 + num2;
                rtfclc.Clear();
                rtfclc.Text = tot.ToString();
                oprtlbl.Text = "Addition : " + num1.ToString() + " + " + num2.ToString() + " = " + tot.ToString();
            }
            else if (recogno == 12)
            {
                rtfclc.Text = tot.ToString();
            }
            else if (recogno == 2)
            {
                num2 = Convert.ToInt32(rtfclc.Text);
                tot = num1 - num2;
                rtfclc.Clear();
                rtfclc.Text = tot.ToString();
                oprtlbl.Text = "Substraction : " + num1.ToString() + " - " + num2.ToString() + " = " + tot.ToString();
            }
            else
            {
                rtfclc.Text = "Unable to determine the result due to invlid entries.";
                rtfclc.SelectAll();
            }*/
        }



        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                btn_num = (Button)sender;
                rtfclc.AppendText(btn_num.Text);
            }
            catch (Exception eclcadd)
            {
            }
        }

        private void equals_Click(object sender, EventArgs e)
        {
            toto();
            accum = 0;
            Resulttf.Text = "Cleared";
        }

        private void clsecalchov(object sender, EventArgs e)
        {
            clsecalc.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            clsecalc.Text = "Close";
        }

        private void clsecalcext(object sender, EventArgs e)
        {
            clsecalc.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void clsecalc_Click(object sender, EventArgs e)
        {
            delcalc.Start();
        }

        private void Calculator_1_Activated(object sender, EventArgs e)
        {
            try
            {
                calcdect.Stop();
            }
            catch (Exception exclcdec)
            {
            }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Calculator_1_Deactivate(object sender, EventArgs e)
        {
            calcdect.Start();
        }


        //beauty
        private void addhov(object sender, EventArgs e)
        {
            Add.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void multdwn(object sender, MouseEventArgs e)
        {
            mult.BackgroundImage = Properties.Resources.btsimp;
        }

        private void multup(object sender, MouseEventArgs e)
        {
            mult.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void dvdehov(object sender, EventArgs e)
        {

        }

        private void dvdeext(object sender, EventArgs e)
        {
            Divide.BackgroundImage = Properties.Resources.btnsim1;
        }

        private void dvdedwn(object sender, MouseEventArgs e)
        {
            Divide.BackgroundImage = Properties.Resources.btsimp;
        }

        private void dvdeup(object sender, MouseEventArgs e)
        {

        }

        private void eqsext(object sender, EventArgs e)
        {
            eqs.BackgroundImage = Properties.Resources.btnsim1;
        }

        private void eqsdwn(object sender, MouseEventArgs e)
        {
            eqs.BackgroundImage = Properties.Resources.btsimp;
        }

        private void eqsup(object sender, MouseEventArgs e)
        {

        }

        private void eqshov(object sender, EventArgs e)
        {

        }

        int chkhx = 0;
        private void hxchk_CheckedChanged(object sender, EventArgs e)
        {
            switch (chkhx)
            {
                case 0:
                    Math.Log10(Convert.ToDouble(tot));
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }

        private void hlpclc_Click(object sender, EventArgs e)
        {
            Helper clchlp = new Helper();
            clchlp.Show();
        }

        private Button btn_num = new Button();
        private void onebtn_Click(object sender, EventArgs e)
        {
            btn_num = (Button)sender;
            rtfclc.AppendText(btn_num.Text);
        }

        private void loglog_Click(object sender, EventArgs e)
        {
            if (tot == 0 || tot == null)
            {
                Resulttf.Text = "Please Enter a Number to Find A Log Value For.";
            }
            else
            {
                if (rtfclc.Text == null || rtfclc.Text == "")
                {
                    double nss = tot;
                    tot = Math.Log(tot);
                    Resulttf.Text = "Log of " + nss.ToString() + " is " + tot.ToString();
                }
                else
                {
                    double reck = Convert.ToDouble(rtfclc.Text);
                    tot = Math.Log(Convert.ToDouble(rtfclc.Text));
                    Resulttf.Text = "Log of " + reck.ToString() + " is " + tot.ToString();
                }
            }
        }

        private void gotolgc_Click(object sender, EventArgs e)
        {
        }

        private void power_Click(object sender, EventArgs e)
        {
            if (tot == 0 || tot == null)
            {
                Resulttf.Text = "No Numbers Have been Entered.";
            }
            else
            {
                try
                {
                    double keeper = tot;
                    this.Text = keeper.ToString();
                    for (int time = 1; time < Convert.ToInt32(rtfclc.Text); time++)
                    {
                        tot = tot * keeper;
                    }
                    this.Text = tot.ToString();
                }
                catch (Exception rtfg) { Resulttf.Text = "Please Enter a Number to Power up Too."; }
            }
        }

        private void Calculator_1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void Calculator_1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        object charsinexp = 0;

        private void fofx_Click(object sender, EventArgs e)
        {
        }

        private void delcalc_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void sqr_Click(object sender, EventArgs e)
        {
            if (tot == 0 || tot == null)
            {
                Resulttf.Text = "Please Enter a Number to Square";
            }
            else
            {
                if (rtfclc.Text == null || rtfclc.Text == "")
                {
                    double ssn = tot;
                    tot = tot * tot;
                    Resulttf.Text = "Square of " + ssn.ToString() + " is " + tot.ToString();
                }
                else
                {
                    double sqre = Convert.ToDouble(rtfclc.Text);
                    double stw = sqre;
                    tot = sqre * sqre;
                    Resulttf.Text = "Square of " + stw.ToString() + " is " + tot.ToString();
                }
                rtfclc.Clear();
            }
        }

        private void cube_Click(object sender, EventArgs e)
        {
            if (tot == 0)
            {
                double cbe = Convert.ToDouble(rtfclc.Text);
                double cberec = cbe;
                cbe = cbe * cbe * cbe;
                Resulttf.Text = "Cube of " + cberec.ToString() + " is " + cbe.ToString();
            }
            else if (tot != 0)
            {
                double acumbackp = tot;
                tot = tot * tot * tot;
                Resulttf.Text = "Cube of " + acumbackp.ToString() + " is " + tot.ToString(); 
            }
            else
            { }
        }

        private void lgtn_Click(object sender, EventArgs e)
        {
            double reck = Convert.ToDouble(rtfclc.Text);
            double dud = Math.Log10(Convert.ToDouble(rtfclc.Text));
            Resulttf.Text = "Log 10 of " + reck.ToString() + " is " + dud.ToString();
        }

        private void clcenbltc(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.lockstat == "Locked")
            {
                this.Enabled = false;
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
            else if (Properties.Settings.Default.lockstat == "none")
            {
                this.Enabled = true;
                this.Visible = true;
                this.ShowInTaskbar = true;
            }
            else
            { }
        }

        private void cos_Click(object sender, EventArgs e)
        {
            if (tot == 0 || tot == null)
            {
                Resulttf.Text = "Please Enter a Number to Find A Cos Value For.";
            }
            else
            {
                if (accum == 0)
                {
                    double dect;
                    double drtf = Convert.ToDouble(rtfclc.Text);
                    dect = Math.Cos(drtf);
                    Resulttf.Text = "Cos of " + drtf.ToString() + " is : " + dect.ToString();
                }
                else if (accum != 0)
                {
                    double dect;
                    double drtf = Convert.ToDouble(accum);
                    dect = Math.Cos(drtf);
                    Resulttf.Text = "Cos of " + drtf.ToString() + " is : " + dect.ToString();
                }
            }
        }

        private void calcdecttc(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                calcdect.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void clcmnuextclc(object sender, EventArgs e)
        {
            delcalc.Start();
        }

        //equals
        private ArrayList al = new ArrayList(); private ArrayList al_opr = new ArrayList(); private int ndx; private string accumu; private double final;
        private double total_val;
        private void button1_Click(object sender, EventArgs e)
        {
            ndx = 0; accumu = ""; final = 0;
            al.Clear(); al_opr.Clear();
            foreach (char ch in rtfclc.Text)
            {
                if (ch != '+' && ch != '-' && ch != '/' && ch != 'x')
                {
                    accumu = accumu + ch;
                }
                else if (ch == '+' || ch == '-' || ch == '/' || ch == 'x')
                {
                    final = Convert.ToDouble(accumu);
                    al.Add(final);
                    al_opr.Add(ch.ToString());
                    accumu = "";
                }
                ndx++;
                if (ndx == rtfclc.Text.Length)
                {
                    final = Convert.ToDouble(accumu);
                    al.Add(final);
                    accumu = "";
                }
            }

            //calculate

            total_val = 0; ndx = 0; int modulo_2 = 1;
            foreach (double d in al)
            {
                //if (modulo_2 % 2 == 0)
                //{
                try
                {
                    if (al_opr[ndx].ToString() == "+")
                    {
                        try
                        {
                            total_val = total_val + (Convert.ToDouble(al[ndx - 1]) + Convert.ToDouble(d));
                        }
                        catch (Exception erty) { }
                    }
                    if (al_opr[ndx].ToString() == "-")
                    {
                    }
                    if (al_opr[ndx].ToString() == "*")
                    {
                    }
                    if (al_opr[ndx].ToString() == "/")
                    {
                    }
                    ndx++;
                }
                catch (Exception erty) { /*total_val = total_val + d;*/ }
                //}
                //modulo_2++;
            }
            Resulttf.Text = total_val.ToString();
        }
    }
}
