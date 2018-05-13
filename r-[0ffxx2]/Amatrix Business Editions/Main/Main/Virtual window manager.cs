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
using System.Threading;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Virtual_window_manager : Form
    {
        public Virtual_window_manager()
        {
            InitializeComponent();
            this.Size = new Size(SystemInformation.WorkingArea.Width, 33);
            Init();
            this.ShowInTaskbar = false;
            Vvm_Wins wns = new Vvm_Wins();
            wns.Show();
        }

        private void Init()
        {
            Amatrix.AL_CLOCKS.Add(tstme);
            if (Properties.Settings.Default.vvmorient == "tp")
            {
                this.Location = new Point(0, 0);
            }
            else if (Properties.Settings.Default.vvmorient == "btt")
            {
                this.Location = new Point(0, SystemInformation.WorkingArea.Height - 33);
            }
            else
            {
                this.Location = new Point(0, 0);
            }
            cmsvvm.AllowTransparency = true;
            cmsvvm.Opacity = 0.90;

            if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }
        }

        private void Virtual_window_manager_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.70;
        }

        private void vvmhov(object sender, EventArgs e)
        {
            try
            {
                decvvm.Stop();
            }
            catch (Exception ntstpble)
            {
            }
            this.Opacity = 0.92;
        }

        private void vvmext(object sender, EventArgs e)
        {
            decvvm.Start();
        }

        private void decvvm_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                decvvm.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.01;
            }
        }

        private void vvmclse_Click(object sender, EventArgs e)
        {
            clsevvm.Start();
        }

        private void clsevvmtk(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void amfcusbtclc(object sender, EventArgs e)
        {
            Main.Amatrix.ActiveForm.Activate();
        }

        private void nwam_Click(object sender, EventArgs e)
        {
            Main.Amatrix amnw = new Amatrix();
            amnw.Show();
        }

        private void oatsclc(object sender, EventArgs e)
        {
            /*Doc_stdio_canopner can = new Doc_stdio_canopner();
            can.Show();*/
        }

        private void devtsclc(object sender, EventArgs e)
        {
        }

        private void shptlsclc(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\Connect\\Amatrix Connect Business Client.exe");
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to Start Amatrix Connect"); }
        }

        private void dbtsclc(object sender, EventArgs e)
        {
            Main.AMDS apamd = new AMDS();
            apamd.Show();
        }

        private void csts_ButtonClick(object sender, EventArgs e)
        {
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void drager_Tick(object sender, EventArgs e)
        {
            this.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
            if (this.Location.X == 0 && this.Location.Y == 0)
            {
                this.Size = new Size(SystemInformation.WorkingArea.Width, 33);
            }
            else
            {
                this.Size = new Size(800, 33);
            }
        }

        private void tstme_Click(object sender, EventArgs e)
        {
            Calendar calvvm = new Calendar();
            calvvm.Show();
        }

        private void tbxqb_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyEventArgs n = new KeyEventArgs(Keys.Enter);
        }

        private void jrnlstrtclc(object sender, EventArgs e)
        {
            acc_journ nej = new acc_journ();
            nej.Show();
        }

        acc_ledg nje;
        private void ldg_Click(object sender, EventArgs e)
        {
            nje = new acc_ledg();
            nje.Show();
            nje = null;
        }

        private void incvd_Click(object sender, EventArgs e)
        {
            acc_invce nvde = new acc_invce();
            nvde.Show();
        }

        private void tpvvm_Click(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            this.Size = new Size(SystemInformation.WorkingArea.Width, 33);
            Properties.Settings.Default.vvmorient = "tp";
            Properties.Settings.Default.Save();
        }

        private void bttvvm_Click(object sender, EventArgs e)
        {
            this.Location = new Point(0, SystemInformation.WorkingArea.Height - 33);
            this.Size = new Size(SystemInformation.WorkingArea.Width, 33);
            Properties.Settings.Default.vvmorient = "btt";
            Properties.Settings.Default.Save();
        }

        private delegate void it();
        private Thread thit;

        private void thinit()
        {
            thit = new Thread(new ThreadStart(delit));
            thit.Start();
        }

        private void delit()
        {
            try
            {
                this.Invoke(new it(Setit));
            }
            catch (Exception uy) { }
        }

        private void Setit()
        {
            accshw.Text = "Avvia Accounting Studio";
            nwam.Text = "Nuova Finestra";
        }

        private delegate void delen();
        Thread then;

        private void thenstrt()
        {
            then = new Thread(new ThreadStart(delenstrt));
            then.Start();
        }

        private void delenstrt()
        {
            try
            {
                this.Invoke(new delen(seten));
            }
            catch (Exception erty)
            { }
        }

        private void seten()
        {
            accshw.Text = "Show Accounting Studio";
            nwam.Text = "New Window";
        }

        private void vvmtr_Click(object sender, EventArgs e)
        {
            acc_trns tn = new acc_trns();
            tn.Show();
        }

        private void vvmcst_Click(object sender, EventArgs e)
        {
        }

        private void shwbustd_Click(object sender, EventArgs e)
        {
        }

        private void employeeAndCompanyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(employeeAndCompanyInformationToolStripMenuItem) == true)
            {
                First_use fs = new First_use();
                fs.Show();
            }
            if (sender.Equals(connectionSettingsToolStripMenuItem1) == true)
            {
                First_use_optn op = new First_use_optn();
                op.Show();
            }
            if (sender.Equals(connectionSettingsToolStripMenuItem) == true)
            {
                loggy lg = new loggy();
                lg.Show();
            }
            if (sender.Equals(runApplicationToolStripMenuItem) == true)
            {
                try
                {
                    System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\ShareP\\Amatrix Document Server.exe");
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to open Share Point, the .exe is missing, Call maintenance for Check-up."); }
            }
            if (sender.Equals(vvmmkt) == true)
            {
                mgmt_opr op = new mgmt_opr();
                op.Show();
            }
            if (sender.Equals(projectManagmentToolStripMenuItem) == true)
            {
                mgmt_stratgy st = new mgmt_stratgy();
                st.Show();
            }
            if (sender.Equals(openHumanResourcesToolStripMenuItem) == true)
            {
                mgmt_hr hr = new mgmt_hr();
                hr.Show();
            }
            if (sender.Equals(openCustomerManagmentToolStripMenuItem) == true)
            {
                mgmt_pr pr = new mgmt_pr();
                pr.Show();
            }
            if (sender.Equals(openProductManagmentToolStripMenuItem) == true)
            {
                mgmt_supch sh = new mgmt_supch();
                sh.Show();
            }
            if (sender.Equals(jrnlstrt) == true)
            {
                acc_journ jrn = new acc_journ();
                jrn.Show();
            }
            if (sender.Equals(ldg) == true)
            {
                acc_ledg ld_g = new acc_ledg();
                ld_g.Show();
            }
            if (sender.Equals(incvd) == true)
            {
                acc_invce vce = new acc_invce();
                vce.Show();
            }
            if (sender.Equals(openRePartitionerToolStripMenuItem) == true)
            {
                reparttn tn = new reparttn();
                tn.Show();
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void closeAmatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private void openTaskManagmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\extr_mgmt.exe");
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Could not Open 'extr_mgmt.exe'."); }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form1 fmm = new Form1();
            fmm.Show();
        }

        bool in_ = false;
        private void reporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports rts = new Reports();
            rts.Show();
        }

        private void grapherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gadg_grph grph = new gadg_grph();
            grph.in_form();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Doc_stdio ds = new Doc_stdio();
            ds.Show();
        }
        //Search End()______________________________________________________________________________________
    }
}
