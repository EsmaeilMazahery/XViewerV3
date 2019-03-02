using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace XViewerV3
{
    class server : NetworkLibrary.server_net
    {
        public Object ByteArrayToObject(byte[] arrBytes)
        {
            if (arrBytes != null && arrBytes.Length > 0)
                using (var memStream = new MemoryStream())
                {
                    var binForm = new BinaryFormatter();
                    memStream.Write(arrBytes, 0, arrBytes.Length);
                    memStream.Seek(0, SeekOrigin.Begin);
                    var obj = binForm.Deserialize(memStream);
                    return obj;
                }
            else
                return null;
        }


        public override object process(object Input,Socket socketForClient)
        {
            if (Input == null)
                return "null";
            if (Input.GetType() == typeof(string))
            {
                string[] inp = Input.ToString().Split('|');
                if (inp[0] == "checknet")
                {
                    return "ok";
                }
                else if (inp[0] == "getlaststatus")
                {
                    return appfile.Currentfile.mysetting.laststatus_me;

                }
                else if (inp[0] == "getfileinfo")
                {
                    if (appfile.Currentfile != null)
                        return appfile.Currentfile;
                    else
                        return "null";
                }
                else if (inp[0] == "getfileiso")
                {
                    if (frm_Start.mainform1 != null)
                        frm_Start.mainform1.changestatus("ارسال اطلاعات");
                    if (frm_Start.mainform2 != null)
                        frm_Start.mainform2.changestatus("ارسال اطلاعات");
                    socketForClient.Send(new byte[] {84,23,56,47 }, 4, 0); 
                    socketForClient.SendFile(appfile.Currentfile.sicks[int.Parse(inp[1])].path);
                    if (frm_Start.mainform1 != null)
                        frm_Start.mainform1.changestatus("اطلاعات ارسال شد");
                    if (frm_Start.mainform2 != null)
                        frm_Start.mainform2.changestatus("اطلاعات ارسال شد");

                    //FileStream fs = new FileStream(appfile.Currentfile.sicks[int.Parse(inp[1])].path, FileMode.Open);

                    //byte[] data;
                    //using (var memStream = new MemoryStream())
                    //{
                    //    while (fs.CanRead)
                    //        memStream.WriteByte((byte)fs.ReadByte());
                    //    memStream.Seek(0, SeekOrigin.Begin);
                    //    data = memStream.ToArray();
                    //}
                    //return ByteArrayToObject(data);
                    return null;
                }
                else if (inp[0] == "deleteitem")
                {
                    if (frm_Start.mainform1 != null)
                        return frm_Start.mainform1.Deleteitemserver(new sick() { Delete = true, name = inp[1], path = inp[2], type = int.Parse(inp[3]), id = appfile.Currentfile.sicks.Count });
                    if (frm_Start.mainform2 != null)
                        return frm_Start.mainform1.Deleteitemserver(new sick() { Delete = true, name = inp[1], path = inp[2], type = int.Parse(inp[3]), id = appfile.Currentfile.sicks.Count });
                    throw new Exception("Exit App");
                }
                else
                    return "Error|unknown_type";
            }
            else if (Input.GetType() == typeof(appfile))
            {
                appfile inp = (appfile)Input;
                foreach (sick s in inp.sicks)
                {
                    if (s.Delete)
                    {
                        if (frm_Start.mainform1 != null)
                            frm_Start.mainform1.Deleteitemserver(s);
                        if (frm_Start.mainform2 != null)
                            frm_Start.mainform2.Deleteitemserver(s);
                    }

                }
                return true;
            }
            else
                return "Error|unknown_type";
        }
    }
}
