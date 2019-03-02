using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XViewerV3
{
    public partial class Settings_Dr : Form
    {
        public Settings_Dr()
        {
            InitializeComponent();
            txt_drivelater.Text = appfile.Currentfile.mysetting.drivelater;
            txt_filespath.Text = appfile.Currentfile.mysetting.filespath;
            txt_new.Text = appfile.Currentfile.mysetting.news;
            txt_virtualapp.Text = appfile.Currentfile.mysetting.virtualapp;
            txt_ipserver.Text = appfile.Currentfile.mysetting.ip;
        }

       

        public Setting result = new Setting();

        private void Client_OnReciveData_checknet(object RecivedData)
        {
            if (RecivedData.ToString() == "ok")
            {
                MessageBox.Show("ارتباط برقرار شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("سرور در دسترس نمی باشد", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Client_OnReciveLog_checknet(string Message)
        {

        }
        private void Client_onFailure_checknet(Exception ex)
        {
            MessageBox.Show("سرور در دسترس نمی باشد", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
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
                result.ip = txt_ipserver.Text;
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

        private void btn_testnet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_ipserver.Text))
            {
                Cursor.Current = Cursors.WaitCursor;

                NetworkLibrary.client cli = new NetworkLibrary.client(txt_ipserver.Text, 1584);
                object output;
                if(cli.getdata("checknet", out output)=="ok" && output.ToString()=="ok")
                    MessageBox.Show("ارتباط برقرار شد");
                else
                    MessageBox.Show("خطا در برقراری ارتباط ");
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("آدرس را وارد نمایید");
            }

        }
    }
}
