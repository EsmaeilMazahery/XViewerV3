using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace NetworkLibrary
{
    public class client
    {
        [Serializable]
        public class pucketfile
        {
            public byte[] data;
        }
        public client(string server_IP, int port)
        {
            server = server_IP;
            this.port = port;
        }
        public string server;
        public int port;

        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        public string getdata(object parameters, out object output)
        {
            output = null;
            try
            {
                var str = parameters;

                Byte[] bytesSent = ObjectToByteArray(parameters);

                Byte[] bytesReceived = new Byte[256];


                //   Socket s = ConnectSocket(server, port);
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ip = new IPEndPoint(Dns.GetHostByAddress(server).AddressList[0], port);
                s.Connect(ip);

                if (s == null)
                    return ("Connection failed");
                
                s.Send(bytesSent, bytesSent.Length, 0);
                
                int bytes = 0;
                System.IO.MemoryStream allmemory = new System.IO.MemoryStream();

                do
                {
                    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                    allmemory.Write(bytesReceived, 0, bytes);
                }
                while (bytes > 0);

                byte[] allbyteArray = new byte[allmemory.Length];
                int count = 0;
                allmemory.Seek(0, SeekOrigin.Begin);
                // Read the remaining bytes, byte by byte. 
                while (count < allmemory.Length)
                {
                    allbyteArray[count++] =
                        Convert.ToByte(allmemory.ReadByte());
                }
                pucketfile pf=null;
                if(allbyteArray.Length>=4 && allbyteArray[0]== 84 && allbyteArray[1] == 23 && allbyteArray[2] == 56 && allbyteArray[3] == 47)//is file
                {
                    pf = new pucketfile();
                    pf.data = allbyteArray.Skip<byte>(4).ToArray();
                }

                if (pf == null)
                    output = ByteArrayToObject(allbyteArray);
                else
                    output = pf;
                return "ok";
            }
            catch(Exception ex)
            {
                return "Error";
            }
        }
    }
}
