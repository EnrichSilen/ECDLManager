﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace ECDLManager
{
    public partial class Entry : Form
    {
        public Entry()
        {
            InitializeComponent();
            G.I.Dof = new DebugOutputForm();
        }

        #region Event handlers

        private void bt_startGenerator_Click(object sender, EventArgs e)
        {
            Form preproc = new Preprocessor();
            preproc.Show();
            WindowState = FormWindowState.Minimized;
            G.I.Dof.WriteInfo("Okno generátoru bylo otevřeno");

        }

        private void bt_startTest_Click(object sender, EventArgs e)
        {
            G.I.PresenterContentBlockXOffset = Convert.ToInt32(nud_contentBlockOffsetX.Value);
            G.I.PresenterContentBlockYOffset = Convert.ToInt32(nud_contentBlockOffsetY.Value);


            Form pres = new Presenter();
            pres.Show();
            G.I.Dof.WriteInfo("Okno testu bylo otevřeno");

            WindowState = FormWindowState.Minimized;

        }

        private void chb_debugOnOff_CheckedChanged(object sender, EventArgs e)
        {
            if (!G.I.debugMod)
            {
                G.I.debugMod = true;
                chb_debugOnOff.ForeColor = Color.Green;
                chb_debugOnOff.Text = "Režim DEBUG je zapnut";
                Text = "ECDL DEBUG";
                G.I.Dof.Show();
            }
            else
            {
                G.I.debugMod = false;
                chb_debugOnOff.ForeColor = Color.Red;
                chb_debugOnOff.Text = "Režim DEBUG je vypnut";
                Text = "ECDL";
                G.I.Dof.Hide();
            }
        }

        private void chb_highlightColor_CheckedChanged(object sender, EventArgs e)
        {
            if (G.I.defaultHlm)
            {
                G.I.defaultHlm = false;
                chb_highlightColor.ForeColor = Color.Black;
                chb_highlightColor.Text = "Zvýraznění pozastavených v testu černá/bílá";
                G.I.Dof.WriteInfo("Změna zvýraznění pozastaveného na černá/bílá");
            }
            else
            {
                G.I.defaultHlm = true;
                chb_highlightColor.ForeColor = Color.DarkGreen;
                chb_highlightColor.Text = "Zvýraznění pozastavených v testu zelená/červená";
                G.I.Dof.WriteInfo("Změna zvýraznění pozastaveného na zelená/červená");
            }

        }

        private void lb_about_Click(object sender, EventArgs e)
        {
            Form about = new About();
            about.Show();
            G.I.Dof.WriteInfo("Okno 'O aplikaci' otevřeno z menu");
        }


        #endregion
        
        #region ToolTip handlers
        ToolTip infoToolTip;

        private void lb_testOffsetX_MouseEnter(object sender, EventArgs e)
        {
            infoToolTip = new ToolTip();
            infoToolTip.IsBalloon = true;
            infoToolTip.Show("Nastavení horizontálního odsazení v pixelech vygenerováného bloku v Testu", (Label)sender, 20, -35, 10000);
        }

        private void lb_testOffsetX_MouseLeave(object sender, EventArgs e)
        {
            infoToolTip.Dispose();
        }

        private void lb_testOffsetY_MouseEnter(object sender, EventArgs e)
        {
            infoToolTip = new ToolTip();
            infoToolTip.IsBalloon = true;
            infoToolTip.Show("Nastavení vertikálního odsazení v pixelech vygenerováného bloku v Testu", (Label)sender, 20, -35, 10000);
        }

        private void lb_testOffsetY_MouseLeave(object sender, EventArgs e)
        {
            infoToolTip.Dispose();
        } 
        #endregion
    }
}
