﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ECDLManager
{
    public partial class Preprocessor : Form
    {
        List<RawStudent> rawStudents = new List<RawStudent>();
        string tempListContent = string.Empty;


        public Preprocessor()
        {
            InitializeComponent();
        }


        #region IO operations

        private bool LoadAndCheckInput()
        {
            using (StreamReader st = new StreamReader(tb_filePath.Text, Encoding.Default))
            {
                try
                {
                    RawStudent[] rs = new RawStudent[1];
                    string[] data = st.ReadLine().Split(' ');
                    rs[0] = new RawStudent(data[0], data[1]);
                    return true;
                }
                catch (Exception ex)
                {
                    G.I.Dof.WriteError(ex.ToString());
                    tb_filePath.Text = string.Empty;
                    return false;
                }
            }
        }

        private void LoadRawData(object sender)
        {
            using (StreamReader sr = new StreamReader(tb_filePath.Text, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(' ');
                    rawStudents.Add(new RawStudent(data[0], data[1]));
                }
                
                (sender as Button).Enabled = false;
                bt_saveFormatedData.Enabled = true;
                lb_inputDataStatus.Text = "Vstuptní data NAČTENA";
                lb_inputDataStatus.ForeColor = Color.Green;
            }
        }

        private void GenerateAndSaveFormatedData()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(new FileStream(fbd.SelectedPath + @"\formatedList.csv", FileMode.Create, FileAccess.ReadWrite), Encoding.Default))
                    {
                        //module,date,time,exam duration
                        sw.WriteLine(tb_date.Text + ";" + tb_time.Text + ";" + tb_testDuration.Text);
                        foreach (RawStudent rs in rawStudents)
                        {
                            //student's name, student's lastname, exam duration in minutes, module
                            sw.WriteLine(rs.name + ";" + rs.lastname + ";" + tb_testDuration.Text + ";" + tb_module.Text);
                        }
                    }
                    G.I.Dof.WriteInfo("Formátovaná data byla vygenerována do " + fbd.SelectedPath + @"\formatedList.csv");
                }
                catch (Exception ex)
                {
                    G.I.Dof.WriteError(ex.ToString());
                }
            }
            else
                G.I.Dof.WriteWarning("Nebyla vybrána žádná cesta pro uložení dat");
        }

        #endregion

        #region Event handlers

        private void tb_filePath_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = ofd_inputFile.ShowDialog();
                if (result == DialogResult.OK)
                {
                    tb_filePath.Text = ofd_inputFile.FileName;
                    bt_loadData.Enabled = true;
                }
                else
                {
                    MessageBox.Show("nebyla vybrána žádná cesta k souboru");
                }
            }
            catch (Exception ex)
            {
                G.I.Dof.WriteError(ex.ToString());
            }
        }


        private void bt_loadData_Click(object sender, EventArgs e)
        {
            if (LoadAndCheckInput())
            {
                LoadRawData(sender);
                G.I.Dof.WriteInfo("Data do generátoru načtena");
            }
            else
                G.I.Dof.WriteWarning("Soubor, který byl načten do generátoru má nesprávný formát nebo je požkozen");
        }

        private void bt_saveFormatedData_Click(object sender, EventArgs e)
        {
            GenerateAndSaveFormatedData();

        }

        private void lb_about_Click(object sender, EventArgs e)
        {
            Form about = new About();
            about.Show();
            G.I.Dof.WriteInfo("Bylo otevřeno okno 'O aplikaci'");
        }

        private void Preprocessor_FormClosed(object sender, FormClosedEventArgs e)
        {
            G.I.entry.WindowState = FormWindowState.Normal;
            G.I.Dof.WriteInfo("Okno generátoru bylo zavřeno");
        }

        #endregion

        #region ToolTip handlers

        ToolTip originFilePathToolTip;


        private void tb_filePath_MouseEnter(object sender, EventArgs e)
        {
            originFilePathToolTip = new ToolTip();
            originFilePathToolTip.IsBalloon = true;
            originFilePathToolTip.Show("Dvojklik pro otevření dialogu na výběr zdrojového souboru", (TextBox)sender, 20, -35, 10000);
        }

        private void tb_filePath_MouseLeave(object sender, EventArgs e)
        {
            originFilePathToolTip.Dispose();
        }


        #endregion
    }
}
