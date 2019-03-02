using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data.SqlClient;

namespace NetworkLibrary
{
    public abstract class server_net
    {
        TcpListener tcpListener;

        public void start(int port)
        {
            Thread th = new Thread(new ThreadStart(serveroject_thread));
            if (!th.IsAlive)
            {

                tcpListener = new TcpListener(port);
                th.Start();
            }
        }
        public bool Active = true;

        private void serveroject_thread()
        {
            tcpListener.Start();
            Thread newThread = new Thread(new ThreadStart(ListenersOBJECT));
            while (true)
            {
                if (!newThread.IsAlive && Active)
                {
                    newThread = new Thread(new ThreadStart(ListenersOBJECT));
                    newThread.Start();
                }
                else
                {
                    if(!Active)
                    {
                        newThread.Abort();
                    }
                }
            }
        }
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
        
        void ListenersOBJECT()
        {
            byte[] byterecieve = new byte[256];
            Socket socketForClient = tcpListener.AcceptSocket();
            if (socketForClient.Connected)
            {
                object output;
                object pr;
                try
                {
                    int bytes = 0;
                    System.IO.MemoryStream allmemory = new System.IO.MemoryStream();

                 //   do
                 //   {
                 //       bytes = socketForClient.Receive(byterecieve, byterecieve.Length, 0);
                 //       if (bytes > 0)
                  //          allmemory.Write(byterecieve, 0, bytes);
                 //   }
                 //   while (bytes > 0);

                    bytes = socketForClient.Receive(byterecieve, byterecieve.Length, 0);
                    if (bytes > 0)
                        allmemory.Write(byterecieve, 0, bytes);

                    byte[] allbyteArray = new byte[allmemory.Length];
                    int count = 0;

                    // Read the remaining bytes, byte by byte. 
                    allmemory.Seek(0, SeekOrigin.Begin);
                    while (count < allmemory.Length)
                    {
                        int te = allmemory.ReadByte();
                        allbyteArray[count++] =
                            Convert.ToByte(te);
                    }
                    output = ByteArrayToObject(allbyteArray);
                }
                catch
                {
                    throw new Exception("recieve error");
                }
                try
                {
                    pr = process(output,socketForClient);
                }
                catch
                {
                    throw new Exception("process error");
                }
                try
                {
                    if(pr!=null)
                    {
                        Byte[] bytesSent = ObjectToByteArray(pr);
                        Byte[] bytesReceived = new Byte[256];
                        socketForClient.Send(bytesSent, bytesSent.Length, 0);
                    }
                }
                catch
                {
                    throw new Exception("send error");
                }
            }
            socketForClient.Close();
        }

        public abstract object process(object Input,Socket socketForClient);
    }
}

