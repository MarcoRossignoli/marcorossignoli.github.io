using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Interview.Http2
{
    public class Http2Test
    {
        async public static Task Test()
        {
            await Http2.SomeTest();
        }
    }

    public static class Http2
    {
        public async static Task SomeTest()
        {
            while (true)
            {
                var uri = new Uri("https://www.akamai.com", UriKind.Absolute);
                IPHostEntry hostEntry = Dns.GetHostEntry(uri.Authority);
                IPEndPoint ipe = new IPEndPoint(hostEntry.AddressList[0], uri.Port);
                using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipe);

                if (!socket.Connected)
                {
                    throw new SocketException();
                }

                //using NetworkStream ns = new NetworkStream(socket);
                //using SslStream ssl = new SslStream(ns);
                //await ssl.AuthenticateAsClientAsync(uri.Host);

                socket.Shutdown(SocketShutdown.Both);
            }
        }
    }
}
