using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    public class HttpClient
    {
        async public static Task Http1()
        {
            Http1 client = new Http1();
            Console.WriteLine(await client.Get("https://www.univr.it"));
        }
    }

    public class Http1
    {
        async public Task<string> Get(string url)
        {
            Uri uri = new Uri(url, UriKind.Absolute);
            IPHostEntry hostEntry = Dns.GetHostEntry(uri.Authority);
            IPEndPoint ipe = new IPEndPoint(hostEntry.AddressList[0], uri.Port);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                await socket.ConnectAsync(ipe);
                string request = $"GET / HTTP/1.1\r\nHost: {uri.Authority} \r\nConnection: Close\r\n\r\n";
                socket.Send(Encoding.ASCII.GetBytes(request));
                byte[] received = new byte[1024];
                int receivedBytes = 0;
                using MemoryStream ms = new MemoryStream();
                while ((receivedBytes = socket.Receive(received)) > 0)
                {
                    ms.Write(received, 0, receivedBytes);
                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
