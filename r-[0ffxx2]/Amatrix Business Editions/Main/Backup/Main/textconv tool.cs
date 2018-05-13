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
using System.IO;


//! Capital Characters
//? Special characters

namespace rewr
{
    class re
    {
        public string amdsrder()
        {
            string gret = "";
            try
            {
                string red = "";
                FileStream fsrde = new FileStream(Main.Properties.Settings.Default.Serverloc, FileMode.Open, FileAccess.Read);
                StreamReader srde = new StreamReader(fsrde);
                red = srde.ReadToEnd();
                fsrde.Flush();
                srde.Close();
                fsrde.Close();
                return red;
            }
            catch (Exception excnofl)
            {
                Main.Am_err mer = new Main.Am_err();
                mer.tx(excnofl.Message.ToString());
            }
            return gret;
        }


        //Formatting
        private void frmt(string gtdat)
        {
            try
            {
                int indx = 0;
                int frntend = 0;
                int endend = 0;
                foreach (char cnt in gtdat)
                {
                    if (gtdat[indx] + gtdat[indx + 1] + gtdat[indx + 2] + gtdat[indx + 3] == 'P' + 'R' + 'T' + '>')
                    {
                        frntend = indx;
                    }
                    else if (gtdat[indx] + gtdat[indx + 1] + gtdat[indx + 2] + gtdat[indx + 3] == '<' + 'P' + 'R' + 'T')
                    {
                        endend = indx;                
                        Main.Am_err mert = new Main.Am_err();
                        stfrmt(frntend, endend, gtdat);
                    }
                    indx++;
                }
            }
            catch (Exception exctrll)
            {
            }
        }
        //Cut String
        private void stfrmt(int frnt, int nd, string vatertof)
        {
            frnt = frnt + 3;
            string accumu = "";
            while (frnt < nd - 1)
            {
                frnt++;
                accumu = accumu + vatertof[frnt];
            }
            sctapp(accumu);
        }
        //Sectionizing
        private void sctapp(string sec)
        {

        }
    }


    
    class wr
    {
        public void wrtejourn()
        {
            FileStream fsrde = new FileStream(Main.Properties.Settings.Default.Serverloc, FileMode.Open, FileAccess.Read);
            StreamReader srde = new StreamReader(fsrde);
            srde.ReadToEnd();
            fsrde.Flush();
            srde.Close();
            fsrde.Close();
        }
    }
    //r.o.m
    class rerom
    {
        public string rderom(string wre)
        {
            
            string gutter;
            string hed = jrnrder(wre);
            return hed;
        }

        public string jrnrder(string gte)
        {
            string give;
            FileStream fsrdeom = new FileStream(gte, FileMode.Open, FileAccess.Read);
            StreamReader srdeom = new StreamReader(fsrdeom);
            give = srdeom.ReadToEnd();
            fsrdeom.Flush();
            srdeom.Close();
            fsrdeom.Close();
            return give;
        }

        public string crcopy(string wre, string tpe)
        {
            string getitright = "";
            getitright = wre + Environment.UserName.ToString() + ".tmp" + tpe.ToString();
            if (File.Exists(getitright))
            {
            }
            else
            {
                File.Copy(wre, getitright, false);
            }
            return getitright;
        }

        public void recov(string wht, string whre)
        {
            Main.Properties.Settings.Default.journrecovwhre = whre;
            Main.Properties.Settings.Default.journrecovwht = wht;
        }

        public void sve(string wht, string whre)
        {

        }
    }

    class remov
    {
       /* private string remventr(int dex)
        {
            int fnt = 0;
            int bck = 0;
            int indx = 0;
            string stemp = Main.Properties.Settings.Default.Journmod.ToString();
            foreach (char sin in stemp)
            {
                if (sin == '>')
                {
                    fnt = indx;
                }
                else if (sin == '<')
                {
                    bck = indx;
                    gteanaly(fnt, bck, stemp);
                }
                    indx++;
            }
        }
        private string gteanaly(int bca, int mca, string vaterintotheshitter2)
        {
            string accum3 = "";
            bca = bca;
            while (bca < mca - 1)
            {
                bca++;
                accum3 = accum3 + vaterintotheshitter2[bca];
            }
            return accum3;
        }*/
    }

    class dbbk
    {
        public void entry(string wht, string itm , int fnet, int bect)
        {
            secndpss(wht, fnet, bect, itm);
        }

        /*private void delstr(string guyt, int fnet2, int bcet2, string itm2)
        {
            int ndx = 0;
            int fnt = 0;
            int bck = 0;
            foreach (char nis in guyt)
            {
                if (nis == '>')
                {
                    fnt = ndx;
                }
                else if (nis == '<')
                {
                    bck = ndx;
                    secndpss(guyt, fnt, bck, fnet2, bcet2, itm2);
                }
                ndx++;
            }
        }*/

        private void secndpss(string guyt2, int fnt2, int bck2, string itm3)
        {
            string accumdel2 = "";
            string bangbang = "";

            if (guyt2 == itm3)
            {
                /*int fromto = 0;
                foreach (char tinny in Main.Properties.Settings.Default.journdb1)
                {
                    if (fromto >= fnt2 && fromto <= bck2)
                    {
                    }
                    else { bangbang = bangbang + tinny; }
                    fromto++;
                }*/
            }
            else
            {
            }
            //Main.Properties.Settings.Default.journdb1 = bangbang;
        }
    }
}