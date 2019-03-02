using DiscUtils.Iso9660;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace XViewerV3
{
    public partial class add : Form
    {
        public add()
        {
            InitializeComponent();
            txt_browse.Text = appfile.Currentfile.mysetting.news;
        }

        private void add_Load(object sender, EventArgs e)
        {

        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (fbd.SelectedPath.Substring(fbd.SelectedPath.Length - 1, 1) != "\\")
                    txt_browse.Text = fbd.SelectedPath + "\\";
                else
                    txt_browse.Text = fbd.SelectedPath;
            }
        }
        Thread th;
        public long progressCount = 0;
        public long progressfail = 0;
        public void createiso()
        {

            result = new sick();
            if (System.IO.Directory.Exists(txt_browse.Text))
            {
                if (System.IO.File.Exists(txt_browse.Text + "\\feelView.exe") ||
                    System.IO.File.Exists(txt_browse.Text + "\\RAYKAVIEWER.exe") ||
                    System.IO.File.Exists(txt_browse.Text + "\\PDIVIEW\\PDIVIEW.exe") ||
                    System.IO.File.Exists(txt_browse.Text + "\\PacsPlusCV.exe") ||
                    System.IO.File.Exists(txt_browse.Text + "\\eFilmLite\\eFilmLt.exe")||
                    System.IO.File.Exists(txt_browse.Text + "\\Startup.exe") ||//firozgar
                    System.IO.File.Exists(txt_browse.Text + "\\CDViewer\\CDViewer.exe"))//rajaee
                {
                    CDBuilder builder = new CDBuilder();
                    builder.UseJoliet = true;
                    builder.VolumeIdentifier = txt_name.Text;
                    long ss = GetDirectorySize(txt_browse.Text);
                    progressBar1.Value = 0;
                    string temprandom = DateTime.Now.Ticks.ToString();
                    progressCount = 0;
                    progressfail = 0;
                    addpath(builder, txt_browse.Text, txt_browse.Text, ss, temprandom);
                    changeprogress(100);
                    result.path = appfile.Currentfile.mysetting.filespath + "\\" + DateTime.Now.Ticks + ".iso";
                    builder.Build(result.path);
                    deletepath = Path.GetDirectoryName(Environment.CurrentDirectory + "\\temp\\" + temprandom);
                    result.name = txt_name.Text;
                    result.type += System.IO.File.Exists(txt_browse.Text + "\\feelView.exe") ? 1 : 0;
                    result.type += System.IO.File.Exists(txt_browse.Text + "\\RAYKAVIEWER.exe") ? 2 : 0;
                    result.type += System.IO.File.Exists(txt_browse.Text + "\\PDIVIEW\\PDIVIEW.exe") ? 3 : 0;
                    result.type += System.IO.File.Exists(txt_browse.Text + "\\PacsPlusCV.exe") ? 4 : 0;
                    result.type += System.IO.File.Exists(txt_browse.Text + "\\eFilmLite\\eFilmLt.exe") ? 5 : 0;
                    result.type += System.IO.File.Exists(txt_browse.Text + "\\Startup.exe") ? 6 : 0;//firozgar
                    result.type += System.IO.File.Exists(txt_browse.Text + "\\CDViewer\\CDViewer.exe") ? 7 : 0;//rajaee
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult dr=MessageBox.Show("مسیر تصویر اشتباه است یا نوع تصویر پشتیبانی نمی شود ادامه می دهید؟ ", "", MessageBoxButtons.YesNo);
                   if(dr==DialogResult.Yes)
                    {
                        CDBuilder builder = new CDBuilder();
                        builder.UseJoliet = true;
                        builder.VolumeIdentifier = txt_name.Text;
                        long ss = GetDirectorySize(txt_browse.Text);
                        progressBar1.Value = 0;
                        string temprandom = DateTime.Now.Ticks.ToString();
                        progressCount = 0;
                        addpath(builder, txt_browse.Text, txt_browse.Text, ss, temprandom);
                        changeprogress(100);
                        result.path = appfile.Currentfile.mysetting.filespath + "\\" + DateTime.Now.Ticks + ".iso";
                        builder.Build(result.path);
                        deletepath = Path.GetDirectoryName(Environment.CurrentDirectory + "\\temp\\" + temprandom);
                        result.name = txt_name.Text;
                        result.type += System.IO.File.Exists(txt_browse.Text + "\\feelView.exe") ? -1 : 0;
                       
                        DialogResult = DialogResult.OK;
                    }

                    ENABLE = true;
                }
            }
            else
            {
                MessageBox.Show("مسیر تصویر اشتباه می باشد");
                ENABLE = true;
            }
        }
        private bool ENABLE
        {
            set
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => btn_browse.Enabled = value), null);
                    BeginInvoke(new Action(() => btn_ok.Enabled = value), null);
                    BeginInvoke(new Action(() => txt_name.Enabled = value), null);
                    BeginInvoke(new Action(() => txt_browse.Enabled = value), null);
                 
                }
            }
        }
        public string deletepath;
        public volatile sick result;
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (txt_browse.Text.Substring(txt_browse.Text.Length - 1, 1) != "\\")
                txt_browse.Text = txt_browse.Text + "\\";

            btn_browse.Enabled = false;
            btn_ok.Enabled = false;
            txt_name.Enabled = false;
            txt_browse.Enabled = false;
            if (txt_name.Text == "")
            {
                MessageBox.Show("نام را وارد نمایید!");
                btn_browse.Enabled = true;
                btn_ok.Enabled = true;
                txt_name.Enabled = true;
                txt_browse.Enabled = true;

               
                return;
            }
            th = new Thread(new ThreadStart(createiso));
            th.Start();
        }

        long GetDirectorySize(string path)
        {


            // 1.
            // Get array of all file names.
            string[] a = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;

            foreach (string name in a)
            {
                // 3.
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            // 4.
            // Return total size
            return b;
        }

        public void addpath(CDBuilder builder, string path, string root, long size, string temprandom)
        {
            foreach (string p in System.IO.Directory.GetDirectories(path))
            {
                addpath(builder, p, root, size, temprandom);
            }
            foreach (string f in System.IO.Directory.GetFiles(path))
            {
                try
                {
                    string str = Environment.CurrentDirectory + "\\temp\\" + temprandom + "\\" + f.Replace(root, "");
                    if (!System.IO.Directory.Exists(Path.GetDirectoryName(str)))
                        System.IO.Directory.CreateDirectory(Path.GetDirectoryName(str));
                    System.IO.File.Copy(f, str);
                    builder.AddFile(f.Replace(root, ""), str);
                    FileInfo info = new FileInfo(str);
                    changeprogress(Convert.ToInt32((info.Length * 100) / size));
                    
                    progressCount++;
                    changeprogressCount();
                }
                catch {
                    progressfail++;
                    changeprogressfail();
                }
            }

        }


        public void changeprogress(int plus)
        {


            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => progressBar1.Value = plus == 100 ? 100 : progressBar1.Value + plus), null);
            }


        }
        public void changeprogressCount()
        {


            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => label3.Text =progressCount.ToString()), null);
            }


        }
        public void changeprogressfail()
        {


            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => label6.Text = progressfail.ToString()), null);
            }


        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void add_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ok_Click(null, null);
            }
        }

        public static InputLanguage GetInputLanguageByName(string layoutname, string DisplayName, string EnglishName, string Name)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().StartsWith(EnglishName) ||
                    lang.LayoutName.ToLower().StartsWith(layoutname) ||
                    lang.Culture.DisplayName.ToLower().StartsWith(DisplayName) ||
                    lang.Culture.Name.ToLower().StartsWith(Name))
                {
                    return lang;
                }
            }
            return null;
        }
        private void SetKeyboardLayout(InputLanguage layout)
        {
            InputLanguage.CurrentInputLanguage = layout;
        }
        InputLanguage firstlayout;
        private void txt_name_Enter(object sender, EventArgs e)
        {
            firstlayout = InputLanguage.CurrentInputLanguage;
            SetKeyboardLayout(GetInputLanguageByName("Per", "Persian", "Persian", "fa"));

        }

        private void txt_name_Leave(object sender, EventArgs e)
        {
            if (firstlayout != null)
                InputLanguage.CurrentInputLanguage = firstlayout;
        }

        private void txt_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ok_Click(null, null);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
