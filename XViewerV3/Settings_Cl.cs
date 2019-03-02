using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XViewerV3
{
    public partial class Settings_Cl : Form
    {
        public Settings_Cl()
        {
            InitializeComponent();
            txt_drivelater.Text = appfile.Currentfile.mysetting.drivelater;
            txt_filespath.Text = appfile.Currentfile.mysetting.filespath;
            txt_new.Text = appfile.Currentfile.mysetting.news;
            txt_virtualapp.Text = appfile.Currentfile.mysetting.virtualapp;
        }

      

        public Setting result = new Setting();


        private void btn_filespath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txt_filespath.Text = fbd.SelectedPath;
                result.filespath = fbd.SelectedPath;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txt_new.Text = fbd.SelectedPath;
                result.news = fbd.SelectedPath;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            if (System.IO.Directory.Exists(txt_filespath.Text) &&
                txt_drivelater.Text.Length == 2 && txt_drivelater.Text.Substring(1, 1) == ":")
            {
                result = new Setting();
                result.filespath = txt_filespath.Text;
                result.news = txt_new.Text;
                result.drivelater = txt_drivelater.Text;
                result.virtualapp = txt_virtualapp.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("اطلاعات اشتباه می باشد");
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btn_virtualapp_Click(object sender, EventArgs e)
        {
            OpenFileDialog fbd = new OpenFileDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txt_virtualapp.Text = fbd.FileName;
                result.virtualapp = fbd.FileName;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
