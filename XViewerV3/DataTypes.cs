using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XViewerV3
{
 

    [Serializable]
    public class sick
    {
        public int id;
        public int type;
        public string name;
        public string path;
        public bool Delete = false;
    }

    [Serializable]
    public class appfile
    {
        public static appfile Currentfile;
        public List<sick> sicks;
        public Setting mysetting;
        public appfile()
        {
            sicks = new List<sick>();
            mysetting = new Setting();
        }
    }

    [Serializable]
    public class Setting
    {
        public string virtualapp;
        public string drivelater;
        public string filespath;
        public string news;
        public int laststatus_me = 0;
        public bool activeserver;
        public string ip;
        public int port;
        public int laststatus_server = -1;
        public int portserver;
    }
}
