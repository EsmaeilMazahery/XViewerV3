using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DiscUtils.Iso9660;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using static NetworkLibrary.client;

namespace XViewerV3
{
    public partial class frm_Main_Dr : Form
    {
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public bool Deleteitemserver(sick item)
        {

            sick temp = appfile.Currentfile.sicks.Find(a => a.name == item.name && a.type == item.type && Path.GetFileNameWithoutExtension(a.path) == Path.GetFileNameWithoutExtension(item.path) && item.Delete == true && a.Delete == false);
            if (temp != null)
            {
                IEnumerable<ListViewItem> lv = lstv.Items.Cast<ListViewItem>();

                var test = from xxx in lv
                           where xxx.SubItems[0].ToString() == temp.name && Path.GetFileNameWithoutExtension(xxx.SubItems[1].ToString()) == Path.GetFileNameWithoutExtension(temp.path) && xxx.SubItems[2].ToString() == temp.type.ToString()
                           select xxx;

                Process[] processes = null;
                if (lstv.SelectedItems[0].SubItems[2].Text == "1")
                    processes = Process.GetProcessesByName("feelView");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "2")
                    processes = Process.GetProcessesByName("RAYKAVIEWER");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "3")
                    processes = Process.GetProcessesByName("PDIVIEW");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "4")
                    processes = Process.GetProcessesByName("PacsPlusCV");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "5")
                    processes = Process.GetProcessesByName("eFilmLite");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "6")//firozgar
                    processes = Process.GetProcessesByName("Startup");//firozgar
                else if (lstv.SelectedItems[0].SubItems[2].Text == "7")
                    processes = Process.GetProcessesByName("CDViewer");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "8")
                    processes = Process.GetProcessesByName("start");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "9")//firozgar
                    processes = Process.GetProcessesByName("diVision Lite");//firozgar

                if (processes != null)
                    foreach (Process proc in processes)
                    {
                        Process tempProc = Process.GetProcessById(proc.Id);
                        tempProc.Kill();
                    }

                Unmount();


                deletesick(test.FirstOrDefault<ListViewItem>().Index);
                if (lstv.Items.Count == 0)
                    btn_deleteEnable = false;

                if (frm_Start.mainform1 != null)
                    frm_Start.mainform1.changestatus("یک بیمار حذف گردید");
                if (frm_Start.mainform2 != null)
                    frm_Start.mainform2.changestatus("یک بیمار حذف گردید");
                return true;
            }
            return false;
        }
        System.Windows.Forms.Timer tsave;

        public void autosave()
        {
            try
            {
                WriteToBinaryFile<List<sick>>("info.xvw", appfile.Currentfile.sicks);
                WriteToBinaryFile<Setting>("config.xvw", appfile.Currentfile.mysetting);
            }
            catch
            {
                MessageBox.Show("خطا در ذخیره اطلاعات");
            }
        }
        private void Tsave_Tick(object sender, EventArgs e)
        {
            autosave();
        }
        public frm_Main_Dr()
        {
            InitializeComponent();
            tsave = new System.Windows.Forms.Timer();
            tsave.Tick += Tsave_Tick;
            tsave.Interval = 30000;
            tsave.Enabled = true;
            tsave.Start();
           
            this.Size = new Size(this.Size.Width, Screen.PrimaryScreen.Bounds.Height - 100);
            if (System.IO.Directory.Exists(Environment.CurrentDirectory + "\\temp"))
                DeleteDirectory(Environment.CurrentDirectory + "\\temp");

            btn_delete.Enabled = false;
            btn_play.Enabled = false;
            btn_deleteEnable = false;
            btn_add.Enabled = false;
            appfile.Currentfile = new appfile();
            if (System.IO.File.Exists("info.xvw"))
            {
                try
                {
                    appfile.Currentfile.sicks = openviafile<List<sick>>("info.xvw");

                    foreach (sick item in appfile.Currentfile.sicks)
                    {
                        if (!item.Delete)
                        {
                            lstv.Items.Add(new ListViewItem(new string[] { item.name, item.path, item.type.ToString(), item.id.ToString() }, -1));
                            btn_deleteEnable = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطا در بازیابی اطلاعات");
                }
            }
            if (System.IO.File.Exists("config.xvw"))
            {
                try
                {
                    appfile.Currentfile.mysetting = openviafile<Setting>("config.xvw");

                    if (appfile.Currentfile.mysetting.virtualapp == null)
                    {
                        btn_add.Enabled = false;
                        lstv.Enabled = false;
                        return;
                    }
                    else
                    {
                        btn_add.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطا در بازیابی تنظیمات برنامه");
                }

            }
            else
            {
                btn_add.Enabled = false;
                lstv.Enabled = false;
            }
            if (appfile.Currentfile.mysetting.ip != "")
            {
                cli = new NetworkLibrary.client(appfile.Currentfile.mysetting.ip, 1584);
                t_client = new System.Threading.Timer(startClientOld, null, 0, 15000);
            }


            btn_play.Enabled = false;
            Random rnd = new Random();
            appfile.Currentfile.mysetting.laststatus_me = rnd.Next();
            appfile.Currentfile.mysetting.laststatus_server = rnd.Next();
        }

     

        public void changestatus(string status)
        {
            lbl_Status.Text = status;
        }

        NetworkLibrary.client cli;

        public void startClientOld(object status)
        {
            int laststatus_new;
            if (!_runclient)
            {
                _runclient = true;
                object output;
                if (cli.getdata("checknet", out output) == "ok" && output.ToString() == "ok")
                {
                    if (cli.getdata("getlaststatus", out output) == "ok" && appfile.Currentfile.mysetting.laststatus_server != int.Parse(output.ToString()))
                    {
                        laststatus_new = int.Parse(output.ToString());

                        if (cli.getdata("getfileinfo", out output) == "ok")
                        {
                            appfile f;
                            f = (appfile)output;
                            update(f);

                            lbl_Status.Text = "اطلاعات با موفقیت به روز رسانی شد";
                            changelastupdate = laststatus_new;
                            _runclient = false;
                        }
                        else
                        {
                            lbl_Status.Text = "ارتباط با سرور قطع می باشد";
                            _runclient = false;
                        }
                    }
                    else
                    {
                        lbl_Status.Text = "اطلاعات به روز می باشد";
                        _runclient = false;
                    }
                }
                else
                {
                    lbl_Status.Text = "ارتباط با سرور قطع می باشد";
                    _runclient = false;
                }
            }
        }
        /*
        public void startclient(object state)
        {
            if (!_runclient)
            {
                _runclient = true;
                client cli = new client(appfile.Currentfile.mysetting.ip, 1584);
                cli.SendData("checknet", Client_OnReciveData_checknet, Client_onFailure_checknet, Client_OnReciveLog_checknet);
            }
        }

        private void Client_OnReciveData_checknet(object RecivedData)
        {
            if (RecivedData.ToString() == "ok")
            {
                client cli = new client(file.mysetting.ip, 1584);
                cli.SendData("getlaststatus", Client_OnReciveData_getlaststatus, Client_onFailure_getlaststatus, Client_OnReciveLog_getlaststatus);
            }
            else
            {
                lbl_Status.Text = "ارتباط با سرور قطع می باشد";
                _runclient = false;
            }
        }
        private void Client_OnReciveLog_checknet(string Message)
        {

        }
        private void Client_onFailure_checknet(Exception ex)
        {
            lbl_Status.Text = "ارتباط با سرور قطع می باشد";
            _runclient = false;
        }
        private void Client_OnReciveData_getlaststatus(object RecivedData)
        {
            if (file.mysetting.laststatus_server != int.Parse(RecivedData.ToString()))
            {
           //     laststatus_new = int.Parse(RecivedData.ToString());
                client cli = new client(file.mysetting.ip, 1584);
                cli.SendData("getfileinfo", Client_OnReciveData_getfileinfo, Client_onFailure_getfileinfo, Client_OnReciveLog_getfileinfo);
            }
            else
            {
                lbl_Status.Text = "اطلاعات به روز می باشد";
                _runclient = false;
            }
        }
        private void Client_OnReciveLog_getlaststatus(string Message)
        {

        }
        private void Client_onFailure_getlaststatus(Exception ex)
        {
            lbl_Status.Text = "ارتباط با سرور قطع می باشد";
            _runclient = false;
        }
        private void Client_OnReciveData_getfileinfo(object RecivedData)
        {
            appfile f;
            f = (appfile)RecivedData;
            update(f);
            client cli = new client(file.mysetting.ip, 1584);
            cli.SendData(file, Client_OnReciveData_file, Client_onFailure_file, Client_OnReciveLog_file);

        }
        private void Client_OnReciveLog_getfileinfo(string Message)
        {

        }
        private void Client_onFailure_getfileinfo(Exception ex)
        {
            lbl_Status.Text = "ارتباط با سرور قطع می باشد";
            _runclient = false;
        }
        private void Client_OnReciveData_file(object RecivedData)
        {
            lbl_Status.Text = "اطلاعات با موفقیت به روز رسانی شد";
         //   changelastupdate = laststatus_new;
            _runclient = false;
        }
        private void Client_OnReciveLog_file(string Message)
        {

        }
        private void Client_onFailure_file(Exception ex)
        {
            lbl_Status.Text = "ارتباط با سرور قطع می باشد";
            _runclient = false;
        }
        */
        private int changelastupdate
        {
            set
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => appfile.Currentfile.mysetting.laststatus_server = value), null);
                }
            }
        }
        private void addlist(ListViewItem value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => lstv.Items.Add(value)), null);
            }
            else
            {
                lstv.Items.Add(value);
            }
        }

        public void addsick(sick item)
        {
            appfile.Currentfile.sicks.Add(item);
            addlist(new ListViewItem(new string[] { item.name, item.path, item.type.ToString(), item.id.ToString() }, -1));
        }

        public void deletesick(int item)
        {
            if (System.IO.File.Exists(lstv.Items[item].SubItems[1].Text))
                System.IO.File.Delete(lstv.Items[item].SubItems[1].Text);

            appfile.Currentfile.sicks[int.Parse(lstv.Items[item].SubItems[3].Text)].Delete = true;

            appfile.Currentfile.mysetting.laststatus_me++;
            lstv.Items.RemoveAt(item);
        }

        System.Threading.Timer t_client;
        bool _runclient;
        private void button3_Click(object sender, EventArgs e)
        {
            Settings_Dr s = new Settings_Dr();
            if (s.ShowDialog() == DialogResult.OK)
            {
                appfile.Currentfile.mysetting = s.result;
                btn_add.Enabled = true;
                lstv.Enabled = true;

                Cursor.Current = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(appfile.Currentfile.mysetting.ip))
                {
                    t_client = new System.Threading.Timer(startClientOld, null, 0, 15000);
                }
                else
                {
                    if (t_client != null)
                        t_client.Dispose();
                }


                Cursor.Current = Cursors.Default;
            }
        }

        private void Client_OnReciveData_getfileiso(object RecivedData)
        {


        }
        private void Client_OnReciveLog_getfileiso(string Message)
        {

        }
        private void Client_onFailure_getfileiso(Exception ex)
        {
            checkcurrentsick = false;
        }
        private void Client_OnReciveStream_getfileiso(Stream Recivedstream)
        {
            FileStream Fs = new FileStream(appfile.Currentfile.mysetting.filespath + "\\" + Path.GetFileName(currentsick.path), FileMode.OpenOrCreate, FileAccess.Write);
            while (Recivedstream.CanRead)
            {
                Fs.WriteByte((byte)Recivedstream.ReadByte());
            }
            Fs.Close();

            currentsick.path = appfile.Currentfile.mysetting.filespath + "\\" + Path.GetFileName(currentsick.path);
            currentsick.id = appfile.Currentfile.sicks.Count;
            addsick(currentsick);
            btn_deleteEnable = true;
            lbl_Status.Text = "بیمار اضافه شد";
            checkcurrentsick = false;
        }
        sick currentsick;
        bool checkcurrentsick = false;
        public void update(appfile f)
        {
            foreach (sick s in f.sicks)
            {
                sick temp = appfile.Currentfile.sicks.Find(a => a.name == s.name && a.type == s.type && Path.GetFileNameWithoutExtension(a.path) == Path.GetFileNameWithoutExtension(s.path));
                if (temp == null && s.Delete == false)
                {
                    lbl_Status.Text = "دریافت اطلاعات ...";
                    object output;
                    currentsick = s;
                    if (cli.getdata("getfileiso|" + f.sicks.FindIndex(a => a == s), out output) == "ok")
                    {
                        FileStream Fs = new FileStream(appfile.Currentfile.mysetting.filespath + "\\" + Path.GetFileName(currentsick.path), FileMode.OpenOrCreate, FileAccess.Write);
                        pucketfile pf = (pucketfile)output;
                        Fs.Write(pf.data, 0, pf.data.Length);
                        Fs.Close();
                        currentsick.path = appfile.Currentfile.mysetting.filespath + "\\" + Path.GetFileName(currentsick.path);
                        currentsick.id = appfile.Currentfile.sicks.Count;
                        addsick(currentsick);
                        btn_deleteEnable = true;
                        lbl_Status.Text = "بیمار اضافه شد";
                    }
                }
            }
        }

        public bool btn_deleteEnable
        {
            set
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => btn_deleteall.Enabled = value), null);
                }
                else
                {
                    btn_deleteall.Enabled = value;
                }
            }
        }
        public static t openviafile<t>(string filepath)
        {
            using (Stream stream = File.Open(filepath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (t)binaryFormatter.Deserialize(stream);
            }
        }


        public static void WriteToBinaryFile<t>(string filePath, t source, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, source);
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            add ad = new add();
            if (ad.ShowDialog() == DialogResult.OK)
            {
                ad.result.id = appfile.Currentfile.sicks.Count;
                addsick(ad.result);
                appfile.Currentfile.mysetting.laststatus_me++;
                btn_deleteEnable = true;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("اطلاعات بیمار " + lstv.SelectedItems[0].Text + " حذف خواهد شد! ", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (lstv.SelectedIndices.Count > 0)
                {
                    Process[] processes = null;
                    if (lstv.SelectedItems[0].SubItems[2].Text == "1")
                        processes = Process.GetProcessesByName("feelView");
                    else if (lstv.SelectedItems[0].SubItems[2].Text == "2")
                        processes = Process.GetProcessesByName("RAYKAVIEWER");
                    else if (lstv.SelectedItems[0].SubItems[2].Text == "3")
                        processes = Process.GetProcessesByName("PDIVIEW");
                    else if (lstv.SelectedItems[0].SubItems[2].Text == "4")
                        processes = Process.GetProcessesByName("PacsPlusCV");
                    else if (lstv.SelectedItems[0].SubItems[2].Text == "5")
                        processes = Process.GetProcessesByName("eFilmLite");
                    else if (lstv.SelectedItems[0].SubItems[2].Text == "6")//firozgar
                        processes = Process.GetProcessesByName("Startup");//firozgar
                    else if (lstv.SelectedItems[0].SubItems[2].Text == "7")
                        processes = Process.GetProcessesByName("CDViewer");
                    else if (lstv.SelectedItems[0].SubItems[2].Text == "8")
                        processes = Process.GetProcessesByName("start");
                    else if (lstv.SelectedItems[0].SubItems[2].Text == "9")//firozgar
                        processes = Process.GetProcessesByName("diVision Lite");//firozgar
                    if (processes != null)
                        foreach (Process proc in processes)
                        {
                            Process tempProc = Process.GetProcessById(proc.Id);
                            tempProc.Kill();

                        }
                    Unmount();



                    deletesick(lstv.SelectedIndices[0]);
                    if (lstv.Items.Count == 0)
                        btn_deleteEnable = false;
                }
            }

        }




        public string runcmd(string appname, string argument)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = appname;// "cmd.exe";
                startInfo.Arguments = argument;// "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                return "";
            }
            catch
            {
                return "";
            }
        }

        private void lstv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstv.SelectedIndices.Count > 0)
            {
                btn_play.Enabled = true;
                btn_delete.Enabled = true;
            }
            else
            {
                btn_play.Enabled = false;
                btn_delete.Enabled = false;
            }

        }

        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                WriteToBinaryFile<List<sick>>("info.xvw", appfile.Currentfile.sicks);
                WriteToBinaryFile<Setting>("config.xvw", appfile.Currentfile.mysetting);
                Environment.Exit(1);
            }
            catch
            {
                e.Cancel = true;
                MessageBox.Show("خطا در ذخیره اطلاعات");
            }
        }
        private Process[] processes;
        private void btn_play_Click(object sender, EventArgs e)
        {
            if(!System.IO.File.Exists(appfile.Currentfile.mysetting.virtualapp))
            {
                MessageBox.Show("مسیر برنامه مجازی سازی اشتباه می باشد");
                return;
            }
            if (!System.IO.File.Exists(lstv.SelectedItems[0].SubItems[1].Text))
            {
                MessageBox.Show("فایل دیسک مجازی حذف شده است");
                return;
            }

            if (lstv.SelectedIndices.Count > 0)
            {
                processes = null;
                if (lstv.SelectedItems[0].SubItems[2].Text == "1")
                    processes = Process.GetProcessesByName("feelView");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "2")
                    processes = Process.GetProcessesByName("RAYKAVIEWER.exe");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "3")
                    processes = Process.GetProcessesByName("PDIVIEW.exe");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "4")
                    processes = Process.GetProcessesByName("PacsPlusCV.exe");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "5")
                    processes = Process.GetProcessesByName("eFilmLt.exe");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "6")//firozgar
                    processes = Process.GetProcessesByName("Startup.exe");//firozgar
                else if (lstv.SelectedItems[0].SubItems[2].Text == "7")
                    processes = Process.GetProcessesByName("CDViewer.exe");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "8")
                    processes = Process.GetProcessesByName("start.exe");
                else if (lstv.SelectedItems[0].SubItems[2].Text == "9")//firozgar
                    processes = Process.GetProcessesByName("diVision Lite.exe");//firozgar
                if (processes != null)
                    foreach (Process proc in processes)
                    {
                        Process tempProc = Process.GetProcessById(proc.Id);
                        tempProc.Kill();
                    }

                Unmount();


                runcmd(appfile.Currentfile.mysetting.virtualapp, "mount " + lstv.SelectedItems[0].SubItems[1].Text + " " + appfile.Currentfile.mysetting.drivelater);

                Cursor.Current = Cursors.WaitCursor;
                int cnt = 0;
                while (!runapp() && cnt < 10)
                {
                    cnt++;
                    Thread.Sleep(500);
                }
                Cursor.Current = Cursors.Default;
            }
        }
        public bool Unmount()
        {
            string v = "";
            if (System.IO.Directory.Exists(appfile.Currentfile.mysetting.drivelater))
                v = runcmd(appfile.Currentfile.mysetting.virtualapp, "unmount " + appfile.Currentfile.mysetting.drivelater);

            if (v == "")
                return true;
            else
                return false;
        }


        public bool runapp()
        {
            try
            {
                if (lstv.SelectedItems[0].SubItems[2].Text == "-1")
                {
                    if (System.IO.Directory.GetFiles(appfile.Currentfile.mysetting.drivelater).Length + System.IO.Directory.GetDirectories(appfile.Currentfile.mysetting.drivelater).Length > 0)
                    {
                        Process.Start(appfile.Currentfile.mysetting.drivelater);
                        return true;
                    }
                    else
                        return false;
                }
                else if (lstv.SelectedItems[0].SubItems[2].Text == "1")
                {
                    if (System.IO.File.Exists(appfile.Currentfile.mysetting.drivelater + "\\feelView.exe"))
                    {
                        Process.Start(appfile.Currentfile.mysetting.drivelater + "\\feelView.exe");
                        return true;
                    }
                    else
                        return false;
                }
                else if (lstv.SelectedItems[0].SubItems[2].Text == "2")
                {
                    if (System.IO.File.Exists(appfile.Currentfile.mysetting.drivelater + "\\RAYKAVIEWER.exe"))
                    {
                        //   Process.Start(file.mysetting.drivelater + "\\RAYKAVIEWER.exe");
                        Process.Start(appfile.Currentfile.mysetting.drivelater);
                        return true;
                    }
                    else
                        return false;
                }
                else if (lstv.SelectedItems[0].SubItems[2].Text == "3")
                {
                    if (System.IO.File.Exists(appfile.Currentfile.mysetting.drivelater + "\\PDIVIEW\\PDIVIEW.exe"))
                    {
                        Process.Start(appfile.Currentfile.mysetting.drivelater + "\\PDIVIEW\\PDIVIEW.exe");
                        // Process.Start(file.mysetting.drivelater);
                        return true;
                    }
                    else
                        return false;
                }
                else if (lstv.SelectedItems[0].SubItems[2].Text == "4")
                {
                    if (System.IO.File.Exists(appfile.Currentfile.mysetting.drivelater + "\\PacsPlusCV.exe"))
                    {
                        Process.Start(appfile.Currentfile.mysetting.drivelater + "\\PacsPlusCV.exe");
                        // Process.Start(file.mysetting.drivelater);
                        return true;
                    }
                    else
                        return false;
                }
                else if (lstv.SelectedItems[0].SubItems[2].Text == "5")
                {
                    if (System.IO.File.Exists(appfile.Currentfile.mysetting.drivelater + "\\eFilmLite\\eFilmLt.exe"))
                    {
                        Process.Start(appfile.Currentfile.mysetting.drivelater + "\\eFilmLite\\eFilmLt.exe");
                        //Process.Start(file.mysetting.drivelater);
                        return true;
                    }
                    else
                        return false;
                }
                else if (lstv.SelectedItems[0].SubItems[2].Text == "6")//firozgar
                {
                    if (System.IO.File.Exists(appfile.Currentfile.mysetting.drivelater + "\\Startup.exe"))
                    {
                        Process.Start(appfile.Currentfile.mysetting.drivelater + "\\System\\diVision Lite.exe");
                        //  Process.Start(file.mysetting.drivelater + "\\Startup.exe");
                      //  Process.Start(appfile.Currentfile.mysetting.drivelater);
                        return true;
                    }
                    else
                        return false;
                }
                else if (lstv.SelectedItems[0].SubItems[2].Text == "7")
                {
                    if (System.IO.File.Exists(appfile.Currentfile.mysetting.drivelater + "\\CDViewer\\CDViewer.exe"))
                    {
                        Process.Start(appfile.Currentfile.mysetting.drivelater + "\\CDViewer\\CDViewer.exe");
                        //Process.Start(file.mysetting.drivelater);
                        return true;
                    }
                    else
                        return false;
                }
                else if (lstv.SelectedItems[0].SubItems[2].Text == "8")
                {
                    if (System.IO.File.Exists(appfile.Currentfile.mysetting.drivelater + "\\start.exe"))
                    {
                        Process.Start(appfile.Currentfile.mysetting.drivelater + "\\start.exe");
                        //Process.Start(file.mysetting.drivelater);
                        return true;
                    }
                    else
                        return false;
                }
                else if (lstv.SelectedItems[0].SubItems[2].Text == "9")//firozgar
                {
                    if (System.IO.File.Exists(appfile.Currentfile.mysetting.drivelater + "\\system\\diVision Lite.exe"))
                    {
                        // Process.Start(file.mysetting.drivelater + "\\system\\diVision Lite.exe");
                        Process.Start(appfile.Currentfile.mysetting.drivelater);
                        return true;
                    }
                    else
                        return false;
                }


                else
                {
                    Process.Start(appfile.Currentfile.mysetting.drivelater);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void aboutme_Click(object sender, EventArgs e)
        {
            aboutme ab = new aboutme();
            ab.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("تمامی اطلاعات حذف خواهد شد! آیا مطمئن هستید؟", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                processes = null;
                processes = Process.GetProcessesByName("RAYKAVIEWER.exe");
                foreach (Process proc in processes)
                {
                    Process tempProc = Process.GetProcessById(proc.Id);
                    tempProc.Kill();
                }

                processes = Process.GetProcessesByName("feelView");
                foreach (Process proc in processes)
                {
                    Process tempProc = Process.GetProcessById(proc.Id);
                    tempProc.Kill();
                }

                processes = Process.GetProcessesByName("PDIVIEW");
                foreach (Process proc in processes)
                {
                    Process tempProc = Process.GetProcessById(proc.Id);
                    tempProc.Kill();
                }

                processes = Process.GetProcessesByName("PacsPlusCV");
                foreach (Process proc in processes)
                {
                    Process tempProc = Process.GetProcessById(proc.Id);
                    tempProc.Kill();
                }
                processes = Process.GetProcessesByName("eFilmLt");
                foreach (Process proc in processes)
                {
                    Process tempProc = Process.GetProcessById(proc.Id);
                    tempProc.Kill();
                }
                processes = Process.GetProcessesByName("CDViewer");
                foreach (Process proc in processes)
                {
                    Process tempProc = Process.GetProcessById(proc.Id);
                    tempProc.Kill();
                }
                processes = Process.GetProcessesByName("diVision Lite");
                foreach (Process proc in processes)
                {
                    Process tempProc = Process.GetProcessById(proc.Id);
                    tempProc.Kill();
                }

                if (System.IO.Directory.Exists(appfile.Currentfile.mysetting.drivelater))
                    runcmd(appfile.Currentfile.mysetting.virtualapp, "unmount " + appfile.Currentfile.mysetting.drivelater);
                
                int cnt = lstv.Items.Count;
                for (int i = 0; i < cnt; i++)
                    deletesick(0);
                btn_deleteEnable = false;
            }
        }


 


        private void lstv_DoubleClick(object sender, EventArgs e)
        {
            btn_play_Click(null, null);
        }
    }
}
